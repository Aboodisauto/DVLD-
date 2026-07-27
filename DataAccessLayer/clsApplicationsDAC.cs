using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsApplicationsDAC
    {
        public static bool IsThereaDuplicate(int ApplicantID , int LicenseID)
        {
            bool Found = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_IsThereADuplicate", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ApplicantID", ApplicantID);
            command.Parameters.AddWithValue("@ClassID", LicenseID);
            try
            {
                connection.Open();
                Found = Convert.ToInt16(command.ExecuteScalar()) > 0;

            }catch(Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return Found;
        }
        public static DataTable FetchLocalApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_FetchLocalApplications", connection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return dt;
        }
        
        public static int AddApplication (int ApplicantID, 
    DateTime ApplicationDate,
    int ApplicationType,
    int ApplicationStatus,
    DateTime StatusDate, 
    double PaidFees,
    int CreatedByUserID
           )
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_AddApplication", connection);
            command.CommandType = CommandType.StoredProcedure;
            // Assuming your SqlCommand object is named 'command'
            command.Parameters.AddWithValue("@ApplicantID", ApplicantID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@ApplicationType", ApplicationType);
            command.Parameters.AddWithValue("@StatusDate", StatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@UserID", CreatedByUserID);
            try
            {
                connection.Open();
                ApplicationID = Convert.ToInt32(command.ExecuteScalar());
            }catch(Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return ApplicationID;
        }
        public static bool FindApplication(
            int ApplicationID,
            ref int ApplicantID,
            ref DateTime ApplicationDate,
            ref int ApplicationType,
            ref int ApplicationStatus,
            ref DateTime StatusDate,
            ref double PaidFees,
            ref int CreatedByUserID)
        {
            bool Found = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_FindApplication", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Found = true;
                    ApplicantID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationType = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = Convert.ToInt32(reader["ApplicationStatus"]);
                    StatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToDouble(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                reader.Close();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return Found;
        }
        public static int GetApplicationID(int LocalApplicationID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_GetApplicationIDByLocalApplicationID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", LocalApplicationID);
            try
            {
                connection.Open();
                ApplicationID = Convert.ToInt32(command.ExecuteScalar());
            }catch(Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return ApplicationID;
        }
        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_DeleteApplication", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", ApplicationID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }catch(Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return rowsAffected > 0;
        }
        public static bool UpdateApplication(int ApplicationID,
            int ApplicantID,
            DateTime ApplicationDate,
            int ApplicationType,
            int ApplicationStatus,
            DateTime StatusDate,
            double PaidFees,
            int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_UpdateApplication", connection);
            command.CommandType = CommandType.StoredProcedure;
            // Link the parameters to your method variables
            command.Parameters.AddWithValue("@ApplicantID", ApplicantID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationType);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@StatusDate", StatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@UserID", CreatedByUserID);

            // Crucial: The ID for the WHERE clause
            command.Parameters.AddWithValue("@ID", ApplicationID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }catch(Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return rowsAffected > 0;
        }
        public static bool CancelApplication(int ApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_CancelApplication", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Date", DateTime.Now);
            command.Parameters.AddWithValue("@ID", ApplicationID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return rowsAffected > 0;
        }
        public static string GetApplicantFullName(int PersonID)
        {
            string FullName = string.Empty;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_GetApplicantFullName", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", PersonID);
            try
            {
                connection.Open();
                FullName = command.ExecuteScalar().ToString();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return FullName;


        }
    }
}

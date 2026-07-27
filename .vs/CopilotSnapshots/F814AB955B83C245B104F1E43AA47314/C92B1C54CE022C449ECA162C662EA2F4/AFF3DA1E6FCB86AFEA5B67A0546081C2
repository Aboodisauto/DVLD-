using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsLocalApplicationDAC
    {
        public static bool Find(int LocalLicenseID,ref int ApplicationID,ref int LicenseClassID)
        {
            bool Found = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LocalLicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Found = true;
                    reader.Read();
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);

                }
                reader.Close();

            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return Found;
        }
        //public static bool Delete(int LocalApplicationID)
        //{
        //    int ApplicationID = clsApplicationsDAC.GetApplicationID(LocalApplicationID);
        //    if (!clsApplicationsDAC.DeleteApplication(ApplicationID))
        //    {
        //        return false;
        //    }
        //    SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
        //    string query = "Delete From LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @ID";
        //    SqlCommand command = new SqlCommand(query, connection);
        //    try
        //    {
        //        connection.Open();

        //    }
        //}
        public static int AddLocalApplication(int ApplicationID, int LicenseClassID)
        {
            int LocalApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "INSERT INTO [dbo].[LocalDrivingLicenseApplications]\r\n           ([ApplicationID]\r\n           ,[LicenseClassID])\r\n     VALUES\r\n           (@ApplicationID\r\n           ,@LicenseClassID); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                LocalApplicationID = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return LocalApplicationID;
        }
        public static bool UpdateLocalApplication(int LicenseClassID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "UPDATE LocalDrivingLicenseApplications SET LicenseClassID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LicenseClassID);
            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return RowsAffected > 0;
        }
        public static bool FindLocalApplication(int LocalApplicationID, ref int LicenesClassID, ref int ApplicationID)
        {
            bool Found = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LocalApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LicenesClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    Found = true;
                }
                reader.Close();

            }catch(Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return Found;
        }
        public static short FetchLicenseClassID(string LicenseClassName)
        {
            short ClassID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT LicenseClassID FROM LicenseClasses WHERE ClassName = @ClassName ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", LicenseClassName);
            try
            {
                connection.Open();
                ClassID = Convert.ToInt16(command.ExecuteScalar());
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return ClassID;
        }
        public static int GetApplicationID(int LocalApplicationID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT ApplicationID FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @ID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LocalApplicationID);
            try
            {
                connection.Open();
                ApplicationID = Convert.ToInt16(command.ExecuteScalar());
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return ApplicationID;
        }
        public static bool Delete(int LocalDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log error using your standard mechanism
                File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n");
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public static int GetLocalApplicationID(int licenseID)
        {
            int LocalApplicationID = -1;
            string query = "SELECT LocalDrivingLicenseApplicationID FROM Licenses  INNER JOIN LocalDrivingLicenseApplications ON LocalDrivingLicenseApplications.ApplicationID = Licenses.ApplicationID WHERE LicenseID = @ID";
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", licenseID);
            try
            {
                connection.Open();
                LocalApplicationID = Convert.ToInt32(command.ExecuteScalar());

            }
            catch (Exception ex)
            {
                File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n");
            }
            finally
            {
                connection.Close();
            }
            return LocalApplicationID;
        }
    }
}

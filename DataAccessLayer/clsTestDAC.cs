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
    public class clsTestDAC
    {
        public static short CountPassedTests(int LocalDrivingLicenseApplicationID)
        {
            short count = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_CountPassedTests", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                count = Convert.ToInt16(command.ExecuteScalar());
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return count;
        }
        public static DataTable FetchTestAppointments(int LocalApplicationID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_FetchTestAppointmentsForLocalApplication", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", LocalApplicationID);
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
        public static bool isEligibleToTakeTest(int PersonID, int TestTypeID,int LicenseClassID)
        {
            bool Can = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_IsEligibleToTakeTest", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                int res = Convert.ToInt32(command.ExecuteScalar());
                if (res == 1)
                    Can = true;
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return Can;
        }
        public static short CountOfFailed(int LocalApplicationID, int TestTypeID)
        {
            short count = 0;
            SqlConnection con = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_CountFailedTestAppointments", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID",LocalApplicationID);
            command.Parameters.AddWithValue("@TID", TestTypeID);
            try
            {
                con.Open();
                count = Convert.ToInt16(command.ExecuteScalar());
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { con.Close(); }
            return count;
        }
        public static bool Find(int TestAppointmentID, ref bool TestResult, ref int TestID, ref string Notes, ref int UserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_FindTestByAppointmentID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                    TestID = Convert.ToInt32(reader["TestID"]);
                    if(Notes != null)
                        Notes = reader["Notes"].ToString();
                    UserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return isFound;
        }
        public static int Add(bool TestResult,int TestAppointmentID,string Notes,int UserID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_AddTest", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            if (Notes != "")
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                ID = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return ID;
        }
        public static bool Update(int ID,bool TestResult, int TestAppointmentID, string Notes, int UserID)
        {
            int rowsAffected = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_UpdateTest",connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return rowsAffected > 0;
        }
        public static bool Delete(int testID)
        {
            int rowsAffected = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_DeleteTest", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", testID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return rowsAffected > 0;
        }
    }
}

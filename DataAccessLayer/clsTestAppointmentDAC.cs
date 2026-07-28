using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataAccessLayer
{
    public class clsTestAppointmentDAC
    {
        public static DataTable FetchTestAppointments(int LocalApplicationID, int TestTypeID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_FetchTestAppointments", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", LocalApplicationID);
            command.Parameters.AddWithValue("@TID", TestTypeID);
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

        public static bool Find(int TestAppointmentID, ref int TestTypeID, ref int LocalApplicationID,
            ref int CreatedByUserID, ref double PaidFees, ref bool isLocked, ref DateTime AppointmentDate, ref int RetakeApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_FindTestAppointmentByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    PaidFees = Convert.ToDouble(reader["PaidFees"]);
                    isLocked = Convert.ToBoolean(reader["IsLocked"]);
                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);

                    // Handle Nullable RetakeTestApplicationID
                    if (reader["RetakeTestApplicationID"] != DBNull.Value)
                        RetakeApplicationID = (int)reader["RetakeTestApplicationID"];
                    else
                        RetakeApplicationID = -1;

                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return isFound;
        }

        public static int AddAppointment(int TestTypeID, int LocalApplicationID, int CreatedByUserID,
            double PaidFees, bool isLocked, DateTime AppointmentDate, int RetakeApplicationID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_AddTestAppointment", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TestTypeId", TestTypeID);
            command.Parameters.AddWithValue("@LocalApplicationID", LocalApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@UserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", isLocked);

            // Handle Nullable Parameter
            if (RetakeApplicationID != -1)
                command.Parameters.AddWithValue("@RetakeApplicationID", RetakeApplicationID);
            else
                command.Parameters.AddWithValue("@RetakeApplicationID", DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                    ID = id;
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return ID;
        }

        public static bool Delete(int ID)
        {
            int rowsAffected = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_DeleteTestAppointment", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int r))
                    rowsAffected = r;
                else
                    rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return rowsAffected > 0;
        }

        public static bool Update(int ID, int TestTypeID, int LocalApplicationID, int CreatedByUserID,
            double PaidFees, bool isLocked, DateTime AppointmentDate, int RetakeApplicationID)
        {
            int rowsAffected = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_UpdateTestAppointment", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@TestTypeId", TestTypeID);
            command.Parameters.AddWithValue("@LocalApplicationID", LocalApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@UserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", isLocked);

            // Handle Nullable Parameter
            if (RetakeApplicationID != -1)
                command.Parameters.AddWithValue("@RetakeApplicationID", RetakeApplicationID);
            else
                command.Parameters.AddWithValue("@RetakeApplicationID", DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int r))
                    rowsAffected = r;
                else
                    rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return rowsAffected > 0;
        }
    }
}
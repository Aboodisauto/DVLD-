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
    public class clsDriverDAC
    {
        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT Drivers.DriverID As 'Driver ID', Drivers.PersonID As 'Person ID', NationalNo As 'National No', FirstName + ' ' + SecondName + ' ' + ThirdName + ' ' + LastName As 'Full Name', Drivers.CreatedDate, COUNT(CASE WHEN IsActive = 1 THEN 1 ELSE NULL END) As 'Active Licenses' FROM Drivers\r\nINNER JOIN People ON People.PersonID = Drivers.PersonID\r\nINNER JOIN Licenses ON Licenses.DriverID = Drivers.DriverID\r\nGROUP BY Drivers.DriverID,\r\n\t\tDrivers.PersonID,\r\n\t\tNationalNo,\r\n\t\tFirstName,\r\n\t\tSecondName,\r\n\t\tThirdName,\r\n\t\tLastName,\r\n\t\tDrivers.CreatedDate";
            SqlCommand command = new SqlCommand(query, connection);

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
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return dt;
        }
        public static int GetDriverIDByPersonID(int PersonID)
        {
            int DriverID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT DriverID FROM Drivers WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                // Check if result is not null (Driver exists)
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID = insertedID;
                }
            }
            catch (Exception ex)
            {
                // Log error
                File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n");
            }
            finally
            {
                connection.Close();
            }
            return DriverID;
        }
        public static bool Find(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }
                reader.Close();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return isFound;
        }

        public static bool FindByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }
                reader.Close();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return isFound;
        }

        public static int AddNewDriver(int PersonID, int CreatedByUserID)
        {
            int DriverID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);

            string query = @"INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
                             VALUES (@PersonID, @CreatedByUserID, @CreatedDate);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID = insertedID;
                }
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return DriverID;
        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            // Note: CreatedDate is usually not updated as it tracks creation time
            string query = @"UPDATE Drivers  
                             SET PersonID = @PersonID, 
                                 CreatedByUserID = @CreatedByUserID
                             WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return rowsAffected > 0;
        }

        // Usually drivers are not deleted if linked to licenses, but here is the method just in case
        public static bool DeleteDriver(int DriverID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "DELETE FROM Drivers WHERE DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return rowsAffected > 0;
        }
        public static bool IsDriverExistByPersonID(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT Found=1 FROM Drivers WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                // Log the error using your standard logging mechanism
                File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n");
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static int GetDriverILID(int driverID)
        {
            int ILID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string quey = "SELECT InternationalLicenseID FROM InternationalLicenses WHERE DriverID = @ID AND isActive = 1";
            SqlCommand command = new SqlCommand(quey, connection);
            command.Parameters.AddWithValue("@ID", driverID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ILID = insertedID;
                }
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return ILID;
            }
    }
}
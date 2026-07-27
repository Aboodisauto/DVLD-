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
    public class clsLicenseDAC
    {
        public static bool Find(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,
            ref decimal PaidFees, ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    // Handle Nullable Notes
                    if (reader["Notes"] == DBNull.Value)
                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = Convert.ToInt32(reader["IssueReason"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return isFound;
        }

        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT * FROM Licenses";
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

        public static DataTable GetDriverLicenses(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT * FROM Licenses WHERE DriverID = @DriverID ORDER BY IssueDate DESC";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass,
             DateTime IssueDate, DateTime ExpirationDate, string Notes,
             decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);

            string query = @"INSERT INTO Licenses 
                               (ApplicationID, DriverID, LicenseClass, 
                                IssueDate, ExpirationDate, Notes, 
                                PaidFees, IsActive, IssueReason, CreatedByUserID)
                             VALUES 
                               (@ApplicationID, @DriverID, @LicenseClass, 
                                @IssueDate, @ExpirationDate, @Notes, 
                                @PaidFees, @IsActive, @IssueReason, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
                }
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return LicenseID;
        }

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
             DateTime IssueDate, DateTime ExpirationDate, string Notes,
             decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);

            string query = @"UPDATE Licenses
                             SET ApplicationID = @ApplicationID,
                                 DriverID = @DriverID,
                                 LicenseClass = @LicenseClass,
                                 IssueDate = @IssueDate,
                                 ExpirationDate = @ExpirationDate,
                                 Notes = @Notes,
                                 PaidFees = @PaidFees,
                                 IsActive = @IsActive,
                                 IssueReason = @IssueReason,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
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

        public static bool Delete(int LicenseID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "DELETE FROM Licenses WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return rowsAffected > 0;
        }
        public static bool DoesPersonAlreadyHasALicense(int PersonID, int LicenseClassID)
        {
            bool does = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT Found = 1 FROM Licenses INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID WHERE Drivers.PersonID = @PID AND Licenses.LicenseClass = @LID AND IsActive = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PID", PersonID);
            command.Parameters.AddWithValue("@LID", LicenseClassID);
            try
            {
                connection.Open();
                int res = Convert.ToInt32(command.ExecuteScalar());
                if (res == 1)
                    does = true;
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return does;
        }
        public static int GetLicenseID(int ApplicationID)
        {
            int LicenseID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT LicenseID FROM Licenses WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int res))
                {
                    LicenseID = res;

                }
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return LicenseID;
        }

        public static bool isLicenseDetained(int licenseID)
        {
            bool isDetained = false;
            string query = "SELECT * FROM DetainedLicenses WHERE LicenseID = @ID AND IsReleased = 0";
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", licenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isDetained = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return isDetained;
        }

        public static DataTable FetchLicenseForPerson(int driverID)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT        Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive\r\nFROM            Licenses INNER JOIN\r\n                         LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID WHERE DriverID = @DriverID";
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", driverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return dataTable;
        }

        public static int DetainLicense(int licenseID, decimal FineFees, int UserID)
        {
            int DetainedLicenseID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "INSERT INTO DetainedLicenses (LicenseID, DetainDate, IsReleased, FineFees, CreatedByUserID) VALUES (@LicenseID, @DetainDate, 0, @FineFees, @UserID);SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", licenseID);
            command.Parameters.AddWithValue("@DetainDate", DateTime.Now);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                object res = command.ExecuteScalar();
                if (res != null && int.TryParse(res.ToString(), out int id))
                {
                    DetainedLicenseID = id;
                }
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return DetainedLicenseID;
        }
        public static bool ReleaseDetainedLicense(int DetainedLicenseID, int UserID, int ReleaseApplicationID)
        {
            bool isReleased = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "UPDATE DetainedLicenses SET IsReleased = 1, ReleaseDate = @ReleaseDate, ReleasedByUserID = @UserID, ReleaseApplicationID = @ReleaseApplicationID WHERE LicenseID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", DetainedLicenseID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isReleased = rowsAffected > 0;
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n"); }
            finally { connection.Close(); }
            return isReleased;
        }
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT        DetainedLicenses.DetainID As 'D.ID', DetainedLicenses.LicenseID As 'L.ID', DetainedLicenses.DetainDate As 'D Date', DetainedLicenses.IsReleased As 'Is Released', DetainedLicenses.FineFees As 'Fine Fees', DetainedLicenses.ReleaseDate As 'Release Date', People.NationalNo As 'N No', (People.FirstName + ' ' +\r\n                         People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName) As 'Full Name', DetainedLicenses.ReleaseApplicationID As 'Release App ID'\r\nFROM            DetainedLicenses INNER JOIN\r\n                         Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID INNER JOIN\r\n                         Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN\r\n                         People ON Drivers.PersonID = People.PersonID";
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
            catch (Exception ex)
            {
                File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n");
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        public static bool GetDetainInfoByLicenseID(int LicenseID,ref decimal FineFees,ref DateTime DetainDate,ref int DetainID)
        {
            bool Found = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT DetainID, FineFees, DetainDate FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Found = true;
                    FineFees = (decimal)reader["FineFees"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    DetainID = (int)reader["DetainID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + " " + ex.Message + "\n");
            }
            finally
            {
                connection.Close();
            }
            return Found;
        }
    }
}
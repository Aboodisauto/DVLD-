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
            string query = "SELECT Found = 1 FROM LocalDrivingLicenseApplications INNER JOIN Applications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID\r\nWHERE Applications.ApplicantPersonID = @ApplicantID AND LicenseClassID = @ClassID";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = @"
SELECT 
    LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, 
    LicenseClasses.ClassName, 
    People.NationalNo, 
    Applications.ApplicationDate, 
    (People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName) AS FullName,
    CASE 
        WHEN Applications.ApplicationStatus = 1 THEN 'OnGoing'
        WHEN Applications.ApplicationStatus = 2 THEN 'Cancelled'
        WHEN Applications.ApplicationStatus = 3 THEN 'Completed'
    END AS Status,
    COUNT(CASE WHEN Tests.TestResult = 1 THEN 1 ELSE NULL END) AS PassedCount
FROM Applications 
INNER JOIN LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID 
INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID
INNER JOIN LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
LEFT JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
LEFT JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
GROUP BY 
    LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, 
    LicenseClasses.ClassName, 
    People.NationalNo, 
    Applications.ApplicationDate, 
    Applications.ApplicationStatus, 
    People.FirstName, 
    People.SecondName, 
    People.ThirdName, 
    People.LastName
ORDER BY LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID DESC"; // Added DESC to see new ones first
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
            string query = "INSERT INTO [dbo].[Applications]\r\n           ([ApplicantPersonID]\r\n           ,[ApplicationDate]\r\n           ,[ApplicationTypeID]\r\n           ,[ApplicationStatus]\r\n           ,[LastStatusDate]\r\n           ,[PaidFees]\r\n           ,[CreatedByUserID])\r\n     VALUES\r\n           (@ApplicantID\r\n           ,@ApplicationDate\r\n           ,@ApplicationType\r\n           ,@ApplicationStatus\r\n           ,@StatusDate\r\n           ,@PaidFees\r\n           ,@UserID);" +
                "SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "SELECT * FROM Applications WHERE ApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "select applicationID from LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "DELETE FROM [dbo].[Applications]\r\n      WHERE ApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "UPDATE [dbo].[Applications]\r\n   SET [ApplicantPersonID] = @ApplicantID\r\n      ,[ApplicationDate] = @ApplicationDate\r\n      ,[ApplicationTypeID] = @ApplicationTypeID\r\n      ,[ApplicationStatus] = @ApplicationStatus\r\n      ,[LastStatusDate] = @StatusDate\r\n      ,[PaidFees] = @PaidFees\r\n      ,[CreatedByUserID] = @UserID\r\n WHERE ApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "UPDATE Applications SET ApplicationStatus = 2, LastStatusDate = @Date WHERE ApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "SELECT distinct FirstName + ' ' + SecondName + ' ' + ThirdName + ' ' + LastName As FullName FROM Applications INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID WHERE People.PersonID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
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

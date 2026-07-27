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
    public class clsApplicationTypesDAC
    {
        public static DataTable GetApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);

            // Changed table to ApplicationTypes
            string query = "SELECT * FROM ApplicationTypes";

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
                File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n");
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static int GetNumberOfApplicationTypes()
        {
            int count = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);

            // Changed table to ApplicationTypes
            string query = "SELECT COUNT(*) FROM ApplicationTypes";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                count = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n");
            }
            finally
            {
                connection.Close();
            }
            return count;
        }

        // Removed 'Description' parameter
        public static bool FindApplicationType(int ID, ref string Title, ref double Fees)
        {
            bool Done = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);

            // Updated query and column names
            // NOTE: Check your database column names. I assumed 'ApplicationTypeID' based on your previous pattern.
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) // Changed to if(reader.Read()) which is cleaner
                {
                    Done = true;
                    // Updated column names
                    Title = reader["ApplicationTypeTitle"].ToString();
                    Fees = Convert.ToDouble(reader["ApplicationFees"]);
                }
                reader.Close();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return Done;
        }

        // Removed 'Description' parameter
        public static bool UpdateApplicationType(int ID, string Title, double Fees)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);

            // Updated query to remove Description and target ApplicationTypes
            string query = @"UPDATE [dbo].[ApplicationTypes]
                             SET [ApplicationTypeTitle] = @Title
                                ,[ApplicationFees] = @Fees
                             WHERE ApplicationTypeID = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return RowsAffected > 0;
        }
        public static string GetApplicationTypeName(int ApplicatinoTypeID)
        {
            string Name = string.Empty;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT ApplicationTypeTitle From ApplicationTypes WHERE ApplicationTypeID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ApplicatinoTypeID);
            try
            {
                connection.Open();
                Name = command.ExecuteScalar().ToString();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return Name;
        }

        public static double GetApplicationTypeFees(int iD)
        {
            double fees = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT ApplicationFees From ApplicationTypes WHERE ApplicationTypeID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", iD);
            try
            {
                connection.Open();
                fees = Convert.ToDouble(command.ExecuteScalar());

            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return fees;
        }
    }
}
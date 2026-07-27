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
    public class clsTestsTypeDAC
    {
        public static DataTable GetTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_GetTestTypes", connection);
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
            catch (Exception ex)
            {
                File.AppendAllText(clsSettingsDAC.LogPath,DateTime.Now + ex.Message + "\n");
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static int GetNumberOfTypes()
        {
            int count = 0;
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_GetNumberOfTestTypes", connection);
            command.CommandType = CommandType.StoredProcedure;
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
        public static bool FindApplicationType(int ID , ref string Title,ref string Description ,ref double Fees)
        {
            bool Done = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_FindTestTypeByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    Done = true;
                    Title = reader["TestTypeTitle"].ToString();
                    Description = reader["TestTypeDescription"].ToString();
                    Fees = Convert.ToDouble(reader["TestTypeFees"]);
                }
            }
            catch(Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return Done;
        }
        public static bool UpdateType(int ID, string Title,string Description, double Fees)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_UpdateTestType", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Fees", Fees);
            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();

            }catch(Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return RowsAffected > 0;
        }
        public static double getTestTypeFees(int ID)
        {
            double fees = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_GetTestTypeFees", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                fees = Convert.ToDouble(command.ExecuteScalar());
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return fees;
        }
        public static double getRetakeTestFees()
        {
            double fees = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_GetRetakeTestFees", connection);
            command.CommandType = CommandType.StoredProcedure;
           
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

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace DataAccessLayer
{
    public static class clsDriverLicensesClassDAC
    {
        public static List<string> getClassesNames()
        {
            List<string> list = new List<string>();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT ClassName From LicenseClasses";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader["ClassName"].ToString());
                }
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return list;
        }
        public static decimal getLicenseFee(string ClassName)
        {
            decimal res = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT ClassFees From LicenseClasses Where ClassName = @ClassName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            try
            {
                connection.Open();
                res = Convert.ToDecimal(command.ExecuteScalar());
            }catch(Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally{ connection.Close(); }
            return res;
        }
        public static decimal getLicenseFee(int ClassID)
        {
            decimal res = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT ClassFees From LicenseClasses Where LicenseClassID = @ClassID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassID", ClassID);
            try
            {
                connection.Open();
                res = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return res;
        }
        public static short GetlicenseID(string ClassName)
        {
            short ID = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "Select LicenseClassID From LicenseClasses Where ClassName = @ClassName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            try
            {
                connection.Open();
                ID = Convert.ToInt16(command.ExecuteScalar());

            }catch(Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return ID;
        }
        public static string GetClassName(int ClassID)
        {
            string Classname = string.Empty;

            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT ClassName from LicenseClasses WHERE LicenseClassID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID",ClassID);
            try
            {
                connection.Open();
                Classname = command.ExecuteScalar().ToString();
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return Classname;
        }
        public static int GetLicenseValidationPeriod(int ClassID)
        {
            int Period = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT DefaultValidityLength FROM LicenseClasses WHERE LicenseClassID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ClassID);
            try
            {
                connection.Open();
                Period = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return Period;
        }
    }
}

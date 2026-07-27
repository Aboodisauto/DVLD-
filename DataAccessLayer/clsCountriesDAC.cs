using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsCountriesDAC
    {
        public static DataTable FetchCountries()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_FetchCountries", connection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static string CountryNamee(int ID)
        {
            string countryName = string.Empty;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_GetCountryNameByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CountryID", ID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null)
                {
                    countryName = result.ToString();
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return countryName;
        }
    }
}

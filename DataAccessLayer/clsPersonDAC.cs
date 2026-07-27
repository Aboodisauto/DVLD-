using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsPersonDAC
    {
        public static int AddPerson(string FirstName, string SecondName, string ThirdName, string LastName,
            string NationalNo, string Address, string Phone, string Email, int Country, int Gendor, DateTime DateOfBirth, string ImagePath)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_AddPerson", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", Country);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            if (ImagePath == null)
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            try
            {
                connection.Open();
                ID = Convert.ToInt32(command.ExecuteScalar());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return ID;
        }
        public static int GetPersonID(string nationalNo)
        {
            int Id = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_GetPersonIDByNationalNo", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NationalNo", nationalNo);
            try
            {
                connection.Open();
                Id = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return Id;
        }
        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName, string ThirdName, string LastName,
            string NationalNo, string Address, string Phone, string Email, int Country, int Gendor, DateTime DateOfBirth, string ImagePath)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_UpdatePerson", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Country", Country);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            if (ImagePath == null)
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }
        public static bool FindPerson(int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
            ref string NationalNo, ref string Address, ref string Phone, ref string Email, ref int Country, ref int Gendor, ref DateTime DateOfBirth, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_FindPersonByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    ThirdName = reader["ThirdName"].ToString();
                    LastName = reader["LastName"].ToString();
                    NationalNo = reader["NationalNo"].ToString();
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"].ToString();
                    Country = Convert.ToInt32(reader["NationalityCountryID"]);
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    ImagePath = reader["ImagePath"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool FindPerson(string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
            ref int PersonID, ref string Address, ref string Phone, ref string Email, ref int Country, ref int Gendor, ref DateTime DateOfBirth, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_FindPersonByNationalNo", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    ThirdName = reader["ThirdName"].ToString();
                    LastName = reader["LastName"].ToString();
                    PersonID = (int)reader["PersonID"];
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"].ToString();
                    Country = Convert.ToInt32(reader["NationalityCountryID"]);
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    ImagePath = reader["ImagePath"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool DeletePerson(int PersonID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_DeletePerson", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }
        public static DataTable FetchPeople()
        {
            DataTable dtPeople = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_FetchPeople", connection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.HasRows)
                {
                    dtPeople.Load(Reader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dtPeople;
        }
        public static bool DoesPersonExist(int ID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_DoesPersonExistByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (Convert.ToInt16(result) == 1)
                    isFound = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool DoesPersonExist(string NationalNo)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_DoesPersonExistByNationalNo", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (Convert.ToInt16(result) == 1)
                    isFound = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static string GetNationalNo(int PersonID)
        {
            string NationalNo = null;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_GetNationalNo", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                    NationalNo = result.ToString();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return NationalNo;
        }
        public static int GetPersonIDByDriverID(int driverID)
        {
            int PersonID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand("sp_GetPersonIDByDriverID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DriverID", driverID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                    PersonID = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            } 

            return PersonID;
        }
    }
}

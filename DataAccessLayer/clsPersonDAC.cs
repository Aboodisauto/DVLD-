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
            string query = "INSERT INTO [dbo].[People]\r\n           ([NationalNo]\r\n           ,[FirstName]\r\n           ,[SecondName]\r\n           ,[ThirdName]\r\n           ,[LastName]\r\n           ,[DateOfBirth]\r\n           ,[Gendor]\r\n           ,[Address]\r\n           ,[Phone]\r\n           ,[Email]\r\n           ,[NationalityCountryID]\r\n           ,[ImagePath])\r\n     VALUES\r\n           (@NationalNo\r\n           ,@FirstName\r\n           ,@SecondName\r\n           ,@ThirdName\r\n           ,@LastName\r\n           ,@DateOfBirth\r\n           ,@Gendor\r\n           ,@Address\r\n           ,@Phone\r\n           ,@Email\r\n           ,@NationalityCountryID\r\n           ,@ImagePath); SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "SELECT PersonID From People WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "UPDATE People SET FirstName=@FirstName, SecondName=@SecondName, ThirdName=@ThirdName, LastName=@LastName, " +
                "NationalNo=@NationalNo, Address=@Address, Phone=@Phone, Email=@Email, NationalityCountryID=@Country, Gendor=@Gendor, DateOfBirth=@DateOfBirth, ImagePath=@ImagePath " +
                "WHERE PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "SELECT * FROM People WHERE PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "SELECT * FROM People WHERE NationalNo=@NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "DELETE FROM People WHERE PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "SELECT        People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,CASE Gendor\r\nWHEN 0 THEN 'Male'\r\nWHEN 1 THEN 'Female'\r\nEND AS Gendor, Countries.CountryName,\r\n\r\nPeople.Phone, People.Email\r\nFROM            Countries INNER JOIN\r\n                         People ON Countries.CountryID = People.NationalityCountryID";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "SELECT Found=1 FROM People WHERE PersonID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "SELECT Found=1 FROM People WHERE NationalNo= @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "SELECT NationalNo FROM People WHERE PersonID= @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
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
            string query = "SELECT PersonID FROM Drivers WHERE DriverID = @DriverID";
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            SqlCommand command = new SqlCommand(query, connection);
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

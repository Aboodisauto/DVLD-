using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using System.IO;

namespace DataAccessLayer
{
    public class clsUserDAC
    {
        public static bool FindUserByID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool isActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT * FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    isFound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    UserName = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    isActive = Convert.ToBoolean(reader["isActive"]);
                }
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
        public static DataTable FetchUsers()
        {
            DataTable dtUsers = new DataTable();
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT UserID as 'User ID',Users.PersonID as 'Person ID',Fullname = FirstName + ' ' + secondname + ' ' + thirdname + ' ' + lastname , Users.UserName, Users.Password, Users.IsActive FROM People Inner join Users ON Users.PersonID = People.PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtUsers.Load(reader);
                }
                reader.Close();
            }
            catch(Exception ex)
            {
            }finally
            {
                connection.Close();
            }
            return dtUsers;
        }
        public static bool DoesUserExist(string username, string password)
        {
            int isFound = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT Isfound = 1 FROM Users WHERE UserName = @UserName AND Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", username);
            command.Parameters.AddWithValue("@Password", password);
            try
            {
                connection.Open();
                isFound = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return isFound == 1;
        }
        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "DELETE FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected > 0;
        }
        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool isActive)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "UPDATE Users SET PersonID = @PersonID, UserName = @UserName, Password = @Password, isActive = @isActive WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isActive", isActive);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected > 0;
        }
        public static bool AddUser(int PersonID, string UserName, string Password,  bool isActive)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "INSERT INTO Users (PersonID, UserName, Password, isActive) VALUES (@PersonID, @UserName, @Password, @isActive)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isActive", isActive);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected > 0;
        }
        public static bool CheckUser(string UserName,string Password)
        {
            bool isValid = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT COUNT(*) FROM Users WHERE UserName = @UserName AND Password = @Password AND isActive = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            try
            {
                connection.Open();
                isValid = Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return isValid;
        }
        public static int GetUserID(string Username,string Password)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT UserID FROM Users WHERE UserName = @UserName AND Password = @Password AND isActive = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", Username);
            command.Parameters.AddWithValue("@Password", Password);
            try
            {
                connection.Open();
                var result = command.ExecuteScalar();
                if(result != null)
                {
                    UserID = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return UserID;
        }
        public static bool GetUserByPersonID(int PersonID, ref int UserID, ref string Username,ref string Password,ref bool isActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT * FROM Users WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader= command.ExecuteReader();
                while(reader.Read())
                {
                    isFound = true;
                    UserID = Convert.ToInt32(reader["UserID"]);
                    Username = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    isActive = Convert.ToBoolean(reader["isActive"]);
                }
                reader.Close();
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
        public static string GetUserName(int UserID)
        {
            string UserName = "";
            SqlConnection connection = new SqlConnection(clsSettingsDAC.connectionString);
            string query = "SELECT UserName FROM Users WHERE UserID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", UserID);
            try
            {
                connection.Open();
                UserName = Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception ex) { File.AppendAllText(clsSettingsDAC.LogPath, DateTime.Now + ex.Message + "\n"); }
            finally { connection.Close(); }
            return UserName;
        }
    }
}

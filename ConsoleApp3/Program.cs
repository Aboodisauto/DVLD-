using System;
using System.Data;
using System.Data.SqlClient;


class Program
{
    static void Main()
    {
        string connectionString = "YourConnectionStringHere";
        using SqlConnection connection = new SqlConnection(connectionString);
        using SqlCommand command = new SqlCommand("SP_AddNewPerson", connection);
        command.CommandType = CommandType.StoredProcedure;


        // Add parameters
        command.Parameters.AddWithValue("@FirstName", "John");
        command.Parameters.AddWithValue("@LastName", "Doe");
        command.Parameters.AddWithValue("@Email", "john.doe@example.com");
        SqlParameter outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };
        command.Parameters.Add(outputIdParam);


        // Execute
        connection.Open();
        command.ExecuteNonQuery();


        // Retrieve the ID of the new person
        int newPersonID = (int)command.Parameters["@NewPersonID"].Value;
        Console.WriteLine($"New Person ID: {newPersonID}");


        connection.Close();
    }
}

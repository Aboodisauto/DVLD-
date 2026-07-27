using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


class Program
{
    public static string connectionString = "Server=.;Database=C21_DB1;User Id=sa;Password=123456;";
    public static DataTable GetAllEmployees()
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string sql = "SELECT * FROM Employees2";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }
        return dt;
    }
    public static void SetSalary(string Name, int Salary)
    {
        using(SqlConnection connection = new SqlConnection(connectionString)) {
            string query = "UPDATE Employees2 SET Salary = @Salary WHERE Name = @Name";
            using(SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Salary", Salary);
                command.Parameters.AddWithValue("@Name", Name);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
    static void Main()
    {
        DataTable employees = GetAllEmployees();
        DataColumn bonuses = new DataColumn("Bonus", typeof(int));
        bonuses.DefaultValue = 0;
        employees.Columns.Add(bonuses);
        foreach(DataRow row in employees.Rows)
        {
            int bonus = Convert.ToInt32(row["salary"]);
            int rating = Convert.ToInt32(row["Performancerating"]);
            string department = row["Department"].ToString();
            switch (department)
            {
                case "HR":
                    {
                        if (Convert.ToInt32(row["Performancerating"]) >= 90)
                        {
                            bonus = Convert.ToInt32(bonus * .15);

                        }
                        else if (rating >= 74 && rating < 90)
                        {
                            bonus = Convert.ToInt32(bonus * .10);
                        }
                        else
                        {
                            bonus = Convert.ToInt32(bonus * .05);
                        }
                        break;
                    }
                case "Marketing":
                    {
                        if (Convert.ToInt32(row["Performancerating"]) >= 90)
                        {
                            bonus = Convert.ToInt32(bonus * .12);

                        }
                        else if (rating >= 74 && rating < 90)
                        {
                            bonus = Convert.ToInt32(bonus * .08);
                        }
                        else
                        {
                            bonus = Convert.ToInt32(bonus * .04);
                        }
                        break;
                    }
                default:
                    if (Convert.ToInt32(row["Performancerating"]) >= 90)
                    {
                        bonus = Convert.ToInt32(bonus * .10);

                    }
                    else if (rating >= 74 && rating < 90)
                    {
                        bonus = Convert.ToInt32(bonus * .06);
                    }
                    else
                    {
                        bonus = Convert.ToInt32(bonus * .03);
                    }
                    break;
            }
            row["Bonus"] = bonus;
            
        }
        foreach(DataRow row in employees.Rows)
        {
            Console.WriteLine($"Name:{row["Name"]}\tDepartment:{row["Department"]}\tBonus:{row["Bonus"]}");

        }

    }
}


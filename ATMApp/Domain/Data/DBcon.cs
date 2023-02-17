using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Domain.Data
{
    public static class DBcon 
    {
        public static SqlConnection GetConnection() 
        {
            string connectionString = "Data Source=DESKTOP-J5V3R18\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";

            //string connectionString = "Data Source=DESKTOP-J5V3R18\\SQLEXPRESS;Initial Catalog=ATMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
           using SqlConnection connection = new SqlConnection(connectionString);
            try
            {

                    connection.Open();
                    string script = System.IO.File.ReadAllText("script.sql");
                    using (SqlCommand command = new SqlCommand(script, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                Console.WriteLine("SQL script executed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadLine();

            return connection;

        }


    }
}

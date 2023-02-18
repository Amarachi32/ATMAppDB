using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ATMApp.Domain.Data
{
    public static class DBcon
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=DESKTOP-J5V3R18\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();
                string script = System.IO.File.ReadAllText("script.sql");
                IEnumerable<string> commands = script.Split(new[] { "GO\r\n", "GO ", "GO\t" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string command in commands)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
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

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
            string connectionString = "Data Source=DESKTOP-J5V3R18\\SQLEXPRESS;Initial Catalog=ATMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}

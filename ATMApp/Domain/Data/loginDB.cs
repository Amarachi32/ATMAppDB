using ATMApp.Domain.Entities;
using ATMApp.UI;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ATMApp.Domain.Data
{
    public class loginDB
    {
        public static UserAccount LoginUser(int cardNumber, int cardPin)
        {
            SqlConnection connection = DBcon.GetConnection();
            SqlCommand command = new SqlCommand("CheckUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CardNumber", cardNumber);
            command.Parameters.AddWithValue("@CardPin", cardPin);
            UserAccount user = new UserAccount();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    user.FullName = reader["FullName"].ToString();
                    user.CardNumber = Convert.ToInt32(reader["CardNumber"].ToString());
                    user.CardPin = Convert.ToInt32(reader["CardPin"].ToString());
                    user.AccountBalance = Convert.ToInt32(reader["AccountBalance"].ToString());
                    user.AccountNumber = Convert.ToInt32(reader["AccountNumber"].ToString());

                    if (user.CardNumber == cardNumber && user.CardPin == cardPin)
                    {
                        Console.WriteLine("login successful");

                    }
                    else
                    {
                        Utility.PrintMessage("\nInvalid card number or PIN.", false);
                        user.IsLocked = user.TotalLogin == 3;
                        if (user.IsLocked)
                        {
                            AppScreen.PrintLockScreen();
                        }
                    }

                }
                reader.Close();


            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return user;
        }
    }
}

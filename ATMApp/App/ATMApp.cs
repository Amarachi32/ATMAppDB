using ATMApp.Domain.Data;
using ATMApp.Domain.Entities;
using ATMApp.Domain.Enums;
using ATMApp.UI;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ATMApp
{
    public class ATMApp
    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;
        private List<Transaction> _listOfTransactions;
        private readonly AppScreen screen;
        private loginDB _loginDB;
        private static UserAccount userAccount;
        public ATMApp()
        {
            screen = new AppScreen();
            _loginDB = new loginDB();
        }

        public void Run()
        {
            AppScreen.Welcome();
            var userDetail = AppScreen.UserLoginForm();
            userAccount = loginDB.LoginUser((int)userDetail.CardNumber, userDetail.CardPin);
            AppScreen.DisplayAppMenu();
            Console.ReadLine();
            //AppScreen.WelcomeCustomer(userAccount.FullName);
            while (true)
            {
                AppScreen.DisplayAppMenu();
                ProcessMenuoption(userAccount);
            }
        }
        public void InitializeData(UserAccount user)
        {

            _listOfTransactions = new List<Transaction>();

        }

        public void ProcessMenuoption(UserAccount user)
        {
            switch (Validator.Convert<int>("an option:"))
            {
                case (int)AppMenu.CheckBalance:
                    CheckBalance(user);
                    break;
                case (int)AppMenu.PlaceDeposit:
                    var amount = Validator.Convert<int>($"amount {AppScreen.cur}");
                    PlaceDeposit(user, amount);
                    break;
                case (int)AppMenu.MakeWithdrawal:
                    var amountW = Validator.Convert<int>($"amount {AppScreen.cur}");
                    MakeWithDrawal(user, amountW);
                    break;
                case (int)AppMenu.InternalTransfer:
                    var internalTransfer = screen.InternalTransferForm();
                    ProcessInternalTransfer(internalTransfer, user);
                    break;
                case (int)AppMenu.ViewTransaction:
                    ViewTransaction(user);
                    break;
                case (int)AppMenu.Logout:
                    AppScreen.LogoutProgress();
                    Utility.PrintMessage("You have successfully logged out. Please collect " +
                        "your ATM card.");
                    Run();
                    break;
                default:
                    Utility.PrintMessage("Invalid Option.", false);
                    break;
            }
        }

        public void CheckBalance(UserAccount user)
        {
            //var user = loginDB.LoginUser();
            Utility.PrintMessage($"Your account balance is: {Utility.FormatAmount(user.AccountBalance)}");
        }

        public void PlaceDeposit(UserAccount user, int amount)
        {
            SqlConnection connection = DBcon.GetConnection();
            SqlCommand command = new SqlCommand("MakeDeposit", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cardPin", user.CardPin);
            command.Parameters.AddWithValue("@amount", amount);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.AccountBalance = Convert.ToInt32(reader["AccountBalance"].ToString());

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
            //InsertTransaction(user.Id, TransactionType.Deposit, amount, "");


        }

        public void MakeWithDrawal(UserAccount user, int amount)
        {
            SqlConnection connection = DBcon.GetConnection();
            SqlCommand command = new SqlCommand("MakeWithrawal", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cardPin", user.CardPin);
            command.Parameters.AddWithValue("@amount", amount);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.AccountBalance = Convert.ToInt32(reader["AccountBalance"].ToString());

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

            //InsertTransaction(user.Id, TransactionType.Withdrawal, amount, "");

        }


        public void InsertTransaction(long _UserBankAccountId, TransactionType _tranType, decimal _tranAmount, string _desc)
        {
            SqlConnection connection = DBcon.GetConnection();
            SqlCommand command = new SqlCommand("InsertTransaction", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@transactionId", Utility.GetTransactionId());
            command.Parameters.AddWithValue("@userAccountId", _UserBankAccountId);
            command.Parameters.AddWithValue("@transactionType", _tranType);
            command.Parameters.AddWithValue("@transactionAmount", _tranAmount);
            command.Parameters.AddWithValue(" @transactionDate", DateTime.Now);
            command.Parameters.AddWithValue(" @desc", _desc);
            // command.ExecuteNonQuery();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //user.AccountBalance = Convert.ToInt32(reader["AccountBalance"].ToString());

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

            //add transaction object to the list
            //_listOfTransactions.Add(transaction);
        }
        /*        public void InsertTransaction(long _UserBankAccountId, TransactionType _tranType, decimal _tranAmount, string _desc)
                {
                    //create a new transaction object
                    var transaction = new Transaction()
                    {
                        //TransactionId = Utility.GetTransactionId(),
                        UserBankAccountId = _UserBankAccountId,
                        TransactionDate = DateTime.Now,
                        TransactionType = _tranType,
                        TransactionAmount = _tranAmount,
                        Descriprion = _desc
                    };

                    //add transaction object to the list
                    _listOfTransactions.Add(transaction);
                }
        */
        public void ViewTransaction(UserAccount user)

        {
            SqlConnection connection = DBcon.GetConnection();
            SqlCommand command = new SqlCommand("ViewTransactions", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", user.Id);

            try
            {
                connection.Open();
                /*                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                                {
                                    DataTable transactionsTable = new DataTable();
                                    adapter.Fill(transactionsTable);


                                    //dataGridView1.DataSource = transactionsTable;
                                }*/
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime TransactionDate = reader.GetDateTime(1);
                        decimal TransactionAmount = reader.GetDecimal(2);
                        string TransactionType = reader.GetString(5);


                        Console.WriteLine($"{TransactionDate}: {TransactionType} {TransactionAmount:C}");
                    }
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        private void ProcessInternalTransfer(InternalTransfer internalTransfer, UserAccount user)
        {

            SqlConnection connection = DBcon.GetConnection();
            SqlCommand command = new SqlCommand("MakeTransfer", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ToAccountNumber", internalTransfer.ReciepeintBankAccountNumber);
            command.Parameters.AddWithValue("@TransferAmount", internalTransfer.TransferAmount);
            command.Parameters.AddWithValue("@FromAccountNumber", user.AccountNumber);
            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    user.AccountBalance = Convert.ToInt32(reader["AccountBalance"].ToString());

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
            var selectedBankAccountReciever = (from userAcc in userAccountList
                                               where userAcc.AccountNumber == internalTransfer.ReciepeintBankAccountNumber
                                               select userAcc).FirstOrDefault();
            if (selectedBankAccountReciever == null)
            {
                Utility.PrintMessage("Transfer failed. Recieber bank account number is invalid.", false);
                return;
            }
            //check receiver's name
            if (selectedBankAccountReciever.FullName != internalTransfer.RecipientBankAccountName)
            {
                Utility.PrintMessage("Transfer Failed. Recipient's bank account name does not match.", false);
                return;
            }

            InsertTransaction(user.Id, TransactionType.Transfer, -internalTransfer.TransferAmount, "Transfered " +
                $"to {selectedBankAccountReciever.AccountNumber} ({selectedBankAccountReciever.FullName})");
            //receiver
            InsertTransaction(selectedBankAccountReciever.Id, TransactionType.Transfer, internalTransfer.TransferAmount, "Transfered from " +
                $"{selectedAccount.AccountNumber}({selectedAccount.FullName})");

            //print success message
            Utility.PrintMessage($"You have successfully transfered" +
                $" {Utility.FormatAmount(internalTransfer.TransferAmount)} to " +
                $"{internalTransfer.RecipientBankAccountName}", true);
        }

    }
}

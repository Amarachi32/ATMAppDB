using ATMApp.Domain.Entities;
using System;

namespace ATMApp.UI
{
    public class AppScreen
    {
        internal const string cur = "#";
        internal static void Welcome()
        {
            Console.Clear();
            Console.Title = "BEZAO ATM App";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n-----------------Welcome to Bezao App-----------------\n\n");
            Console.WriteLine("Please insert your ATM card");
            Console.WriteLine("Note: Bezao ATM machine will" +
                "  read the card number and validate it.");
            Utility.PressEnterToContinue();
        }

        internal static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new UserAccount();

            tempUserAccount.CardNumber = Validator.Convert<int>("your card number.");
            tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretInput("Enter your card PIN"));
            return tempUserAccount;
        }

        internal static void LoginProgress()
        {
            Console.WriteLine("\nChecking card number and PIN...");
            Utility.PrintDotAnimation();
        }

        internal static void PrintLockScreen()
        {
            Console.Clear();
            Utility.PrintMessage("Your account is locked. Please go to the nearest branch" +
                " to unlock your account. Thank you.", true);
            Environment.Exit(1);
        }

        internal static void WelcomeCustomer(UserAccount user)
        {
            Console.WriteLine($"Welcome back, {user.FullName}");
            Utility.PressEnterToContinue();
        }

        internal static void DisplayAppMenu()
        {
            Console.Clear();
            Console.WriteLine("-------My ATM App Menu-------");
            Console.WriteLine(":                           :");
            Console.WriteLine("1. Account Balance          :");
            Console.WriteLine("2. Cash Deposit             :");
            Console.WriteLine("3. Withdrawal               :");
            Console.WriteLine("4. Transfer                 :");
         //   Console.WriteLine("5. Transactions             :");
            Console.WriteLine("6. Logout                   :");
        }

        internal static void LogoutProgress()
        {
            Console.WriteLine("Thank you for using My ATM App.");
            Utility.PrintDotAnimation();
            Console.Clear();
        }

        internal InternalTransfer InternalTransferForm()
        {
            var internalTransfer = new InternalTransfer();
            internalTransfer.ReciepeintBankAccountNumber = Validator.Convert<long>("recipient's account number:");
            internalTransfer.TransferAmount = Validator.Convert<decimal>($"amount {cur}");
            return internalTransfer;
        }
    }
}

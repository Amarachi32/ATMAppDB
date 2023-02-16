using ATMApp.Domain.Data;
using ATMApp.Domain.Entities;
using ATMApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.App
{
    class Entry
    {
        private static UserAccount userAccount;
        static void Main(string[] args)
        {

            
            ATMApp atmApp = new ATMApp();
            atmApp.Run();
            var userDetail = AppScreen.UserLoginForm();
            userAccount = loginDB.LoginUser((int)userDetail.CardNumber, userDetail.CardPin);
            AppScreen.DisplayAppMenu();
            atmApp.ProcessMenuoption(userAccount);
                        
           
        }
    }
}

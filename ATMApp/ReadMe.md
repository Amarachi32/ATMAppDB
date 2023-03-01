# ATM Operations with ADO.NET🤷‍♀️

This program demonstrates how to use ADO.NET to interact with a SQL Server database to perform basic ATM operations,
including withdrawing cash, depositing cash, and checking account balances.
The program uses a console-based interface to prompt the user for input and display the results of each operation.

## Requirements

To run this program, you will need the following:

 + Visual Studio 2019 or later
 + SQL Server
 

 ## Getting Started
 To get started, clone the repository to your local machine and open the solution in Visual Studio.
 
 ## Database Setup
 
 Before running the application, you will need to create a database and update the
 connection string inside the program. Here are the steps to create the database:
 
 + copy the sql script at the root folder and paste it inside your bin file.
 + Run the program to create the database and the necessary tables:
  + Open SQL Server Management Studio or other database management tool.
 + update the connection string in your file to point to your database.

 ## Running the Application

 When you run the application, you will be prompted to log in with a cardnumber and cardpin.
 You can use the following credentials to log in:
   + CardNumber: 321321/ 654654 / 987987
   + CardPin: 123123/    456456 / 789789
   
   **other detail**
   + Account Number:  123456 / 456789 / 123555

   After logging in, you will be presented with a menu of options:
   1. View Balance
   2. Deposit
   3. Withdraw
   4. Transfer
   5. View Transaction
   Log Out

To perform an operation, simply select the corresponding menu option and follow the prompts.


## Code Overview
The application is built using Entity Framework Core and C#. The main classes are:
   
`UserAccount`: The entity that represents a user in the database. It has properties for the user's ID, cardnumber, cardpin, accountnumber etc.

`Transaction`: The entity that represents a transaction in the database. It has properties for the transaction's ID, user ID, type (deposit or withdrawal), amount, and date.

`Entry`: The main program class that handles user input and output.

The application uses Entity Framework Core to interact with the database. When the user performs an operation, the application retrieves the necessary data from the database,
performs the operation, and saves the changes back to the database.

## Conclusion
This project demonstrates how to perform basic ATM operations using Entity Framework Core in C#. It includes functionality for user login, deposit, withdrawal, and transaction history. You can use this project as a starting point 
for your own ATM application or as a learning resource to help you understand Entity Framework Core.

## Software Development Summary😃👓👓
* **Technology**: C#👓
* **Framework**: .NET6
* **Project Type**: Console
* **IDE**: Visual Studio (Version 2022)
* **Paradigm or pattern of programming**: Object-Oriented Programming (OOP)
* **DataBase**:Sql Server
* **Data**:Entity Framework





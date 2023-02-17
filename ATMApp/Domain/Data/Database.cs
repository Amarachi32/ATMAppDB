using System.Threading.Tasks;

namespace ATMApp.Domain.Data
{
    public class Database
    {
        public async Task CreateSchemaAndData()
        {
            // Connection string for the local SQL Server instance
            string database = @"
                USE [master]
                GO
                /****** Object:  Database [ATMDB]    Script Date: 16/02/2023 19:17:10 ******/
                CREATE DATABASE [ATMDB]
                 CONTAINMENT = NONE
                 ON  PRIMARY 
                ( NAME = N'ATMDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ATMDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
                 LOG ON 
                ( NAME = N'ATMDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ATMDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
                 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
                GO
                ALTER DATABASE [ATMDB] SET COMPATIBILITY_LEVEL = 160
                GO
                IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
                begin
                EXEC [ATMDB].[dbo].[sp_fulltext_database] @action = 'enable'
                end
                GO
                ALTER DATABASE [ATMDB] SET ANSI_NULL_DEFAULT OFF 
                GO
                ALTER DATABASE [ATMDB] SET ANSI_NULLS OFF 
                GO
                ALTER DATABASE [ATMDB] SET ANSI_PADDING OFF 
                GO
                ALTER DATABASE [ATMDB] SET ANSI_WARNINGS OFF 
                GO
                ALTER DATABASE [ATMDB] SET ARITHABORT OFF 
                GO
                ALTER DATABASE [ATMDB] SET AUTO_CLOSE OFF 
                GO
                ALTER DATABASE [ATMDB] SET AUTO_SHRINK OFF 
                GO
                ALTER DATABASE [ATMDB] SET AUTO_UPDATE_STATISTICS ON 
                GO
                ALTER DATABASE [ATMDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
                GO
                ALTER DATABASE [ATMDB] SET CURSOR_DEFAULT  GLOBAL 
                GO
                ALTER DATABASE [ATMDB] SET CONCAT_NULL_YIELDS_NULL OFF 
                GO
                ALTER DATABASE [ATMDB] SET NUMERIC_ROUNDABORT OFF 
                GO
                ALTER DATABASE [ATMDB] SET QUOTED_IDENTIFIER OFF 
                GO
                ALTER DATABASE [ATMDB] SET RECURSIVE_TRIGGERS OFF 
                GO
                ALTER DATABASE [ATMDB] SET  DISABLE_BROKER 
                GO
                ALTER DATABASE [ATMDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
                GO
                ALTER DATABASE [ATMDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
                GO
                ALTER DATABASE [ATMDB] SET TRUSTWORTHY OFF 
                GO
                ALTER DATABASE [ATMDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
                GO
                ALTER DATABASE [ATMDB] SET PARAMETERIZATION SIMPLE 
                GO
                ALTER DATABASE [ATMDB] SET READ_COMMITTED_SNAPSHOT OFF 
                GO
                ALTER DATABASE [ATMDB] SET HONOR_BROKER_PRIORITY OFF 
                GO
                ALTER DATABASE [ATMDB] SET RECOVERY SIMPLE 
                GO
                ALTER DATABASE [ATMDB] SET  MULTI_USER 
                GO
                ALTER DATABASE [ATMDB] SET PAGE_VERIFY CHECKSUM  
                GO
                ALTER DATABASE [ATMDB] SET DB_CHAINING OFF 
                GO
                ALTER DATABASE [ATMDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
                GO
                ALTER DATABASE [ATMDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
                GO
                ALTER DATABASE [ATMDB] SET DELAYED_DURABILITY = DISABLED 
                GO
                ALTER DATABASE [ATMDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
                GO
                ALTER DATABASE [ATMDB] SET QUERY_STORE = ON
                GO
                ALTER DATABASE [ATMDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
                GO
                USE [ATMDB]
                GO
                /****** Object:  Table [dbo].[InternalTransfer]    Script Date: 16/02/2023 19:17:10 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                CREATE TABLE [dbo].[InternalTransfer](
	                [TransferAmount] [datetime] NOT NULL,
	                [ReciepeintBankAccountNumber] [int] NOT NULL,
	                [RecipientBankAccountName] [varchar](1) NULL,
	                [InternalTransferId] [int] NOT NULL,
                 CONSTRAINT [PK_InternalTransfer] PRIMARY KEY CLUSTERED 
                (
	                [InternalTransferId] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                ) ON [PRIMARY]
                GO
                /****** Object:  Table [dbo].[Transactions]    Script Date: 16/02/2023 19:17:10 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                CREATE TABLE [dbo].[Transactions](
	                [TransactionDate] [datetime] NOT NULL,
	                [TransactionAmount] [decimal](18, 0) NOT NULL,
	                [Descriprion] [varchar](1) NULL,
	                [TransactionType] [int] NOT NULL,
	                [TransactionId] [int] NOT NULL,
	                [UserAccountId] [int] NOT NULL,
                 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
                (
	                [TransactionId] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                ) ON [PRIMARY]
                GO
                /****** Object:  Table [dbo].[TransactionTypes]    Script Date: 16/02/2023 19:17:10 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                CREATE TABLE [dbo].[TransactionTypes](
	                [id] [int] NOT NULL,
	                [TypeName] [varchar](20) NOT NULL,
                PRIMARY KEY CLUSTERED 
                (
	                [id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                ) ON [PRIMARY]
                GO
                /****** Object:  Table [dbo].[UserAccounts]    Script Date: 16/02/2023 19:17:10 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                CREATE TABLE [dbo].[UserAccounts](
	                [CardNumber] [int] NOT NULL,
	                [CardPin] [int] NOT NULL,
	                [AccountNumber] [int] NOT NULL,
	                [FullName] [nvarchar](50) NOT NULL,
	                [AccountBalance] [decimal](18, 0) NOT NULL,
	                [TotalLogin] [int] NULL,
	                [IsLocked] [bit] NOT NULL,
	                [UserAccountId] [int] IDENTITY(1,1) NOT NULL,
                PRIMARY KEY CLUSTERED 
                (
	                [UserAccountId] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                ) ON [PRIMARY]
                GO
                INSERT [dbo].[TransactionTypes] ([id], [TypeName]) VALUES (1, N'Deposit')
                GO
                INSERT [dbo].[TransactionTypes] ([id], [TypeName]) VALUES (3, N'ransfer')
                GO
                INSERT [dbo].[TransactionTypes] ([id], [TypeName]) VALUES (2, N'Withdrawal')
                GO
                SET IDENTITY_INSERT [dbo].[UserAccounts] ON 
                GO
                INSERT [dbo].[UserAccounts] ([CardNumber], [CardPin], [AccountNumber], [FullName], [AccountBalance], [TotalLogin], [IsLocked], [UserAccountId]) VALUES (321321, 123123, 123456, N'maraxhi', CAST(55820 AS Decimal(18, 0)), NULL, 0, 1)
                GO
                INSERT [dbo].[UserAccounts] ([CardNumber], [CardPin], [AccountNumber], [FullName], [AccountBalance], [TotalLogin], [IsLocked], [UserAccountId]) VALUES (987987, 789789, 123555, N'Chris', CAST(61200 AS Decimal(18, 0)), NULL, 1, 2)
                GO
                INSERT [dbo].[UserAccounts] ([CardNumber], [CardPin], [AccountNumber], [FullName], [AccountBalance], [TotalLogin], [IsLocked], [UserAccountId]) VALUES (654654, 456456, 456789, N'mccoy', CAST(71000 AS Decimal(18, 0)), NULL, 0, 3)
                GO
                SET IDENTITY_INSERT [dbo].[UserAccounts] OFF
                GO
                SET ANSI_PADDING ON
                GO
                /****** Object:  Index [UQ__Transact__D4E7DFA85A706091]    Script Date: 16/02/2023 19:17:10 ******/
                ALTER TABLE [dbo].[TransactionTypes] ADD UNIQUE NONCLUSTERED 
                (
	                [TypeName] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                GO
                ALTER TABLE [dbo].[UserAccounts] ADD  CONSTRAINT [DF__UserAccou__IsLoc__440B1D61]  DEFAULT ((0)) FOR [IsLocked]
                GO
                ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD FOREIGN KEY([TransactionType])
                REFERENCES [dbo].[TransactionTypes] ([id])
                GO
                /****** Object:  StoredProcedure [dbo].[CheckUser]    Script Date: 16/02/2023 19:17:10 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                create PROCEDURE [dbo].[CheckUser]  
                (  
                  @CardNumber as int, @CardPin as int  
                )  
                AS  
                SELECT * FROM UserAccounts WHERE CardNumber = @CardNumber AND CardPin = @CardPin 
                GO
                /****** Object:  StoredProcedure [dbo].[InsertTransaction]    Script Date: 16/02/2023 19:17:10 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                CREATE PROCEDURE [dbo].[InsertTransaction]
                    @transactionId int,
                    @userAccountId INT,
                    @transactionType VARCHAR(50),
                    @transactionAmount DECIMAL(18, 2),
                    @transactionDate DATETIME,
	                @desc VARCHAR(50)
                AS
                BEGIN
                    INSERT INTO Transactions (TransactionId,UserAccountId,TransactionDate, TransactionAmount, Descriprion, [TransactionType])
                    VALUES (@transactionId, @userAccountId, @transactionType, @transactionAmount, @transactionDate,@desc)
                END
                GO
                /****** Object:  StoredProcedure [dbo].[MakeDeposit]    Script Date: 16/02/2023 19:17:10 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                create procedure [dbo].[MakeDeposit]
                 @amount as decimal, @CardPin as int  
                as
                UPDATE UserAccounts
                SET AccountBalance = AccountBalance + @amount
                WHERE CardPin = @cardPin;
                GO
                /****** Object:  StoredProcedure [dbo].[MakeTransfer]    Script Date: 16/02/2023 19:17:10 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                create procedure [dbo].[MakeTransfer]
                    @FromAccountNumber INT,
                    @ToAccountNumber INT,
                    @TransferAmount DECIMAL(18,2)
	                AS
                BEGIN
                    SET NOCOUNT ON;
	                 -- Check if the account balance is greater than the amount to transfer
                    IF ((SELECT AccountBalance FROM UserAccounts WHERE AccountNumber = @FromAccountNumber) < @TransferAmount)
                    BEGIN
                        RAISERROR('Insufficient funds in the account.', 16, 1);
                        RETURN;
                    END

                    BEGIN TRY
                        BEGIN TRANSACTION;
                        UPDATE [dbo].[UserAccounts]
                        SET AccountBalance = AccountBalance - @TransferAmount
                        WHERE AccountNumber = @FromAccountNumber;

		                UPDATE UserAccounts
                        SET AccountBalance = AccountBalance + @TransferAmount
                        WHERE AccountNumber = @ToAccountNumber;

		                --INSERT INTO [dbo].[Transactions] (TransactionAmount, sender_account_number, receiver_account_number,[TransactionDate])
                --VALUES (@TransferAmount, sender_account_number, receiver_account_numbe, GETDATE());

		                COMMIT TRANSACTION;
                    END TRY
                    BEGIN CATCH
                        ROLLBACK TRANSACTION;
                        THROW;
                    END CATCH
                END
                GO
                /****** Object:  StoredProcedure [dbo].[MakeWithrawal]    Script Date: 16/02/2023 19:17:10 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                create procedure [dbo].[MakeWithrawal]
                 @amount as decimal, @CardPin as int  
                as
                UPDATE UserAccounts
                SET AccountBalance = AccountBalance - @amount
                WHERE CardPin = @cardPin;
                select AccountBalance from [dbo].[UserAccounts];
                GO
                /****** Object:  StoredProcedure [dbo].[ViewTransactions]    Script Date: 16/02/2023 19:17:10 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                CREATE PROCEDURE [dbo].[ViewTransactions]
                    @UserId int
                AS
                BEGIN
                    SELECT *
                    FROM Transactions
                    WHERE UserAccountId = @UserId
                END
                GO
                USE [master]
                GO
                ALTER DATABASE [ATMDB] SET  READ_WRITE 
                GO


            ";


           

        }
        public void InsertTransaction()
        {

        }
        public void ViewTransactions()
        {

        }
        public void Transactions()
        {

        }
        public void InternalTranfer()
        {

        }

    }
}

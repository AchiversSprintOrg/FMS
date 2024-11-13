USE [master]
GO
/****** Object:  Database [FinanceMangementSystem]    Script Date: 12-11-2024 22:06:09 ******/
CREATE DATABASE [FinanceMangementSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Finance', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Finance.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Finance_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Finance_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [FinanceMangementSystem] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FinanceMangementSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FinanceMangementSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [FinanceMangementSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FinanceMangementSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FinanceMangementSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FinanceMangementSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FinanceMangementSystem] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FinanceMangementSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FinanceMangementSystem] SET  MULTI_USER 
GO
ALTER DATABASE [FinanceMangementSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FinanceMangementSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FinanceMangementSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FinanceMangementSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FinanceMangementSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FinanceMangementSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FinanceMangementSystem] SET QUERY_STORE = ON
GO
ALTER DATABASE [FinanceMangementSystem] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [FinanceMangementSystem]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12-11-2024 22:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Budgets]    Script Date: 12-11-2024 22:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Budgets](
	[BudgetId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Category] [nvarchar](max) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Budgets] PRIMARY KEY CLUSTERED 
(
	[BudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expenses]    Script Date: 12-11-2024 22:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenses](
	[ExpenseId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Category] [nvarchar](max) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[ExpenseDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Expenses] PRIMARY KEY CLUSTERED 
(
	[ExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Incomes]    Script Date: 12-11-2024 22:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Incomes](
	[IncomeId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Source] [nvarchar](max) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[IncomeDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Incomes] PRIMARY KEY CLUSTERED 
(
	[IncomeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12-11-2024 22:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12-11-2024 22:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Budgets_UserId]    Script Date: 12-11-2024 22:06:11 ******/
CREATE NONCLUSTERED INDEX [IX_Budgets_UserId] ON [dbo].[Budgets]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Expenses_UserId]    Script Date: 12-11-2024 22:06:11 ******/
CREATE NONCLUSTERED INDEX [IX_Expenses_UserId] ON [dbo].[Expenses]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Incomes_UserId]    Script Date: 12-11-2024 22:06:11 ******/
CREATE NONCLUSTERED INDEX [IX_Incomes_UserId] ON [dbo].[Incomes]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_RoleId]    Script Date: 12-11-2024 22:06:11 ******/
CREATE NONCLUSTERED INDEX [IX_Users_RoleId] ON [dbo].[Users]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Budgets]  WITH CHECK ADD  CONSTRAINT [FK_Budgets_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Budgets] CHECK CONSTRAINT [FK_Budgets_Users_UserId]
GO
ALTER TABLE [dbo].[Expenses]  WITH CHECK ADD  CONSTRAINT [FK_Expenses_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Expenses] CHECK CONSTRAINT [FK_Expenses_Users_UserId]
GO
ALTER TABLE [dbo].[Incomes]  WITH CHECK ADD  CONSTRAINT [FK_Incomes_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Incomes] CHECK CONSTRAINT [FK_Incomes_Users_UserId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles_RoleId]
GO
/****** Object:  StoredProcedure [dbo].[SpExpenses]    Script Date: 12-11-2024 22:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SpExpenses] 
	-- Add the parameters for the stored procedure here
	
	 @UserId int,
     @Category nvarchar(max),
     @Amount decimal(18,2),
     @ExpenseDate datetime2(7)
AS
BEGIN
	

	INSERT INTO [dbo].[Expenses]
           ([UserId]
           ,[Category]
           ,[Amount]
           ,[ExpenseDate])
     VALUES
           (@UserId,@Category,@Amount,@ExpenseDate)
END
GO
USE [master]
GO
ALTER DATABASE [FinanceMangementSystem] SET  READ_WRITE 
GO

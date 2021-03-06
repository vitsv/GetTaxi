USE [master]
GO
/****** Object:  Database [GetTaxi]    Script Date: 01/12/2014 22:08:59 ******/
CREATE DATABASE [GetTaxi] ON  PRIMARY 
( NAME = N'GetTaxi', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\GetTaxi.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GetTaxi_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\GetTaxi_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GetTaxi] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GetTaxi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GetTaxi] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [GetTaxi] SET ANSI_NULLS OFF
GO
ALTER DATABASE [GetTaxi] SET ANSI_PADDING OFF
GO
ALTER DATABASE [GetTaxi] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [GetTaxi] SET ARITHABORT OFF
GO
ALTER DATABASE [GetTaxi] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [GetTaxi] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [GetTaxi] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [GetTaxi] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [GetTaxi] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [GetTaxi] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [GetTaxi] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [GetTaxi] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [GetTaxi] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [GetTaxi] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [GetTaxi] SET  DISABLE_BROKER
GO
ALTER DATABASE [GetTaxi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [GetTaxi] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [GetTaxi] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [GetTaxi] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [GetTaxi] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [GetTaxi] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [GetTaxi] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [GetTaxi] SET  READ_WRITE
GO
ALTER DATABASE [GetTaxi] SET RECOVERY SIMPLE
GO
ALTER DATABASE [GetTaxi] SET  MULTI_USER
GO
ALTER DATABASE [GetTaxi] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [GetTaxi] SET DB_CHAINING OFF
GO
USE [GetTaxi]
GO
/****** Object:  ForeignKey [FK_Car_Company]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Car] DROP CONSTRAINT [FK_Car_Company]
GO
/****** Object:  ForeignKey [FK_Address_CityFrom]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_CityFrom]
GO
/****** Object:  ForeignKey [FK_Address_CityTo]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_CityTo]
GO
/****** Object:  ForeignKey [FK_Right_Role]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Right] DROP CONSTRAINT [FK_Right_Role]
GO
/****** Object:  ForeignKey [FK_OrderProperties_Company]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[OrderProperties] DROP CONSTRAINT [FK_OrderProperties_Company]
GO
/****** Object:  ForeignKey [FK_User_Company]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Company]
GO
/****** Object:  ForeignKey [FK_UserRole_Role]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Role]
GO
/****** Object:  ForeignKey [FK_UserRole_User]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_User]
GO
/****** Object:  ForeignKey [FK_Order_Address]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Address]
GO
/****** Object:  ForeignKey [FK_Order_Car]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Car]
GO
/****** Object:  ForeignKey [FK_Order_Client]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Client]
GO
/****** Object:  ForeignKey [FK_Order_OrderProperties]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_OrderProperties]
GO
/****** Object:  ForeignKey [FK_OrderNote_Order]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[OrderNote] DROP CONSTRAINT [FK_OrderNote_Order]
GO
/****** Object:  Table [dbo].[OrderNote]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[OrderNote] DROP CONSTRAINT [FK_OrderNote_Order]
GO
DROP TABLE [dbo].[OrderNote]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Address]
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Car]
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Client]
GO
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_OrderProperties]
GO
DROP TABLE [dbo].[Order]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Role]
GO
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_User]
GO
DROP TABLE [dbo].[UserRole]
GO
/****** Object:  Table [dbo].[User]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Company]
GO
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[OrderProperties]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[OrderProperties] DROP CONSTRAINT [FK_OrderProperties_Company]
GO
DROP TABLE [dbo].[OrderProperties]
GO
/****** Object:  Table [dbo].[Right]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Right] DROP CONSTRAINT [FK_Right_Role]
GO
DROP TABLE [dbo].[Right]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_CityFrom]
GO
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_CityTo]
GO
DROP TABLE [dbo].[Address]
GO
/****** Object:  Table [dbo].[Car]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Car] DROP CONSTRAINT [FK_Car_Company]
GO
DROP TABLE [dbo].[Car]
GO
/****** Object:  Table [dbo].[City]    Script Date: 01/12/2014 22:09:03 ******/
DROP TABLE [dbo].[City]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 01/12/2014 22:09:02 ******/
DROP TABLE [dbo].[Client]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 01/12/2014 22:09:02 ******/
DROP TABLE [dbo].[Company]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 01/12/2014 22:09:02 ******/
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 01/12/2014 22:09:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](300) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([RoleId], [Name], [Description]) VALUES (1, N'User', N'User')
INSERT [dbo].[Role] ([RoleId], [Name], [Description]) VALUES (2, N'Company', N'Company')
INSERT [dbo].[Role] ([RoleId], [Name], [Description]) VALUES (3, N'NotActive', N'NotActive')
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[Company]    Script Date: 01/12/2014 22:09:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 01/12/2014 22:09:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](200) NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Salt] [nvarchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ActivateCode] [nvarchar](10) NULL,
	[SmsSentCount] [int] NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 01/12/2014 22:09:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DisplayOrder] [int] NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[City] ON
INSERT [dbo].[City] ([CityId], [Name], [DisplayOrder]) VALUES (1, N'Kraków', 1)
SET IDENTITY_INSERT [dbo].[City] OFF
/****** Object:  Table [dbo].[Car]    Script Date: 01/12/2014 22:09:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[CarId] [int] IDENTITY(1,1) NOT NULL,
	[Mark] [nvarchar](50) NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Color] [nvarchar](50) NOT NULL,
	[NrOfSeats] [int] NULL,
	[DriverName] [nvarchar](150) NOT NULL,
	[CarNumber] [nvarchar](50) NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED 
(
	[CarId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 01/12/2014 22:09:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[CityFrom] [int] NOT NULL,
	[AddressFrom] [nvarchar](250) NOT NULL,
	[CityTo] [int] NULL,
	[AddressTo] [nvarchar](250) NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
	[Accurancy] [float] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Address] ON
INSERT [dbo].[Address] ([AddressId], [CityFrom], [AddressFrom], [CityTo], [AddressTo], [Latitude], [Longitude], [Accurancy]) VALUES (6, 1, N'dfdfgd 55', 1, N'fdgfgd 44', NULL, NULL, NULL)
INSERT [dbo].[Address] ([AddressId], [CityFrom], [AddressFrom], [CityTo], [AddressTo], [Latitude], [Longitude], [Accurancy]) VALUES (7, 1, N'Odrzańska 8', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Address] ([AddressId], [CityFrom], [AddressFrom], [CityTo], [AddressTo], [Latitude], [Longitude], [Accurancy]) VALUES (8, 1, N'Ulica 6', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Address] OFF
/****** Object:  Table [dbo].[Right]    Script Date: 01/12/2014 22:09:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Right](
	[RightId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Value] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Right] PRIMARY KEY CLUSTERED 
(
	[RightId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderProperties]    Script Date: 01/12/2014 22:09:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderProperties](
	[OrderPropertiesId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NULL,
	[OrderClass] [int] NOT NULL,
	[Priority] [int] NOT NULL,
	[Childer] [bit] NOT NULL,
	[Nosmoking] [bit] NOT NULL,
	[Animal] [bit] NOT NULL,
	[English] [bit] NOT NULL,
	[Card] [bit] NOT NULL,
 CONSTRAINT [PK_OrderProperties] PRIMARY KEY CLUSTERED 
(
	[OrderPropertiesId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[OrderProperties] ON
INSERT [dbo].[OrderProperties] ([OrderPropertiesId], [CompanyId], [OrderClass], [Priority], [Childer], [Nosmoking], [Animal], [English], [Card]) VALUES (5, NULL, 0, 1, 0, 0, 0, 0, 0)
INSERT [dbo].[OrderProperties] ([OrderPropertiesId], [CompanyId], [OrderClass], [Priority], [Childer], [Nosmoking], [Animal], [English], [Card]) VALUES (6, NULL, 0, 1, 0, 0, 0, 0, 0)
INSERT [dbo].[OrderProperties] ([OrderPropertiesId], [CompanyId], [OrderClass], [Priority], [Childer], [Nosmoking], [Animal], [English], [Card]) VALUES (7, NULL, 0, 1, 0, 0, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[OrderProperties] OFF
/****** Object:  Table [dbo].[User]    Script Date: 01/12/2014 22:09:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NULL,
	[CompanyId] [int] NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](200) NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Salt] [nvarchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NULL,
	[BlockDate] [datetime] NULL,
	[SuspendDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[Phone] [nvarchar](15) NULL,
	[ActivateCode] [nvarchar](10) NULL,
	[SmsSentCount] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([UserId], [Login], [CompanyId], [FirstName], [LastName], [Email], [Password], [Salt], [CreationDate], [LastLoginDate], [BlockDate], [SuspendDate], [IsActive], [Phone], [ActivateCode], [SmsSentCount]) VALUES (1, N'admin', NULL, N'Admin', N'Adminycz', N'admin@gettaxi.pl', N'a196ba59691776e7af575decc85e9e0d', N'JVg/MJHaEy+mwQTIoL7Y8tSmbsjyZniQpeRcAFU+zqI=', CAST(0x0000A29800E066AB AS DateTime), CAST(0x0000A2B100C8827D AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([UserId], [Login], [CompanyId], [FirstName], [LastName], [Email], [Password], [Salt], [CreationDate], [LastLoginDate], [BlockDate], [SuspendDate], [IsActive], [Phone], [ActivateCode], [SmsSentCount]) VALUES (2, NULL, NULL, N'Vitali', NULL, NULL, N'04169842b91f8f8c636885b9632ab97c', N'9iUbEoq91xRaK7XqJTTIZn7a1rTeBLx6rPRsAUlUY3w=', CAST(0x0000A2A400B09341 AS DateTime), CAST(0x0000A2B100C25FB6 AS DateTime), NULL, NULL, 1, N'515436413', N'17451', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[UserRole]    Script Date: 01/12/2014 22:09:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (1, 1, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (3, 2, 1)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
/****** Object:  Table [dbo].[Order]    Script Date: 01/12/2014 22:09:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[OrderPropertiesId] [int] NOT NULL,
	[CarId] [int] NULL,
	[Deadline] [datetime] NOT NULL,
	[AddressId] [int] NOT NULL,
	[Tariff] [int] NULL,
	[EstimatedPrice] [float] NULL,
	[IsPrepaid] [bit] NOT NULL,
	[IsPlanned] [bit] NOT NULL,
	[PlannedOn] [datetime] NULL,
	[Status] [int] NOT NULL,
	[TimeCreated] [datetime] NOT NULL,
	[TimeAssigned] [datetime] NULL,
	[TimeArrived] [datetime] NULL,
	[TimeInCar] [datetime] NULL,
	[TimeDone] [datetime] NULL,
	[TimeCanceled] [datetime] NULL,
	[CanceledBy] [int] NULL,
	[CancelCause] [int] NULL,
	[FinalPrice] [float] NULL,
	[UserComment] [nvarchar](500) NULL,
	[TaxiComment] [nvarchar](500) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderNote]    Script Date: 01/12/2014 22:09:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderNote](
	[OrderNoteId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[NoteType] [int] NOT NULL,
	[Vote] [int] NULL,
	[UserComment] [nvarchar](500) NULL,
	[CreationTime] [datetime] NOT NULL,
 CONSTRAINT [PK_OrderNote] PRIMARY KEY CLUSTERED 
(
	[OrderNoteId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Car_Company]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_Company]
GO
/****** Object:  ForeignKey [FK_Address_CityFrom]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_CityFrom] FOREIGN KEY([CityFrom])
REFERENCES [dbo].[City] ([CityId])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_CityFrom]
GO
/****** Object:  ForeignKey [FK_Address_CityTo]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_CityTo] FOREIGN KEY([CityTo])
REFERENCES [dbo].[City] ([CityId])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_CityTo]
GO
/****** Object:  ForeignKey [FK_Right_Role]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Right]  WITH CHECK ADD  CONSTRAINT [FK_Right_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[Right] CHECK CONSTRAINT [FK_Right_Role]
GO
/****** Object:  ForeignKey [FK_OrderProperties_Company]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[OrderProperties]  WITH CHECK ADD  CONSTRAINT [FK_OrderProperties_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[OrderProperties] CHECK CONSTRAINT [FK_OrderProperties_Company]
GO
/****** Object:  ForeignKey [FK_User_Company]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Company]
GO
/****** Object:  ForeignKey [FK_UserRole_Role]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO
/****** Object:  ForeignKey [FK_UserRole_User]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO
/****** Object:  ForeignKey [FK_Order_Address]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Address]
GO
/****** Object:  ForeignKey [FK_Order_Car]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Car] FOREIGN KEY([CarId])
REFERENCES [dbo].[Car] ([CarId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Car]
GO
/****** Object:  ForeignKey [FK_Order_Client]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Client]
GO
/****** Object:  ForeignKey [FK_Order_OrderProperties]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderProperties] FOREIGN KEY([OrderPropertiesId])
REFERENCES [dbo].[OrderProperties] ([OrderPropertiesId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_OrderProperties]
GO
/****** Object:  ForeignKey [FK_OrderNote_Order]    Script Date: 01/12/2014 22:09:03 ******/
ALTER TABLE [dbo].[OrderNote]  WITH CHECK ADD  CONSTRAINT [FK_OrderNote_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderNote] CHECK CONSTRAINT [FK_OrderNote_Order]
GO

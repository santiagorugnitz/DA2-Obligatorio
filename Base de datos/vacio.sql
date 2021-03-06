USE [master]
GO
/****** Object:  Database [Tourism]    Script Date: 26/11/2020 18:47:18 ******/
CREATE DATABASE [Tourism]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Tourism', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Tourism.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Tourism_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Tourism_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Tourism] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Tourism].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Tourism] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Tourism] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Tourism] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Tourism] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Tourism] SET ARITHABORT OFF 
GO
ALTER DATABASE [Tourism] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Tourism] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Tourism] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Tourism] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Tourism] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Tourism] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Tourism] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Tourism] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Tourism] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Tourism] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Tourism] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Tourism] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Tourism] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Tourism] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Tourism] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Tourism] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Tourism] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Tourism] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Tourism] SET  MULTI_USER 
GO
ALTER DATABASE [Tourism] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Tourism] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Tourism] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Tourism] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Tourism] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Tourism] SET QUERY_STORE = OFF
GO
USE [Tourism]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 26/11/2020 18:47:18 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accommodations]    Script Date: 26/11/2020 18:47:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accommodations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Stars] [float] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Fee] [float] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Available] [bit] NOT NULL,
	[Telephone] [nvarchar](max) NULL,
	[ContactInformation] [nvarchar](max) NULL,
	[TouristSpotId] [int] NULL,
 CONSTRAINT [PK_Accommodations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Administrators]    Script Date: 26/11/2020 18:47:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrators](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Token] [nvarchar](max) NULL,
 CONSTRAINT [PK_Administrators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 26/11/2020 18:47:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 26/11/2020 18:47:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[AccommodationId] [int] NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Regions]    Script Date: 26/11/2020 18:47:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Regions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 26/11/2020 18:47:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CheckIn] [datetime2](7) NOT NULL,
	[CheckOut] [datetime2](7) NOT NULL,
	[BabyQuantity] [int] NOT NULL,
	[ChildrenQuantity] [int] NOT NULL,
	[AdultQuantity] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Surname] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[ReservationState] [int] NOT NULL,
	[StateDescription] [nvarchar](max) NULL,
	[Total] [float] NOT NULL,
	[RetiredQuantity] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[Score] [float] NULL,
	[AccommodationId] [int] NULL,
 CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TouristSpotCategory]    Script Date: 26/11/2020 18:47:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TouristSpotCategory](
	[TouristSpotId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_TouristSpotCategory] PRIMARY KEY CLUSTERED 
(
	[TouristSpotId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TouristSpots]    Script Date: 26/11/2020 18:47:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TouristSpots](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[RegionId] [int] NULL,
	[ImageId] [int] NULL,
 CONSTRAINT [PK_TouristSpots] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Accommodations_TouristSpotId]    Script Date: 26/11/2020 18:47:18 ******/
CREATE NONCLUSTERED INDEX [IX_Accommodations_TouristSpotId] ON [dbo].[Accommodations]
(
	[TouristSpotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Images_AccommodationId]    Script Date: 26/11/2020 18:47:18 ******/
CREATE NONCLUSTERED INDEX [IX_Images_AccommodationId] ON [dbo].[Images]
(
	[AccommodationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reservations_AccommodationId]    Script Date: 26/11/2020 18:47:18 ******/
CREATE NONCLUSTERED INDEX [IX_Reservations_AccommodationId] ON [dbo].[Reservations]
(
	[AccommodationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TouristSpotCategory_CategoryId]    Script Date: 26/11/2020 18:47:18 ******/
CREATE NONCLUSTERED INDEX [IX_TouristSpotCategory_CategoryId] ON [dbo].[TouristSpotCategory]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TouristSpots_ImageId]    Script Date: 26/11/2020 18:47:18 ******/
CREATE NONCLUSTERED INDEX [IX_TouristSpots_ImageId] ON [dbo].[TouristSpots]
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TouristSpots_RegionId]    Script Date: 26/11/2020 18:47:18 ******/
CREATE NONCLUSTERED INDEX [IX_TouristSpots_RegionId] ON [dbo].[TouristSpots]
(
	[RegionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Reservations] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [Total]
GO
ALTER TABLE [dbo].[Reservations] ADD  DEFAULT ((0)) FOR [RetiredQuantity]
GO
ALTER TABLE [dbo].[Accommodations]  WITH CHECK ADD  CONSTRAINT [FK_Accommodations_TouristSpots_TouristSpotId] FOREIGN KEY([TouristSpotId])
REFERENCES [dbo].[TouristSpots] ([Id])
GO
ALTER TABLE [dbo].[Accommodations] CHECK CONSTRAINT [FK_Accommodations_TouristSpots_TouristSpotId]
GO
ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_Images_Accommodations_AccommodationId] FOREIGN KEY([AccommodationId])
REFERENCES [dbo].[Accommodations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Images_Accommodations_AccommodationId]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservations_Accommodations_AccommodationId] FOREIGN KEY([AccommodationId])
REFERENCES [dbo].[Accommodations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservations_Accommodations_AccommodationId]
GO
ALTER TABLE [dbo].[TouristSpotCategory]  WITH CHECK ADD  CONSTRAINT [FK_TouristSpotCategory_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TouristSpotCategory] CHECK CONSTRAINT [FK_TouristSpotCategory_Categories_CategoryId]
GO
ALTER TABLE [dbo].[TouristSpotCategory]  WITH CHECK ADD  CONSTRAINT [FK_TouristSpotCategory_TouristSpots_TouristSpotId] FOREIGN KEY([TouristSpotId])
REFERENCES [dbo].[TouristSpots] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TouristSpotCategory] CHECK CONSTRAINT [FK_TouristSpotCategory_TouristSpots_TouristSpotId]
GO
ALTER TABLE [dbo].[TouristSpots]  WITH CHECK ADD  CONSTRAINT [FK_TouristSpots_Images_ImageId] FOREIGN KEY([ImageId])
REFERENCES [dbo].[Images] ([Id])
GO
ALTER TABLE [dbo].[TouristSpots] CHECK CONSTRAINT [FK_TouristSpots_Images_ImageId]
GO
ALTER TABLE [dbo].[TouristSpots]  WITH CHECK ADD  CONSTRAINT [FK_TouristSpots_Regions_RegionId] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([Id])
GO
ALTER TABLE [dbo].[TouristSpots] CHECK CONSTRAINT [FK_TouristSpots_Regions_RegionId]
GO
USE [master]
GO
ALTER DATABASE [Tourism] SET  READ_WRITE 
GO

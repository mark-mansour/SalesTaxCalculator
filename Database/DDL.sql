USE [master]
GO

/****** Object:  Database [AvalaraAssessment]    Script Date: 7/4/2021 2:10:03 PM ******/
DROP DATABASE [AvalaraAssessment]
GO

/****** Object:  Database [AvalaraAssessment]    Script Date: 7/4/2021 2:10:03 PM ******/
CREATE DATABASE [AvalaraAssessment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AvalaraAssessment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\AvalaraAssessment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AvalaraAssessment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\AvalaraAssessment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

USE [AvalaraAssessment]
GO

/****** Object:  Table [dbo].[CountyTax]    Script Date: 7/4/2021 2:11:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CountyTax](
	[CountyTaxKey] [uniqueidentifier] NOT NULL,
	[CountyName] [nvarchar](50) NOT NULL,
	[TaxRate] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_CountyTax] PRIMARY KEY CLUSTERED 
(
	[CountyTaxKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Index [IX_CountyTax_CountyName]    Script Date: 7/4/2021 2:12:12 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CountyTax_CountyName] ON [dbo].[CountyTax]
(
	[CountyName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO




USE [master]
GO

/****** Object:  Login [mark]    Script Date: 7/4/2021 8:35:43 PM ******/
DROP LOGIN [mark]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [mark]    Script Date: 7/4/2021 8:35:43 PM ******/
CREATE LOGIN [mark] WITH PASSWORD=N'mark1', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [mark] ENABLE
GO


USE [AvalaraAssessment]
GO
ALTER ROLE [db_datareader] ADD MEMBER [mark]
GO
USE [AvalaraAssessment]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [mark]
GO


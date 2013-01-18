USE [master]
GO

/****** Object:  Drop Database if exist ******/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Questionnaire')

ALTER DATABASE [Questionnaire] SET SINGLE_USER WITH ROLLBACK IMMEDIATE

BEGIN 
DROP DATABASE [Questionnaire]; 
END 

GO
/****** Object:  Database [HouHai]    Script Date: 11/11/2012 13:26:35 ******/
CREATE DATABASE [Questionnaire] ON PRIMARY 
( NAME = N'Questionnaire', FILENAME = N'C:\DB\Questionnaire.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Questionnaire_log', FILENAME = N'C:\DB\Questionnaire_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
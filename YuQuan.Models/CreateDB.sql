USE [master]
GO

/****** Object:  Drop Database [HouHai] if exist ******/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'KB')

ALTER DATABASE [KB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO

BEGIN 
DROP DATABASE [KB]; 
END 

/****** Object:  Database [HouHai]    Script Date: 11/11/2012 13:26:35 ******/
CREATE DATABASE [KB] ON PRIMARY 
( NAME = N'KB', FILENAME = N'C:\DB\KB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'KB_log', FILENAME = N'C:\DB\KB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
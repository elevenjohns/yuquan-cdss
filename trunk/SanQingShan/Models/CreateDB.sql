USE [master]
GO

/****** Object:  Drop Database if exist ******/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'CP')

ALTER DATABASE [CP] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO

BEGIN 
DROP DATABASE [CP]; 
END 

/****** Object:  Database [HouHai]    Script Date: 11/11/2012 13:26:35 ******/
CREATE DATABASE [CP] ON PRIMARY 
( NAME = N'CP', FILENAME = N'C:\DB\CP.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CP_log', FILENAME = N'C:\DB\CP_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
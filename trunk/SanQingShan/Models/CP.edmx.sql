
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/09/2013 12:22:19
-- Generated from EDMX file: C:\YuQuan\SanQingShan\Models\CP.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CP];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CP_EXAM]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CP_EXAM];
GO
IF OBJECT_ID(N'[dbo].[CP_LAB_TEST]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CP_LAB_TEST];
GO
IF OBJECT_ID(N'[dbo].[CP_MEDICAL_DOC]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CP_MEDICAL_DOC];
GO
IF OBJECT_ID(N'[dbo].[CP_ORDER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CP_ORDER];
GO
IF OBJECT_ID(N'[CPModelStoreContainer].[CP_VISIT]', 'U') IS NOT NULL
    DROP TABLE [CPModelStoreContainer].[CP_VISIT];
GO
IF OBJECT_ID(N'[dbo].[CP_VITAL_SIGN]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CP_VITAL_SIGN];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CP_EXAM'
CREATE TABLE [dbo].[CP_EXAM] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HOSPITAL_ID] nvarchar(50)  NULL,
    [OUTPATIENT_ID] nvarchar(50)  NULL,
    [CP_ID] nvarchar(50)  NULL,
    [EXAM_CLASS] nvarchar(max)  NULL,
    [EXAM_SUB_CLASS] nvarchar(max)  NULL,
    [EXAM_TIME] datetime  NULL,
    [RESULT_PARAGRAPH] nvarchar(max)  NULL,
    [RESULT_DESCRIPTION] nvarchar(max)  NULL,
    [RESULT_IMPRESSION] nvarchar(max)  NULL,
    [IS_ABNORMAL] nvarchar(max)  NULL,
    [ORDER_ID] nvarchar(max)  NULL
);
GO

-- Creating table 'CP_ORDER'
CREATE TABLE [dbo].[CP_ORDER] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HOSPITAL_ID] nvarchar(50)  NULL,
    [CP_ID] nvarchar(50)  NULL,
    [OUTPATIENT_ID] nvarchar(max)  NULL,
    [ORDER_TYPE_ID] nvarchar(max)  NULL,
    [ORDER_CONTENT] nvarchar(max)  NULL,
    [DOSAGE] nvarchar(max)  NULL,
    [UNIT] nvarchar(max)  NULL,
    [METHOD] nvarchar(max)  NULL,
    [START_TIME] datetime  NULL,
    [END_TIME] datetime  NULL,
    [FREQ] nvarchar(max)  NULL,
    [FREQ_UNIT] nvarchar(max)  NULL,
    [EXPECTED_EXECUTIVE_TIME] nvarchar(max)  NULL,
    [ISSUER] nvarchar(max)  NULL,
    [TERMINATOR] nvarchar(max)  NULL,
    [EXECUTOR] nvarchar(max)  NULL,
    [CHECKER] nvarchar(max)  NULL,
    [REAL_EXECUTIVE_TIME] datetime  NULL
);
GO

-- Creating table 'CP_VITAL_SIGN'
CREATE TABLE [dbo].[CP_VITAL_SIGN] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HOSPITAL_ID] nvarchar(50)  NULL,
    [OUTPATIENT_ID] nvarchar(max)  NULL,
    [CP_ID] nvarchar(50)  NULL,
    [VISIT_ID] nvarchar(max)  NULL,
    [MEASURING_TIME] datetime  NULL,
    [VITAL_SIGN_NAME] nvarchar(max)  NULL,
    [RESULT_VALUE] nvarchar(max)  NULL,
    [RESULT_UNIT] nvarchar(max)  NULL,
    [ORDER_ID] nvarchar(max)  NULL
);
GO

-- Creating table 'CP_MEDICAL_DOC'
CREATE TABLE [dbo].[CP_MEDICAL_DOC] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CP_ID] nvarchar(50)  NULL,
    [FULL_PATH] nvarchar(max)  NULL,
    [FILE_EXTENSION] nvarchar(max)  NULL
);
GO

-- Creating table 'CP_LAB_TEST'
CREATE TABLE [dbo].[CP_LAB_TEST] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HOSPITAL_ID] nvarchar(50)  NULL,
    [OUTPATIENT_ID] nvarchar(50)  NULL,
    [CP_ID] nvarchar(50)  NULL,
    [REPORT_ITEM_NAME] nvarchar(max)  NULL,
    [RESULT_VALUE] nvarchar(max)  NULL,
    [RESULT_UNIT] nvarchar(max)  NULL,
    [ABNORMAL_INDICATOR] nvarchar(max)  NULL,
    [REPORT_TIME] datetime  NULL,
    [TEST_NO] nvarchar(max)  NULL,
    [ITEM_NO] nvarchar(50)  NULL,
    [VID] nvarchar(max)  NULL,
    [ORDER_ID] nvarchar(max)  NULL
);
GO

-- Creating table 'CP_VISIT'
CREATE TABLE [dbo].[CP_VISIT] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HOSPITAL_ID] nvarchar(50)  NULL,
    [OUTPATIENT_ID] nvarchar(50)  NULL,
    [INPATIENT_NO] nvarchar(50)  NULL,
    [CP_ID] nvarchar(50)  NULL,
    [VISIT_NO] nvarchar(50)  NULL,
    [ADMISSION_TIME] datetime  NULL,
    [DISCHARGE_TIME] datetime  NULL,
    [DISCHARGE_TYPE] nvarchar(max)  NULL,
    [DIAGNOSIS] nvarchar(max)  NULL,
    [STRUCTURED_DIAGNOSIS] nvarchar(max)  NULL,
    [DIAGNOSIS_TIME] datetime  NULL,
    [BIRTHDAY] datetime  NULL,
    [GENDER] nvarchar(50)  NULL,
    [OCCUPATION] nvarchar(50)  NULL,
    [CONDITION] nvarchar(50)  NULL,
    [DEPARTMENT] nvarchar(50)  NULL,
    [TOTAL_COSTS] nvarchar(50)  NULL,
    [TOTAL_PAYMENTS] nvarchar(50)  NULL,
    [DOCTOR] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CP_EXAM'
ALTER TABLE [dbo].[CP_EXAM]
ADD CONSTRAINT [PK_CP_EXAM]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CP_ORDER'
ALTER TABLE [dbo].[CP_ORDER]
ADD CONSTRAINT [PK_CP_ORDER]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CP_VITAL_SIGN'
ALTER TABLE [dbo].[CP_VITAL_SIGN]
ADD CONSTRAINT [PK_CP_VITAL_SIGN]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CP_MEDICAL_DOC'
ALTER TABLE [dbo].[CP_MEDICAL_DOC]
ADD CONSTRAINT [PK_CP_MEDICAL_DOC]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CP_LAB_TEST'
ALTER TABLE [dbo].[CP_LAB_TEST]
ADD CONSTRAINT [PK_CP_LAB_TEST]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CP_VISIT'
ALTER TABLE [dbo].[CP_VISIT]
ADD CONSTRAINT [PK_CP_VISIT]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
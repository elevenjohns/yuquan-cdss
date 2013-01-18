
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/21/2012 16:16:23
-- Generated from EDMX file: C:\YuQuan\WuKeSong.Models\Dict.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Dict];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ACTION_ADT_DICT'
CREATE TABLE [dbo].[ACTION_ADT_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'ADMIN_ORDERS_DICT'
CREATE TABLE [dbo].[ADMIN_ORDERS_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'ADMISSION_CAUSE_DICT'
CREATE TABLE [dbo].[ADMISSION_CAUSE_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'ANAESTHESIA_DICT'
CREATE TABLE [dbo].[ANAESTHESIA_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'AREA_DICT'
CREATE TABLE [dbo].[AREA_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'CHARGE_TYPE_DICT'
CREATE TABLE [dbo].[CHARGE_TYPE_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'CLINIC_CLASS_DICT'
CREATE TABLE [dbo].[CLINIC_CLASS_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'CLINIC_ITEM_DICT'
CREATE TABLE [dbo].[CLINIC_ITEM_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL,
    [ITEM_CLASS] varchar(50)  NOT NULL,
    [ITEM_CLASS_VER] decimal(3,0)  NOT NULL,
    [THUMB] varbinary(max)  NULL
);
GO

-- Creating table 'COUNTRY_DICT'
CREATE TABLE [dbo].[COUNTRY_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'DIAGNOSIS_DICT'
CREATE TABLE [dbo].[DIAGNOSIS_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL,
    [INFECT_INDICATOR] varchar(1)  NULL
);
GO

-- Creating table 'DIAGNOSIS_TYPE_DICT'
CREATE TABLE [dbo].[DIAGNOSIS_TYPE_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'DOC_TYPE_DICT'
CREATE TABLE [dbo].[DOC_TYPE_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'DRUG_CLASS_DICT'
CREATE TABLE [dbo].[DRUG_CLASS_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL,
    [BELONG_TO] varchar(50)  NULL,
    [BELONG_TO_VER] decimal(3,0)  NULL
);
GO

-- Creating table 'EXAM_CLASS_DICT'
CREATE TABLE [dbo].[EXAM_CLASS_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'EXAM_MODE_DICT'
CREATE TABLE [dbo].[EXAM_MODE_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'EXAM_SUBCLASS_DICT'
CREATE TABLE [dbo].[EXAM_SUBCLASS_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [EXAM_CLASS_ID] varchar(50)  NOT NULL,
    [EXAM_CLASS_VER] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'INSURANCE_TYPE_DICT'
CREATE TABLE [dbo].[INSURANCE_TYPE_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL
);
GO

-- Creating table 'LAB_TEST_RESULT_DICT'
CREATE TABLE [dbo].[LAB_TEST_RESULT_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL,
    [ITEM_NO] decimal(5,0)  NOT NULL,
    [RESULT_TYPE] varchar(8)  NULL,
    [LOWER_LIMIT] decimal(10,3)  NULL,
    [UPPER_LIMIT] decimal(10,3)  NULL,
    [UNITS] varchar(16)  NULL,
    [PRINT_CONTEXT] varchar(80)  NULL,
    [MINI_INCREMENT] decimal(8,3)  NULL,
    [NOTES] varchar(50)  NULL,
    [DEFAULT_VALUE] varchar(25)  NULL
);
GO

-- Creating table 'NATION_DICT'
CREATE TABLE [dbo].[NATION_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'OPERATION_DICT'
CREATE TABLE [dbo].[OPERATION_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL,
    [OPERATION_SCALE] varchar(2)  NULL
);
GO

-- Creating table 'PATIENT_CLASS_DICT'
CREATE TABLE [dbo].[PATIENT_CLASS_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'SEGMENT_TYPE_DICT'
CREATE TABLE [dbo].[SEGMENT_TYPE_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'TITLE_DICT'
CREATE TABLE [dbo].[TITLE_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL
);
GO

-- Creating table 'TOP_UNIT_DICT'
CREATE TABLE [dbo].[TOP_UNIT_DICT] (
    [DICT_ID] varchar(50)  NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'UNITS_IN_CONTRACT_DICT'
CREATE TABLE [dbo].[UNITS_IN_CONTRACT_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL,
    [ALIAS] varchar(50)  NULL,
    [LOCATION_ID] varchar(50)  NULL,
    [LOCATION_VER] decimal(3,0)  NULL,
    [MAILING_ADDR] varchar(100)  NULL,
    [ZIP_CODE] varchar(6)  NULL,
    [SUBORDINATE_TO_ID] varchar(50)  NULL,
    [SUBORDINATE_TO_VER] decimal(3,0)  NULL
);
GO

-- Creating table 'VISIT_CLASS_DICT'
CREATE TABLE [dbo].[VISIT_CLASS_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL
);
GO

-- Creating table 'VITAL_SIGNS_DICT'
CREATE TABLE [dbo].[VITAL_SIGNS_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- Creating table 'WOUND_GRADE_DICT'
CREATE TABLE [dbo].[WOUND_GRADE_DICT] (
    [DICT_ID] varchar(50)  NOT NULL,
    [DICT_VER] decimal(3,0)  NOT NULL,
    [DICT_VER_PREVIOUS] decimal(3,0)  NOT NULL,
    [CODE_TYPE] varchar(50)  NULL,
    [CODE] varchar(50)  NULL,
    [NAME] varchar(100)  NOT NULL,
    [INPUT_CODE] varchar(8)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DICT_ID] in table 'ACTION_ADT_DICT'
ALTER TABLE [dbo].[ACTION_ADT_DICT]
ADD CONSTRAINT [PK_ACTION_ADT_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'ADMIN_ORDERS_DICT'
ALTER TABLE [dbo].[ADMIN_ORDERS_DICT]
ADD CONSTRAINT [PK_ADMIN_ORDERS_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'ADMISSION_CAUSE_DICT'
ALTER TABLE [dbo].[ADMISSION_CAUSE_DICT]
ADD CONSTRAINT [PK_ADMISSION_CAUSE_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'ANAESTHESIA_DICT'
ALTER TABLE [dbo].[ANAESTHESIA_DICT]
ADD CONSTRAINT [PK_ANAESTHESIA_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'AREA_DICT'
ALTER TABLE [dbo].[AREA_DICT]
ADD CONSTRAINT [PK_AREA_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'CHARGE_TYPE_DICT'
ALTER TABLE [dbo].[CHARGE_TYPE_DICT]
ADD CONSTRAINT [PK_CHARGE_TYPE_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'CLINIC_CLASS_DICT'
ALTER TABLE [dbo].[CLINIC_CLASS_DICT]
ADD CONSTRAINT [PK_CLINIC_CLASS_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'CLINIC_ITEM_DICT'
ALTER TABLE [dbo].[CLINIC_ITEM_DICT]
ADD CONSTRAINT [PK_CLINIC_ITEM_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'COUNTRY_DICT'
ALTER TABLE [dbo].[COUNTRY_DICT]
ADD CONSTRAINT [PK_COUNTRY_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'DIAGNOSIS_DICT'
ALTER TABLE [dbo].[DIAGNOSIS_DICT]
ADD CONSTRAINT [PK_DIAGNOSIS_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'DIAGNOSIS_TYPE_DICT'
ALTER TABLE [dbo].[DIAGNOSIS_TYPE_DICT]
ADD CONSTRAINT [PK_DIAGNOSIS_TYPE_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'DOC_TYPE_DICT'
ALTER TABLE [dbo].[DOC_TYPE_DICT]
ADD CONSTRAINT [PK_DOC_TYPE_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'DRUG_CLASS_DICT'
ALTER TABLE [dbo].[DRUG_CLASS_DICT]
ADD CONSTRAINT [PK_DRUG_CLASS_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'EXAM_CLASS_DICT'
ALTER TABLE [dbo].[EXAM_CLASS_DICT]
ADD CONSTRAINT [PK_EXAM_CLASS_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'EXAM_MODE_DICT'
ALTER TABLE [dbo].[EXAM_MODE_DICT]
ADD CONSTRAINT [PK_EXAM_MODE_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'EXAM_SUBCLASS_DICT'
ALTER TABLE [dbo].[EXAM_SUBCLASS_DICT]
ADD CONSTRAINT [PK_EXAM_SUBCLASS_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'INSURANCE_TYPE_DICT'
ALTER TABLE [dbo].[INSURANCE_TYPE_DICT]
ADD CONSTRAINT [PK_INSURANCE_TYPE_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'LAB_TEST_RESULT_DICT'
ALTER TABLE [dbo].[LAB_TEST_RESULT_DICT]
ADD CONSTRAINT [PK_LAB_TEST_RESULT_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'NATION_DICT'
ALTER TABLE [dbo].[NATION_DICT]
ADD CONSTRAINT [PK_NATION_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'OPERATION_DICT'
ALTER TABLE [dbo].[OPERATION_DICT]
ADD CONSTRAINT [PK_OPERATION_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'PATIENT_CLASS_DICT'
ALTER TABLE [dbo].[PATIENT_CLASS_DICT]
ADD CONSTRAINT [PK_PATIENT_CLASS_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'SEGMENT_TYPE_DICT'
ALTER TABLE [dbo].[SEGMENT_TYPE_DICT]
ADD CONSTRAINT [PK_SEGMENT_TYPE_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'TITLE_DICT'
ALTER TABLE [dbo].[TITLE_DICT]
ADD CONSTRAINT [PK_TITLE_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_VER], [DICT_VER_PREVIOUS], [NAME] in table 'TOP_UNIT_DICT'
ALTER TABLE [dbo].[TOP_UNIT_DICT]
ADD CONSTRAINT [PK_TOP_UNIT_DICT]
    PRIMARY KEY CLUSTERED ([DICT_VER], [DICT_VER_PREVIOUS], [NAME] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'UNITS_IN_CONTRACT_DICT'
ALTER TABLE [dbo].[UNITS_IN_CONTRACT_DICT]
ADD CONSTRAINT [PK_UNITS_IN_CONTRACT_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'VISIT_CLASS_DICT'
ALTER TABLE [dbo].[VISIT_CLASS_DICT]
ADD CONSTRAINT [PK_VISIT_CLASS_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'VITAL_SIGNS_DICT'
ALTER TABLE [dbo].[VITAL_SIGNS_DICT]
ADD CONSTRAINT [PK_VITAL_SIGNS_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- Creating primary key on [DICT_ID] in table 'WOUND_GRADE_DICT'
ALTER TABLE [dbo].[WOUND_GRADE_DICT]
ADD CONSTRAINT [PK_WOUND_GRADE_DICT]
    PRIMARY KEY CLUSTERED ([DICT_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
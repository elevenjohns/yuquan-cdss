
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/13/2013 21:20:11
-- Generated from EDMX file: C:\YuQuan\YuQuan.Models\KB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [KB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Fact_Encounter_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Fact] DROP CONSTRAINT [FK_Fact_Encounter_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_Fact_ChangeRecord_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Fact] DROP CONSTRAINT [FK_Fact_ChangeRecord_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_Fact_Report_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Fact] DROP CONSTRAINT [FK_Fact_Report_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_Fact_ContextItemDefinition_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Fact] DROP CONSTRAINT [FK_Fact_ContextItemDefinition_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_ClinicalProblemDefinition_TriggerRule_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TriggerRule] DROP CONSTRAINT [FK_ClinicalProblemDefinition_TriggerRule_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_PlanInstance_PlanDefinition_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanInstance] DROP CONSTRAINT [FK_PlanInstance_PlanDefinition_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_ClinicalProblemInstance_ClinicalProblemDefinition_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClinicalProblemInstance] DROP CONSTRAINT [FK_ClinicalProblemInstance_ClinicalProblemDefinition_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_ClinicalProblemInstance_Encounter_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClinicalProblemInstance] DROP CONSTRAINT [FK_ClinicalProblemInstance_Encounter_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_Encounter_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Encounter_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_Report_Event_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Report] DROP CONSTRAINT [FK_Report_Event_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_PhaseDefinition_PlanDefinition_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhaseDefinition] DROP CONSTRAINT [FK_PhaseDefinition_PlanDefinition_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_TaskDefinition_PhaseDefinition_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TaskDefinition] DROP CONSTRAINT [FK_TaskDefinition_PhaseDefinition_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_MedicalOrderDefinition_TaskDefinition_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedicalOrderDefinition] DROP CONSTRAINT [FK_MedicalOrderDefinition_TaskDefinition_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_MedicalOrderInstance_MedicalOrderDefinition_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedicalOrderInstance] DROP CONSTRAINT [FK_MedicalOrderInstance_MedicalOrderDefinition_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_MedicalOrderInstance_ClinicalProblemInstance_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedicalOrderInstance] DROP CONSTRAINT [FK_MedicalOrderInstance_ClinicalProblemInstance_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_PlanInstance_ClinicalProblemInstance_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanInstance] DROP CONSTRAINT [FK_PlanInstance_ClinicalProblemInstance_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_ChangeRecord_ClinicalProblemInstance_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChangeRecord] DROP CONSTRAINT [FK_ChangeRecord_ClinicalProblemInstance_Association];
GO
IF OBJECT_ID(N'[dbo].[FK_ClinicalProblemInstance_TriggerRule_Association]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClinicalProblemInstance] DROP CONSTRAINT [FK_ClinicalProblemInstance_TriggerRule_Association];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ChangeRecord]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChangeRecord];
GO
IF OBJECT_ID(N'[dbo].[ClinicalProblemDefinition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClinicalProblemDefinition];
GO
IF OBJECT_ID(N'[dbo].[ClinicalProblemInstance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClinicalProblemInstance];
GO
IF OBJECT_ID(N'[dbo].[ContextItemDefinition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContextItemDefinition];
GO
IF OBJECT_ID(N'[dbo].[EBM]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EBM];
GO
IF OBJECT_ID(N'[dbo].[Encounter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Encounter];
GO
IF OBJECT_ID(N'[dbo].[Event]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Event];
GO
IF OBJECT_ID(N'[dbo].[Fact]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fact];
GO
IF OBJECT_ID(N'[dbo].[MedicalOrderDefinition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedicalOrderDefinition];
GO
IF OBJECT_ID(N'[dbo].[MedicalOrderDefinition_EBM_Association]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedicalOrderDefinition_EBM_Association];
GO
IF OBJECT_ID(N'[dbo].[MedicalOrderInstance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedicalOrderInstance];
GO
IF OBJECT_ID(N'[dbo].[PhaseDefinition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhaseDefinition];
GO
IF OBJECT_ID(N'[dbo].[PhaseDefinition_EBM_Association]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhaseDefinition_EBM_Association];
GO
IF OBJECT_ID(N'[dbo].[PlanDefinition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlanDefinition];
GO
IF OBJECT_ID(N'[dbo].[PlanDefinition_EBM_Association]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlanDefinition_EBM_Association];
GO
IF OBJECT_ID(N'[dbo].[PlanInstance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlanInstance];
GO
IF OBJECT_ID(N'[dbo].[Report]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Report];
GO
IF OBJECT_ID(N'[dbo].[TaskDefinition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaskDefinition];
GO
IF OBJECT_ID(N'[dbo].[TaskDefinition_EBM_Association]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaskDefinition_EBM_Association];
GO
IF OBJECT_ID(N'[dbo].[TriggerRule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TriggerRule];
GO
IF OBJECT_ID(N'[dbo].[TriggerRule_EBM_Association]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TriggerRule_EBM_Association];
GO
IF OBJECT_ID(N'[dbo].[ClinicalProblemDefinition_ContextItemDefinition_AssociationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClinicalProblemDefinition_ContextItemDefinition_AssociationSet];
GO
IF OBJECT_ID(N'[dbo].[ClinicalProblemDefinition_PlanDefinition_AssociationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClinicalProblemDefinition_PlanDefinition_AssociationSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ChangeRecord'
CREATE TABLE [dbo].[ChangeRecord] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Operator] nvarchar(max)  NULL,
    [TimeStamp] datetime  NULL,
    [OldState] nvarchar(max)  NULL,
    [NewState] nvarchar(max)  NULL,
    [Reason] nvarchar(max)  NULL,
    [ClinicalProblemInstance_Id] int  NOT NULL
);
GO

-- Creating table 'ClinicalProblemDefinition'
CREATE TABLE [dbo].[ClinicalProblemDefinition] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ReferenceURL] nvarchar(max)  NULL,
    [Code] nvarchar(max)  NULL,
    [CodingSystem] nvarchar(max)  NULL,
    [Infectious] bit  NULL,
    [InputCode] nvarchar(max)  NULL,
    [Version] nvarchar(max)  NULL,
    [Author] nvarchar(max)  NULL,
    [Status] nvarchar(50)  NULL,
    [TimeStamp] datetime  NULL
);
GO

-- Creating table 'ClinicalProblemInstance'
CREATE TABLE [dbo].[ClinicalProblemInstance] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [State] nvarchar(max)  NULL,
    [Priority] nvarchar(max)  NULL,
    [ClinicalProblemDefinition_Id] int  NOT NULL,
    [Encounter_Id] int  NULL,
    [TriggerRule_Id] int  NULL
);
GO

-- Creating table 'ContextItemDefinition'
CREATE TABLE [dbo].[ContextItemDefinition] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Comment] nvarchar(max)  NULL,
    [Unit] nvarchar(max)  NULL,
    [DataType] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NULL,
    [CodingSystem] nvarchar(max)  NULL,
    [ReferenceRange] nvarchar(max)  NULL,
    [UpperBound] decimal(18,0)  NULL,
    [LowerBound] decimal(18,0)  NULL,
    [DefaultValue] nvarchar(max)  NULL,
    [InputCode] nvarchar(max)  NULL,
    [Version] nvarchar(max)  NULL,
    [Status] nvarchar(50)  NULL,
    [NavigationPath] nvarchar(max)  NULL,
    [Type] nvarchar(max)  NULL,
    [Author] nvarchar(max)  NULL,
    [TimeStamp] datetime  NULL
);
GO

-- Creating table 'EBM'
CREATE TABLE [dbo].[EBM] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EvidenceLevel] nvarchar(max)  NULL,
    [RecommendationClass] nvarchar(max)  NULL,
    [Content] nvarchar(max)  NULL,
    [Source] nvarchar(max)  NULL,
    [URL] nvarchar(max)  NULL
);
GO

-- Creating table 'Encounter'
CREATE TABLE [dbo].[Encounter] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FK_EMR_Encounter_Id] nvarchar(max)  NULL
);
GO

-- Creating table 'Event'
CREATE TABLE [dbo].[Event] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [EventType] nvarchar(max)  NULL,
    [TimeStamp] datetime  NOT NULL,
    [Encounter_Id] int  NULL
);
GO

-- Creating table 'Fact'
CREATE TABLE [dbo].[Fact] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NumericValue] float  NULL,
    [BooleanValue] bit  NULL,
    [StringValue] nvarchar(max)  NULL,
    [IsAbnormal] bit  NULL,
    [Confidence] decimal(18,0)  NULL,
    [LifeSpan] nvarchar(50)  NULL,
    [Encounter_Id] int  NULL,
    [ChangeRecord_Id] int  NULL,
    [Report_Id] int  NULL,
    [ContextItemDefinition_Id] int  NULL
);
GO

-- Creating table 'MedicalOrderDefinition'
CREATE TABLE [dbo].[MedicalOrderDefinition] (
    [OrderType] nvarchar(max)  NULL,
    [AdministrationRoute] nvarchar(max)  NULL,
    [TemporalType] nvarchar(max)  NULL,
    [Frequency] nvarchar(max)  NULL,
    [Dosage] nvarchar(max)  NULL,
    [AdditionalInstruction] nvarchar(max)  NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NULL,
    [CodingSystem] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [TaskDefinition_Id] int  NULL
);
GO

-- Creating table 'MedicalOrderDefinition_EBM_Association'
CREATE TABLE [dbo].[MedicalOrderDefinition_EBM_Association] (
    [MedicalOrderDefinition_Id] int  NOT NULL,
    [EBM_Id] int  NOT NULL
);
GO

-- Creating table 'MedicalOrderInstance'
CREATE TABLE [dbo].[MedicalOrderInstance] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FK_EMR_Order_Id] nvarchar(max)  NULL,
    [MedicalOrderDefinition_Id] int  NULL,
    [ClinicalProblemInstance_Id] int  NULL
);
GO

-- Creating table 'PhaseDefinition'
CREATE TABLE [dbo].[PhaseDefinition] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Duration] nvarchar(max)  NULL,
    [Period] nvarchar(max)  NULL,
    [PlanDefinition_Id] int  NULL
);
GO

-- Creating table 'PhaseDefinition_EBM_Association'
CREATE TABLE [dbo].[PhaseDefinition_EBM_Association] (
    [PhaseDefinition_Id] int  NOT NULL,
    [EBM_Id] int  NOT NULL
);
GO

-- Creating table 'PlanDefinition'
CREATE TABLE [dbo].[PlanDefinition] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Duration] nvarchar(max)  NULL,
    [Objective] nvarchar(max)  NULL,
    [Cost] nvarchar(max)  NULL,
    [Criteria] nvarchar(max)  NULL,
    [Version] nvarchar(max)  NULL,
    [Status] nvarchar(50)  NULL,
    [Author] nvarchar(max)  NULL,
    [TimeStamp] datetime  NULL
);
GO

-- Creating table 'PlanDefinition_EBM_Association'
CREATE TABLE [dbo].[PlanDefinition_EBM_Association] (
    [PlanDefinition_Id] int  NOT NULL,
    [EBM_Id] int  NOT NULL
);
GO

-- Creating table 'PlanInstance'
CREATE TABLE [dbo].[PlanInstance] (
    [State] nvarchar(max)  NULL,
    [CurrentPhase] nvarchar(max)  NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [PlanDefinition_Id] int  NOT NULL,
    [ClinicalProblemInstance_Id] int  NULL
);
GO

-- Creating table 'Report'
CREATE TABLE [dbo].[Report] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [URL] nvarchar(max)  NULL,
    [ReportType] nvarchar(max)  NULL,
    [TimeStamp] datetime  NOT NULL,
    [Event_Id] int  NULL
);
GO

-- Creating table 'TaskDefinition'
CREATE TABLE [dbo].[TaskDefinition] (
    [Optional] bit  NOT NULL,
    [MultiSelect] bit  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NULL,
    [CodingSystem] nvarchar(max)  NULL,
    [PhaseDefinition_Id] int  NULL
);
GO

-- Creating table 'TaskDefinition_EBM_Association'
CREATE TABLE [dbo].[TaskDefinition_EBM_Association] (
    [TaskDefinition_Id] int  NOT NULL,
    [EBM_Id] int  NOT NULL
);
GO

-- Creating table 'TriggerRule'
CREATE TABLE [dbo].[TriggerRule] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Version] nvarchar(max)  NULL,
    [PPV] nvarchar(max)  NULL,
    [Name] nvarchar(128)  NULL,
    [RuleSet] nvarchar(max)  NOT NULL,
    [Status] nvarchar(50)  NULL,
    [AssemblyPath] nvarchar(256)  NULL,
    [ActivityName] nvarchar(128)  NULL,
    [TimeStamp] datetime  NULL,
    [ClinicalProblemDefinition_Id] int  NULL
);
GO

-- Creating table 'TriggerRule_EBM_Association'
CREATE TABLE [dbo].[TriggerRule_EBM_Association] (
    [TriggerRule_Id] int  NOT NULL,
    [EBM_Id] int  NOT NULL
);
GO

-- Creating table 'ClinicalProblemDefinition_ContextItemDefinition_AssociationSet'
CREATE TABLE [dbo].[ClinicalProblemDefinition_ContextItemDefinition_AssociationSet] (
    [ContextItemDefinition_Id] int  NOT NULL,
    [ClinicalProblemDefinition_Id] int  NOT NULL
);
GO

-- Creating table 'ClinicalProblemDefinition_PlanDefinition_AssociationSet'
CREATE TABLE [dbo].[ClinicalProblemDefinition_PlanDefinition_AssociationSet] (
    [ClinicalProblemDefinition_Id] int  NOT NULL,
    [PlanDefinition_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ChangeRecord'
ALTER TABLE [dbo].[ChangeRecord]
ADD CONSTRAINT [PK_ChangeRecord]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClinicalProblemDefinition'
ALTER TABLE [dbo].[ClinicalProblemDefinition]
ADD CONSTRAINT [PK_ClinicalProblemDefinition]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClinicalProblemInstance'
ALTER TABLE [dbo].[ClinicalProblemInstance]
ADD CONSTRAINT [PK_ClinicalProblemInstance]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContextItemDefinition'
ALTER TABLE [dbo].[ContextItemDefinition]
ADD CONSTRAINT [PK_ContextItemDefinition]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EBM'
ALTER TABLE [dbo].[EBM]
ADD CONSTRAINT [PK_EBM]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Encounter'
ALTER TABLE [dbo].[Encounter]
ADD CONSTRAINT [PK_Encounter]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [PK_Event]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Fact'
ALTER TABLE [dbo].[Fact]
ADD CONSTRAINT [PK_Fact]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MedicalOrderDefinition'
ALTER TABLE [dbo].[MedicalOrderDefinition]
ADD CONSTRAINT [PK_MedicalOrderDefinition]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [MedicalOrderDefinition_Id], [EBM_Id] in table 'MedicalOrderDefinition_EBM_Association'
ALTER TABLE [dbo].[MedicalOrderDefinition_EBM_Association]
ADD CONSTRAINT [PK_MedicalOrderDefinition_EBM_Association]
    PRIMARY KEY CLUSTERED ([MedicalOrderDefinition_Id], [EBM_Id] ASC);
GO

-- Creating primary key on [Id] in table 'MedicalOrderInstance'
ALTER TABLE [dbo].[MedicalOrderInstance]
ADD CONSTRAINT [PK_MedicalOrderInstance]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PhaseDefinition'
ALTER TABLE [dbo].[PhaseDefinition]
ADD CONSTRAINT [PK_PhaseDefinition]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [PhaseDefinition_Id], [EBM_Id] in table 'PhaseDefinition_EBM_Association'
ALTER TABLE [dbo].[PhaseDefinition_EBM_Association]
ADD CONSTRAINT [PK_PhaseDefinition_EBM_Association]
    PRIMARY KEY CLUSTERED ([PhaseDefinition_Id], [EBM_Id] ASC);
GO

-- Creating primary key on [Id] in table 'PlanDefinition'
ALTER TABLE [dbo].[PlanDefinition]
ADD CONSTRAINT [PK_PlanDefinition]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [PlanDefinition_Id], [EBM_Id] in table 'PlanDefinition_EBM_Association'
ALTER TABLE [dbo].[PlanDefinition_EBM_Association]
ADD CONSTRAINT [PK_PlanDefinition_EBM_Association]
    PRIMARY KEY CLUSTERED ([PlanDefinition_Id], [EBM_Id] ASC);
GO

-- Creating primary key on [Id] in table 'PlanInstance'
ALTER TABLE [dbo].[PlanInstance]
ADD CONSTRAINT [PK_PlanInstance]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Report'
ALTER TABLE [dbo].[Report]
ADD CONSTRAINT [PK_Report]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TaskDefinition'
ALTER TABLE [dbo].[TaskDefinition]
ADD CONSTRAINT [PK_TaskDefinition]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [TaskDefinition_Id], [EBM_Id] in table 'TaskDefinition_EBM_Association'
ALTER TABLE [dbo].[TaskDefinition_EBM_Association]
ADD CONSTRAINT [PK_TaskDefinition_EBM_Association]
    PRIMARY KEY CLUSTERED ([TaskDefinition_Id], [EBM_Id] ASC);
GO

-- Creating primary key on [Id] in table 'TriggerRule'
ALTER TABLE [dbo].[TriggerRule]
ADD CONSTRAINT [PK_TriggerRule]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [TriggerRule_Id], [EBM_Id] in table 'TriggerRule_EBM_Association'
ALTER TABLE [dbo].[TriggerRule_EBM_Association]
ADD CONSTRAINT [PK_TriggerRule_EBM_Association]
    PRIMARY KEY CLUSTERED ([TriggerRule_Id], [EBM_Id] ASC);
GO

-- Creating primary key on [ContextItemDefinition_Id], [ClinicalProblemDefinition_Id] in table 'ClinicalProblemDefinition_ContextItemDefinition_AssociationSet'
ALTER TABLE [dbo].[ClinicalProblemDefinition_ContextItemDefinition_AssociationSet]
ADD CONSTRAINT [PK_ClinicalProblemDefinition_ContextItemDefinition_AssociationSet]
    PRIMARY KEY CLUSTERED ([ContextItemDefinition_Id], [ClinicalProblemDefinition_Id] ASC);
GO

-- Creating primary key on [ClinicalProblemDefinition_Id], [PlanDefinition_Id] in table 'ClinicalProblemDefinition_PlanDefinition_AssociationSet'
ALTER TABLE [dbo].[ClinicalProblemDefinition_PlanDefinition_AssociationSet]
ADD CONSTRAINT [PK_ClinicalProblemDefinition_PlanDefinition_AssociationSet]
    PRIMARY KEY CLUSTERED ([ClinicalProblemDefinition_Id], [PlanDefinition_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Encounter_Id] in table 'Fact'
ALTER TABLE [dbo].[Fact]
ADD CONSTRAINT [FK_Fact_Encounter_Association]
    FOREIGN KEY ([Encounter_Id])
    REFERENCES [dbo].[Encounter]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Fact_Encounter_Association'
CREATE INDEX [IX_FK_Fact_Encounter_Association]
ON [dbo].[Fact]
    ([Encounter_Id]);
GO

-- Creating foreign key on [ChangeRecord_Id] in table 'Fact'
ALTER TABLE [dbo].[Fact]
ADD CONSTRAINT [FK_Fact_ChangeRecord_Association]
    FOREIGN KEY ([ChangeRecord_Id])
    REFERENCES [dbo].[ChangeRecord]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Fact_ChangeRecord_Association'
CREATE INDEX [IX_FK_Fact_ChangeRecord_Association]
ON [dbo].[Fact]
    ([ChangeRecord_Id]);
GO

-- Creating foreign key on [Report_Id] in table 'Fact'
ALTER TABLE [dbo].[Fact]
ADD CONSTRAINT [FK_Fact_Report_Association]
    FOREIGN KEY ([Report_Id])
    REFERENCES [dbo].[Report]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Fact_Report_Association'
CREATE INDEX [IX_FK_Fact_Report_Association]
ON [dbo].[Fact]
    ([Report_Id]);
GO

-- Creating foreign key on [ContextItemDefinition_Id] in table 'Fact'
ALTER TABLE [dbo].[Fact]
ADD CONSTRAINT [FK_Fact_ContextItemDefinition_Association]
    FOREIGN KEY ([ContextItemDefinition_Id])
    REFERENCES [dbo].[ContextItemDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Fact_ContextItemDefinition_Association'
CREATE INDEX [IX_FK_Fact_ContextItemDefinition_Association]
ON [dbo].[Fact]
    ([ContextItemDefinition_Id]);
GO

-- Creating foreign key on [ClinicalProblemDefinition_Id] in table 'TriggerRule'
ALTER TABLE [dbo].[TriggerRule]
ADD CONSTRAINT [FK_ClinicalProblemDefinition_TriggerRule_Association]
    FOREIGN KEY ([ClinicalProblemDefinition_Id])
    REFERENCES [dbo].[ClinicalProblemDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClinicalProblemDefinition_TriggerRule_Association'
CREATE INDEX [IX_FK_ClinicalProblemDefinition_TriggerRule_Association]
ON [dbo].[TriggerRule]
    ([ClinicalProblemDefinition_Id]);
GO

-- Creating foreign key on [PlanDefinition_Id] in table 'PlanInstance'
ALTER TABLE [dbo].[PlanInstance]
ADD CONSTRAINT [FK_PlanInstance_PlanDefinition_Association]
    FOREIGN KEY ([PlanDefinition_Id])
    REFERENCES [dbo].[PlanDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanInstance_PlanDefinition_Association'
CREATE INDEX [IX_FK_PlanInstance_PlanDefinition_Association]
ON [dbo].[PlanInstance]
    ([PlanDefinition_Id]);
GO

-- Creating foreign key on [ClinicalProblemDefinition_Id] in table 'ClinicalProblemInstance'
ALTER TABLE [dbo].[ClinicalProblemInstance]
ADD CONSTRAINT [FK_ClinicalProblemInstance_ClinicalProblemDefinition_Association]
    FOREIGN KEY ([ClinicalProblemDefinition_Id])
    REFERENCES [dbo].[ClinicalProblemDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClinicalProblemInstance_ClinicalProblemDefinition_Association'
CREATE INDEX [IX_FK_ClinicalProblemInstance_ClinicalProblemDefinition_Association]
ON [dbo].[ClinicalProblemInstance]
    ([ClinicalProblemDefinition_Id]);
GO

-- Creating foreign key on [Encounter_Id] in table 'ClinicalProblemInstance'
ALTER TABLE [dbo].[ClinicalProblemInstance]
ADD CONSTRAINT [FK_ClinicalProblemInstance_Encounter_Association]
    FOREIGN KEY ([Encounter_Id])
    REFERENCES [dbo].[Encounter]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClinicalProblemInstance_Encounter_Association'
CREATE INDEX [IX_FK_ClinicalProblemInstance_Encounter_Association]
ON [dbo].[ClinicalProblemInstance]
    ([Encounter_Id]);
GO

-- Creating foreign key on [Encounter_Id] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [FK_Event_Encounter_Association]
    FOREIGN KEY ([Encounter_Id])
    REFERENCES [dbo].[Encounter]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_Encounter_Association'
CREATE INDEX [IX_FK_Event_Encounter_Association]
ON [dbo].[Event]
    ([Encounter_Id]);
GO

-- Creating foreign key on [Event_Id] in table 'Report'
ALTER TABLE [dbo].[Report]
ADD CONSTRAINT [FK_Report_Event_Association]
    FOREIGN KEY ([Event_Id])
    REFERENCES [dbo].[Event]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Report_Event_Association'
CREATE INDEX [IX_FK_Report_Event_Association]
ON [dbo].[Report]
    ([Event_Id]);
GO

-- Creating foreign key on [PlanDefinition_Id] in table 'PhaseDefinition'
ALTER TABLE [dbo].[PhaseDefinition]
ADD CONSTRAINT [FK_PhaseDefinition_PlanDefinition_Association]
    FOREIGN KEY ([PlanDefinition_Id])
    REFERENCES [dbo].[PlanDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PhaseDefinition_PlanDefinition_Association'
CREATE INDEX [IX_FK_PhaseDefinition_PlanDefinition_Association]
ON [dbo].[PhaseDefinition]
    ([PlanDefinition_Id]);
GO

-- Creating foreign key on [PhaseDefinition_Id] in table 'TaskDefinition'
ALTER TABLE [dbo].[TaskDefinition]
ADD CONSTRAINT [FK_TaskDefinition_PhaseDefinition_Association]
    FOREIGN KEY ([PhaseDefinition_Id])
    REFERENCES [dbo].[PhaseDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskDefinition_PhaseDefinition_Association'
CREATE INDEX [IX_FK_TaskDefinition_PhaseDefinition_Association]
ON [dbo].[TaskDefinition]
    ([PhaseDefinition_Id]);
GO

-- Creating foreign key on [TaskDefinition_Id] in table 'MedicalOrderDefinition'
ALTER TABLE [dbo].[MedicalOrderDefinition]
ADD CONSTRAINT [FK_MedicalOrderDefinition_TaskDefinition_Association]
    FOREIGN KEY ([TaskDefinition_Id])
    REFERENCES [dbo].[TaskDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MedicalOrderDefinition_TaskDefinition_Association'
CREATE INDEX [IX_FK_MedicalOrderDefinition_TaskDefinition_Association]
ON [dbo].[MedicalOrderDefinition]
    ([TaskDefinition_Id]);
GO

-- Creating foreign key on [MedicalOrderDefinition_Id] in table 'MedicalOrderInstance'
ALTER TABLE [dbo].[MedicalOrderInstance]
ADD CONSTRAINT [FK_MedicalOrderInstance_MedicalOrderDefinition_Association]
    FOREIGN KEY ([MedicalOrderDefinition_Id])
    REFERENCES [dbo].[MedicalOrderDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MedicalOrderInstance_MedicalOrderDefinition_Association'
CREATE INDEX [IX_FK_MedicalOrderInstance_MedicalOrderDefinition_Association]
ON [dbo].[MedicalOrderInstance]
    ([MedicalOrderDefinition_Id]);
GO

-- Creating foreign key on [ClinicalProblemInstance_Id] in table 'MedicalOrderInstance'
ALTER TABLE [dbo].[MedicalOrderInstance]
ADD CONSTRAINT [FK_MedicalOrderInstance_ClinicalProblemInstance_Association]
    FOREIGN KEY ([ClinicalProblemInstance_Id])
    REFERENCES [dbo].[ClinicalProblemInstance]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MedicalOrderInstance_ClinicalProblemInstance_Association'
CREATE INDEX [IX_FK_MedicalOrderInstance_ClinicalProblemInstance_Association]
ON [dbo].[MedicalOrderInstance]
    ([ClinicalProblemInstance_Id]);
GO

-- Creating foreign key on [ClinicalProblemInstance_Id] in table 'PlanInstance'
ALTER TABLE [dbo].[PlanInstance]
ADD CONSTRAINT [FK_PlanInstance_ClinicalProblemInstance_Association]
    FOREIGN KEY ([ClinicalProblemInstance_Id])
    REFERENCES [dbo].[ClinicalProblemInstance]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanInstance_ClinicalProblemInstance_Association'
CREATE INDEX [IX_FK_PlanInstance_ClinicalProblemInstance_Association]
ON [dbo].[PlanInstance]
    ([ClinicalProblemInstance_Id]);
GO

-- Creating foreign key on [ClinicalProblemInstance_Id] in table 'ChangeRecord'
ALTER TABLE [dbo].[ChangeRecord]
ADD CONSTRAINT [FK_ChangeRecord_ClinicalProblemInstance_Association]
    FOREIGN KEY ([ClinicalProblemInstance_Id])
    REFERENCES [dbo].[ClinicalProblemInstance]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ChangeRecord_ClinicalProblemInstance_Association'
CREATE INDEX [IX_FK_ChangeRecord_ClinicalProblemInstance_Association]
ON [dbo].[ChangeRecord]
    ([ClinicalProblemInstance_Id]);
GO

-- Creating foreign key on [TriggerRule_Id] in table 'ClinicalProblemInstance'
ALTER TABLE [dbo].[ClinicalProblemInstance]
ADD CONSTRAINT [FK_ClinicalProblemInstance_TriggerRule_Association]
    FOREIGN KEY ([TriggerRule_Id])
    REFERENCES [dbo].[TriggerRule]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClinicalProblemInstance_TriggerRule_Association'
CREATE INDEX [IX_FK_ClinicalProblemInstance_TriggerRule_Association]
ON [dbo].[ClinicalProblemInstance]
    ([TriggerRule_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
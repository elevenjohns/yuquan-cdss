
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/05/2013 12:45:19
-- Generated from EDMX file: C:\YuQuan\SanQingShan\Models\Questionnaire.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Questionnaire];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_QuestionOption]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConceptSet_Option] DROP CONSTRAINT [FK_QuestionOption];
GO
IF OBJECT_ID(N'[dbo].[FK_AnswerQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnswerSet] DROP CONSTRAINT [FK_AnswerQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_AnswerChoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChoiceSet] DROP CONSTRAINT [FK_AnswerChoice];
GO
IF OBJECT_ID(N'[dbo].[FK_ChoiceOption]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChoiceSet] DROP CONSTRAINT [FK_ChoiceOption];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionnaireQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConceptSet_Question] DROP CONSTRAINT [FK_QuestionnaireQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionnaireFeedback]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FeedbackSet] DROP CONSTRAINT [FK_QuestionnaireFeedback];
GO
IF OBJECT_ID(N'[dbo].[FK_FeedbackAnswer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnswerSet] DROP CONSTRAINT [FK_FeedbackAnswer];
GO
IF OBJECT_ID(N'[dbo].[FK_FeedbackSubject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubjectSet] DROP CONSTRAINT [FK_FeedbackSubject];
GO
IF OBJECT_ID(N'[dbo].[FK_Question_inherits_Concept]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConceptSet_Question] DROP CONSTRAINT [FK_Question_inherits_Concept];
GO
IF OBJECT_ID(N'[dbo].[FK_Option_inherits_Concept]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConceptSet_Option] DROP CONSTRAINT [FK_Option_inherits_Concept];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ConceptSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConceptSet];
GO
IF OBJECT_ID(N'[dbo].[AnswerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnswerSet];
GO
IF OBJECT_ID(N'[dbo].[ChoiceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChoiceSet];
GO
IF OBJECT_ID(N'[dbo].[QuestionnaireSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionnaireSet];
GO
IF OBJECT_ID(N'[dbo].[FeedbackSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FeedbackSet];
GO
IF OBJECT_ID(N'[dbo].[SubjectSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubjectSet];
GO
IF OBJECT_ID(N'[dbo].[ConceptSet_Question]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConceptSet_Question];
GO
IF OBJECT_ID(N'[dbo].[ConceptSet_Option]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConceptSet_Option];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ConceptSet'
CREATE TABLE [dbo].[ConceptSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(max)  NULL,
    [CodingSystem] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [Literal] nvarchar(max)  NULL
);
GO

-- Creating table 'AnswerSet'
CREATE TABLE [dbo].[AnswerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FreeText] nvarchar(max)  NULL,
    [Question_Id] int  NOT NULL,
    [Feedback_Id] int  NULL
);
GO

-- Creating table 'ChoiceSet'
CREATE TABLE [dbo].[ChoiceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsSelected] bit  NOT NULL,
    [Answer_Id] int  NULL,
    [Option_Id] int  NOT NULL
);
GO

-- Creating table 'QuestionnaireSet'
CREATE TABLE [dbo].[QuestionnaireSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Version] nvarchar(max)  NULL
);
GO

-- Creating table 'FeedbackSet'
CREATE TABLE [dbo].[FeedbackSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Questionnaire_Id] int  NOT NULL
);
GO

-- Creating table 'SubjectSet'
CREATE TABLE [dbo].[SubjectSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Age] int  NULL,
    [Role] nvarchar(max)  NULL,
    [Title] nvarchar(max)  NULL,
    [Specialty] nvarchar(max)  NULL,
    [Dept] nvarchar(max)  NULL,
    [Feedback_Id] int  NULL
);
GO

-- Creating table 'ConceptSet_Question'
CREATE TABLE [dbo].[ConceptSet_Question] (
    [Id] int  NOT NULL,
    [Questionnaire_Id] int  NULL
);
GO

-- Creating table 'ConceptSet_Option'
CREATE TABLE [dbo].[ConceptSet_Option] (
    [Id] int  NOT NULL,
    [Question_Id] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ConceptSet'
ALTER TABLE [dbo].[ConceptSet]
ADD CONSTRAINT [PK_ConceptSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AnswerSet'
ALTER TABLE [dbo].[AnswerSet]
ADD CONSTRAINT [PK_AnswerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ChoiceSet'
ALTER TABLE [dbo].[ChoiceSet]
ADD CONSTRAINT [PK_ChoiceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionnaireSet'
ALTER TABLE [dbo].[QuestionnaireSet]
ADD CONSTRAINT [PK_QuestionnaireSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FeedbackSet'
ALTER TABLE [dbo].[FeedbackSet]
ADD CONSTRAINT [PK_FeedbackSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SubjectSet'
ALTER TABLE [dbo].[SubjectSet]
ADD CONSTRAINT [PK_SubjectSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConceptSet_Question'
ALTER TABLE [dbo].[ConceptSet_Question]
ADD CONSTRAINT [PK_ConceptSet_Question]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConceptSet_Option'
ALTER TABLE [dbo].[ConceptSet_Option]
ADD CONSTRAINT [PK_ConceptSet_Option]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Question_Id] in table 'ConceptSet_Option'
ALTER TABLE [dbo].[ConceptSet_Option]
ADD CONSTRAINT [FK_QuestionOption]
    FOREIGN KEY ([Question_Id])
    REFERENCES [dbo].[ConceptSet_Question]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionOption'
CREATE INDEX [IX_FK_QuestionOption]
ON [dbo].[ConceptSet_Option]
    ([Question_Id]);
GO

-- Creating foreign key on [Question_Id] in table 'AnswerSet'
ALTER TABLE [dbo].[AnswerSet]
ADD CONSTRAINT [FK_AnswerQuestion]
    FOREIGN KEY ([Question_Id])
    REFERENCES [dbo].[ConceptSet_Question]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AnswerQuestion'
CREATE INDEX [IX_FK_AnswerQuestion]
ON [dbo].[AnswerSet]
    ([Question_Id]);
GO

-- Creating foreign key on [Answer_Id] in table 'ChoiceSet'
ALTER TABLE [dbo].[ChoiceSet]
ADD CONSTRAINT [FK_AnswerChoice]
    FOREIGN KEY ([Answer_Id])
    REFERENCES [dbo].[AnswerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AnswerChoice'
CREATE INDEX [IX_FK_AnswerChoice]
ON [dbo].[ChoiceSet]
    ([Answer_Id]);
GO

-- Creating foreign key on [Option_Id] in table 'ChoiceSet'
ALTER TABLE [dbo].[ChoiceSet]
ADD CONSTRAINT [FK_ChoiceOption]
    FOREIGN KEY ([Option_Id])
    REFERENCES [dbo].[ConceptSet_Option]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ChoiceOption'
CREATE INDEX [IX_FK_ChoiceOption]
ON [dbo].[ChoiceSet]
    ([Option_Id]);
GO

-- Creating foreign key on [Questionnaire_Id] in table 'ConceptSet_Question'
ALTER TABLE [dbo].[ConceptSet_Question]
ADD CONSTRAINT [FK_QuestionnaireQuestion]
    FOREIGN KEY ([Questionnaire_Id])
    REFERENCES [dbo].[QuestionnaireSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionnaireQuestion'
CREATE INDEX [IX_FK_QuestionnaireQuestion]
ON [dbo].[ConceptSet_Question]
    ([Questionnaire_Id]);
GO

-- Creating foreign key on [Questionnaire_Id] in table 'FeedbackSet'
ALTER TABLE [dbo].[FeedbackSet]
ADD CONSTRAINT [FK_QuestionnaireFeedback]
    FOREIGN KEY ([Questionnaire_Id])
    REFERENCES [dbo].[QuestionnaireSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionnaireFeedback'
CREATE INDEX [IX_FK_QuestionnaireFeedback]
ON [dbo].[FeedbackSet]
    ([Questionnaire_Id]);
GO

-- Creating foreign key on [Feedback_Id] in table 'AnswerSet'
ALTER TABLE [dbo].[AnswerSet]
ADD CONSTRAINT [FK_FeedbackAnswer]
    FOREIGN KEY ([Feedback_Id])
    REFERENCES [dbo].[FeedbackSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FeedbackAnswer'
CREATE INDEX [IX_FK_FeedbackAnswer]
ON [dbo].[AnswerSet]
    ([Feedback_Id]);
GO

-- Creating foreign key on [Feedback_Id] in table 'SubjectSet'
ALTER TABLE [dbo].[SubjectSet]
ADD CONSTRAINT [FK_FeedbackSubject]
    FOREIGN KEY ([Feedback_Id])
    REFERENCES [dbo].[FeedbackSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FeedbackSubject'
CREATE INDEX [IX_FK_FeedbackSubject]
ON [dbo].[SubjectSet]
    ([Feedback_Id]);
GO

-- Creating foreign key on [Id] in table 'ConceptSet_Question'
ALTER TABLE [dbo].[ConceptSet_Question]
ADD CONSTRAINT [FK_Question_inherits_Concept]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ConceptSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'ConceptSet_Option'
ALTER TABLE [dbo].[ConceptSet_Option]
ADD CONSTRAINT [FK_Option_inherits_Concept]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ConceptSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
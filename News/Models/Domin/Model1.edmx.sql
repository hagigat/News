
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/03/2018 14:42:19
-- Generated from EDMX file: C:\Users\Sajjad\Desktop\News\News\Models\Domin\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [News];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Tbl_Posts_Tbl_Cat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tbl_Posts] DROP CONSTRAINT [FK_Tbl_Posts_Tbl_Cat];
GO
IF OBJECT_ID(N'[dbo].[FK_Tbl_Posts_Tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tbl_Posts] DROP CONSTRAINT [FK_Tbl_Posts_Tbl_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Tbl_User_Tbl_Roll]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tbl_User] DROP CONSTRAINT [FK_Tbl_User_Tbl_Roll];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Tbl_Call]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_Call];
GO
IF OBJECT_ID(N'[dbo].[Tbl_Cat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_Cat];
GO
IF OBJECT_ID(N'[dbo].[Tbl_Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_Posts];
GO
IF OBJECT_ID(N'[dbo].[Tbl_Roll]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_Roll];
GO
IF OBJECT_ID(N'[dbo].[Tbl_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Tbl_Call'
CREATE TABLE [dbo].[Tbl_Call] (
    [id] int IDENTITY(1,1) NOT NULL,
    [email] nvarchar(50)  NOT NULL,
    [request] nvarchar(max)  NOT NULL,
    [name] nvarchar(50)  NULL
);
GO

-- Creating table 'Tbl_Cat'
CREATE TABLE [dbo].[Tbl_Cat] (
    [Cat_id] int IDENTITY(1,1) NOT NULL,
    [Cat_name] nvarchar(100)  NULL
);
GO

-- Creating table 'Tbl_Posts'
CREATE TABLE [dbo].[Tbl_Posts] (
    [Post_id] int IDENTITY(1,1) NOT NULL,
    [Post_Title] nvarchar(150)  NOT NULL,
    [Post_Summery] nvarchar(250)  NOT NULL,
    [Post_Description] nvarchar(max)  NOT NULL,
    [Post_Date] datetime  NOT NULL,
    [Post_Image] nvarchar(350)  NULL,
    [Post_Userid] int  NOT NULL,
    [Post_Catid] int  NOT NULL,
    [Post_IsBreaking] bit  NULL
);
GO

-- Creating table 'Tbl_Roll'
CREATE TABLE [dbo].[Tbl_Roll] (
    [Roll_id] int IDENTITY(1,1) NOT NULL,
    [Roll_Name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Tbl_User'
CREATE TABLE [dbo].[Tbl_User] (
    [User_id] int IDENTITY(1,1) NOT NULL,
    [User_Name] nvarchar(100)  NOT NULL,
    [User_Email] nvarchar(200)  NOT NULL,
    [User_Password] nvarchar(100)  NOT NULL,
    [User_Roll] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Tbl_Call'
ALTER TABLE [dbo].[Tbl_Call]
ADD CONSTRAINT [PK_Tbl_Call]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Cat_id] in table 'Tbl_Cat'
ALTER TABLE [dbo].[Tbl_Cat]
ADD CONSTRAINT [PK_Tbl_Cat]
    PRIMARY KEY CLUSTERED ([Cat_id] ASC);
GO

-- Creating primary key on [Post_id] in table 'Tbl_Posts'
ALTER TABLE [dbo].[Tbl_Posts]
ADD CONSTRAINT [PK_Tbl_Posts]
    PRIMARY KEY CLUSTERED ([Post_id] ASC);
GO

-- Creating primary key on [Roll_id] in table 'Tbl_Roll'
ALTER TABLE [dbo].[Tbl_Roll]
ADD CONSTRAINT [PK_Tbl_Roll]
    PRIMARY KEY CLUSTERED ([Roll_id] ASC);
GO

-- Creating primary key on [User_id] in table 'Tbl_User'
ALTER TABLE [dbo].[Tbl_User]
ADD CONSTRAINT [PK_Tbl_User]
    PRIMARY KEY CLUSTERED ([User_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Post_Catid] in table 'Tbl_Posts'
ALTER TABLE [dbo].[Tbl_Posts]
ADD CONSTRAINT [FK_Tbl_Posts_Tbl_Cat]
    FOREIGN KEY ([Post_Catid])
    REFERENCES [dbo].[Tbl_Cat]
        ([Cat_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Tbl_Posts_Tbl_Cat'
CREATE INDEX [IX_FK_Tbl_Posts_Tbl_Cat]
ON [dbo].[Tbl_Posts]
    ([Post_Catid]);
GO

-- Creating foreign key on [Post_Userid] in table 'Tbl_Posts'
ALTER TABLE [dbo].[Tbl_Posts]
ADD CONSTRAINT [FK_Tbl_Posts_Tbl_User]
    FOREIGN KEY ([Post_Userid])
    REFERENCES [dbo].[Tbl_User]
        ([User_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Tbl_Posts_Tbl_User'
CREATE INDEX [IX_FK_Tbl_Posts_Tbl_User]
ON [dbo].[Tbl_Posts]
    ([Post_Userid]);
GO

-- Creating foreign key on [User_Roll] in table 'Tbl_User'
ALTER TABLE [dbo].[Tbl_User]
ADD CONSTRAINT [FK_Tbl_User_Tbl_Roll]
    FOREIGN KEY ([User_Roll])
    REFERENCES [dbo].[Tbl_Roll]
        ([Roll_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Tbl_User_Tbl_Roll'
CREATE INDEX [IX_FK_Tbl_User_Tbl_Roll]
ON [dbo].[Tbl_User]
    ([User_Roll]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
/*******************
Author:			Duane Billue
Date:			2020-01-22
Description:	Main SQL script / code page for AV.API tutorial
*******************/

USE [AV]

--******************************--
-- Begin Create Tables
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Persons')

BEGIN
	ALTER TABLE [dbo].[Persons] DROP CONSTRAINT [FK_Persons_States]
	DROP TABLE Persons
END

CREATE TABLE dbo.Persons
(
	[PersonId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[FirstName] VARCHAR(50) NOT NULL,
	[MIddleName] VARCHAR(50) NOT NULL,
	[LastName] VARCHAR(50) NOT NULL,
	[Gender] VARCHAR(20) NOT NULL,
	[Age] INT NOT NULL,  -- Add trigger for age calc on update
	[Country] VARCHAR(50) NOT NULL,
	[City] VARCHAR(50) NOT NULL,
	[StateId] INT NOT NULL,
	[DateOfBirth] DATE NOT NULL,
	[CreateDate] DATETIME DEFAULT GETDATE()
)

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'States')

BEGIN
	DROP TABLE States
END

CREATE TABLE dbo.States
(
	[StateId] INT IDENTITY(1,1) PRIMARY KEY,
	[State] VARCHAR(50) NOT NULL,
	[Abbreviation] VARCHAR(50) NOT NULL
)

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Pets')

BEGIN
	DROP TABLE Pets
END

CREATE TABLE dbo.Pets
(
	[PetId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	[NickName] VARCHAR(50),
	[PetTypeId] INT NOT NULL,
	[PersonId] UNIQUEIDENTIFIER NOT NULL,
	[CreateDate] DATETIME DEFAULT GETDATE()
)

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'PetTypes')

BEGIN
	DROP TABLE PetTypes
END

CREATE TABLE dbo.PetTypes
(
	[PetTypeId] INT IDENTITY(1,1) PRIMARY KEY,
	[Type] VARCHAR(50) NOT NULL
)

-- End Create Tables
--******************************--


--******************************--
-- Begin Create Relationships

ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [FK_Persons_States] FOREIGN KEY([StateId])
REFERENCES [dbo].[States] ([StateId])
GO

ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_Persons_States]
GO

ALTER TABLE [dbo].[Pets]  WITH CHECK ADD  CONSTRAINT [FK_Pets_PetTypes] FOREIGN KEY([PetTypeId])
REFERENCES [dbo].[PetTypes] ([PetTypeId])
GO

ALTER TABLE [dbo].[Pets] CHECK CONSTRAINT [FK_Pets_PetTypes]
GO

ALTER TABLE [dbo].[Pets]  WITH CHECK ADD  CONSTRAINT [FK_Pets_Persons] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Persons] ([PersonId])
GO

ALTER TABLE [dbo].[Pets] CHECK CONSTRAINT [FK_Pets_Persons]
GO

-- End Create Relationships
--******************************--
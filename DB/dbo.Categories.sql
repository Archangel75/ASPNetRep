USE [ReviewStorage]
GO

/****** Объект: Table [dbo].[Categories] Дата скрипта: 15.03.2018 21:40:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Categories];


GO
CREATE TABLE [dbo].[Categories] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL
);



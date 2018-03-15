USE [ReviewStorage]
GO

/****** Объект: Table [dbo].[Subjects] Дата скрипта: 15.03.2018 21:41:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Subjects];


GO
CREATE TABLE [dbo].[Subjects] (
    [SubjectId]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    [SubCategoryId] INT           NOT NULL,
    [AverageRating] FLOAT (53)    NOT NULL
);



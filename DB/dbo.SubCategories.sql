USE [ReviewStorage]
GO

/****** Объект: Table [dbo].[SubCategories] Дата скрипта: 15.03.2018 21:41:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[SubCategories];


GO
CREATE TABLE [dbo].[SubCategories] (
    [SubCategoryId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    [CategoryId]    INT           NOT NULL
);



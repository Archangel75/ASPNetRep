USE [ReviewStorage]
GO

/****** Объект: Table [dbo].[Comments] Дата скрипта: 23.03.2018 18:46:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Comments];


GO
CREATE TABLE [dbo].[Comments] (
    [Id]         INT            NOT NULL,
    [AuthorId]   NVARCHAR (128) NOT NULL,
    [Content]    NVARCHAR (MAX) NOT NULL,
    [CreateTime] DATETIME       NOT NULL,
    [EditTime]   DATETIME       NULL,
    [ReptyToId]  INT            NULL,
    [Likes]      INT            NULL,
    [ReviewId]   INT            NULL
);


USE [ReviewStorage]
GO

/****** Объект: Table [dbo].[Reviews] Дата скрипта: 15.03.2018 21:41:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Reviews];


GO
CREATE TABLE [dbo].[Reviews] (
    [ReviewId]   INT            IDENTITY (1, 1) NOT NULL,
    [SubjectId]  INT            NOT NULL,
    [DateCreate] DATETIME       NOT NULL,
    [Content]    NVARCHAR (MAX) NOT NULL,
    [Rating]     FLOAT (53)     NOT NULL,
    [Image]      IMAGE          NULL,
    [Recommend]  TINYINT        NOT NULL,
    [Exp]        TINYINT        NOT NULL,
    [Like]       NVARCHAR (MAX) NOT NULL,
    [Dislike]    NVARCHAR (MAX) NOT NULL,
    [ImageId]    INT            NULL,
    [AuthorId]   NVARCHAR (128) NULL
);



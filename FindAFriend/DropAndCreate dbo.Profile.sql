USE [aspnet-FindAFriend-642368E5-74C3-40C8-8A21-F771B01908B9]
GO

/****** Object: Table [dbo].[Profile] Script Date: 2021-01-06 15:26:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Profile];


GO
CREATE TABLE [dbo].[Profile] (
    [ProfileID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [Birthday]    DATETIME2 (7)  NOT NULL,
    [Image]		  VARBINARY (MAX) NULL,
    [Description] NVARCHAR (MAX) NULL,
    [City]        NVARCHAR (MAX) NULL,
    [UserID]      NVARCHAR (MAX) NULL
);



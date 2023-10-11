CREATE TABLE [dbo].[Tasks] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [title]        VARCHAR (60)  NOT NULL,
    [description]  VARCHAR (250) NOT NULL,
    [registeredAt] DATETIME      NOT NULL,
    [isCompleted]  BIT           DEFAULT ((0)) NOT NULL,
    [completedAt]  DATETIME      DEFAULT (NULL) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


CREATE TABLE [dbo].[Game]
(
	[GameId]		BIGINT			IDENTITY (1, 1) NOT NULL,
	[Description]	NVARCHAR (200)	NULL,
	[StatusId]		BIGINT			NOT NULL,
	[UserId]		BIGINT			NOT NULL,
	[FactionId]		BIGINT			NOT NULL,
	[CreatedDate]	DATETIME2 (7)	NOT NULL,
	[StartDate]		DATETIME2 (7)	NULL,
	[CompletedDate]	DATETIME2 (7)	NULL,
	[ts]			ROWVERSION		NOT NULL,
	PRIMARY KEY CLUSTERED ([GameId] ASC),
	FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([StatusId]),
	FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]),
	FOREIGN KEY ([FactionId]) REFERENCES [dbo].[Faction] ([FactionId])
)

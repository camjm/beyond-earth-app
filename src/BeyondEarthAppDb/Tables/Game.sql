CREATE TABLE [dbo].[Game]
(
	[GameId]		BIGINT			IDENTITY (1, 1) NOT NULL,
	[Description]	NVARCHAR (200)	NULL,
	[UserId]		BIGINT			NOT NULL,
	[FactionId]		BIGINT			NOT NULL,
	[CreatedDate]	DATETIME2 (7)	NOT NULL,
	[ts]			ROWVERSION		NOT NULL,
	PRIMARY KEY CLUSTERED ([GameId] ASC),
	FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]),
	FOREIGN KEY ([FactionId]) REFERENCES [dbo].[Faction] ([FactionId])
)

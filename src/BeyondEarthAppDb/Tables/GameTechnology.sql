CREATE TABLE [dbo].[GameTechnology]
(
	[GameId]		BIGINT			NOT NULL,
	[TechnologyId]	BIGINT			NOT NULL,
	[ts]			ROWVERSION		NOT NULL,
	PRIMARY KEY (GameId, TechnologyId),
	FOREIGN KEY ([GameId]) REFERENCES [dbo].[Game] ([GameId]),
	FOREIGN KEY ([TechnologyId]) REFERENCES [dbo].[Technology] ([TechnologyId])
)

GO

CREATE INDEX ix_GameTechnology_GameId ON [GameTechnology]([GameId])
GO

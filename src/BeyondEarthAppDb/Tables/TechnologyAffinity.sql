CREATE TABLE [dbo].[TechnologyAffinity]
(
	[TechnologyId]	BIGINT			NOT NULL,
	[AffinityId]	BIGINT			NOT NULL,
	[Amount]		INT				NOT NULL,
	[ts]			ROWVERSION		NOT NULL,
	PRIMARY KEY (TechnologyId, AffinityId),
	FOREIGN KEY ([AffinityId]) REFERENCES [dbo].[Affinity] ([AffinityId]),
	FOREIGN KEY ([TechnologyId]) REFERENCES [dbo].[Technology] ([TechnologyId])
)

GO

CREATE INDEX ix_TechnologyAffinity_AffinityId ON [TechnologyAffinity]([AffinityId])
GO
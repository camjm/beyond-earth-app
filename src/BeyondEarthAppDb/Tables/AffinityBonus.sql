CREATE TABLE [dbo].[AffinityBonus]
(
	[AffinityBonusId]	BIGINT			IDENTITY(1, 1) NOT NULL,
	[AffinityId]		BIGINT			NOT NULL,
	[Level]				SMALLINT		NOT NULL,
	[Description]		NVARCHAR (200)	NOT NULL,
	[ts]				ROWVERSION		NOT NULL,
	PRIMARY KEY CLUSTERED ([AffinityBonusId] ASC),
	FOREIGN KEY ([AffinityId]) REFERENCES [dbo].[Affinity] ([AffinityId])
)

GO

CREATE INDEX ix_AffinityBonus_AffinityId ON [AffinityBonus]([AffinityId])
GO
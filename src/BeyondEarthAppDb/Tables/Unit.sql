CREATE TABLE [dbo].[Unit] (
	[UnitId]		BIGINT			IDENTITY (1, 1) NOT NULL,
	[TechnologyId]	BIGINT			NOT NULL,
	[Name]			NVARCHAR (100)	NOT NULL,
	[Cost]			INT				NOT NULL,
	[Strength]		SMALLINT		NOT NULL,
	[ts]			ROWVERSION		NOT NULL,
	PRIMARY KEY CLUSTERED ([UnitId] ASC),
	FOREIGN KEY ([TechnologyId]) REFERENCES [dbo].[Technology] ([TechnologyId])
);
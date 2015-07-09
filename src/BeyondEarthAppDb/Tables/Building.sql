CREATE TABLE [dbo].[Building] (
	[BuildingId]	BIGINT			IDENTITY (1, 1) NOT NULL,
	[TechnologyId]	BIGINT			NOT NULL,
	[Name]			NVARCHAR (100)	NOT NULL,
	[Cost]			INT				NOT NULL,
	[ts]			ROWVERSION		NOT NULL,
	PRIMARY KEY CLUSTERED ([BuildingId] ASC),
	FOREIGN KEY ([TechnologyId]) REFERENCES [dbo].[Technology] ([TechnologyId])
);
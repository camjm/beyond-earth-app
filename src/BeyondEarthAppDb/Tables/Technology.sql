CREATE TABLE [dbo].[Technology] (
	[TechnologyId]	BIGINT			IDENTITY (1, 1) NOT NULL,
	[Name]			NVARCHAR (100)	NOT NULL,
	[Cost]			INT				NOT NULL,
	[ts]			ROWVERSION		NOT NULL,
	PRIMARY KEY CLUSTERED ([TechnologyId] ASC)
);
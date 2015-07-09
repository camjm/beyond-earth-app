DECLARE @technologyId INT

SELECT @technologyId = TechnologyId FROM Technology WHERE Name = 'Habitation';
IF @technologyId IS NOT NULL
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.Unit WHERE Name = 'Worker')
		INSERT INTO dbo.Unit(TechnologyId, Name, Cost, Strength) 
		VALUES (@technologyId, 'Worker', 60, 0);

	IF NOT EXISTS (SELECT * FROM dbo.Unit WHERE Name = 'Explorer')
		INSERT INTO dbo.Unit(TechnologyId, Name, Cost, Strength) 
		VALUES (@technologyId, 'Explorer', 40, 3);

	IF NOT EXISTS (SELECT * FROM dbo.Unit WHERE Name = 'Soldier')
		INSERT INTO dbo.Unit(TechnologyId, Name, Cost, Strength) 
		VALUES (@technologyId, 'Soldier', 50, 10);
END
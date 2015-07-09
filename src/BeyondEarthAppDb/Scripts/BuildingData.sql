DECLARE @technologyId INT

SELECT @technologyId = TechnologyId FROM Technology WHERE Name = 'Habitation';
IF @technologyId IS NOT NULL
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.Building WHERE Name = 'Old Earth Relic')
		INSERT INTO dbo.Building(TechnologyId, Name, Cost) 
		VALUES (@technologyId, 'Old Earth Relic', 40);

	IF NOT EXISTS (SELECT * FROM dbo.Building WHERE Name = 'Clinic')
		INSERT INTO dbo.Building(TechnologyId, Name, Cost) 
		VALUES (@technologyId, 'Clinic', 60);
END

SELECT @technologyId = TechnologyId FROM Technology WHERE Name = 'Chemistry';
IF @technologyId IS NOT NULL
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.Building WHERE Name = 'Laboratory')
		INSERT INTO dbo.Building(TechnologyId, Name, Cost) 
		VALUES (@technologyId, 'Laboratory', 80);

	IF NOT EXISTS (SELECT * FROM dbo.Building WHERE Name = 'Recycler')
		INSERT INTO dbo.Building(TechnologyId, Name, Cost) 
		VALUES (@technologyId, 'Recycler', 75);
END


SELECT @technologyId = TechnologyId FROM Technology WHERE Name = 'Ecology';
IF @technologyId IS NOT NULL
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.Building WHERE Name = 'Vivarium')
		INSERT INTO dbo.Building(TechnologyId, Name, Cost) 
		VALUES (@technologyId, 'Vivarium', 80);

	IF NOT EXISTS (SELECT * FROM dbo.Building WHERE Name = 'Ultrasonic Fence')
		INSERT INTO dbo.Building(TechnologyId, Name, Cost) 
		VALUES (@technologyId, 'Ultrasonic Fence', 120);
END
DECLARE @buildingTechnologyId INT

SELECT @buildingTechnologyId = TechnologyId FROM Technology WHERE Name = 'Habitation';
IF @buildingTechnologyId IS NOT NULL
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.Building WHERE Name = 'Old Earth Relic')
		INSERT INTO dbo.Building(TechnologyId, Name, Cost) 
		VALUES (@buildingTechnologyId, 'Old Earth Relic', 40);

	IF NOT EXISTS (SELECT * FROM dbo.Building WHERE Name = 'Clinic')
		INSERT INTO dbo.Building(TechnologyId, Name, Cost) 
		VALUES (@buildingTechnologyId, 'Clinic', 60);
END

SELECT @buildingTechnologyId = TechnologyId FROM Technology WHERE Name = 'Chemistry';
IF @buildingTechnologyId IS NOT NULL
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.Building WHERE Name = 'Laboratory')
		INSERT INTO dbo.Building(TechnologyId, Name, Cost) 
		VALUES (@buildingTechnologyId, 'Laboratory', 80);

	IF NOT EXISTS (SELECT * FROM dbo.Building WHERE Name = 'Recycler')
		INSERT INTO dbo.Building(TechnologyId, Name, Cost) 
		VALUES (@buildingTechnologyId, 'Recycler', 75);
END


SELECT @buildingTechnologyId = TechnologyId FROM Technology WHERE Name = 'Ecology';
IF @buildingTechnologyId IS NOT NULL
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.Building WHERE Name = 'Vivarium')
		INSERT INTO dbo.Building(TechnologyId, Name, Cost) 
		VALUES (@buildingTechnologyId, 'Vivarium', 80);

	IF NOT EXISTS (SELECT * FROM dbo.Building WHERE Name = 'Ultrasonic Fence')
		INSERT INTO dbo.Building(TechnologyId, Name, Cost) 
		VALUES (@buildingTechnologyId, 'Ultrasonic Fence', 120);
END
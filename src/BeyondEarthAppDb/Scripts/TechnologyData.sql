IF NOT EXISTS (SELECT * FROM dbo.Technology WHERE Name = 'Habitation')
	INSERT INTO dbo.Technology(Name, Cost) VALUES ('Habitation', 0);

IF NOT EXISTS (SELECT * FROM dbo.Technology WHERE Name = 'Chemistry')
	INSERT INTO dbo.Technology(Name, Cost) VALUES ('Chemistry', 95);

IF NOT EXISTS (SELECT * FROM dbo.Technology WHERE Name = 'Ecology')
	INSERT INTO dbo.Technology(Name, Cost) VALUES ('Ecology', 95);

-- TODO: Maybe do it like in TestData.sql, using SCOPE_IDENTITY()
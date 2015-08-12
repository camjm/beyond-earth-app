IF NOT EXISTS (SELECT * FROM dbo.Status WHERE Name = 'Not Started')
	INSERT INTO dbo.Status(Name, Ordinal) VALUES ('Not Started', 0);

IF NOT EXISTS (SELECT * FROM dbo.Status WHERE Name = 'In Progress')
	INSERT INTO dbo.Status(Name, Ordinal) VALUES ('In Progress', 1);

IF NOT EXISTS (SELECT * FROM dbo.Status WHERE Name = 'Completed')
	INSERT INTO dbo.Status(Name, Ordinal) VALUES ('Completed', 2);
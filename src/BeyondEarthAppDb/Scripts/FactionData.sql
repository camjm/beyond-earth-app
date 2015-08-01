IF NOT EXISTS (SELECT * FROM dbo.Faction WHERE Name = 'American Reclamation Corporation')
	INSERT INTO dbo.Faction(Name, Leader, Capital, Ability) 
	VALUES ('American Reclamation Corporation', 'Suzanne Fielding', 'Central', 'Covert Operations are 25% faster and cause 25% more Intrigue');

IF NOT EXISTS (SELECT * FROM dbo.Faction WHERE Name = 'Brasilia')
	INSERT INTO dbo.Faction(Name, Leader, Capital, Ability) 
	VALUES ('Brasilia', 'Rejinaldo de Alencar', 'Cidadela', 'Units have +10% Strength in melee combat');

IF NOT EXISTS (SELECT * FROM dbo.Faction WHERE Name = 'Franco-Iberia')
	INSERT INTO dbo.Faction(Name, Leader, Capital, Ability) 
	VALUES ('Franco-Iberia', 'Elodie', 'Le Coeur', 'Gain a free Technology for every 10 Virtues developed');

IF NOT EXISTS (SELECT * FROM dbo.Faction WHERE Name = 'Kavithan Protectorate')
	INSERT INTO dbo.Faction(Name, Leader, Capital, Ability) 
	VALUES ('Kavithan Protectorate', 'Kavitha Thakur', 'Mandira', 'Cities and Outposts acquire new tiles twice as fast');

IF NOT EXISTS (SELECT * FROM dbo.Faction WHERE Name = 'Pan-Asian Cooperative')
	INSERT INTO dbo.Faction(Name, Leader, Capital, Ability) 
	VALUES ('Pan-Asian Cooperative', 'Daoming Sochua', 'Tiangong', '+10% Production towards Wonders, and +25% Worker speed');

IF NOT EXISTS (SELECT * FROM dbo.Faction WHERE Name = 'People''s African Union')
	INSERT INTO dbo.Faction(Name, Leader, Capital, Ability) 
	VALUES ('People''s African Union', 'Samatar Jama Barre', 'Magan', '+10% Food in growing Cities when Healthy');

IF NOT EXISTS (SELECT * FROM dbo.Faction WHERE Name = 'Polystralia')
	INSERT INTO dbo.Faction(Name, Leader, Capital, Ability) 
	VALUES ('Polystralia', 'Hutama', 'Freeland', '+2 Trade Routes available for the Capital');

IF NOT EXISTS (SELECT * FROM dbo.Faction WHERE Name = 'Slavic Federation')
	INSERT INTO dbo.Faction(Name, Leader, Capital, Ability) 
	VALUES ('Slavic Federation', 'Vadim Kozlov', 'Khrabrost', 'Orbital Units stay in orbit 20% longer, and the first one launched grants a free Technology');
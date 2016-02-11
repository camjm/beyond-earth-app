DECLARE
	@gameFactionId int,
	@gameStatusId int,
	@gameUserId int

IF NOT EXISTS (SELECT * FROM [User] WHERE Username = 'bhogg')
	INSERT INTO [dbo].[User] ([Firstname], [Lastname], [Username])
	VALUES (N'Boss', N'Hogg', N'bhogg')

IF NOT EXISTS (SELECT * FROM [User] WHERE Username = 'jbob')
	INSERT INTO [dbo].[User] ([Firstname], [Lastname], [Username])
	VALUES (N'Jim', N'Bib', N'jbob')

IF NOT EXISTS (SELECT * FROM [User] WHERE Username = 'jdoe')
	INSERT INTO [dbo].[User] ([Firstname], [Lastname], [Username])
	VALUES (N'John', N'Doe', N'jdoe')

IF NOT EXISTS (SELECT * FROM dbo.[Game] WHERE Description = 'Test Game')
BEGIN
	SELECT TOP 1 @gameFactionId = FactionId FROM [Faction] ORDER BY FactionId;
	SELECT TOP 1 @gameStatusId = StatusId FROM [Status] ORDER BY StatusId;
	SELECT TOP 1 @gameUserId = UserId FROM [User] ORDER BY UserId;

	INSERT INTO dbo.Game([Description], StatusId, UserId, FactionId, CreatedDate)
	VALUES ('Test Game', @gameStatusId, @gameUserId, @gameFactionId, getdate());
END
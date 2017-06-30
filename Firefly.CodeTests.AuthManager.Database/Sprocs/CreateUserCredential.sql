CREATE PROCEDURE [dbo].[CreateUserCredential]
	@username nvarchar(100),
	@passwordHash nvarchar(50),
	@passwordSalt nvarchar(50)
AS
	DECLARE @userId int = (SELECT UserId from [User] where Username = @username)
	IF(@@ROWCOUNT > 0)
	BEGIN
		INSERT INTO UserCredential Values(@userId, @passwordHash, @passwordSalt)
	END



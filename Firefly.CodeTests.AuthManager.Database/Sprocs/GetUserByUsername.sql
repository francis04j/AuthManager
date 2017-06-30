CREATE PROCEDURE [dbo].[GetUserByUsername]
	@username nvarchar(100)
AS
	SELECT username from [User] where username = @username
Go

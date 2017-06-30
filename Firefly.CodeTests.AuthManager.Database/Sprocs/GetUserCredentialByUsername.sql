CREATE PROCEDURE [dbo].[GetUserCredentialByUsername]
	@Username nvarchar(100)
AS
	SELECT [Hash], Salt from UserCredential where UserId in (SELECT UserId from [User] where Username=@Username)


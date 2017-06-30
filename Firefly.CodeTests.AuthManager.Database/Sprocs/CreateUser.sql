CREATE PROCEDURE [dbo].[CreateUser]
	@Username nvarchar(255)
	
AS
	INSERT INTO [User] values(@Username, GETDATE(), GetDate())	

GO
	


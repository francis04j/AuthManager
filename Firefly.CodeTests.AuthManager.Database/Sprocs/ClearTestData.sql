CREATE PROCEDURE [dbo].[ClearTestData]
	
AS
	DELETE from UserCredential where UserId in (SELECT UserId FROM [User] WHERE Username like '%@testmail.com')

	DELETE FROM [User] WHERE Username like '%@testmail.com'



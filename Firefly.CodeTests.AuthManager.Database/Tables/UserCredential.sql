CREATE TABLE [dbo].[UserCredential]
(
	[CredentialId] INT IDENTITY(1,1) NOT NULL , 
    [UserId] INT NOT NULL, 
    [Hash] NVARCHAR(50) NOT NULL, 
    [Salt] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_UserCredential_User] FOREIGN KEY (UserId) REFERENCES [User]([UserId]), 
    CONSTRAINT [PK_UserCredential] PRIMARY KEY ([CredentialId]) 
)

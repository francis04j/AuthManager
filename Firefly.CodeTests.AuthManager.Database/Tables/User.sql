CREATE TABLE [dbo].[User]
(
	[UserId] int IDENTITY(1,1) NOT NULL, 
    [Username] NVARCHAR(100) NOT NULL, 
    [Created] SMALLDATETIME NOT NULL, 
    [Updated] SMALLDATETIME NOT NULL, 
    PRIMARY KEY ([UserId]) 
)

GO

CREATE INDEX [IX_User_Username] ON [dbo].[User] ([Username])

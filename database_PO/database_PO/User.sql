CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Role] INT NOT NULL FOREIGN KEY REFERENCES [Roles]([Id_Roles]), 
    [FirstName] VARCHAR(100) NOT NULL, 
    [SecondName] VARCHAR(100) NULL, 
    [LastName] VARCHAR(100) NOT NULL, 
    [Number_of_phone] VARCHAR(20) NOT NULL, 
    [Password] VARCHAR(100) NOT NULL, 
    [NamePhoto] VARCHAR(100) NULL 
)

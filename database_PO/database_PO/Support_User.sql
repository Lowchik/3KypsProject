CREATE TABLE [dbo].[Support_User]
(
	[Id_Support_User] INT NOT NULL PRIMARY KEY, 
    [Id_Applications] INT NOT NULL FOREIGN KEY REFERENCES [Applications]([Id_applications]), 
    [Data_Support] DATE NULL, 
    [Time_Support] TIME NULL 
)

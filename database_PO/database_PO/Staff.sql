CREATE TABLE [dbo].[Staff]
(
	[Id_staff] INT NOT NULL PRIMARY KEY, 
    [Passport] INT NOT NULL  FOREIGN KEY REFERENCES [Passport]([Id_Passport]), 
    [Post] NCHAR(10) NULL
)

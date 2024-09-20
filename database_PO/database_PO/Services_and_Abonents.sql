CREATE TABLE [dbo].[Services_and_Abonents]
(
	[Id_Abonents] INT NOT NULL PRIMARY KEY, 
    [Id_Services ] INT NOT NULL FOREIGN KEY REFERENCES [Service]([Id_Services]) 

)

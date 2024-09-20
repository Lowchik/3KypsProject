CREATE TABLE [dbo].[Applications]
(
	[Id_applications] INT NOT NULL PRIMARY KEY, 
    [Staff] INT NOT NULL FOREIGN KEY REFERENCES [Staff]([Id_staff]), 
    [Abonent] INT NOT NULL FOREIGN KEY REFERENCES [Abonent]([Id_Abonent]), 
    [Application_place] VARCHAR(100) NOT NULL, 
    [Status] VARCHAR(100) NOT NULL, 
    [Type_of_application] VARCHAR(100) NOT NULL, 
    [Description_of_problem] VARCHAR(100) NULL, 
    [Date_of_create] DATE NOT NULL, 
    [Date_of_close] DATE NOT NULL
)

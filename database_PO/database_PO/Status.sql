CREATE TABLE [dbo].[Status]
(
	[Id_Status] INT NOT NULL PRIMARY KEY, 
    [Status] VARCHAR(100) NOT NULL, 
    [Type_Of_Ports] VARCHAR(50) NOT NULL, 
    [Bandwidth] INT NOT NULL, 
    [TheDestination] VARCHAR(100) NULL
)

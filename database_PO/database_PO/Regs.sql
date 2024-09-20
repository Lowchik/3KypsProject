CREATE TABLE [dbo].[Regs]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Abonent] INT NOT NULL FOREIGN KEY REFERENCES [Abonent]([Id_Abonent]),
    [Equipment] INT NOT NULL  FOREIGN KEY REFERENCES [Equipment]([Id_Equipment]), 
    [Date_of_connect] DATE NOT NULL
)

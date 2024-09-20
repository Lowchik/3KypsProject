CREATE TABLE [dbo].[Equipment]
(
	[Id_Equipment] INT NOT NULL PRIMARY KEY,
    [Type_Of_Equipment] int NOT NULL  FOREIGN KEY REFERENCES [Type_of_equipment]([Id_Type]),
    [List_of_number] VARCHAR(50) NOT NULL ,
    [Port] int NOT NULL FOREIGN KEY REFERENCES [Ports]([Id_Port]),   
    [Name] VARCHAR(50) NOT NULL,
    [MAC] VARCHAR(20) NULL, 
    [Inventory_number] INT NOT NULL, 
    [Ip] VARCHAR(25) NULL, 
    [Serial_number] VARCHAR(6) NULL, 
    [Adress_of_build] VARCHAR(100) NOT NULL, 
    [Place_of_build] VARCHAR(100) NOT NULL, 
    [Connection_point] VARCHAR(50) NULL
)

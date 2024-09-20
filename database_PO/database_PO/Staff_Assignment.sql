CREATE TABLE [dbo].[Staff_Assignment]
(
    [id_staff_assignment] INT NOT NULL PRIMARY KEY, 
    [Date] DATE NULL, 
    [Time] TIME NULL, 
    [Reason] VARCHAR(255) NULL, 
    [Worker] VARCHAR(255) NULL	
);
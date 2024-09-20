CREATE TABLE [dbo].[Passport]
(
	[Id_Passport] INT NOT NULL PRIMARY KEY, 
    [FirstName ] VARCHAR(100) NOT NULL, 
    [LastName] VARCHAR(100) NOT NULL, 
    [SecondName] VARCHAR(100) NOT NULL, 
    [Series_Passport] INT NOT NULL, 
    [Date_Of_birth] DATE NOT NULL, 
    [Gender] VARCHAR(100) NOT NULL, 
    [Passport_Number] INT NOT NULL, 
    [Department_Code] INT NOT NULL, 
    [Issued_By_Whom] VARCHAR(100) NOT NULL, 
    [Date_Of_Issue_Of_The_passport] DATE NOT NULL, 
    [Registration_Address] VARCHAR(100) NOT NULL, 
    [Address_Of_Residence] VARCHAR(100) NOT NULL
)

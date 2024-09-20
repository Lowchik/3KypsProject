CREATE TABLE [dbo].[Abonent]
(
	[Id_Abonent] INT NOT NULL PRIMARY KEY, 
    [Passport] INT NOT NULL FOREIGN KEY REFERENCES [Passport]([Id_Passport]), 
    [Service] INT NOT NULL FOREIGN KEY REFERENCES [Services_and_Abonents]([Id_Abonents]), 
    [Name_Tarif] VARCHAR (100) NOT NULL , 
    [Cost_package] int NOT NULL,
    [Debt_information] VARCHAR(100) NOT NULL, 
    [Date_payment] DATE NOT NULL,
    [Suma_payment] int NOT NULL,
    [Balance] int NOT NULL,
    [History_payment] VARCHAR (30) NOT NULL,
    [Personal_account] VARCHAR(100) NOT NULL, 
    [Number_Of_phone] VARCHAR(20) NOT NULL, 
    [eMail] VARCHAR(100) NOT NULL, 
    [ContractNumber] VARCHAR(100) NOT NULL, 
    [TypeOfAgreement] VARCHAR(100) NOT NULL, 
    [Reason_for_termination_of_the_contract] VARCHAR(20) NOT NULL
)

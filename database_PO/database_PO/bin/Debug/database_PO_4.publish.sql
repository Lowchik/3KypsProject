/*
Скрипт развертывания для database_PO

Этот код был создан программным средством.
Изменения, внесенные в этот файл, могут привести к неверному выполнению кода и будут потеряны
в случае его повторного формирования.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "database_PO"
:setvar DefaultFilePrefix "database_PO"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Проверьте режим SQLCMD и отключите выполнение скрипта, если режим SQLCMD не поддерживается.
Чтобы повторно включить скрипт после включения режима SQLCMD выполните следующую инструкцию:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Для успешного выполнения этого скрипта должен быть включен режим SQLCMD.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Идет создание базы данных $(DatabaseName)…'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE Cyrillic_General_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Параметры базы данных изменить нельзя. Применить эти параметры может только пользователь SysAdmin.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Параметры базы данных изменить нельзя. Применить эти параметры может только пользователь SysAdmin.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Идет создание Таблица [dbo].[Abonent]…';


GO
CREATE TABLE [dbo].[Abonent] (
    [Id_Abonent]                             INT           NOT NULL,
    [Passport]                               INT           NOT NULL,
    [Service]                                INT           NOT NULL,
    [Name_Tarif]                             VARCHAR (100) NOT NULL,
    [Cost_package]                           INT           NOT NULL,
    [Debt_information]                       VARCHAR (100) NOT NULL,
    [Date_payment]                           DATE          NOT NULL,
    [Suma_payment]                           INT           NOT NULL,
    [Balance]                                INT           NOT NULL,
    [History_payment]                        VARCHAR (30)  NOT NULL,
    [Personal_account]                       VARCHAR (100) NOT NULL,
    [Number_Of_phone]                        VARCHAR (20)  NOT NULL,
    [eMail]                                  VARCHAR (100) NOT NULL,
    [ContractNumber]                         VARCHAR (100) NOT NULL,
    [TypeOfAgreement]                        VARCHAR (100) NOT NULL,
    [Reason_for_termination_of_the_contract] VARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Abonent] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Applications]…';


GO
CREATE TABLE [dbo].[Applications] (
    [Id_applications]        INT           NOT NULL,
    [Staff]                  INT           NOT NULL,
    [Abonent]                INT           NOT NULL,
    [Application_place]      VARCHAR (100) NOT NULL,
    [Status]                 VARCHAR (100) NOT NULL,
    [Type_of_application]    VARCHAR (100) NOT NULL,
    [Description_of_problem] VARCHAR (100) NULL,
    [Date_of_create]         DATE          NOT NULL,
    [Date_of_close]          DATE          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_applications] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Equipment]…';


GO
CREATE TABLE [dbo].[Equipment] (
    [Id_Equipment]      INT           NOT NULL,
    [Type_Of_Equipment] INT           NOT NULL,
    [List_of_number]    VARCHAR (50)  NOT NULL,
    [Port]              INT           NOT NULL,
    [Name]              VARCHAR (50)  NOT NULL,
    [MAC]               VARCHAR (20)  NULL,
    [Inventory_number]  INT           NOT NULL,
    [Ip]                VARCHAR (25)  NULL,
    [Serial_number]     VARCHAR (6)   NULL,
    [Adress_of_build]   VARCHAR (100) NOT NULL,
    [Place_of_build]    VARCHAR (100) NOT NULL,
    [Connection_point]  VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id_Equipment] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Events]…';


GO
CREATE TABLE [dbo].[Events] (
    [Id_Events]  INT          NOT NULL,
    [Name_event] VARCHAR (50) NULL,
    [time]       TIME (7)     NULL,
    PRIMARY KEY CLUSTERED ([Id_Events] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Passport]…';


GO
CREATE TABLE [dbo].[Passport] (
    [Id_Passport]                   INT           NOT NULL,
    [FirstName ]                    VARCHAR (100) NOT NULL,
    [LastName]                      VARCHAR (100) NOT NULL,
    [SecondName]                    VARCHAR (100) NOT NULL,
    [Series_Passport]               INT           NOT NULL,
    [Date_Of_birth]                 DATE          NOT NULL,
    [Gender]                        VARCHAR (100) NOT NULL,
    [Passport_Number]               INT           NOT NULL,
    [Department_Code]               INT           NOT NULL,
    [Issued_By_Whom]                VARCHAR (100) NOT NULL,
    [Date_Of_Issue_Of_The_passport] DATE          NOT NULL,
    [Registration_Address]          VARCHAR (100) NOT NULL,
    [Address_Of_Residence]          VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Passport] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Ports]…';


GO
CREATE TABLE [dbo].[Ports] (
    [Id_Port] INT          NOT NULL,
    [Status]  VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Port] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Regs]…';


GO
CREATE TABLE [dbo].[Regs] (
    [Id]              INT  NOT NULL,
    [Abonent]         INT  NOT NULL,
    [Equipment]       INT  NOT NULL,
    [Date_of_connect] DATE NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Roles]…';


GO
CREATE TABLE [dbo].[Roles] (
    [Id_Roles]     INT           NOT NULL,
    [Name_of_role] VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Roles] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Service]…';


GO
CREATE TABLE [dbo].[Service] (
    [Id_Services]            INT           NOT NULL,
    [Name_Service]           VARCHAR (100) NOT NULL,
    [Type_Of_services]       INT           NOT NULL,
    [Subspecies_of_services] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Services] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Services_and_Abonents]…';


GO
CREATE TABLE [dbo].[Services_and_Abonents] (
    [Id_Abonents]  INT NOT NULL,
    [Id_Services ] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Abonents] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Staff]…';


GO
CREATE TABLE [dbo].[Staff] (
    [Id_staff] INT        NOT NULL,
    [Passport] INT        NOT NULL,
    [Post]     NCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Id_staff] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Staff_Assignment]…';


GO
CREATE TABLE [dbo].[Staff_Assignment] (
    [id_staff_assignment] INT           NOT NULL,
    [Date]                DATE          NULL,
    [Time]                TIME (7)      NULL,
    [Reason]              VARCHAR (255) NULL,
    [Worker]              VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([id_staff_assignment] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Status]…';


GO
CREATE TABLE [dbo].[Status] (
    [Id_Status]      INT           NOT NULL,
    [Status]         VARCHAR (100) NOT NULL,
    [Type_Of_Ports]  VARCHAR (50)  NOT NULL,
    [Bandwidth]      INT           NOT NULL,
    [TheDestination] VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id_Status] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Subspecies_of_services]…';


GO
CREATE TABLE [dbo].[Subspecies_of_services] (
    [Id_Subspecies_Of_Services] INT          NOT NULL,
    [Name]                      VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Subspecies_Of_Services] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Support_User]…';


GO
CREATE TABLE [dbo].[Support_User] (
    [Id_Support_User] INT      NOT NULL,
    [Id_Applications] INT      NOT NULL,
    [Data_Support]    DATE     NULL,
    [Time_Support]    TIME (7) NULL,
    PRIMARY KEY CLUSTERED ([Id_Support_User] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Type_of_equipment]…';


GO
CREATE TABLE [dbo].[Type_of_equipment] (
    [Id_Type]      INT           NOT NULL,
    [Name_of_type] VARCHAR (100) NULL,
    CONSTRAINT [PK_Type_of_equipment] PRIMARY KEY CLUSTERED ([Id_Type] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[Type_Of_services]…';


GO
CREATE TABLE [dbo].[Type_Of_services] (
    [Id_Type_Of_services] INT          NOT NULL,
    [Name]                VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Type_Of_services] ASC)
);


GO
PRINT N'Идет создание Таблица [dbo].[User]…';


GO
CREATE TABLE [dbo].[User] (
    [Id]              INT           NOT NULL,
    [Role]            INT           NOT NULL,
    [FirstName]       VARCHAR (100) NOT NULL,
    [SecondName]      VARCHAR (100) NULL,
    [LastName]        VARCHAR (100) NOT NULL,
    [Number_of_phone] VARCHAR (20)  NOT NULL,
    [Password]        VARCHAR (100) NOT NULL,
    [NamePhoto]       VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[Abonent]…';


GO
ALTER TABLE [dbo].[Abonent]
    ADD FOREIGN KEY ([Passport]) REFERENCES [dbo].[Passport] ([Id_Passport]);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[Abonent]…';


GO
ALTER TABLE [dbo].[Abonent]
    ADD FOREIGN KEY ([Service]) REFERENCES [dbo].[Services_and_Abonents] ([Id_Abonents]);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[Applications]…';


GO
ALTER TABLE [dbo].[Applications]
    ADD FOREIGN KEY ([Staff]) REFERENCES [dbo].[Staff] ([Id_staff]);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[Applications]…';


GO
ALTER TABLE [dbo].[Applications]
    ADD FOREIGN KEY ([Abonent]) REFERENCES [dbo].[Abonent] ([Id_Abonent]);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[Equipment]…';


GO
ALTER TABLE [dbo].[Equipment]
    ADD FOREIGN KEY ([Type_Of_Equipment]) REFERENCES [dbo].[Type_of_equipment] ([Id_Type]);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[Equipment]…';


GO
ALTER TABLE [dbo].[Equipment]
    ADD FOREIGN KEY ([Port]) REFERENCES [dbo].[Ports] ([Id_Port]);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[Regs]…';


GO
ALTER TABLE [dbo].[Regs]
    ADD FOREIGN KEY ([Abonent]) REFERENCES [dbo].[Abonent] ([Id_Abonent]);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[Regs]…';


GO
ALTER TABLE [dbo].[Regs]
    ADD FOREIGN KEY ([Equipment]) REFERENCES [dbo].[Equipment] ([Id_Equipment]);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[Services_and_Abonents]…';


GO
ALTER TABLE [dbo].[Services_and_Abonents]
    ADD FOREIGN KEY ([Id_Services ]) REFERENCES [dbo].[Service] ([Id_Services]);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[Staff]…';


GO
ALTER TABLE [dbo].[Staff]
    ADD FOREIGN KEY ([Passport]) REFERENCES [dbo].[Passport] ([Id_Passport]);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[Support_User]…';


GO
ALTER TABLE [dbo].[Support_User]
    ADD FOREIGN KEY ([Id_Applications]) REFERENCES [dbo].[Applications] ([Id_applications]);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[User]…';


GO
ALTER TABLE [dbo].[User]
    ADD FOREIGN KEY ([Role]) REFERENCES [dbo].[Roles] ([Id_Roles]);


GO
-- Выполняется этап рефакторинга для обновления развернутых журналов транзакций на целевом сервере

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'f87380e4-1d76-4023-8fdf-7f2976f603b3')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('f87380e4-1d76-4023-8fdf-7f2976f603b3')

GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Обновление завершено.';


GO

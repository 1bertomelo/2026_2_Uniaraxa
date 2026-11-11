-- Execute no banco Uniaraxa antes de utilizar o repositório.
IF OBJECT_ID(N'dbo.Students', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students
    (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Students PRIMARY KEY,
        [Name] NVARCHAR(200) NOT NULL,
        Email NVARCHAR(320) NOT NULL
    );
END;

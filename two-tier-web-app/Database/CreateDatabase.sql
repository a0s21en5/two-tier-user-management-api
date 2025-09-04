-- Create Database (if not exists)
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TwoTierWebAppDB')
BEGIN
    CREATE DATABASE TwoTierWebAppDB;
END
GO

-- Use the database
USE TwoTierWebAppDB;
GO

-- Create Users table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
BEGIN
    CREATE TABLE Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        Phone NVARCHAR(20) NOT NULL,
        CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedDate DATETIME2 NULL,
        IsActive BIT NOT NULL DEFAULT 1
    );

    -- Create indexes for better performance
    CREATE INDEX IX_Users_Email ON Users(Email);
    CREATE INDEX IX_Users_IsActive ON Users(IsActive);
    CREATE INDEX IX_Users_CreatedDate ON Users(CreatedDate);
END
GO

-- Insert sample data
IF NOT EXISTS (SELECT * FROM Users)
BEGIN
    INSERT INTO Users (FirstName, LastName, Email, Phone, CreatedDate, IsActive)
    VALUES 
        ('John', 'Doe', 'john.doe@example.com', '+1234567890', GETUTCDATE(), 1),
        ('Jane', 'Smith', 'jane.smith@example.com', '+1987654321', GETUTCDATE(), 1),
        ('Bob', 'Johnson', 'bob.johnson@example.com', '+1122334455', GETUTCDATE(), 1),
        ('Alice', 'Brown', 'alice.brown@example.com', '+1555666777', GETUTCDATE(), 1),
        ('Charlie', 'Wilson', 'charlie.wilson@example.com', '+1888999000', GETUTCDATE(), 1);
END
GO

PRINT 'Database and Users table created successfully with sample data!';

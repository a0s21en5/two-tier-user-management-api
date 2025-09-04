using Microsoft.Data.SqlClient;
using System.Data;

namespace two_tier_web_app.Infrastructure;

public static class DatabaseInitializer
{
    public static async Task InitializeDatabase(string connectionString)
    {
        try
        {
            // First, create the database if it doesn't exist
            var masterConnectionString = connectionString.Replace("Database=TwoTierWebAppDB;", "Database=master;");
            
            using var masterConnection = new SqlConnection(masterConnectionString);
            await masterConnection.OpenAsync();

            var createDbCommand = masterConnection.CreateCommand();
            createDbCommand.CommandText = @"
                IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TwoTierWebAppDB')
                BEGIN
                    CREATE DATABASE TwoTierWebAppDB;
                END";
            
            await createDbCommand.ExecuteNonQueryAsync();
            Console.WriteLine("Database created or already exists.");

            // Now use the target database
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var createTableCommand = connection.CreateCommand();
            createTableCommand.CommandText = @"
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

                    CREATE INDEX IX_Users_Email ON Users(Email);
                    CREATE INDEX IX_Users_IsActive ON Users(IsActive);
                    CREATE INDEX IX_Users_CreatedDate ON Users(CreatedDate);
                END";

            await createTableCommand.ExecuteNonQueryAsync();
            Console.WriteLine("Users table created or already exists.");

            // Insert sample data
            var insertDataCommand = connection.CreateCommand();
            insertDataCommand.CommandText = @"
                IF NOT EXISTS (SELECT * FROM Users)
                BEGIN
                    INSERT INTO Users (FirstName, LastName, Email, Phone, CreatedDate, IsActive)
                    VALUES 
                        ('John', 'Doe', 'john.doe@example.com', '+1234567890', GETUTCDATE(), 1),
                        ('Jane', 'Smith', 'jane.smith@example.com', '+1987654321', GETUTCDATE(), 1),
                        ('Bob', 'Johnson', 'bob.johnson@example.com', '+1122334455', GETUTCDATE(), 1),
                        ('Alice', 'Brown', 'alice.brown@example.com', '+1555666777', GETUTCDATE(), 1),
                        ('Charlie', 'Wilson', 'charlie.wilson@example.com', '+1888999000', GETUTCDATE(), 1);
                END";

            await insertDataCommand.ExecuteNonQueryAsync();
            Console.WriteLine("Sample data inserted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database initialization error: {ex.Message}");
            // For demo purposes, we'll continue without throwing
            // In production, you might want to handle this differently
        }
    }
}

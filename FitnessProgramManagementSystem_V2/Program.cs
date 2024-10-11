using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProgramManagementSystem_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SetConnection();



        }

        static void SetConnection()
        {
            string masterDbConnectionString = "Server=(localdb)\\MSSQLLocalDB; Database=master; Trusted_Connection=True; TrustServerCertificate=True;";
            string DbConnectionString = "Server=(localdb)\\MSSQLLocalDB; Database=FitnessProgramManagement; Trusted_Connection=True; TrustServerCertificate=True;";

            string dbQuery = @"
                                IF NOT EXISTS (SELECT * FROM sys.databases WHERE name='FitnessProgramManagement')
                                CREATE DATABASE FitnessProgramManagement;";

            string tableQuery = @"
                                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FitnessPrograms' AND xtype='U')
                                CREATE TABLE FitnessPrograms(
                                FitnessProgramId NVARCHAR(20) PRIMARY KEY,
                                Title NVARCHAR(50) NOT NULL,
                                Duration NVARCHAR(50) NOT NULL,
                                Price DECIMAL(10,1) NOT NULL,
                                );";

            string insertQuery = @"INSERT INTO FitnessPrograms(FitnessProgramId,Title,Duration,Price)
                                   VALUES('FIT 001', 'weight Training', '6 Months', '10');";

            using (SqlConnection connection = new SqlConnection(masterDbConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(dbQuery, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Database created successfully");

                }
            }

            using (SqlConnection connection = new SqlConnection(DbConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(tableQuery, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Table created successfully");

                }
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Data created successfully");

                }
            }
            Console.ReadLine();
        }
    }
}

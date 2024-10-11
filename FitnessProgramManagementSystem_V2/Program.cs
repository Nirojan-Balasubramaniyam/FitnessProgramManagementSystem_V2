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
            FitnessProgramRepository repository = new FitnessProgramRepository();

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("FitnessProgram Management System: ");
                Console.WriteLine("1. Add a FitnessProgram");
                Console.WriteLine("2. View All FitnessPrograms");
                Console.WriteLine("3. Update a FitnessProgram");
                Console.WriteLine("4. Delete a FitnessProgram");
                Console.WriteLine("5. View FitnessProgram BY ID");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option:");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        CreateFitnessProgram(repository);
                        break;

                    case "2":
                        Console.Clear();
                        ReadPrograms(repository);
                        break;

                    case "3":
                        Console.Clear();
                        UpdateFitnessProgram(repository);
                        break;

                    case "4":
                        Console.Clear();
                        DeleteFitnessProgram(repository);
                        break;

                    case "5":
                        Console.Clear();
                        ReadProgramByID(repository);    
                        break;

                    case "6":
                        Console.Clear();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input!!. Please select avalid input");
                        break;

                }

                if (!exit)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                }
            }


        }

        static void CreateFitnessProgram(FitnessProgramRepository repo)
        {
            Console.WriteLine("Enter FitnessProgram ID: ");
            string programId = Console.ReadLine();

            Console.WriteLine("Enter FitnessProgram Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Enter FitnessProgram Duration: ");
            string duration = Console.ReadLine();

            Console.WriteLine("Enter FitnessProgram Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            repo.CreateFitnessProgram(programId, title, duration, price);
        }

        static void UpdateFitnessProgram(FitnessProgramRepository repo)
        {
            Console.WriteLine("Enter FitnessProgram ID: ");
            string programId = Console.ReadLine();


            var program = repo.ReadFitnessProgramByID(programId);
            if (program != null)
            {
                Console.WriteLine("Enter FitnessProgram Title: ");
                string title = Console.ReadLine();

                Console.WriteLine("Enter FitnessProgram Duration: ");
                string duration = Console.ReadLine();

                Console.WriteLine("Enter FitnessProgram Price: ");
                decimal price = decimal.Parse(Console.ReadLine());

                repo.UpdateFitnessProgram(programId, title, duration, price);
            }
            else
            {
                Console.WriteLine($"No Program available for ID: {programId}");
            }

        }


        static void DeleteFitnessProgram(FitnessProgramRepository repo)
        {
            Console.WriteLine("Enter FitnessProgram ID: ");
            string programId = Console.ReadLine();

            var program = repo.ReadFitnessProgramByID(programId);
            if (program != null)
            {
                repo.DeleteFitnessProgram(programId);
            }
            else
            {
                Console.WriteLine($"No Program available for ID: {programId}");
            }
        }

        static void ReadProgramByID(FitnessProgramRepository repo)
        {
            Console.WriteLine("Enter FitnessProgram ID: ");
            string programId = Console.ReadLine();

            var program = repo.ReadFitnessProgramByID(programId);
            if (program != null)
            {
                Console.WriteLine(program.ToString());
            }
            else
            {
                Console.WriteLine($"No Program available for ID: {programId}");
            }
        }

        static void ReadPrograms(FitnessProgramRepository repo)
        {
            var programsList = repo.ReadFitnessPrograms();
            if (programsList.Count()>0)
            {
                Console.WriteLine("Programs List: ");
                foreach(var program in programsList)
                {
                    Console.WriteLine(program.ToString());
                }
            }
            else
            {
                Console.WriteLine($"No Program available ");
            }
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
                /*using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Data created successfully");

                }*/
            }

            //Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProgramManagementSystem_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {

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
                                );";

        }
    }
}

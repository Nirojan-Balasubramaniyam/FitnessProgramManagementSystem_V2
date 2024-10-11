using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProgramManagementSystem_V2
{
    internal class FitnessProgramRepository
    {
        string DbConnectionString = "Server=(localdb)\\MSSQLLocalDB; Database=FitnessProgramManagement; Trusted_Connection=True; TrustServerCertificate=True;";

        public void CreateFitnessProgram(string id, string title, string duration, decimal price)
        {
            try
            {
                string insertQuery = @"INSERT INTO FitnessPrograms(FitnessProgramId,Title,Duration,Price)
                                   VALUES(@id, @title, @duration, @price);";
                using (SqlConnection connection = new SqlConnection(DbConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@title", title);
                        command.Parameters.AddWithValue("@duration", duration);
                        command.Parameters.AddWithValue("@price", price);
                        command.ExecuteNonQuery();
                        Console.WriteLine("Program created successfully");

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: "+ ex.Message);
            }

        }

        public void UpdateFitnessProgram(string id, string title, string duration, decimal price)
        {
            try
            {
                string updateQuery = @"UPDATE FitnessPrograms SET Title=@title,Duration=@duration,Price=@price WHERE FitnessProgramId=@id;";
                using (SqlConnection connection = new SqlConnection(DbConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@title", title);
                        command.Parameters.AddWithValue("@duration", duration);
                        command.Parameters.AddWithValue("@price", price);
                        command.ExecuteNonQuery();
                        Console.WriteLine("Program updated successfully");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        public void DeleteFitnessProgram(string id)
        {
            try
            {
                string deleteQuery = @"DELETE FitnessPrograms WHERE FitnessProgramId=@id;";
                using (SqlConnection connection = new SqlConnection(DbConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        Console.WriteLine("Program deleted successfully");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        public FitnessProgram ReadFitnessProgramByID(string id)
        {
            FitnessProgram program = null;
            try
            {

                string readQuery = @"SELECT * FROM FitnessPrograms WHERE FitnessProgramId=@id;";
                using (SqlConnection connection = new SqlConnection(DbConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(readQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string programId = reader.GetString(0);
                                string title = reader.GetString(1);
                                string duration = reader.GetString(2);
                                decimal price = reader.GetDecimal(3);
                                program = new FitnessProgram(programId,title,duration,price);
                            }
                        }
                        Console.WriteLine("Program deleted successfully");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return program;

        }

        public List<FitnessProgram> ReadFitnessPrograms()
        {
            var programList = new List<FitnessProgram>();
            try
            {

                string readQuery = @"SELECT * FROM FitnessPrograms;";
                using (SqlConnection connection = new SqlConnection(DbConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(readQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string programId = reader.GetString(0);
                                string title = reader.GetString(1);
                                string duration = reader.GetString(2);
                                decimal price = reader.GetDecimal(3);
                                programList.Add(new FitnessProgram(programId, title, duration, price));
                            }
                        }
                        Console.WriteLine("Program deleted successfully");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return programList;

        }
    }
}

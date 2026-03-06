using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;
using System.Data;
using System.Xml.Linq;

namespace DBProgEx3
{
    internal class Program
    {
        static string connectionString = "Server=(local);Database=Northwind;Integrated Security=SSPI;TrustServerCertificate=True";
        static void Main(string[] args)
        {
            Menu();
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        static void Menu()
        {
            string choice = "";

            while (choice != "Q")
            {
                Console.WriteLine("");
                Console.WriteLine("1. My first queries");
                Console.WriteLine("2. My second queries");
                Console.WriteLine("Q. Quit");
                Console.WriteLine("");

                choice = Console.ReadLine().ToUpper();

                if (choice == "Q")
                    return;

                switch (choice)
                {
                    case "1":
                        Q1();
                        break;
                    case "2":
                        Q2();
                        break;
                }
            }
        }

        private static void Q2()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection; //first connect the command
                cmd.CommandText = "SELECT * FROM Shippers"; // second write your query
                cmd.CommandType = CommandType.Text; // third tell it if its a text or a procedure


                connection.Open();//open a connection after esstablishing the command

                SqlDataReader reader = cmd.ExecuteReader();  // run the reader

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["CompanyName"].ToString());
                    }
                }
                else
                {
                    Console.WriteLine("There are no rows");
                }
                
            }
            catch
            {
                Console.WriteLine("Connection Failed");
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void Q1()
        {
            try
            {
                int numberOfRecords = 0;
                int shipperID;
                string? companyName;
                string? phone;
                string? display = string.Empty;

                // Connection
                // Command
                // SqlDataReader

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT * FROM Shippers";
                        cmd.CommandType = CommandType.Text;


                        connection.Open();


                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Console.WriteLine($"There are {reader.FieldCount} columns in my reader");

                                while (reader.Read())
                                {
                                    numberOfRecords++;
                                    shipperID = Convert.ToInt32(reader["ShipperID"]);
                                    companyName = Convert.ToString(reader["CompanyName"]);
                                    phone = reader["Phone"].ToString();

                                    display += $"ID: {shipperID} Company Name: {companyName} Phone: {phone}\r\n";
                                }

                                Console.WriteLine(display);
                                Console.WriteLine($"Number of Records: {numberOfRecords}r");
                            }
                            else
                            {
                                Console.WriteLine("No records found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

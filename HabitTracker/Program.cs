using Microsoft.Data.Sqlite;

// Habit Tracker Console App that utilizes SQLite
// User can view, insert, update and delete records

namespace HabitTracker
{
    public class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            using (SqliteConnection sqlConnection = CreateConnection())
            {
                int output;
                bool runApp = true;

                while (runApp)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("HABIT TRACKER\n\nMAIN MENU\n");
                        Console.WriteLine("0 - Close Application");
                        Console.WriteLine("1 - View All Records");
                        Console.WriteLine("2 - INSERT Record");
                        Console.WriteLine("3 - DELETE Record");
                        Console.WriteLine("4 - UPDATE Record");
                        Console.Write("Select from the following: ");

                        int.TryParse(Console.ReadLine(), out output);

                        switch (output)
                        {
                            case 0:
                                runApp = false;
                                break;
                            case 1:
                                ReadDB(sqlConnection);
                                break;
                            case 2:
                                InsertData(sqlConnection);
                                break;
                            case 3:
                                DeleteData(sqlConnection);
                                break;
                            case 4:
                                UpdateData(sqlConnection);
                                break;
                            default:
                                break;
                        }
                    } while (output < 0 || output > 4);
                }
            }
        }

        static SqliteConnection CreateConnection()
        {
            SqliteConnection sqlite_conn;

            // Create a new database connection
            sqlite_conn = new SqliteConnection("Data Source = Habit_Tracker_Database.db");

            // Open the connection
            sqlite_conn.Open();

            return sqlite_conn;
        }

        static void DeleteData(SqliteConnection conn)
        {
            string output = "";
            string query = "";

            using (SqliteCommand sqlCommand = ReturnCommand(conn, "SELECT * FROM HabitTracker"))
            {
                using (SqliteDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    do
                    {
                        Console.WriteLine("\nHabits:");
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine($"{sqlDataReader[0]}: {sqlDataReader[1]}");
                        }

                        Console.WriteLine("Type record to delete: ");
                        output = Console.ReadLine().ToLower();
                    } while (string.IsNullOrEmpty(output));

                    if (output == "all")
                    {
                        query = "DELETE FROM HabitTracker";
                    }
                    else
                    {
                        query = $"DELETE FROM HabitTracker WHERE Habit = '{output}'";
                    }
                }
            }

            // Query user input and delete habit(s)
            using (SqliteCommand sqlCommand = ReturnCommand(conn, query))
            {
                using (SqliteDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("delete complete");
                }
            }
            Console.Write("Press any key to continue: ");
            Console.ReadLine();
        }

        static void UpdateData(SqliteConnection conn)
        {
            string query = "";

            using (SqliteCommand sqlCommand = ReturnCommand(conn, "SELECT * FROM HabitTracker"))
            {
                using (SqliteDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    string output = "";

                    do
                    {
                        Console.WriteLine("\nHabits:");
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine($"{sqlDataReader[0]}: {sqlDataReader[1]}");
                        }

                        Console.Write("Type your habit: ");
                        output = Console.ReadLine();

                    } while (string.IsNullOrEmpty(output));

                    query = $"UPDATE HabitTracker SET Quantity = Quantity + 1 WHERE Habit='{output}'";
                }
            }

            // Query user input and update habit
            using (SqliteCommand sqlCommand = ReturnCommand(conn, query))
            {
                using (SqliteDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("update complete");

                }
            }
            Console.Write("Press any key to continue: ");
            Console.ReadLine();
        }
        static void InsertData(SqliteConnection conn)
        {
            string output = "";
            Console.WriteLine("");
            do
            {
                Console.Write("Create new habit: ");
                output = Console.ReadLine();
            } while (string.IsNullOrEmpty(output));

            string query = $"INSERT INTO HabitTracker (Habit, Quantity) Values ('{output}', 1)";

            // Query user input and create new habit
            using (SqliteCommand sqlCommand = ReturnCommand(conn, query))
            {
                using (SqliteDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("insert complete");
                }
            }

            Console.Write("Press any key to continue: ");
            Console.ReadLine();
        }

        static void ReadDB(SqliteConnection conn)
        {
            SqliteDataReader sqlDataReader;
            SqliteCommand sqlCommand = ReturnCommand(conn, "SELECT * FROM HabitTracker");
            sqlDataReader = sqlCommand.ExecuteReader();

            Console.WriteLine("\nHabits:");
            while (sqlDataReader.Read())
            {
                Console.WriteLine($"{sqlDataReader[0]}: {sqlDataReader[1]}: {sqlDataReader[2]}");
            }

            Console.Write("Press any key to continue: ");
            Console.ReadLine();

        }

        static SqliteCommand ReturnCommand(SqliteConnection conn, string query)
        {
            SqliteDataReader sqlDataReader;
            SqliteCommand sqlCommand;
            sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = query;

            return sqlCommand;
        }
    }
}
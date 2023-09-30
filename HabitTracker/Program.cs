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
            SqliteConnection sqlite_conn;
            sqlite_conn = CreateConnection();

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
                            ReadDB(sqlite_conn);
                            break;
                        case 2:
                            InsertData(sqlite_conn);
                            break;
                        default:
                            break;
                    }
                } while (output < 0 || output > 4);
            }
        }


        static SqliteConnection CreateConnection()
        {
            SqliteConnection sqlite_conn;

            // Create a new database connection:
            sqlite_conn = new SqliteConnection("Data Source = Habit_Tracker_Database.db");

            // Open the connection:
            sqlite_conn.Open();

            CreateTable(sqlite_conn);

            return sqlite_conn;
        }

        static void CreateTable(SqliteConnection conn)
        {

            SqliteCommand sqlite_cmd;

            //sqlite_cmd = conn.CreateCommand();
            //sqlite_cmd.CommandText = "";

            //sqlite_cmd.ExecuteNonQuery();

            //conn.Close();
        }

        static void InsertData(SqliteConnection conn)
        {
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test Text ', 1); ";
        }

        static void ReadDB(SqliteConnection conn)
        {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM HabitTracker";

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                Console.WriteLine($"{sqlite_datareader[0]}: {sqlite_datareader[1]}: {sqlite_datareader[2]}");
            }

            Console.Write("Press any key to continue: ");
            Console.ReadLine();

            conn.Close();
        }
    }
}
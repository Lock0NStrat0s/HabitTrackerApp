using Microsoft.Data.Sqlite;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "";

            sqlite_cmd.ExecuteNonQuery();

            //conn.Close();
        }

        static void InsertData(SqliteConnection conn)
        {
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test Text ', 1); ";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test1 Text1 ', 2); ";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test2 Text2 ', 3); ";
            sqlite_cmd.ExecuteNonQuery();


            sqlite_cmd.CommandText = "INSERT INTO SampleTable1(Col1, Col2) VALUES('Test3 Text3 ', 3); ";
            sqlite_cmd.ExecuteNonQuery();

        }

        static void ReadDB(SqliteConnection conn)
        {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
        }
    }
}
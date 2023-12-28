using Microsoft.Data.Sqlite;

namespace FuelEconomy.Services
{
    public class DataAccessService
    {


        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder
                    .CreateFileAsync("sqliteSample.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path,
                                         "sqliteSample.db");
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                    "Text_Entry NVARCHAR(2048) NULL)";

                var createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
    }
}

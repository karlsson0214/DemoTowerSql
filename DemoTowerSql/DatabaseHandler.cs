
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DemoTowerSql
{
    class DatabaseHandler
    {
        // connection string to database
        private String connectionString = "server=127.0.0.1;user id=user;password=pass;database=my_database";
        private MySqlConnection db;

        public DatabaseHandler()
        {
            db = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Create database if not exists.
        /// 
        /// Add table towers to database if it does not exist.
        /// 
        /// Add some towers if the table is empty.
        /// </summary>
        public async void InitializeDatabase()
        {

            
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS tower (id INTEGER PRIMARY KEY AUTO_INCREMENT , " +
                    "name VARCHAR(128) NOT NULL, " +
                    "height INT )";

                MySqlCommand createTable = new MySqlCommand(tableCommand, db);

                var result = createTable.ExecuteReader();

                db.Close();
            
            if (NumberOfTowers() == 0)
            {
                AddSomeTowers();
            }
           
        }
        
        /// <summary>
        /// Add a tower with the speicified name and height.
        /// </summary>
        /// <param name="nameOfTower"></param>
        /// <param name="heightOfTower"></param>
        public void AddTower(string nameOfTower, int heightOfTower)
        {
            db.Open();

                MySqlCommand insertCommand = new MySqlCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO tower (name, height) VALUES (@name, @height);";

                insertCommand.Parameters.AddWithValue("@name", nameOfTower);
                insertCommand.Parameters.AddWithValue("@height", heightOfTower);

                insertCommand.ExecuteReader();

                db.Close();
            

        }

        /// <summary>
        /// Get a list of all towers.
        /// </summary>
        /// <returns></returns>
        public  List<Tower> GetTowers()
        {
            //List<String> entries = new List<string>();
            List<Tower> towers = new List<Tower>();

            db.Open();

                MySqlCommand selectCommand = new MySqlCommand("SELECT * from tower ORDER BY height DESC", db);

                MySqlDataReader query = selectCommand.ExecuteReader();

                // add each tower to the list
                while (query.Read())
                {
                    if (int.TryParse(query.GetString(0), out int id))
                    {
                        Tower tower = new Tower(id);
                        tower.Name = query.GetString(1);
                        if (int.TryParse(query.GetString(2), out int height))
                        {
                            tower.Height = height;
                        }
                        towers.Add(tower);
                    } 
                    
                }

                db.Close();
            

            return towers;
        }

        /// <summary>
        /// Get the tower with the specified id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the tower with the selected id if it exists, otherwise null.</returns>
        public Tower GetTower(int id)
        {
            Tower tower = null;
            db.Open();

                MySqlCommand selectCommand = new MySqlCommand();
                selectCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                selectCommand.CommandText = "SELECT * FROM tower WHERE id=@id";

                selectCommand.Parameters.AddWithValue("@id", id);

                MySqlDataReader query = selectCommand.ExecuteReader();

                if (query.Read())
                {
                    tower = new Tower(id);
                    tower.Name = query.GetString(1);
                    int height = int.Parse(query.GetString(2));
                    tower.Height = height;
                }
                
                db.Close();
            

            return tower;
        }

        /// <summary>
        /// Edit the tower with the same id. Set new name and height.
        /// </summary>
        /// <param name="tower"></param>
        public void EditTower(Tower tower)
        {

            db.Open();

                MySqlCommand sqlCommand = new MySqlCommand();
                sqlCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                sqlCommand.CommandText = "UPDATE tower SET name=@name, height=@height WHERE id=@id";

                sqlCommand.Parameters.AddWithValue("@id", tower.Id);
                sqlCommand.Parameters.AddWithValue("@name", tower.Name);
                sqlCommand.Parameters.AddWithValue("@height", tower.Height);

                sqlCommand.ExecuteReader();

                db.Close();
            
        }

        /// <summary>
        /// Delete tower with the same id.
        /// </summary>
        /// <param name="tower"></param>
        public void DeleteTower(Tower tower)
        {
            db.Open();

                MySqlCommand sqlCommand = new MySqlCommand();
                sqlCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                sqlCommand.CommandText = "DELETE FROM tower WHERE id=@id";

                sqlCommand.Parameters.AddWithValue("@id", tower.Id);

                sqlCommand.ExecuteReader();

                db.Close();
            
        }

        // Add some startup towers.
        private void AddSomeTowers()
        {
            AddTower("Burj Khalifa", 828);
            AddTower("Shanghai Tower", 632);
            AddTower("Abraj Al-Bait Clock Tower", 601);
            AddTower("Ping An Finance Center", 599);
            AddTower("Goldin Finance 117", 596);
            AddTower("Lotte World Tower", 554);
            AddTower("One World Tower", 541);
        }

        // Count rows in table.
        private int NumberOfTowers()
        {
            int count = 0;
            db.Open();

                MySqlCommand sqlCommand = new MySqlCommand();
                sqlCommand.Connection = db;

                sqlCommand.CommandText = "SELECT COUNT(id) FROM tower";
                MySqlDataReader query = sqlCommand.ExecuteReader();
                if (query.Read())
                {
                    count = int.Parse(query.GetString(0));
                }

                db.Close();         

            return count;
        }
    }
}

using System;
using MySqlConnector;

namespace database
{
    public class DBConnections
    {
         public MySqlConnection Connect(string server, string user, string pass, string db)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = server,
                UserID = user,
                Password = pass,
                Database = db,
            };

            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);

            return connection;
        }

        public bool OpenConnection(MySqlConnection connection)
        {
            try
            {
                connection.Open();
                return true;
            }
            catch(MySqlException ex)
            {
                return false;
            }
        }

        public void CloseConnection(MySqlConnection connection)
        {
            connection.Close();
        }
    }
}
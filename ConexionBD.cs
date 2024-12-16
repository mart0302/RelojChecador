using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelojChecadorF
{
    internal class ConexionBD
    {
        private MySqlConnection connection;

        public ConexionBD()
        {
            string connectionString = "Server=127.0.0.1;Database=sistema_relojes;Uid=root;Pwd=";
            connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}

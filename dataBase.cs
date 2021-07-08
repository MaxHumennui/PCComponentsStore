using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCShop
{
    class dataBase
    {
        MySqlConnection connection = new MySqlConnection("Server=192.168.0.152; Port=3306; Database=PCShop; Uid=groot; Pwd=I_Am_Groot2020;");

        public void dbConnect()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void dbDisconnect()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection getConnect()
        {
            return connection;
        }
    }
}

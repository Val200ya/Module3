using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3.Model
{
    public class Database
    {
        private SqlConnection connection = new SqlConnection(
            "Data Source=510EC4;Initial Catalog=hotel_miami_db;Integrated Security=True;");
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection GetSqlConnection()
        {
            return connection;
        }
    }
}

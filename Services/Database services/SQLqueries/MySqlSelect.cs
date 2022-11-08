using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Services.Database_services.SQLqueries
{
    public class MySqlSelect : SqlSelect
    {
        private MySqlConnection DatabaseConnection;

        public MySqlSelect(MySqlConnection databaseConnection)
        {
            this.DatabaseConnection = databaseConnection;
        }

        public List<User> SelectAll()
        {
            return null;
        }

        public User SelectOne(int id)
        {
            User result = null;
            DatabaseConnection.Open();

            MySqlCommand getUserId = new MySqlCommand(
                $"SELECT * FROM users WHERE Id = {id};",
                DatabaseConnection);

            MySqlDataReader dataReader = getUserId.ExecuteReader();

            while (dataReader.Read())
            {
                result = new User();
                result.Id = dataReader.GetInt32(0);
                result.Name = dataReader.GetString(1);
                result.Password = dataReader.GetString(2);
            }

            DatabaseConnection.Close();
            return result;
        }

        public User SelectWithCriteria(UserLoginData UserLoginDataInput)
        {
            User result = null;
            DatabaseConnection.Open();

            MySqlCommand getUserId = new MySqlCommand(
                $"SELECT * FROM users WHERE Name='{UserLoginDataInput.Name}' AND Password='{UserLoginDataInput.Password}';",
                DatabaseConnection);

            MySqlDataReader dataReader = getUserId.ExecuteReader();

            while (dataReader.Read())
            {
                result = new User();
                result.Id = dataReader.GetInt32(0);
                result.Name = dataReader.GetString(1);
                result.Password = dataReader.GetString(2);
            }

            DatabaseConnection.Close();
            return result;
        }
        
    }
}

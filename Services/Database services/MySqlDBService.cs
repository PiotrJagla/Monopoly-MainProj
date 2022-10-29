using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Services.Database_services
{
    public class MySqlDBService : DBservice
    {
        private MySqlConnection databaseConnection;

        public MySqlDBService()
        {
            connectToDatabase();
        }

        private void connectToDatabase()
        {
            databaseConnection = new MySqlConnection(Constants.MySQLConnectionString);
        }

        public User GetUserData(int ID)
        {
            throw new NotImplementedException();
        }

        public bool InsertUser(UserLoginData userData)
        {
            throw new NotImplementedException();
        }

        public bool UserExist(UserLoginData userLoginData)
        {
            int userID = -1;
            databaseConnection.Open();

            MySqlCommand getUserId = new MySqlCommand(
                "SELECT users.ID FROM users " +
                $"WHERE users.Name='{userLoginData.Name}' AND users.Password='{userLoginData.Password}';",
                databaseConnection);

            MySqlDataReader dataReader = getUserId.ExecuteReader();
            
            while(dataReader.Read())
            {
                userID = dataReader.GetInt32(0);
            }

            databaseConnection.Close();
            return userID != -1;
        }
    }
}

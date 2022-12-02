using Models.UsersManagment;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Database_services.SQLqueries
{
    public class MySqlInsert : SqlInsert
    {
        private MySqlConnection DatabaseConnection;

        public MySqlInsert(MySqlConnection databaseConnection)
        {
            this.DatabaseConnection = databaseConnection;
        }

        public bool InsertUser(UserLoginData UserDataToRegister)
        {
            bool IsDataInserted = false;
            DatabaseConnection.Open();

            MySqlCommand InsertUserQuery = new MySqlCommand(
                "INSERT INTO users(Name,Password) " +
                $"VALUES('{UserDataToRegister.Name}','{UserDataToRegister.Password}');",
                DatabaseConnection
            );

            try
            {
                InsertUserQuery.ExecuteNonQuery();
                IsDataInserted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            DatabaseConnection.Close();
            return IsDataInserted;
        }
    }
}

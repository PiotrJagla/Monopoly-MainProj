using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Services.Database_services.SQLqueries;
using Models.UsersManagment;

namespace Services.Database_services
{
    public class MySqlDBService : DBservice
    {
        private MySqlConnection DatabaseConnection;
        private SqlSelect SelectQuery;
        private SqlInsert InsertQuery;

        public MySqlDBService()
        {
            ConnectToDatabase();
            InitSqlSelect();
            InitSqlInsert();
        }

        private void ConnectToDatabase()
        {
            DatabaseConnection = new MySqlConnection(Constants.MySQLConnectionString);
        }

        private void InitSqlSelect()
        {
            SelectQuery = new MySqlSelect(DatabaseConnection);
        }

        private void InitSqlInsert()
        {
            InsertQuery = new MySqlInsert(DatabaseConnection);
        }

        public bool InsertUser(UserLoginData UserDataToRegister)
        {
            return InsertQuery.InsertUser(UserDataToRegister);
        }

        public bool UserExist(UserLoginData userLoginData)
        {
            return SelectQuery.SelectWithCriteria(userLoginData) != null;
        }
    }
}

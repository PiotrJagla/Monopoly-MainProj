using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Database_services
{
    public interface DBservice
    {
        User GetUserData(int ID);

        bool UserExist(UserLoginData userLoginData);

        bool InsertUser(UserLoginData UserDataToRegister);
    }
}

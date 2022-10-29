using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Database_services.SQLqueries
{
    public interface SqlInsert
    {
        bool InsertUser(UserLoginData UserDataToRegister);
    }
}

using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Database_services.SQLqueries
{
    public interface SqlSelect
    {
        List<User> SelectAll();
        User SelectOne(int id);
        User SelectWithCriteria(UserLoginData UserLoginDataInput);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.API_services
{
    public interface IAPIservice
    {
        Task<string> GetMessage();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsola_NetFramework_Ternium.Interfaces
{
    public interface IConfigManager
    {
        string GetConnectionString();
        string GetLogFilePath();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsola_NetFramework_Ternium.Interfaces
{
    public interface ILogManager
    {
        void CreateProcessTitle();
        void LogError(string message);
        void WriteSectionTitle(string title);
        string GetLogFilePath();
    }
}

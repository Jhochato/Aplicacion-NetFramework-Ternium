using AppConsola_NetFramework_Ternium.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsola_NetFramework_Ternium.Implementaciones
{
    public class LogManager : ILogManager
    {
        private readonly string _logFilePath;

        public LogManager(string logFilePath)
        {
            _logFilePath = logFilePath;
            CreateProcessTitle();
        }

        public void CreateProcessTitle()
        {
            try
            {
                File.AppendAllText(_logFilePath, Environment.NewLine + $"--- NUEVO PROCESO: {DateTime.Now} ---" + Environment.NewLine);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al escribir en el archivo de log: {ex.Message}");
            }
        }

        public void LogError(string message)
        {
            try
            {
                File.AppendAllText(_logFilePath, $"ERROR: {DateTime.Now}: {message}" + Environment.NewLine);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al escribir en el archivo de log: {ex.Message}");
            }
        }

        public void WriteSectionTitle(string title)
        {
            try
            {
                File.AppendAllText(_logFilePath, $"{Environment.NewLine}--- {title} ---{Environment.NewLine}");
            }
            catch (IOException ex)
            {
                LogError($"Error al escribir en el archivo de log: {ex.Message}");
            }
        }

        public string GetLogFilePath() => _logFilePath;
    }
}

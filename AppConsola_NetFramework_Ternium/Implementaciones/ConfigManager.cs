using AppConsola_NetFramework_Ternium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppConsola_NetFramework_Ternium.Implementaciones
{
    public class ConfigManager : IConfigManager
    {
        private readonly string _configPath;
        private readonly XDocument _config;

        public ConfigManager(string configPath)
        {
            _configPath = configPath;
            _config = XDocument.Load(configPath);
        }

        public string GetConnectionString()
        {
            var connectionStringElement = _config.Root.Element("connectionStrings")?.Element("add");
            if (connectionStringElement == null)
                throw new InvalidOperationException("No se encontró el elemento 'connectionStrings' en el archivo XML.");

            var connectionString = connectionStringElement.Attribute("connectionString")?.Value;
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("La cadena de conexión no está configurada en el archivo XML.");

            return connectionString;
        }

        public string GetLogFilePath()
        {
            var logFilePathElement = _config.Root.Element("appSettings")?.Element("add");
            if (logFilePathElement == null)
                throw new InvalidOperationException("No se encontró el elemento 'appSettings' en el archivo XML.");

            var logFilePath = logFilePathElement.Attribute("value")?.Value;
            if (string.IsNullOrEmpty(logFilePath))
                throw new InvalidOperationException("La ruta del archivo de log no está configurada en el archivo XML.");

            return logFilePath;
        }
    }
}

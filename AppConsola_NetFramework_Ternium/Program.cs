using AppConsola_NetFramework_Ternium.Implementaciones;
using AppConsola_NetFramework_Ternium.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsola_NetFramework_Ternium
{
    class Program
    {
        static void Main()
        {
            IConfigManager configManager = new ConfigManager("config.xml");
            string connectionString = configManager.GetConnectionString();
            string logFilePath = configManager.GetLogFilePath();

            ILogManager logManager = new LogManager(logFilePath);
            IUserInput userInput = new UserInput();

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    var nombre = userInput.GetInput("Ingrese el Nombre del Usuario:");
                    var apellido = userInput.GetInput("Ingrese el Apellido del Usuario:");
                    var edad = userInput.GetIntInput("Ingrese la Edad del Usuario:", logManager);
                    var correo = userInput.GetInput("Ingrese el Correo del Usuario:");
                    var hobbies = userInput.GetInput("Ingrese los Hobbies del Usuario (separados por guiones):");
                    var activo = userInput.GetBoolInput("¿Está el Usuario Activo? (S/N):");
                    var creadoPor = userInput.GetInput("Nombre de quien crea el Usuario:");

                    IUsuarioRepository usuarioRepo = new UsuarioRepository(conn, logManager);

                    try
                    {
                        usuarioRepo.InsertarUsuario(nombre, apellido, edad, correo, hobbies, activo, creadoPor);
                    }
                    catch (Exception ex)
                    {
                        logManager.LogError($"Error al ejecutar el procedimiento InsertarUsuario: {ex.Message}");
                    }

                    var edadParaConsulta = userInput.GetIntInput("Ingrese la Edad para obtener usuarios:", logManager);
                    try
                    {
                        usuarioRepo.ObtenerUsuariosPorEdad(edadParaConsulta);
                    }
                    catch (Exception ex)
                    {
                        logManager.LogError($"Error al ejecutar el procedimiento ObtenerUsuariosPorEdad: {ex.Message}");
                    }

                    try
                    {
                        usuarioRepo.ObtUsuarioCreadosUltimas2Horas();
                    }
                    catch (Exception ex)
                    {
                        logManager.LogError($"Error al ejecutar el procedimiento ObtUsuarioCreadosUltimas2Horas: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                logManager.LogError($"Error en el proceso principal: {ex.Message}");
            }
        }
    }
}

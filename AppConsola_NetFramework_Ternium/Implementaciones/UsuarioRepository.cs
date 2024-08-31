using AppConsola_NetFramework_Ternium.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsola_NetFramework_Ternium.Implementaciones
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SqlConnection _conn;
        private readonly ILogManager _logManager;

        public UsuarioRepository(SqlConnection conn, ILogManager logManager)
        {
            _conn = conn;
            _logManager = logManager;
        }

        public void InsertarUsuario(string nombre, string apellido, int edad, string correo, string hobbies, bool activo, string creadoPor)
        {
            using (var cmd = new SqlCommand("InsertarUsuario", _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                cmd.Parameters.AddWithValue("@Edad", edad);
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Hobbies", hobbies);
                cmd.Parameters.AddWithValue("@Activo", activo);
                cmd.Parameters.AddWithValue("@UsuarioCreacion", creadoPor);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    _logManager.LogError($"Error al ejecutar el procedimiento InsertarUsuario: {ex.Message}");
                    throw;
                }
            }
        }

        public void ObtenerUsuariosPorEdad(int edad)
        {
            _logManager.WriteSectionTitle("Resultados de usuarios de acuerdo a la edad ingresada");

            using (var cmd = new SqlCommand("ObtenerUsuariosPorEdad", _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Edad", edad);

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string logEntry = $"{DateTime.Now}: {string.Concat(reader.GetValue(1), "|", reader.GetValue(2), "|", reader.GetValue(3), "|", reader.GetValue(4), "|", reader.GetValue(5))}";
                            File.AppendAllText(_logManager.GetLogFilePath(), logEntry + Environment.NewLine);
                        }
                    }
                }
                catch (IOException ex)
                {
                    _logManager.LogError($"Error al escribir en el archivo de log: {ex.Message}");
                }
            }
        }

        public void ObtUsuarioCreadosUltimas2Horas()
        {
            _logManager.WriteSectionTitle("Usuarios Creados en las Ultimas 2 Horas");

            using (var cmd = new SqlCommand("ObtUsuarioCreadosUltimas2Horas", _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string logEntry = $"{DateTime.Now}: {string.Concat(reader.GetValue(1), "|", reader.GetValue(2), "|", reader.GetValue(3), "|", reader.GetValue(4), "|", reader.GetValue(5))}";
                            File.AppendAllText(_logManager.GetLogFilePath(), logEntry + Environment.NewLine);
                        }
                    }
                }
                catch (IOException ex)
                {
                    _logManager.LogError($"Error al escribir en el archivo de log: {ex.Message}");
                }
            }
        }
    }
}

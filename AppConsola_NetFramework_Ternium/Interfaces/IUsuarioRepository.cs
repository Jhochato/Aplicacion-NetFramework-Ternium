using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsola_NetFramework_Ternium.Interfaces
{
    public interface IUsuarioRepository
    {
        void InsertarUsuario(string nombre, string apellido, int edad, string correo, string hobbies, bool activo, string creadoPor);
        void ObtenerUsuariosPorEdad(int edad);
        void ObtUsuarioCreadosUltimas2Horas();
    }
}

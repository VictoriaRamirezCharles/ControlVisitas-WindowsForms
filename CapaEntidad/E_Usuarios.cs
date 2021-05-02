using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class E_Usuarios
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public bool TipoUsuario { get; set; }

        public E_Usuarios(string nombre, string apelligo, DateTime fecha_nacimiento, bool tipousuario)
        {
            this.Nombre = nombre;
            this.Apellido = apelligo;
            this.Fecha_Nacimiento = fecha_nacimiento;
            this.TipoUsuario = tipousuario;
        }
        public E_Usuarios()
        {

        }
    }
}

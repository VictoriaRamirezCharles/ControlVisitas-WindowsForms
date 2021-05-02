using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class E_Carreras
    {
        public int IdCarrera { get; set; }
        public string Nombre { get; set; }

        public E_Carreras()
        {

        }

        public E_Carreras(string nombre)
        {
            this.Nombre = nombre;
        }
    }
}

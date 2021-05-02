using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class E_Lugar_Destino
    {
        public int IdLugar_Destino { get; set; }
        public string Nombre { get; set; }

        public E_Lugar_Destino()
        {

        }

        public E_Lugar_Destino(string nombre)
        {
            this.Nombre = nombre;
        }
    }
}

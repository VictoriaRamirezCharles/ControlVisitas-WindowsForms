using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class E_Aula
    {
        public int IdAula { get; set; }
        public string Nombre { get; set; }
        public int Id_Edificio { get; set; }
        public string Edificio { get; set; }

        public E_Aula()
        {

        }

        public E_Aula(string nombre, int id_edicio)
        {
            this.Nombre = nombre;
            this.Id_Edificio = id_edicio;
        }
    }
}

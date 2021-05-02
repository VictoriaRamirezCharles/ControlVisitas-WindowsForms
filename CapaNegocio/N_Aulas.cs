using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class N_Aulas
    {
        D_Aulas objDato = new D_Aulas();

        public List<E_Aula> ListarAulas(int Id_Edicio)
        {
            return objDato.ListarAulas(Id_Edicio);
        }

        public List<E_Aula> GetAulas(string buscar)
        {
            return objDato.GetAulas(buscar);
        }
        public void InsertarAula(E_Aula Aula)
        {
            objDato.InsertarAula(Aula);
        }
        public void EditarAula(E_Aula Aula)
        {
            objDato.EditarAula(Aula);
        }
        public void EliminarAula(int id)
        {
            objDato.EliminarAula(id);
        }
    }
}

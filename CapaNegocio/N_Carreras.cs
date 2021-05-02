using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class N_Carreras
    {
        D_Carreras objDato = new D_Carreras();

        public List<E_Carreras> ListarCarreras(string buscar)
        {
            return objDato.ListarCarreras(buscar);
        }
        public void InsertarCarrera(E_Carreras Carrera)
        {
            objDato.InsertarCarrera(Carrera);
        }
        public void EditarCarrera(E_Carreras Carrera)
        {
            objDato.EditarCarrera(Carrera);
        }
        public void EliminarCarrera(int id)
        {
            objDato.EliminarCarrera(id);
        }
    }
}

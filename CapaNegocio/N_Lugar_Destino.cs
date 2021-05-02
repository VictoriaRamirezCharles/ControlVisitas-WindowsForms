using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class N_Lugar_Destino
    {
        D_Lugar_Destino objDato = new D_Lugar_Destino();

        public List<E_Lugar_Destino> ListarLugarDestino(string buscar)
        {
            return objDato.ListarLugarDestino(buscar);
        }
        public void InsertarLugarDestino(E_Lugar_Destino Lugar)
        {
            objDato.InsertarLugarDestino(Lugar);
        }
        public void EditarLugarDestino(E_Lugar_Destino Lugar)
        {
            objDato.EditarLugarDestino(Lugar);
        }
        public void EliminarLugarDestino(int id)
        {
            objDato.EliminarLugarDestino(id);
        }
    }
}

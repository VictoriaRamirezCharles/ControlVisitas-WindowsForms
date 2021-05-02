using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class N_Usuarios
    {
        D_Usuarios objDato = new D_Usuarios();

        public List<E_Usuarios> ListarUsuarios(string buscar)
        {
            return objDato.ListarUsuarios(buscar);
        }
        public E_Usuarios Login(string buscar)
        {
            return objDato.Login(buscar);
        }
        public void InsertarUsuario(E_Usuarios Usuario)
        {
            objDato.InsertarUsuario(Usuario);
        }

        public void EditarUsuario(E_Usuarios Usuario)
        {
            objDato.EditarUsuario(Usuario);
        }
        public void EliminarUsuario(int id)
        {
            objDato.EliminarUsuario(id);
        }
    }
}

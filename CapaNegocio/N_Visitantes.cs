using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class N_Visitantes
    {
        D_Visitantes objDato = new D_Visitantes();

        public List<E_Visitantes> ListarVisitantes(string buscar)
        {
            return objDato.ListarVisitantes(buscar);
        }
        public List<E_Visitantes> GetEditList()
        {
            return objDato.GetEditList();
        }
        public List<E_Visitantes> GetAll()
        {
            return objDato.GetAll();
        }


        public void InsertarVisitante(E_Visitantes Visitante)
        {
            objDato.InsertarVisitante(Visitante);
        }

        public void EditarVisitante(E_Visitantes Visitante)
        {
            objDato.EditarVisitante(Visitante);
        }
        public void EliminarVisitante(int id)
        {
            objDato.EliminarVisitante(id);
        }

        public string GetPhotoPath(int id_visitante)
        {
            return objDato.GetPhotoPath(id_visitante);
        }

        public int GetLastId()
        {
            return objDato.GetLastId();
        }

        public bool SavePhoto(int id, string destination)
        {
            return objDato.SavePhoto(id,destination);
        }
        public bool darSalida(int id, bool status)
        {
            return objDato.darSalida(id,status);
        }
    }
}

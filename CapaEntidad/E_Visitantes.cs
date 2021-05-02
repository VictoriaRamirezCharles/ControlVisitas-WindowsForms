using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class E_Visitantes
    {
        public int IdVisitante { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Carrera_Id { get; set; }
        public string CarreraNombre { get; set; }
        public string Correo { get; set; }
        public string Motivo_Visita { get; set; }
        public string Foto { get; set; }
        public int Lugar_Destino_Id { get; set; }
        public string Edificio { get; set; }
        public int Aula_Id { get; set; }
        public string AulaNombre { get; set; }
        public string Hora_Entrada { get; set; }
        public string Hora_Salida { get; set; }
        public bool Status { get; set; }

        public E_Visitantes()
        {

        }
        public E_Visitantes(string nombre, string apellido, int carrera_id,string correo, string motivo_visita, string foto, int lugar_destino_id, int aula_id, string hora_entrada, string hora_salida, bool status)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Carrera_Id = carrera_id;
            this.Correo = correo;
            this.Motivo_Visita = motivo_visita;
            this.Foto = foto;
            this.Lugar_Destino_Id = lugar_destino_id;
            this.Hora_Entrada = hora_entrada;
            this.Hora_Salida = hora_salida;
            this.Aula_Id = aula_id;
            this.Status = status;
        }
    }
}

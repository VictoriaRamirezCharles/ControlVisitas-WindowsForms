using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class D_Aulas
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        public List<E_Aula> ListarAulas(int Id_Edificio)
        {
            SqlDataReader leerFilas;
            SqlCommand cmd = new SqlCommand("SP_BUSCARAULAS", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@ID_EDIFICIO ", Id_Edificio);
            leerFilas = cmd.ExecuteReader();
            List<E_Aula> Listar = new List<E_Aula>();
            while (leerFilas.Read())
            {
                Listar.Add(new E_Aula
                {
                    IdAula = leerFilas.GetInt32(0),
                    Nombre = leerFilas.GetString(1),
                    Id_Edificio = leerFilas.GetInt32(2)
                });
            }
            conexion.Close();
            leerFilas.Close();
            return Listar;
        }

        public List<E_Aula> GetAulas(string buscar)
        {
            List<E_Aula> Listar = new List<E_Aula>();

            conexion.Open();

            SqlCommand command = new SqlCommand($"select a.IDAULA,a.NOMBRE,a.ID_EDIFICIO,d.NOMBRE as Edificio from AULAS a inner join LUGAR_DESTINO d on a.ID_EDIFICIO = d.IDLUGAR_DESTINO where a.nombre LIKE '{buscar}%'", conexion);

            using (SqlDataReader reader = command.ExecuteReader())
            {


                while (reader.Read())
                {
                    Listar.Add(new E_Aula
                    {
                        IdAula = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Id_Edificio = reader.GetInt32(2),
                        Edificio = reader.GetString(3)

                    });
                }

            }

            conexion.Close();

            return Listar;

        }

        public void InsertarAula(E_Aula Carrera)
        {

            SqlCommand cmd = new SqlCommand("SP_INSERTARAULA", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@NOMBRE", Carrera.Nombre);
            cmd.Parameters.AddWithValue("@ID_EDIFICIO", Carrera.Id_Edificio);

            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public void EditarAula(E_Aula Aula)
        {

            SqlCommand cmd = new SqlCommand("SP_EDITARAULAS", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@IDAULA", Aula.IdAula);
            cmd.Parameters.AddWithValue("@NOMBRE", Aula.Nombre);
            cmd.Parameters.AddWithValue("@ID_EDIFICIO", Aula.Id_Edificio);

            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public void EliminarAula(int id)
        {

            SqlCommand cmd = new SqlCommand("SP_ELIMINARAULA", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@IDAULA", id);


            cmd.ExecuteNonQuery();
            conexion.Close();

        }
    }
}

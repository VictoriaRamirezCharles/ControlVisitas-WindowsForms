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
    public class D_Carreras
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        public List<E_Carreras> ListarCarreras(string buscar)
        {
            SqlDataReader leerFilas;
            SqlCommand cmd = new SqlCommand("SP_BUSCARCARRERAS", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@BUSCAR", buscar);
            leerFilas = cmd.ExecuteReader();
            List<E_Carreras> Listar = new List<E_Carreras>();
            while (leerFilas.Read())
            {
                Listar.Add(new E_Carreras
                {
                    IdCarrera = leerFilas.GetInt32(0),
                    Nombre = leerFilas.GetString(1)
                });
            }
            conexion.Close();
            leerFilas.Close();
            return Listar;
        }

        public void InsertarCarrera(E_Carreras Carrera)
        {

            SqlCommand cmd = new SqlCommand("SP_INSERTARCARRERA", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@NOMBRE", Carrera.Nombre);

            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public void EditarCarrera(E_Carreras Carrera)
        {

            SqlCommand cmd = new SqlCommand("SP_EDITARCARRERA", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@IDCARRERA", Carrera.IdCarrera);
            cmd.Parameters.AddWithValue("@NOMBRE", Carrera.Nombre);

            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public void EliminarCarrera(int id)
        {

            SqlCommand cmd = new SqlCommand("SP_ELIMINARCARRERA", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@IDCARRERA", id);


            cmd.ExecuteNonQuery();
            conexion.Close();

        }
    }
}

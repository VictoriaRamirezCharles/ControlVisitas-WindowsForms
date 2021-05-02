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
    public class D_Lugar_Destino
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        public List<E_Lugar_Destino> ListarLugarDestino(string buscar)
        {
            SqlDataReader leerFilas;
            SqlCommand cmd = new SqlCommand("SP_BUSCARLUGAR", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@BUSCAR", buscar);
            leerFilas = cmd.ExecuteReader();
            List<E_Lugar_Destino> Listar = new List<E_Lugar_Destino>();
            while (leerFilas.Read())
            {
                Listar.Add(new E_Lugar_Destino
                {
                    IdLugar_Destino = leerFilas.GetInt32(0),
                    Nombre = leerFilas.GetString(1)
                });
            }
            conexion.Close();
            leerFilas.Close();
            return Listar;
        }

        public void InsertarLugarDestino(E_Lugar_Destino Lugar)
        {

            SqlCommand cmd = new SqlCommand("SP_INSERTARLUGAR", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@NOMBRE", Lugar.Nombre);
   

            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public void EditarLugarDestino(E_Lugar_Destino Lugar)
        {

            SqlCommand cmd = new SqlCommand("SP_EDITARLUGAR", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@IDLUGAR_DESTINO", Lugar.IdLugar_Destino);
            cmd.Parameters.AddWithValue("@NOMBRE", Lugar.Nombre);


            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public void EliminarLugarDestino(int id)
        {

            SqlCommand cmd = new SqlCommand("SP_ELIMINARLUGAR", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@IDLUGAR_DESTINO", id);


            cmd.ExecuteNonQuery();
            conexion.Close();

        }
    }
}


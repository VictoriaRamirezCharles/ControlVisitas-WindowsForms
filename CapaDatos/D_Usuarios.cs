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
    public class D_Usuarios
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        public List<E_Usuarios> ListarUsuarios(string buscar)
        {
            SqlDataReader leerFilas;
            SqlCommand cmd = new SqlCommand("SP_BUSCARUSUARIOS", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@BUSCAR", buscar);
            leerFilas = cmd.ExecuteReader();
            List<E_Usuarios> Listar = new List<E_Usuarios>();
            while (leerFilas.Read())
            {
                Listar.Add(new E_Usuarios
                {
                    IdUsuario = leerFilas.GetInt32(0),
                    Usuario = leerFilas.GetString(1),
                    Nombre = leerFilas.GetString(2),
                    Apellido = leerFilas.GetString(3),
                    Fecha_Nacimiento = leerFilas.GetDateTime(4),
                    TipoUsuario = leerFilas.GetBoolean(5)
                });
            }
            conexion.Close();
            leerFilas.Close();
            return Listar;
        }

        public void InsertarUsuario(E_Usuarios Usuario)
        {

            SqlCommand cmd = new SqlCommand("SP_INSERTARUSUARIO", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@NOMBRE", Usuario.Nombre);
            cmd.Parameters.AddWithValue("@APELLIDO", Usuario.Apellido);
            cmd.Parameters.AddWithValue("@FECHA_NACIMIENTO", Usuario.Fecha_Nacimiento);
            cmd.Parameters.AddWithValue("@TIPOUSUARIO", Usuario.TipoUsuario);
            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public void EditarUsuario(E_Usuarios Usuario)
        {

            SqlCommand cmd = new SqlCommand("SP_EDITARUSUARIO", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@IDUSUARIO", Usuario.IdUsuario);
            cmd.Parameters.AddWithValue("@NOMBRE", Usuario.Nombre);
            cmd.Parameters.AddWithValue("@APELLIDO", Usuario.Apellido);
            cmd.Parameters.AddWithValue("@FECHA_NACIMIENTO", Usuario.Fecha_Nacimiento);
            cmd.Parameters.AddWithValue("@TIPOUSUARIO", Usuario.TipoUsuario);

            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public void EliminarUsuario(int id)
        {

            SqlCommand cmd = new SqlCommand("SP_ELIMINARUSUARIO", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@IDUSUARIO", id);


            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public E_Usuarios Login(string nombre)
        {
            SqlDataReader leerFilas;
            SqlCommand cmd = new SqlCommand("SP_BUSCARUSUARIOS", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@BUSCAR", nombre);
            leerFilas = cmd.ExecuteReader();
            List<E_Usuarios> Listar = new List<E_Usuarios>();
            while (leerFilas.Read())
            {
                Listar.Add(new E_Usuarios
                {
                    IdUsuario = leerFilas.GetInt32(0),
                    Usuario = leerFilas.GetString(1),
                    Nombre = leerFilas.GetString(2),
                    Apellido = leerFilas.GetString(3),
                    Fecha_Nacimiento = leerFilas.GetDateTime(4),
                    TipoUsuario = leerFilas.GetBoolean(5)
                });
            }
            conexion.Close();
            leerFilas.Close();
            return Listar.FirstOrDefault();
        }
    }
}

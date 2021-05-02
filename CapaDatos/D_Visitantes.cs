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
    public class D_Visitantes
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        public List<E_Visitantes> ListarVisitantes(string buscar)
        {
            SqlDataReader leerFilas;
            SqlCommand cmd = new SqlCommand("SP_BUSCARVISITANTES", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@BUSCAR", buscar);
            leerFilas = cmd.ExecuteReader();
            List<E_Visitantes> Listar = new List<E_Visitantes>();
            while (leerFilas.Read())
            {
                Listar.Add(new E_Visitantes
                {
                    IdVisitante = leerFilas.GetInt32(0),
                    Codigo = leerFilas.GetString(1),
                    Nombre = leerFilas.GetString(2),
                    Apellido = leerFilas.GetString(3),
                    Carrera_Id = leerFilas.GetInt32(4),
                    Correo = leerFilas.GetString(5),
                    Motivo_Visita = leerFilas.GetString(6),
                    Foto = leerFilas.GetString(7),
                    Lugar_Destino_Id = leerFilas.GetInt32(8),
                    Aula_Id = leerFilas.GetInt32(9),
                    Hora_Entrada = leerFilas.GetString(10),
                    Hora_Salida = leerFilas.GetString(11),
                    Status = leerFilas.GetBoolean(12),

                });
            }
            conexion.Close();
            leerFilas.Close();
            return Listar;
        }

        public void InsertarVisitante(E_Visitantes Visitante)
        {

            SqlCommand cmd = new SqlCommand("SP_INSERTARVISITANTE", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@NOMBRE", Visitante.Nombre);
            cmd.Parameters.AddWithValue("@APELLIDO", Visitante.Apellido);
            cmd.Parameters.AddWithValue("@CARRERA_ID", Visitante.Carrera_Id);
            cmd.Parameters.AddWithValue("@CORREO", Visitante.Correo);
            cmd.Parameters.AddWithValue("@MOTIVO_VISITA", Visitante.Motivo_Visita);
            cmd.Parameters.AddWithValue("@FOTO", Visitante.Foto);
            cmd.Parameters.AddWithValue("@LUGAR_DESTINO_ID", Visitante.Lugar_Destino_Id);
            cmd.Parameters.AddWithValue("@AULA_ID", Visitante.Aula_Id);
            cmd.Parameters.AddWithValue("@HORA_ENTRADA", Visitante.Hora_Entrada);
            cmd.Parameters.AddWithValue("@HORA_SALIDA", Visitante.Hora_Salida);
            cmd.Parameters.AddWithValue("@STATUS", Visitante.Status);
            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public void EditarVisitante(E_Visitantes Visitante)
        {

            SqlCommand cmd = new SqlCommand("SP_EDITARVISITANTE", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@IDVISITANTE", Visitante.IdVisitante);
            cmd.Parameters.AddWithValue("@NOMBRE", Visitante.Nombre);
            cmd.Parameters.AddWithValue("@APELLIDO", Visitante.Apellido);
            cmd.Parameters.AddWithValue("@CARRERA_ID", Visitante.Carrera_Id);
            cmd.Parameters.AddWithValue("@CORREO", Visitante.Correo);
            cmd.Parameters.AddWithValue("@MOTIVO_VISITA", Visitante.Motivo_Visita);
            cmd.Parameters.AddWithValue("@FOTO", Visitante.Foto);
            cmd.Parameters.AddWithValue("@LUGAR_DESTINO_ID", Visitante.Lugar_Destino_Id);
            cmd.Parameters.AddWithValue("@AULA_ID", Visitante.Aula_Id);
            cmd.Parameters.AddWithValue("@HORA_ENTRADA", Visitante.Hora_Entrada);
            cmd.Parameters.AddWithValue("@HORA_SALIDA", Visitante.Hora_Salida);
            cmd.Parameters.AddWithValue("@STATUS", Visitante.Status);

            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public void EliminarVisitante(int id)
        {

            SqlCommand cmd = new SqlCommand("SP_ELIMINARVISITANTE", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@IDVISITANTE", id);


            cmd.ExecuteNonQuery();
            conexion.Close();

        }
        public int GetLastId()
        {
            int lastId = 0;
            conexion.Open();

            using (SqlCommand command = new SqlCommand("select max(IDVISITANTE) as Id from VISITANTES", conexion))

            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    lastId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                }

            }
            conexion.Close();
            return lastId;
        }

        public string GetPhotoPath(int id)
        {
            string path = "";

            conexion.Open();

            SqlCommand command = new SqlCommand("select FOTO from VISITANTES where IDVISITANTE=@IDVISITANTE", conexion);
            command.Parameters.AddWithValue("@IDVISITANTE", id);


            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    path = reader.IsDBNull(0) ? "" : reader.GetString(0);
                }

            }

           conexion.Close();

            return path;

        }

        public bool SavePhoto(int id, string destination)
        {
            SqlCommand command = new SqlCommand("update VISITANTES set Foto=@foto where IDVISITANTE = @id",
             conexion);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@foto", destination);

            return ExecuteDml(command);
        }
        public bool darSalida(int id, bool status)
        {
            SqlCommand command = new SqlCommand("update VISITANTES set Status=@status where IDVISITANTE = @id",
             conexion);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@status", status);

            return ExecuteDml(command);
        }
        public bool ExecuteDml(SqlCommand query)
        {
            try
            {
                conexion.Open();

                query.ExecuteNonQuery();

                conexion.Close();


                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;


            }
        }

        public List<E_Visitantes> GetAll()
        {
            List<E_Visitantes> Listar = new List<E_Visitantes>();

            conexion.Open();

            SqlCommand command = new SqlCommand("select v.IDVISITANTE, v.Codigo, v.NOMBRE,v.APELLIDO,c.NOMBRE as Carrera,v.CORREO,v.MOTIVO_VISITA,v.FOTO,d.NOMBRE as Edificio, a.NOMBRE as Aula,v.HORA_ENTRADA, v.HORA_SALIDA  from VISITANTES v inner join CARRERAS c on v.CARRERA_ID = c.IDCARRERA inner join LUGAR_DESTINO d on v.LUGAR_DESTINO_ID = d.IDLUGAR_DESTINO inner join Aulas a on v.AULA_ID = a.IDAULA where v.Status = 1", conexion);

            using (SqlDataReader reader = command.ExecuteReader())
            {

               
                while (reader.Read())
                {
                    Listar.Add(new E_Visitantes
                    {
                        IdVisitante = reader.GetInt32(0),
                        Codigo = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        Apellido = reader.GetString(3),
                        CarreraNombre = reader.GetString(4),
                        Correo = reader.GetString(5),
                        Motivo_Visita = reader.GetString(6),
                        Foto = reader.GetString(7),
                        Edificio = reader.GetString(8),
                        AulaNombre = reader.GetString(9),
                        Hora_Entrada = reader.GetString(10),
                        Hora_Salida = reader.GetString(11)

                    });
                }

            }

            conexion.Close();

            return Listar;

        }

        public List<E_Visitantes> GetEditList()
        {
            List<E_Visitantes> Listar = new List<E_Visitantes>();

            conexion.Open();

            SqlCommand command = new SqlCommand("select v.IDVISITANTE, v.Codigo, v.NOMBRE,v.APELLIDO,c.NOMBRE as Carrera,v.CORREO,v.MOTIVO_VISITA,v.FOTO,d.NOMBRE as Edificio, a.NOMBRE as Aula,v.HORA_ENTRADA, v.HORA_SALIDA  from VISITANTES v inner join CARRERAS c on v.CARRERA_ID = c.IDCARRERA inner join LUGAR_DESTINO d on v.LUGAR_DESTINO_ID = d.IDLUGAR_DESTINO inner join Aulas a on v.AULA_ID = a.IDAULA ", conexion);

            using (SqlDataReader reader = command.ExecuteReader())
            {


                while (reader.Read())
                {
                    Listar.Add(new E_Visitantes
                    {
                        IdVisitante = reader.GetInt32(0),
                        Codigo = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        Apellido = reader.GetString(3),
                        CarreraNombre = reader.GetString(4),
                        Correo = reader.GetString(5),
                        Motivo_Visita = reader.GetString(6),
                        Foto = reader.GetString(7),
                        Edificio = reader.GetString(8),
                        AulaNombre = reader.GetString(9),
                        Hora_Entrada = reader.GetString(10),
                        Hora_Salida = reader.GetString(11)

                    });
                }

            }

            conexion.Close();

            return Listar;

        }
    }
}

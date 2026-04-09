using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Proyecto.Conexion_Base_Datos;
using Proyecto.Modelos;

namespace Proyecto.Clases
{
    public class TareaRepository
    {
        // GET ALL con búsqueda opcional
        public List<TareasModel> GetAll(string searchTerm = "")
        {
            List<TareasModel> tareas = new List<TareasModel>();

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"SELECT id_tarea, id_proyecto, ID, titulo, descripcion, 
                                               prioridad, estado, fehca_Inicio, fechaVencimiento, horaEstimadas 
                                       FROM Tarea 
                                       WHERE (@searchTerm = '' 
                                          OR titulo LIKE @searchTermLike 
                                          OR descripcion LIKE @searchTermLike
                                          OR prioridad LIKE @searchTermLike
                                          OR estado LIKE @searchTermLike
                                          OR CAST(id_tarea AS VARCHAR) LIKE @searchTermLike)";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@searchTerm", searchTerm);
                    comando.Parameters.AddWithValue("@searchTermLike", "%" + searchTerm + "%");

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tareas.Add(new TareasModel
                            {
                                id_tarea = reader.GetInt32(0),
                                id_proyecto = reader.GetInt32(1),
                                ID = reader.GetInt32(2),
                                titulo = reader.GetString(3),
                                descripcion = reader.GetString(4),
                                prioridad = reader.GetString(5),
                                estado = reader.GetString(6),
                                fehca_Inicio = reader.GetDateTime(7),
                                fechaVencimiento = reader.GetDateTime(8),
                                horaEstimadas = reader.GetInt32(9)
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return tareas;
        }

        // GET ALL sin parámetro
        public List<TareasModel> GetAll()
        {
            return GetAll("");
        }

        // FIND por id_tarea
        public TareasModel Find(int id_tarea)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"SELECT id_tarea, id_proyecto, ID, titulo, descripcion, 
                                               prioridad, estado, fehca_Inicio, fechaVencimiento, horaEstimadas 
                                       FROM Tarea WHERE id_tarea = @id_tarea";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_tarea", id_tarea);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TareasModel
                            {
                                id_tarea = reader.GetInt32(0),
                                id_proyecto = reader.GetInt32(1),
                                ID = reader.GetInt32(2),
                                titulo = reader.GetString(3),
                                descripcion = reader.GetString(4),
                                prioridad = reader.GetString(5),
                                estado = reader.GetString(6),
                                fehca_Inicio = reader.GetDateTime(7),
                                fechaVencimiento = reader.GetDateTime(8),
                                horaEstimadas = reader.GetInt32(9)
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        // INSERT - Nueva tarea
        public bool Insert(TareasModel tarea)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"INSERT INTO Tarea (id_proyecto, ID, titulo, descripcion, 
                                                           prioridad, estado, fehca_Inicio, fechaVencimiento, horaEstimadas) 
                                       VALUES (@id_proyecto, @ID, @titulo, @descripcion, 
                                               @prioridad, @estado, @fehca_Inicio, @fechaVencimiento, @horaEstimadas)";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_proyecto", tarea.id_proyecto);
                    comando.Parameters.AddWithValue("@ID", tarea.ID);
                    comando.Parameters.AddWithValue("@titulo", tarea.titulo);
                    comando.Parameters.AddWithValue("@descripcion", tarea.descripcion);
                    comando.Parameters.AddWithValue("@prioridad", tarea.prioridad);
                    comando.Parameters.AddWithValue("@estado", tarea.estado);
                    comando.Parameters.AddWithValue("@fehca_Inicio", tarea.fehca_Inicio);
                    comando.Parameters.AddWithValue("@fechaVencimiento", tarea.fechaVencimiento);
                    comando.Parameters.AddWithValue("@horaEstimadas", tarea.horaEstimadas);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        // UPDATE - Actualizar tarea
        public bool Update(TareasModel tarea)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"UPDATE Tarea 
                                       SET id_proyecto = @id_proyecto,
                                           ID = @ID,
                                           titulo = @titulo,
                                           descripcion = @descripcion,
                                           prioridad = @prioridad,
                                           estado = @estado,
                                           fehca_Inicio = @fehca_Inicio,
                                           fechaVencimiento = @fechaVencimiento,
                                           horaEstimadas = @horaEstimadas
                                       WHERE id_tarea = @id_tarea";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_tarea", tarea.id_tarea);
                    comando.Parameters.AddWithValue("@id_proyecto", tarea.id_proyecto);
                    comando.Parameters.AddWithValue("@ID", tarea.ID);
                    comando.Parameters.AddWithValue("@titulo", tarea.titulo);
                    comando.Parameters.AddWithValue("@descripcion", tarea.descripcion);
                    comando.Parameters.AddWithValue("@prioridad", tarea.prioridad);
                    comando.Parameters.AddWithValue("@estado", tarea.estado);
                    comando.Parameters.AddWithValue("@fehca_Inicio", tarea.fehca_Inicio);
                    comando.Parameters.AddWithValue("@fechaVencimiento", tarea.fechaVencimiento);
                    comando.Parameters.AddWithValue("@horaEstimadas", tarea.horaEstimadas);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        // DELETE - Eliminar tarea
        public bool Delete(int id_tarea)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "DELETE FROM Tarea WHERE id_tarea = @id_tarea";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_tarea", id_tarea);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
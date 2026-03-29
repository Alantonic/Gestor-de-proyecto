using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Proyecto.Conexion_Base_Datos; 
using Proyecto.Modelos;
using static Proyecto.Modelos.ProyectosModel;

namespace Proyecto.Repositorio
{
    public class ProyectoRepository
    {
       
        // GET ALL con búsqueda (ADO.NET)
      
        public List<ProyectoModel> GetAll(string searchTerm = "")
        {
            List<ProyectoModel> proyectos = new List<ProyectoModel>();

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    // NOTA: En tu tabla la columna se llama 'descripcion' (sin tilde en la BD)
                    // pero en el modelo puedes manejarlo como 'Descripcion'.
                    // Asegúrate de que los nombres de las columnas en el SQL coincidan con tu BD.
                    string consulta = @"
                        SELECT 
                            id_proyecto,
                            ID,
                            Nombre,
                            descripcion,    -- Nombre exacto de la columna en BD
                            Fecha_Inicio,
                            Fecha_fin,
                            Estado,
                            Presupuesto
                        FROM Proyecto
                        WHERE 
                            (@searchTerm = '' 
                            OR CAST(ID AS VARCHAR) LIKE @searchTermLike
                            OR Nombre LIKE @searchTermLike
                            OR descripcion LIKE @searchTermLike
                            OR Estado LIKE @searchTermLike
                            OR CAST(Fecha_Inicio AS VARCHAR) LIKE @searchTermLike
                            OR CAST(Fecha_fin AS VARCHAR) LIKE @searchTermLike
                            OR CAST(Presupuesto AS VARCHAR) LIKE @searchTermLike
                            OR CAST(id_proyecto AS VARCHAR) LIKE @searchTermLike)";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@searchTerm", searchTerm);
                    comando.Parameters.AddWithValue("@searchTermLike", "%" + searchTerm + "%");

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProyectoModel proyecto = new ProyectoModel
                            {
                                id_proyecto = reader.GetInt32(0),
                                ID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                Nombre = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                descripcion = reader.IsDBNull(3) ? "" : reader.GetString(3), // Mapeo a la propiedad del modelo
                                Fecha_Inicio = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                                Fecha_fin = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                                Estado =reader.IsDBNull(6) ? "" : reader.GetString(6),
                                Presupuesto = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7)
                            };
                            proyectos.Add(proyecto);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Aquí podrías registrar el error o manejarlo según tu lógica
                Console.WriteLine(e);
                // Si lo deseas, puedes relanzar la excepción o manejarla de otra forma.
                // Por ahora, devolvemos la lista vacía.
            }
            return proyectos;
        }


        // GET ALL (Sobrecarga sin parámetro por si acaso)
     
        public List<ProyectoModel> GetAll()
        {
            return GetAll("");
        }

       
        // FIND por id_proyecto
       
        public ProyectoModel Find(int idProyecto)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"
                        SELECT 
                            id_proyecto,
                            ID,
                            Nombre,
                            descripcion,
                            Fecha_Inicio,
                            Fecha_fin,
                            Estado,
                            Presupuesto
                        FROM Proyecto
                        WHERE id_proyecto = @idProyecto";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@idProyecto", idProyecto);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProyectoModel
                            {
                                id_proyecto = reader.GetInt32(0),
                                ID = reader.GetInt32(1),
                                Nombre = reader.GetString(2),
                                descripcion = reader.GetString(3),
                                Fecha_Inicio = reader.GetDateTime(4),
                                Fecha_fin = reader.GetDateTime(5),
                                Estado = reader.GetString(6),
                                Presupuesto = reader.GetDecimal(7)
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

        
        // INSERT
       
        public int Insert(ProyectoModel proyecto)
        {
            int filasAfectadas = 0;
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"
                        INSERT INTO Proyecto 
                            (ID, Nombre, descripcion, Fecha_Inicio, Fecha_fin, Estado, Presupuesto)
                        VALUES 
                            (@ID, @Nombre, @descripcion, @Fecha_Inicio, @Fecha_fin, @Estado, @Presupuesto)";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@ID", proyecto.ID);
                    comando.Parameters.AddWithValue("@Nombre", proyecto.Nombre);
                    comando.Parameters.AddWithValue("@descripcion", proyecto.descripcion);
                    comando.Parameters.AddWithValue("@Fecha_Inicio", proyecto.Fecha_Inicio);
                    comando.Parameters.AddWithValue("@Fecha_fin", proyecto.Fecha_fin);
                    comando.Parameters.AddWithValue("@Estado", proyecto.Estado);
                    comando.Parameters.AddWithValue("@Presupuesto", proyecto.Presupuesto);

                    filasAfectadas = comando.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return filasAfectadas;
        }

        // UPDATE por id_proyecto
        public bool Update(ProyectoModel proyecto)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"
                        UPDATE Proyecto
                        SET 
                            ID = @ID,
                            Nombre = @Nombre,
                            descripcion = @descripcion,
                            Fecha_Inicio = @Fecha_Inicio,
                            Fecha_fin = @Fecha_fin,
                            Estado = @Estado,
                            Presupuesto = @Presupuesto
                        WHERE id_proyecto = @id_proyecto";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_proyecto", proyecto.id_proyecto);
                    comando.Parameters.AddWithValue("@ID", proyecto.ID);
                    comando.Parameters.AddWithValue("@Nombre", proyecto.Nombre);
                    comando.Parameters.AddWithValue("@descripcion", proyecto.descripcion);
                    comando.Parameters.AddWithValue("@Fecha_Inicio", proyecto.Fecha_Inicio);
                    comando.Parameters.AddWithValue("@Fecha_fin", proyecto.Fecha_fin);
                    comando.Parameters.AddWithValue("@Estado", proyecto.Estado);
                    comando.Parameters.AddWithValue("@Presupuesto", proyecto.Presupuesto);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        //Eliminar por id_proyecto
        public bool Delete(int idProyecto)
        {
            // NOTA: Si tu tabla tiene restricciones de clave foránea, asegúrate de manejar esas relaciones antes de eliminar un proyecto.
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    // Elimina el proyecto por id_proyecto
                    string consulta = "DELETE FROM Proyecto WHERE id_proyecto = @id_proyecto";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_proyecto", idProyecto);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception e)
            {
                // Aquí podrías registrar el error o manejarlo según tu lógica
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
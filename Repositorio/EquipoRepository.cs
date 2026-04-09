using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Proyecto.Conexion_Base_Datos;
using Proyecto.Modelos;

namespace Proyecto.Repositorio
{
    public class EquipoRepository
    {
        // GET ALL
        public List<EquipoModel> GetAll(string searchTerm = "")
        {
            List<EquipoModel> equipos = new List<EquipoModel>();

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"
                        SELECT ID_equipo, Nombre, descripciòn
                        FROM equipo
                        WHERE (@searchTerm = '' 
                            OR Nombre LIKE @searchTermLike
                            OR descripciòn LIKE @searchTermLike
                            OR CAST(ID_equipo AS VARCHAR) LIKE @searchTermLike)";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@searchTerm", searchTerm);
                    comando.Parameters.AddWithValue("@searchTermLike", "%" + searchTerm + "%");

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EquipoModel equipo = new EquipoModel
                            {
                                ID_equipo = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                descripciòn = reader.GetString(2)
                            };
                            equipos.Add(equipo);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return equipos;
        }

        public List<EquipoModel> GetAll()
        {
            return GetAll("");
        }

        // FIND por ID_equipo
        public EquipoModel Find(int id_equipo)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "SELECT ID_equipo, Nombre, descripciòn FROM equipo WHERE ID_equipo = @id_equipo";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_equipo", id_equipo);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new EquipoModel
                            {
                                ID_equipo = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                descripciòn = reader.GetString(2)
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
        public bool Insert(EquipoModel equipo)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "INSERT INTO equipo (Nombre, descripciòn) VALUES (@Nombre, @descripciòn)";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@Nombre", equipo.Nombre);
                    comando.Parameters.AddWithValue("@descripciòn", equipo.descripciòn);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        // UPDATE
        public bool Update(EquipoModel equipo)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "UPDATE equipo SET Nombre = @Nombre, descripciòn = @descripciòn WHERE ID_equipo = @id_equipo";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_equipo", equipo.ID_equipo);
                    comando.Parameters.AddWithValue("@Nombre", equipo.Nombre);
                    comando.Parameters.AddWithValue("@descripciòn", equipo.descripciòn);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        // DELETE
        public bool Delete(int id_equipo)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "DELETE FROM equipo WHERE ID_equipo = @id_equipo";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_equipo", id_equipo);

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





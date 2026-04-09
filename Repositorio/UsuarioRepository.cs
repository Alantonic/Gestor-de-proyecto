using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Proyecto.Modelos;
using Proyecto.Conexion_Base_Datos;

namespace Proyecto.Clases
{
    public class UsuarioRepository
    {
        // GET ALL con búsqueda - CORREGIDO (tabla en minúsculas)
        public List<UsuariosModel> GetAll(string searchTerm = "")
        {
            List<UsuariosModel> usuarios = new List<UsuariosModel>();

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    // IMPORTANTE: Usar "usuario" no "Usuario"
                    string consulta = @"SELECT ID, Nombre, Telefono, Gmail 
                                       FROM usuario 
                                       WHERE (@searchTerm = '' 
                                          OR Nombre LIKE @searchTermLike 
                                          OR Gmail LIKE @searchTermLike 
                                          OR Telefono LIKE @searchTermLike
                                          OR CAST(ID AS VARCHAR) LIKE @searchTermLike)";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@searchTerm", searchTerm);
                    comando.Parameters.AddWithValue("@searchTermLike", "%" + searchTerm + "%");

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UsuariosModel usuario = new UsuariosModel();
                            usuario.ID = Convert.ToInt32(reader["ID"]);
                            usuario.Nombre = reader["Nombre"].ToString();
                            usuario.Telefono = reader["Telefono"].ToString();
                            usuario.Gmail = reader["Gmail"].ToString();
                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return usuarios;
        }

        // GET ALL sin parámetro
        public List<UsuariosModel> GetAll()
        {
            return GetAll("");
        }

        // FIND por ID
        public UsuariosModel Find(int ID)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "SELECT ID, Nombre, Telefono, Gmail FROM usuario WHERE ID = @ID";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@ID", ID);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UsuariosModel
                            {
                                ID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Telefono = reader.GetString(2),
                                Gmail = reader.GetString(3)
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
        public bool Insert(UsuariosModel user)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"INSERT INTO usuario (Nombre, Telefono, Gmail) 
                                       VALUES (@Nombre, @Telefono, @Gmail)";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@Nombre", user.Nombre);
                    comando.Parameters.AddWithValue("@Telefono", user.Telefono);
                    comando.Parameters.AddWithValue("@Gmail", user.Gmail);

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
        public bool Update(UsuariosModel user)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"UPDATE usuario 
                                       SET Nombre = @Nombre, 
                                           Telefono = @Telefono, 
                                           Gmail = @Gmail 
                                       WHERE ID = @ID";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@ID", user.ID);
                    comando.Parameters.AddWithValue("@Nombre", user.Nombre);
                    comando.Parameters.AddWithValue("@Telefono", user.Telefono);
                    comando.Parameters.AddWithValue("@Gmail", user.Gmail);

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
        public bool Delete(int ID)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "DELETE FROM usuario WHERE ID = @ID";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@ID", ID);

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
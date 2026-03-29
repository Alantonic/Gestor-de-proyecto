using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Proyecto.Modelos;
using Proyecto.Conexion_Base_Datos;
namespace Proyecto.Clases
{
    public class UsuarioRepository
    {
        // GET ALL con búsqueda (ADO.NET)
        public List<UsuariosModel> GetAll(string searchTerm = "")
        {
            List<UsuariosModel> usuarios = new List<UsuariosModel>();

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"SELECT ID, Nombre, Telefono, Gmail 
                                       FROM Usuario 
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
                            usuarios.Add(new UsuariosModel
                            {
                                ID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Telefono = reader.GetString(2),
                                Gmail = reader.GetString(3)
                            });
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

        
        // FIND por ID (ADO.NET)
  
        public UsuariosModel Find(int ID)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "SELECT ID, Nombre, Telefono, Gmail FROM Usuario WHERE ID = @ID";
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

        // FIND por Gmail (ADO.NET)
        public UsuariosModel Find(string Gmail)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "SELECT ID, Nombre, Telefono, Gmail FROM Usuario WHERE Gmail = @Gmail";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@Gmail", Gmail);

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

        // FIND por Telefono (ADO.NET)
        public UsuariosModel FindTelefono(string Telefono)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "SELECT ID, Nombre, Telefono, Gmail FROM Usuario WHERE Telefono = @Telefono";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@Telefono", Telefono);

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

        //Insertar nuevo usuario (ADO.NET)
        public bool Insert(UsuariosModel user)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"INSERT INTO Usuario (Nombre, Telefono, Gmail) 
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

        // Actualizar usuario existente (ADO.NET)
        public bool Update(UsuariosModel user)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"UPDATE Usuario 
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
        // Eliminar usuario por ID (ADO.NET)
        public bool Delete(int ID)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "DELETE FROM Usuario WHERE ID = @ID";
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


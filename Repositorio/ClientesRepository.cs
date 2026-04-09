using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Proyecto.Conexion_Base_Datos;
using Proyecto.Modelos;

namespace Proyecto.Repositorio
{
    public class ClientesRepository
    {
        // GET ALL
        public List<ClientesModel> GetAll(string searchTerm = "")
        {
            List<ClientesModel> clientes = new List<ClientesModel>();

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"
                        SELECT Id_Cliente, Nombre, Apellido, Telefono, Gmail
                        FROM Clientes
                        WHERE (@searchTerm = '' 
                            OR Nombre LIKE @searchTermLike
                            OR Apellido LIKE @searchTermLike
                            OR Telefono LIKE @searchTermLike
                            OR Gmail LIKE @searchTermLike
                            OR CAST(Id_Cliente AS VARCHAR) LIKE @searchTermLike)";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@searchTerm", searchTerm);
                    comando.Parameters.AddWithValue("@searchTermLike", "%" + searchTerm + "%");

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClientesModel cliente = new ClientesModel
                            {
                                Id_Cliente = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Telefono = reader.GetString(3),
                                Gmail = reader.GetString(4)
                            };
                            clientes.Add(cliente);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return clientes;
        }

        public List<ClientesModel> GetAll()
        {
            return GetAll("");
        }

        // FIND por Id_Cliente
        public ClientesModel Find(int id_cliente)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "SELECT Id_Cliente, Nombre, Apellido, Telefono, Gmail FROM Clientes WHERE Id_Cliente = @id_cliente";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_cliente", id_cliente);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ClientesModel
                            {
                                Id_Cliente = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Telefono = reader.GetString(3),
                                Gmail = reader.GetString(4)
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

        // INSERT (requiere ID manual porque no es IDENTITY)
        public bool Insert(ClientesModel cliente)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"INSERT INTO Clientes (Id_Cliente, Nombre, Apellido, Telefono, Gmail) 
                                       VALUES (@Id_Cliente, @Nombre, @Apellido, @Telefono, @Gmail)";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@Id_Cliente", cliente.Id_Cliente);
                    comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                    comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    comando.Parameters.AddWithValue("@Gmail", cliente.Gmail);

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
        public bool Update(ClientesModel cliente)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"UPDATE Clientes 
                                       SET Nombre = @Nombre,
                                           Apellido = @Apellido,
                                           Telefono = @Telefono,
                                           Gmail = @Gmail
                                       WHERE Id_Cliente = @Id_Cliente";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@Id_Cliente", cliente.Id_Cliente);
                    comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                    comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    comando.Parameters.AddWithValue("@Gmail", cliente.Gmail);

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
        public bool Delete(int id_cliente)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "DELETE FROM Clientes WHERE Id_Cliente = @id_cliente";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_cliente", id_cliente);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        // Método para obtener el último ID (para generar nuevo ID manualmente)
        public int GetLastId()
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "SELECT ISNULL(MAX(Id_Cliente), 0) FROM Clientes";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    return (int)comando.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
    }
}
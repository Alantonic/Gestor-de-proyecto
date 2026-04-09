using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Proyecto.Conexion_Base_Datos;
using Proyecto.Modelos;

namespace Proyecto.Repositorio
{
    public class DocumentoRepository
    {
        // GET ALL
        public List<DocumentoModel> GetAll(string searchTerm = "")
        {
            List<DocumentoModel> documentos = new List<DocumentoModel>();

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"
                        SELECT id_documento, id_proyecto, nombre, tipoArchivo, url, fechaSubida
                        FROM Documento
                        WHERE (@searchTerm = '' 
                            OR nombre LIKE @searchTermLike
                            OR tipoArchivo LIKE @searchTermLike
                            OR CAST(id_documento AS VARCHAR) LIKE @searchTermLike)";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@searchTerm", searchTerm);
                    comando.Parameters.AddWithValue("@searchTermLike", "%" + searchTerm + "%");

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DocumentoModel documento = new DocumentoModel
                            {
                                id_documento = reader.GetInt32(0),
                                id_proyecto = reader.GetInt32(1),
                                nombre = reader.GetString(2),
                                tipoArchivo = reader.GetString(3),
                                url = reader.GetString(4),
                                fechaSubida = reader.GetDateTime(5)
                            };
                            documentos.Add(documento);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return documentos;
        }

        public List<DocumentoModel> GetAll()
        {
            return GetAll("");
        }

        // FIND
        public DocumentoModel Find(int id_documento)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "SELECT id_documento, id_proyecto, nombre, tipoArchivo, url, fechaSubida FROM Documento WHERE id_documento = @id_documento";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_documento", id_documento);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DocumentoModel
                            {
                                id_documento = reader.GetInt32(0),
                                id_proyecto = reader.GetInt32(1),
                                nombre = reader.GetString(2),
                                tipoArchivo = reader.GetString(3),
                                url = reader.GetString(4),
                                fechaSubida = reader.GetDateTime(5)
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
        public bool Insert(DocumentoModel documento)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"INSERT INTO Documento (id_proyecto, nombre, tipoArchivo, url, fechaSubida) 
                                       VALUES (@id_proyecto, @nombre, @tipoArchivo, @url, @fechaSubida)";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_proyecto", documento.id_proyecto);
                    comando.Parameters.AddWithValue("@nombre", documento.nombre);
                    comando.Parameters.AddWithValue("@tipoArchivo", documento.tipoArchivo);
                    comando.Parameters.AddWithValue("@url", documento.url);
                    comando.Parameters.AddWithValue("@fechaSubida", documento.fechaSubida);

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
        public bool Update(DocumentoModel documento)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"UPDATE Documento 
                                       SET id_proyecto = @id_proyecto,
                                           nombre = @nombre,
                                           tipoArchivo = @tipoArchivo,
                                           url = @url,
                                           fechaSubida = @fechaSubida
                                       WHERE id_documento = @id_documento";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_documento", documento.id_documento);
                    comando.Parameters.AddWithValue("@id_proyecto", documento.id_proyecto);
                    comando.Parameters.AddWithValue("@nombre", documento.nombre);
                    comando.Parameters.AddWithValue("@tipoArchivo", documento.tipoArchivo);
                    comando.Parameters.AddWithValue("@url", documento.url);
                    comando.Parameters.AddWithValue("@fechaSubida", documento.fechaSubida);

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
        public bool Delete(int id_documento)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "DELETE FROM Documento WHERE id_documento = @id_documento";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@id_documento", id_documento);

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
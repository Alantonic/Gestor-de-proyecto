using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Base_Datos;
using Proyecto.Clases;
using Proyecto.Forms;
using Proyecto.Modelos;

namespace Proyecto.Clases
{


    public class Presentar
    {
        private static SqlConnection ObtenerConexion()
        {
            return BD.ObtenerConexion();
        }
        public static int AgregarProducto(UsuariosModel usuario)
        {
            int r = 0;
            using (SqlConnection conexion = ObtenerConexion())
            {

                string consulta = "INSERT INTO Usuario (ID, Nombre, Telefono, Gmail) " +
                    "VALUES (@ID, @Nombre, @Telefono, @Gmail)";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                //Parametros para evitar inyecciones SQL

                comando.Parameters.AddWithValue("@ID", usuario.ID);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                comando.Parameters.AddWithValue("@Gmail", usuario.Gmail);




                r = comando.ExecuteNonQuery();

            }
            return r;
        }


        public class UsuarioRepository
        {
            // GET ALL con búsqueda
            public List<usuario> GetAll(string searchTerm = "")
            {
                List<usuario> usuarios = new List<usuario>();

                try
                {
                    using (Proyecto.Base_Datos.BD db = new Proyecto.Base_Datos.BD())
                    {
                        usuarios = db.usuario
                            .Where(u =>
                                u.Nombre.Contains(searchTerm) ||
                                u.Gmail.Contains(searchTerm) ||
                                u.Telefono.Contains(searchTerm) ||
                                u.ID.ToString().Contains(searchTerm)
                            )
                            .ToList();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    usuarios = new List<usuario>();
                }

                return usuarios;
            }

            //Find por ID

            public usuario Find(int ID)
            {
                try
                {
                    using (Proyecto.Base_Datos.BD db = new Proyecto.Base_Datos.BD())
                    {
                        return db.usuario.FirstOrDefault(u => u.ID == ID);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }

            }

           


            //Find por Gmail

            public usuario Find(string Gmail)
            {
                try
                {
                    using (Proyecto.Base_Datos.BD db = new Proyecto.Base_Datos.BD())
                    {
                        return db.usuario.FirstOrDefault(u => u.Gmail == Gmail);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }

            //Find por Telefono

            public usuario FindTelefono(string Telefono)
            {
                try
                {
                    using (Proyecto.Base_Datos.BD db = new Proyecto.Base_Datos.BD())
                    {
                        return db.usuario.FirstOrDefault(u => u.Telefono == Telefono);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }

            //Find por Nombre

            public bool Insert(usuario user)
            {
                try
                {
                    using (Proyecto.Base_Datos.BD db = new Proyecto.Base_Datos.BD())
                    {
                        db.usuario.Add(user);
                        db.SaveChanges();
                        return true;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }

            //Update por ID

            public bool Update(usuario user)
            {
                try
                {
                    using (Proyecto.Base_Datos.BD db = new Proyecto.Base_Datos.BD())
                    {
                        var existingUser = db.usuario.FirstOrDefault(u => u.ID == user.ID);
                        if (existingUser != null)
                        {
                            existingUser.Nombre = user.Nombre;
                            existingUser.Telefono = user.Telefono;
                            existingUser.Gmail = user.Gmail;
                            db.SaveChanges();
                            return true;
                        }
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            

            public bool Delete(int ID)
            {
                try
                {
                    using (Proyecto.Base_Datos.BD db = new Proyecto.Base_Datos.BD())
                    {
                        var user = db.usuario.Find(ID);
                        if (user != null)
                        {
                            db.usuario.Remove(user);
                            db.SaveChanges();
                            return true;
                        }
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
         


            public static List<UsuariosModel> Representar()
            {
                List<UsuariosModel> lista = new List<UsuariosModel>();
                using (SqlConnection conexion = ObtenerConexion())
                {
                    string consulta = "SELECT ID, Nombre, Telefono, Gmail FROM Usuario";
                    SqlCommand comando = new SqlCommand(consulta, conexion);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UsuariosModel prod = new UsuariosModel();
                            prod.ID = reader.GetInt32(0);
                            prod.Nombre = reader.GetString(1);
                            prod.Telefono = reader.GetString(2);
                            prod.Gmail = reader.GetString(3);

                            lista.Add(prod);
                        }

                    }
                }
                return lista;
            }

        }
    }
}

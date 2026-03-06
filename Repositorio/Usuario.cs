using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Clases
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Gmail { get; set; }

        internal static SqlConnection Conectar()
        {
            return BD.ObtenerConexion();
        }

        internal static SqlConnection ObtenerConexion()
        {
            throw new NotImplementedException();
        }



        //Hace consultas
        internal static List<Usuario> Representar()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection conexion = Conectar())
            {
                string query = "SELECT * FROM usuario";
                SqlCommand comando = new SqlCommand(query, conexion);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario
                        {
                            ID = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Telefono = reader.GetString(2),
                            Gmail = reader.GetString(3)
                        };
                        lista.Add(usuario);
                    }


                }
            }
            return lista;


        }
    }
}
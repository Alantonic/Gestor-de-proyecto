using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proyecto.Clases
{
    public class Presentar
    {
        public static int AgregarProducto(Usuario usuario)
        {
            int r = 0;
            using (SqlConnection conexion = BD.ObtenerConexion())
            {

                string consulta = "INSERT INTO Usuario (ID, Nombre, Telefono, Gmail) " +
                    "VALUES (@ID, @Nombre, @Telefono, @Gmail)";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                //comando.Parameters.AddWithValue("@iProduct", Guid.NewGuid());

                comando.Parameters.AddWithValue("@ID", usuario.ID);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                comando.Parameters.AddWithValue("@Gmail", usuario.Gmail);




                r = comando.ExecuteNonQuery();

            }
            return r;
        }
        public static List<Usuario> Representar()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection conexion = Usuario.ObtenerConexion())
            {
                string consulta = "SELECT ID, Nombre, Telefono, Gmail FROM Usuario";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario prod = new Usuario();
                        prod.ID = reader.GetInt16(0);
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

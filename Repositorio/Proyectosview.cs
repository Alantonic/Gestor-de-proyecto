using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proyecto.Clases
{
    public class PresentarProyecto
    {
        public static int AgregarProyecto(Proyectos proyecto)
        {
            int r = 0;
            using (SqlConnection conexion = BD.ObtenerConexion())
            {

                string consulta = "INSERT INTO Proyecto ( ID, Nombre, descripcion, Fecha_Inicio, Fecha_fin, Estado, Presupuesto) " +
                    "VALUES (@ID, @Nombre, @descripcion, @Fecha_Inicio, @Fecha_fin, @Estado, @Presupuesto)";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue("@ID", proyecto.ID);
                comando.Parameters.AddWithValue("@Nombre", proyecto.Nombre);
                comando.Parameters.AddWithValue("@descripcion", proyecto.descripcion);
                comando.Parameters.AddWithValue("@Fecha_Inicio", proyecto.Fecha_Inicio);
                comando.Parameters.AddWithValue("@Fecha_fin", proyecto.Fecha_fin);
                comando.Parameters.AddWithValue("@Estado", proyecto.Estado);
                comando.Parameters.AddWithValue("@Presupuesto", proyecto.Presupuesto);






                r = comando.ExecuteNonQuery();

            }
            return r;
        }
        public static List<Proyectos> Representar()
        {
            List<Proyectos> lista = new List<Proyectos>();
            using (SqlConnection conexion = Proyectos.Conectar())
            {
                conexion.Open();
                string consulta = "SELECT id_proyecto,ID, Nombre, descripcion, Fecha_Inicio,Fecha_fin, Estado, Presupuesto FROM Proyecto";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Proyectos proyecto = new Proyectos();
                       proyecto.id_proyecto = reader.GetInt32(0);
                        proyecto.ID = reader.GetInt32(1);
                        proyecto.Nombre = reader.GetString(2);
                        proyecto.descripcion = reader.GetString(3);
                        proyecto.Fecha_Inicio = reader.GetDateTime(4);
                        proyecto.Fecha_fin = reader.GetDateTime(5);
                        proyecto.Estado = reader.GetString(6);
                        proyecto.Presupuesto = reader.GetDecimal(7);

                        lista.Add(proyecto);
                    }

                }
            }
            return lista;
        }
    }
}

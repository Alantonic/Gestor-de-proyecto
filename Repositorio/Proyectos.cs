using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Clases;

namespace Proyecto

{
    public class Proyectos
    {
        public int id_proyecto { get; set; }
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_fin { get; set; }
        public string Estado { get; set; }
        public decimal Presupuesto { get; set; }


        internal static SqlConnection Conectar()
        {
            return BD.ObtenerConexion();
        }

   
        internal static List<Proyectos> Representar()
        {
            List<Proyectos> lista = new List<Proyectos>();

            using (SqlConnection conexion = Conectar())
            {
               // conexion.Open();
                string query = "SELECT * FROM Proyecto";
                SqlCommand comando = new SqlCommand(query, conexion);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Proyectos Proyectos = new Proyectos
                        {
                            ID = reader.GetInt32(1),
                            id_proyecto = reader.GetInt32(0),
                            Nombre = reader.GetString(2),
                            descripcion = reader.GetString(3),
                            Fecha_Inicio = reader.GetDateTime(4),
                            Fecha_fin = reader.GetDateTime(5),
                            Estado = reader.GetString(6),
                            Presupuesto = reader.GetDecimal(7)


                        };
                        lista.Add(Proyectos);
                    }


                }
            }
            return lista;


        }
    }
}
        
    
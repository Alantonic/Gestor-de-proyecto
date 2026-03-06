using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Clases;

namespace Proyecto.Repositorio
{
    internal class Equipo
    {
        public int ID_equipo  { get; set; }

        public int Nombre { get; set; }
        public int descripción { get; set; }
       
        
        internal static SqlConnection Conectar()
        {
            return BD.ObtenerConexion();
        }

        internal static SqlConnection ObtenerConexion()
        {
            throw new NotImplementedException();
        }



        //Hace consultas
        internal static List<Equipo> Representar()
        {
            List <Equipo> lista = new List<Equipo>();

            using (SqlConnection conexion = Conectar())
            {
                string query = "SELECT * FROM equipo";
                SqlCommand comando = new SqlCommand(query, conexion);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Equipo equipo = new Equipo
                        {
                            ID_equipo = reader.GetInt32(0),
                            Nombre = reader.GetInt32(1),
                            descripción = reader.GetInt32(2),
                         
                        };
                        lista.Add(equipo);
                    }


                }
            }
            return lista;


        }
    }
    
    }



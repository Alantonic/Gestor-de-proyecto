using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Conexion_Base_Datos
{
    public class BD
    {
        public static SqlConnection ObtenerConexion()
        {
            // Lee la cadena de conexión desde el archivo de configuración (App.config o Web.config)
            string connectionString = ConfigurationManager.ConnectionStrings["ProyectoDB"].ConnectionString;


            // Crea una nueva conexión utilizando la cadena de conexión y ábrela
            SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();
            return conexion;

        }

    }
}

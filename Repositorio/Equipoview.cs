using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Repositorio;

namespace Proyecto.Clases
{
    internal class Presentarequipo
    {
        public static int AgregarEquipo(Equipo equipo)
        {
            int r = 0;
            using (SqlConnection conexion = BD.ObtenerConexion())
            {

                string consulta = "INSERT INTO Equipo (ID_equipo, Nombre, descripción ) " +
                    "VALUES (@ID_equipo, @Nombre, @descripción)";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue("@ID_equipo", equipo.ID_equipo);
                comando.Parameters.AddWithValue("@Nombre", equipo.Nombre);
                comando.Parameters.AddWithValue("@descripción", equipo.descripción);

                r = comando.ExecuteNonQuery();

            }
            return r;

        }
    }
}

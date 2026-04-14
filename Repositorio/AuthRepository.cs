using System;
using System.Data.SqlClient;
using Proyecto.Conexion_Base_Datos;

namespace Proyecto.Repositorio
{
    public class AuthRepository
    {
        // Registro de nuevo usuario
        public bool RegistrarUsuario(string usuario, string contraseña)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion()) // Usa tu clase BD existente
                {
                    string consulta = "INSERT INTO usuarios_login (usuario_sesion, contraseña) VALUES (@usuario, @contraseña)";
                    SqlCommand command = new SqlCommand(consulta, conexion);
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@contraseña", contraseña); // Idealmente hash, pero por ahora directo

                    int filas = command.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                // Violación de clave única (usuario duplicado)
                if (ex.Number == 2627 || ex.Number == 2601)
                    return false;
                throw;
            }
        }

        // Validar inicio de sesión
        public bool ValidarUsuario(string usuario, string contraseña)
        {
            using (SqlConnection conexion = BD.ObtenerConexion())
            {
                string consulta = "SELECT COUNT(*) FROM usuarios_login WHERE usuario_sesion = @usuario AND contraseña = @contraseña";
                SqlCommand command = new SqlCommand(consulta, conexion);
                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@contraseña", contraseña);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}

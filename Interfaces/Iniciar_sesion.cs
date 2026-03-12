using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto.Forms;

namespace Proyecto.Interfaces
{
    public partial class Iniciar_sesion : Form
    {
        private SqlConnection conexion;
        public Iniciar_sesion()
        {
            InitializeComponent();
        }
    

        private void label4_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.ShowDialog();

        }

      
        private void Iniciar_sesion_Load(object sender, EventArgs e)
        {

        }

            

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(TU2.Text) || string.IsNullOrWhiteSpace(TC2.Text))
            {
                MessageBox.Show("Ingrese usuario y contraseña");
                return;
            }

            try
            {
                conexion.Open();

                string usuario = TU2.Text;
                string contraseña = TC2.Text;

                // Consulta para verificar si existe el usuario
                SqlCommand command = new SqlCommand(
         "SELECT COUNT(*) FROM usuarios_login WHERE usuario_sesion = @usuario_sesion AND contraseña = @contraseña",
         conexion
     );

                command.Parameters.AddWithValue("@usuario_sesion", usuario);
                command.Parameters.AddWithValue("@contraseña", contraseña);

                int count = (int)command.ExecuteScalar();  // Devuelve el número de coincidencias

                conexion.Close();

                if (count > 0)
                {
                    MessageBox.Show("Inicio de sesión exitoso");

                    // Abre el formulario principal
                    Main mainForm = new Main();
                    mainForm.Show();
                    this.Hide();  // Oculta el formulario de login
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}


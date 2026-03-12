using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto.Base_Datos;
using Proyecto.Forms;
using Proyecto.Interfaces;

namespace Proyecto
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string Nombre = TU1.Text;
            string Contraseña = TC1.Text;

            SqlCommand command = new SqlCommand(
           "INSERT INTO usuarios_login (usuario_sesion, contraseña) VALUES (@usuario_sesion, @contraseña)",
           conexion
       );

            command.Parameters.AddWithValue("@usuario_sesion", Nombre);  // 
            command.Parameters.AddWithValue("@contraseña", Contraseña);

            int ejecutar = command.ExecuteNonQuery();
            conexion.Close();
           

            Main form = new Main();
            form.ShowDialog();
        }

       SqlConnection conexion = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=proyecto;Data Source=ALANTONIC\\SQLEXPRESS");








        private void label4_Click(object sender, EventArgs e)
            {
                try
                {
                    TextWriter Registro = new StreamWriter(@"C:\Users\alanv\source\repos\Proyecto\Proyecto\bin\Debug\" + TU1.Text + ".txt", true);
                    Registro.WriteLine(TC1.Text);
                    Iniciar_sesion form = new Iniciar_sesion();
                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al escribir en el archivo: " + ex.ToString());
                    TU1.Clear();
                    TC1.Clear();
                }
            }
        }
    }


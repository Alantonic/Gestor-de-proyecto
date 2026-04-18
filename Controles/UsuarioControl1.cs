using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Proyecto.Conexion_Base_Datos;
using Proyecto.Forms;
using Proyecto.Modelos;

namespace Proyecto.Controles
{
    public partial class UserControl1 : UserControl
    {
        public bool DatosGuardados { get; private set; }

        // Constructor para NUEVO usuario
        public UserControl1()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            DatosGuardados = false;
        }

        // Constructor para EDITAR (aunque solo inserte, se mantiene por compatibilidad)
        public UserControl1(UsuariosModel usuario)
        {
            InitializeComponent();
            this.AutoSize = false;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            DatosGuardados = false;
            CargarDatos(usuario);
        }

        private void CargarDatos(UsuariosModel usuario)
        {
            if (usuario != null)
            {
                txtNombre.Text = usuario.Nombre;
                txtTelefono.Text = usuario.Telefono;
                txtCorreo.Text = usuario.Gmail;
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del usuario es obligatorio");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("El correo electrónico es obligatorio");
                return;
            }

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"INSERT INTO usuario (Nombre, Telefono, Gmail) 
                                        VALUES (@Nombre, @Telefono, @Gmail)";

                    SqlCommand command = new SqlCommand(consulta, conexion);
                    command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    command.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    command.Parameters.AddWithValue("@Gmail", txtCorreo.Text);

                    int ejecutar = command.ExecuteNonQuery();

                    if (ejecutar > 0)
                    {
                        MessageBox.Show("Usuario guardado exitosamente");
                        DatosGuardados = true;
                        Main main = new Main();
                        main.Show();
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar el usuario");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

       
    }
}
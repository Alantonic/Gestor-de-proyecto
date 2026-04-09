using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Proyecto.Conexion_Base_Datos;
using Proyecto.Modelos;
using Proyecto.Clases;

namespace Proyecto.Controles
{
    public partial class UserControl1 : UserControl
    {
        private UsuariosModel _usuarioActual;
        public bool DatosGuardados { get; private set; }

        // Constructor para NUEVO usuario
        public UserControl1()
        {
            InitializeComponent();
            DatosGuardados = false;
            _usuarioActual = null;
            ConfigurarParaNuevo();
        }

        // Constructor para EDITAR usuario
        public UserControl1(UsuariosModel usuario)
        {
            InitializeComponent();
            DatosGuardados = false;
            _usuarioActual = usuario;
            CargarDatosEnControles();
            ConfigurarParaEdicion();
        }

        private void ConfigurarParaNuevo()
        {
            btnGuardar.Text = "Guardar";
            LimpiarCampos();
        }

        private void ConfigurarParaEdicion()
        {
            btnGuardar.Text = "Agregar";
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
        }

        private void CargarDatosEnControles()
        {
            if (_usuarioActual != null)
            {
                txtNombre.Text = _usuarioActual.Nombre ?? "";
                txtTelefono.Text = _usuarioActual.Telefono ?? "";
                txtCorreo.Text = _usuarioActual.Gmail ?? "";
            }
        }

        // Botón Guardar/Actualizar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del usuario es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("El correo electrónico es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            if (!txtCorreo.Text.Contains("@"))
            {
                MessageBox.Show("Ingrese un correo electrónico válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            try
            {
                if (_usuarioActual == null)
                {
                    InsertarUsuario();
                }
                else
                {
                    ActualizarUsuario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertarUsuario()
        {
            using (SqlConnection conexion = BD.ObtenerConexion())
            {
                string consulta = @"INSERT INTO usuario (Nombre, Telefono, Gmail) 
                                    VALUES (@Nombre, @Telefono, @Gmail)";

                SqlCommand command = new SqlCommand(consulta, conexion);
                command.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                command.Parameters.AddWithValue("@Telefono", string.IsNullOrWhiteSpace(txtTelefono.Text) ? "" : txtTelefono.Text.Trim());
                command.Parameters.AddWithValue("@Gmail", txtCorreo.Text.Trim());

                int ejecutar = command.ExecuteNonQuery();

                if (ejecutar > 0)
                {
                    MessageBox.Show("Usuario guardado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DatosGuardados = true;
                    CerrarFormularioContenedor();
                }
                else
                {
                    MessageBox.Show("Error al guardar el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ActualizarUsuario()
        {
            using (SqlConnection conexion = BD.ObtenerConexion())
            {
                string consulta = @"UPDATE usuario 
                                    SET Nombre = @Nombre, 
                                        Telefono = @Telefono, 
                                        Gmail = @Gmail 
                                    WHERE ID = @ID";

                SqlCommand command = new SqlCommand(consulta, conexion);
                command.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                command.Parameters.AddWithValue("@Telefono", string.IsNullOrWhiteSpace(txtTelefono.Text) ? "" : txtTelefono.Text.Trim());
                command.Parameters.AddWithValue("@Gmail", txtCorreo.Text.Trim());
                command.Parameters.AddWithValue("@ID", _usuarioActual.ID);  // ← Aquí estaba probablemente el error

                int ejecutar = command.ExecuteNonQuery();

                if (ejecutar > 0)
                {
                    MessageBox.Show("Usuario actualizado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DatosGuardados = true;
                    CerrarFormularioContenedor();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CerrarFormularioContenedor()
        {
            // Cierra el formulario que contiene este UserControl
            this.FindForm()?.Close();
        }

        // Botón Cancelar (si existe)
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas cancelar? Los cambios no se guardarán.",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                CerrarFormularioContenedor();
            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            // Configuración adicional si es necesario
        }
    }
}
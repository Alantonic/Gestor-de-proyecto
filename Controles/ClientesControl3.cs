using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Proyecto.Conexion_Base_Datos;
using Proyecto.Modelos;
using Proyecto.Repositorio;

namespace Proyecto.Controles
{
    public partial class ClientesControl3 : UserControl
    {
        public bool Datos_guardados { get; private set; }
        private ClientesModel _clienteEditando;
        private bool _esEdicion = false;

        // Constructor para NUEVO cliente
        public ClientesControl3()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            Datos_guardados = false;
            _esEdicion = false;
            _clienteEditando = null;
            ConfigurarParaNuevo();
        }

        // Constructor para EDITAR cliente
        public ClientesControl3(ClientesModel cliente)
        {
            InitializeComponent();
            this.AutoSize = false;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            Datos_guardados = false;
            _esEdicion = true;
            _clienteEditando = cliente;
            CargarDatos(cliente);
            ConfigurarParaEdicion();
        }

        private void ConfigurarParaNuevo()
        {
            Btnguardar.Text = "Guardar Cliente";
            LimpiarCampos();
            // Generar nuevo ID automáticamente
            var repo = new ClientesRepository();
            int nuevoId = repo.GetLastId() + 1;
            txtIdCliente.Text = nuevoId.ToString();
            txtIdCliente.ReadOnly = true; // El ID no se puede editar
        }

        private void ConfigurarParaEdicion()
        {
            Btnguardar.Text = "Actualizar Cliente";
            txtIdCliente.ReadOnly = true; // El ID no se puede editar
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtGmail1.Clear();
        }

        private void CargarDatos(ClientesModel cliente)
        {
            if (cliente != null)
            {
                txtIdCliente.Text = cliente.Id_Cliente.ToString();
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtTelefono.Text = cliente.Telefono;
                txtGmail1.Text = cliente.Gmail;
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del cliente es obligatorio");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido del cliente es obligatorio");
                txtApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGmail.Text))
            {
                MessageBox.Show("El email del cliente es obligatorio");
                txtGmail.Focus();
                return false;
            }

            if (!txtGmail.Text.Contains("@"))
            {
                MessageBox.Show("Ingrese un email válido");
                txtGmail.Focus();
                return false;
            }

            return true;
        }

        private void Btnguardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    if (_esEdicion)
                    {
                        // UPDATE
                        string consulta = @"UPDATE Clientes 
                                            SET Nombre = @Nombre,
                                                Apellido = @Apellido,
                                                Telefono = @Telefono,
                                                Gmail = @Gmail
                                            WHERE Id_Cliente = @Id_Cliente";

                        SqlCommand command = new SqlCommand(consulta, conexion);
                        command.Parameters.AddWithValue("@Id_Cliente", Convert.ToInt32(txtIdCliente.Text));
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                        command.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                        command.Parameters.AddWithValue("@Gmail", txtGmail1.Text);

                        int ejecutar = command.ExecuteNonQuery();

                        if (ejecutar > 0)
                        {
                            MessageBox.Show("Cliente actualizado exitosamente");
                            Datos_guardados = true;
                            this.FindForm()?.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error al actualizar el cliente");
                        }
                    }
                    else
                    {
                        // INSERT
                        string consulta = @"INSERT INTO Clientes (Id_Cliente, Nombre, Apellido, Telefono, Gmail) 
                                            VALUES (@Id_Cliente, @Nombre, @Apellido, @Telefono, @Gmail)";

                        SqlCommand command = new SqlCommand(consulta, conexion);
                        command.Parameters.AddWithValue("@Id_Cliente", Convert.ToInt32(txtIdCliente.Text));
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                        command.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                        command.Parameters.AddWithValue("@Gmail", txtGmail1.Text);

                        int ejecutar = command.ExecuteNonQuery();

                        if (ejecutar > 0)
                        {
                            MessageBox.Show("Cliente guardado exitosamente");
                            Datos_guardados = true;
                            this.FindForm()?.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error al guardar el cliente");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClientesControl3_Load(object sender, EventArgs e)
        {

        }
    }
}

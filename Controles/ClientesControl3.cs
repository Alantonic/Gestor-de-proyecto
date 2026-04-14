using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Proyecto.Conexion_Base_Datos;
using Proyecto.Modelos;

namespace Proyecto.Controles
{
    public partial class ClientesControl3 : UserControl
    {
        public bool Datos_guardados { get; private set; }

        // Constructor para NUEVO cliente
        public ClientesControl3()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            // Generar el siguiente ID disponible automáticamente
            txtIdCliente.Text = ObtenerSiguienteId().ToString();
            txtIdCliente.ReadOnly = true; // El ID no se edita
        }

        // Constructor para EDITAR cliente
        public ClientesControl3(ClientesModel cliente)
        {
            InitializeComponent();
            Datos_guardados = false;
            CargarDatos(cliente);
            txtIdCliente.ReadOnly = true; // El ID no se edita
        }

        // Método para obtener el siguiente ID disponible
        private int ObtenerSiguienteId()
        {
            int siguienteId = 1;
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "SELECT ISNULL(MAX(Id_Cliente), 0) + 1 FROM Clientes";
                    SqlCommand command = new SqlCommand(consulta, conexion);
                    siguienteId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar ID: " + ex.Message);
            }
            return siguienteId;
        }

        private void CargarDatos(ClientesModel cliente)
        {
            if (cliente != null)
            {
                txtIdCliente.Text = cliente.Id_Cliente.ToString();
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtTelefono.Text = cliente.Telefono;
                txtGmail.Text = cliente.Gmail;
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

            return true;
        }

        private void Btnguardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    // Verificar si es edición o nuevo
                    if (txtIdCliente.ReadOnly && !string.IsNullOrWhiteSpace(txtIdCliente.Text) && EsClienteExistente(Convert.ToInt32(txtIdCliente.Text)))
                    {
                        // UPDATE - Editar cliente existente
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
                        command.Parameters.AddWithValue("@Gmail", txtGmail.Text);

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
                        // INSERT - Nuevo cliente (usar el ID generado)
                        string consulta = @"INSERT INTO Clientes 
                                            (Id_Cliente, Nombre, Apellido, Telefono, Gmail) 
                                            VALUES 
                                            (@Id_Cliente, @Nombre, @Apellido, @Telefono, @Gmail)";

                        SqlCommand command = new SqlCommand(consulta, conexion);
                        command.Parameters.AddWithValue("@Id_Cliente", Convert.ToInt32(txtIdCliente.Text));
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                        command.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                        command.Parameters.AddWithValue("@Gmail", txtGmail.Text);

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

        // Método para verificar si un cliente ya existe
        private bool EsClienteExistente(int id)
        {
            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = "SELECT COUNT(*) FROM Clientes WHERE Id_Cliente = @Id";
                    SqlCommand command = new SqlCommand(consulta, conexion);
                    command.Parameters.AddWithValue("@Id", id);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        private void ClientesControl3_Load(object sender, EventArgs e)
        {
        }

        
    }
}

using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Proyecto.Conexion_Base_Datos;
using Proyecto.Modelos;

namespace Proyecto.Controles
{
    public partial class DocumentosControl3 : UserControl
    {
        public bool Datos_guardados { get; private set; } // Propiedad para indicar si los datos se guardaron correctamente
        private DocumentoModel _documentoEditando;
        private bool _esEdicion = false;

        // Constructor para NUEVO documento
        public DocumentosControl3(ProyectosModel.ProyectoModel selectedProject)
        {
            InitializeComponent();
            this.AutoSize = false;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            Datos_guardados = false;
            _esEdicion = false;
            _documentoEditando = null;
            ConfigurarParaNuevo();
        }
        public DocumentosControl3(int idProyecto)
        {
            InitializeComponent();
            this.AutoSize = false;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            Datos_guardados = false;
            _esEdicion = false;
            _documentoEditando = null;
            ConfigurarParaNuevo();

            // Si se pasa un ID de proyecto, lo asignamos automáticamente
            if (idProyecto > 0)
            {
                txtIdProyecto.Text = idProyecto.ToString();
                txtIdProyecto.ReadOnly = true; // Opcional: hacerlo de solo lectura
            }
        }

        // Constructor para EDITAR documento
        public DocumentosControl3(DocumentoModel documento)
        {
            InitializeComponent();
            this.AutoSize = false;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            Datos_guardados = false;
            _esEdicion = true;
            _documentoEditando = documento;
            CargarDatos(documento);
            ConfigurarParaEdicion();
        }

        private void ConfigurarParaNuevo()
        {
            Btnguardar.Text = "Guardar Documento";
            LimpiarCampos();
        }

        private void ConfigurarParaEdicion()
        {
            Btnguardar.Text = "Actualizar Documento";
        }

        private void LimpiarCampos()
        {
            txtIdProyecto.Clear();
            txtNombre.Clear();
            txtTipoArchivo.Clear();
            txtUrl.Clear();
            Fecha_subidadtp.Value = DateTime.Now;
        }

        private void CargarDatos(DocumentoModel documento)
        {
            if (documento != null)
            {
                txtIdProyecto.Text = documento.id_proyecto.ToString();
                txtNombre.Text = documento.nombre;
                txtTipoArchivo.Text = documento.tipoArchivo;
                txtUrl.Text = documento.url;
                Fecha_subidadtp.Value = documento.fechaSubida;
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del documento es obligatorio");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUrl.Text))
            {
                MessageBox.Show("La URL del documento es obligatoria");
                txtUrl.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtIdProyecto.Text))
            {
                MessageBox.Show("El ID del proyecto es obligatorio");
                txtIdProyecto.Focus();
                return false;
            }

            return true;
        }

        private void Btnguardar_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (!ValidarCampos()) return;

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    if (_esEdicion)
                    {
                        // UPDATE - Editar documento existente
                        string consulta = @"UPDATE Documento 
                                            SET id_proyecto = @id_proyecto,
                                                nombre = @nombre,
                                                tipoArchivo = @tipoArchivo,
                                                url = @url,
                                                fechaSubida = @fechaSubida
                                            WHERE id_documento = @id_documento";

                        SqlCommand command = new SqlCommand(consulta, conexion);
                        command.Parameters.AddWithValue("@id_documento", _documentoEditando.id_documento);
                        command.Parameters.AddWithValue("@id_proyecto", Convert.ToInt32(txtIdProyecto.Text));
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@tipoArchivo", txtTipoArchivo.Text);
                        command.Parameters.AddWithValue("@url", txtUrl.Text);
                        command.Parameters.AddWithValue("@fechaSubida", Fecha_subidadtp.Value);

                        int ejecutar = command.ExecuteNonQuery();

                        if (ejecutar > 0)
                        {
                            MessageBox.Show("Documento actualizado exitosamente");
                            Datos_guardados = true;
                            this.FindForm()?.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error al actualizar el documento");
                        }
                    }
                    else
                    {
                        // INSERT - Nuevo documento
                        string consulta = @"INSERT INTO Documento 
                                            (id_proyecto, nombre, tipoArchivo, url, fechaSubida) 
                                            VALUES 
                                            (@id_proyecto, @nombre, @tipoArchivo, @url, @fechaSubida)";

                        SqlCommand command = new SqlCommand(consulta, conexion);
                        command.Parameters.AddWithValue("@id_proyecto", Convert.ToInt32(txtIdProyecto.Text));
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@tipoArchivo", txtTipoArchivo.Text);
                        command.Parameters.AddWithValue("@url", txtUrl.Text);
                        command.Parameters.AddWithValue("@fechaSubida", Fecha_subidadtp.Value);

                        int ejecutar = command.ExecuteNonQuery();

                        if (ejecutar > 0)
                        {
                            MessageBox.Show("Documento guardado exitosamente");
                            Datos_guardados = true;
                            this.FindForm()?.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error al guardar el documento");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DocumentosControl3_Load(object sender, EventArgs e)
        {

        }
    }
}
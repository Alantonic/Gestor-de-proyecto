using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Proyecto.Conexion_Base_Datos;
using Proyecto.Modelos;
using Proyecto.Repositorio;

namespace Proyecto.Forms
{
    public partial class btnAgregar : Form
    {
        private TareasModel _tareaEditando;
        private bool _esEdicion = false;
        public bool DatosGuardados { get; private set; }

        // Constructor para NUEVA tarea
        public btnAgregar()
        {
            InitializeComponent();
            DatosGuardados = false;
            _esEdicion = false;
            _tareaEditando = null;
            ConfigurarParaNueva();
            CargarCombos();

            // Conectar eventos
            btnAdd.Click += btnAdd_Click;
            btnRegresar.Click += btnRegresar_Click;
        }

        // Constructor para EDITAR tarea
        public btnAgregar(TareasModel tarea)
        {
            InitializeComponent();
            DatosGuardados = false;
            _esEdicion = true;
            _tareaEditando = tarea;
            CargarCombos();
            CargarDatosEnFormulario();
            ConfigurarParaEdicion();

            // Conectar eventos
            btnEditar.Click += btnEditar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnRegresar.Click += btnRegresar_Click;
        }

        private void ConfigurarParaNueva()
        {
            this.Text = "Módulo de Tareas - Nueva Tarea";
            btnAdd.Visible = true;
            btnEditar.Visible = true;
            btnEliminar.Visible = true;
            LimpiarCampos();
        }

        private void ConfigurarParaEdicion()
        {
            this.Text = "Módulo de Tareas - Editar Tarea";
            btnAdd.Visible = true;      // Oculta el botón Agregar
            btnEditar.Visible = true;    // Muestra Editar
            btnEliminar.Visible = true;  // Muestra Eliminar
        }

        private void LimpiarCampos()
        {
            Titulotxt.Clear();
            Responsabletxt.SelectedIndex = -1;
            Proyectotxt.SelectedIndex = -1;
            Prioridadtxt.SelectedIndex = -1;
            Fecha_Iniciotxt.Value = DateTime.Now;
            Fecha_Vencimiento.Value = DateTime.Now.AddDays(7);
            Estimaciontxt.Value = 0;
            Descripciontxt.Clear();
        }

        private void CargarCombos()
        {
            Prioridadtxt.Items.Clear();
            Prioridadtxt.Items.AddRange(new string[] { "Alta", "Media", "Baja" });
        }

        private void CargarDatosEnFormulario()
        {
            if (_tareaEditando != null)
            {
                Titulotxt.Text = _tareaEditando.titulo;
                Responsabletxt.Text = _tareaEditando.ID.ToString();
                Proyectotxt.Text = _tareaEditando.id_proyecto.ToString();
                Prioridadtxt.Text = _tareaEditando.prioridad;
                Fecha_Iniciotxt.Value = _tareaEditando.fehca_Inicio;
                Fecha_Vencimiento.Value = _tareaEditando.fechaVencimiento;
                Estimaciontxt.Value = _tareaEditando.horaEstimadas;
                Descripciontxt.Text = _tareaEditando.descripcion;
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(Titulotxt.Text))
            {
                MessageBox.Show("El título es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Titulotxt.Focus();
                return false;
            }

            if (Responsabletxt.SelectedIndex == -1 && string.IsNullOrWhiteSpace(Responsabletxt.Text))
            {
                MessageBox.Show("El responsable es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Responsabletxt.Focus();
                return false;
            }

            if (Proyectotxt.SelectedIndex == -1 && string.IsNullOrWhiteSpace(Proyectotxt.Text))
            {
                MessageBox.Show("El proyecto es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Proyectotxt.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Prioridadtxt.Text))
            {
                MessageBox.Show("La prioridad es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Prioridadtxt.Focus();
                return false;
            }

            if (Fecha_Vencimiento.Value < Fecha_Iniciotxt.Value)
            {
                MessageBox.Show("La fecha de vencimiento no puede ser menor a la fecha de inicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Botón AGREGAR (INSERT)
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"INSERT INTO Tarea 
                                        (id_proyecto, ID, titulo, descripcion, prioridad, estado, fehca_Inicio, fechaVencimiento, horaEstimadas) 
                                        VALUES 
                                        (@id_proyecto, @ID, @titulo, @descripcion, @prioridad, @estado, @fehca_Inicio, @fechaVencimiento, @horaEstimadas)";

                    SqlCommand command = new SqlCommand(consulta, conexion);
                    command.Parameters.AddWithValue("@id_proyecto", Convert.ToInt32(Proyectotxt.Text));
                    command.Parameters.AddWithValue("@ID", Convert.ToInt32(Responsabletxt.Text));
                    command.Parameters.AddWithValue("@titulo", Titulotxt.Text);
                    command.Parameters.AddWithValue("@descripcion", Descripciontxt.Text);
                    command.Parameters.AddWithValue("@prioridad", Prioridadtxt.Text);
                    command.Parameters.AddWithValue("@estado", "Pendiente");
                    command.Parameters.AddWithValue("@fehca_Inicio", Fecha_Iniciotxt.Value);
                    command.Parameters.AddWithValue("@fechaVencimiento", Fecha_Vencimiento.Value);
                    command.Parameters.AddWithValue("@horaEstimadas", Convert.ToInt32(Estimaciontxt.Value));

                    int ejecutar = command.ExecuteNonQuery();

                    if (ejecutar > 0)
                    {
                        MessageBox.Show("Tarea guardada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DatosGuardados = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar la tarea", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Botón EDITAR (UPDATE)
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"UPDATE Tarea 
                                        SET id_proyecto = @id_proyecto,
                                            ID = @ID,
                                            titulo = @titulo,
                                            descripcion = @descripcion,
                                            prioridad = @prioridad,
                                            fehca_Inicio = @fehca_Inicio,
                                            fechaVencimiento = @fechaVencimiento,
                                            horaEstimadas = @horaEstimadas
                                        WHERE id_tarea = @id_tarea";

                    SqlCommand command = new SqlCommand(consulta, conexion);
                    command.Parameters.AddWithValue("@id_tarea", _tareaEditando.id_tarea);
                    command.Parameters.AddWithValue("@id_proyecto", Convert.ToInt32(Proyectotxt.Text));
                    command.Parameters.AddWithValue("@ID", Convert.ToInt32(Responsabletxt.Text));
                    command.Parameters.AddWithValue("@titulo", Titulotxt.Text);
                    command.Parameters.AddWithValue("@descripcion", Descripciontxt.Text);
                    command.Parameters.AddWithValue("@prioridad", Prioridadtxt.Text);
                    command.Parameters.AddWithValue("@fehca_Inicio", Fecha_Iniciotxt.Value);
                    command.Parameters.AddWithValue("@fechaVencimiento", Fecha_Vencimiento.Value);
                    command.Parameters.AddWithValue("@horaEstimadas", Convert.ToInt32(Estimaciontxt.Value));

                    int ejecutar = command.ExecuteNonQuery();

                    if (ejecutar > 0)
                    {
                        MessageBox.Show("Tarea actualizada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DatosGuardados = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar la tarea", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Botón ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_tareaEditando == null) return;

            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar esta tarea?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conexion = BD.ObtenerConexion())
                    {
                        string consulta = "DELETE FROM Tarea WHERE id_tarea = @id_tarea";
                        SqlCommand command = new SqlCommand(consulta, conexion);
                        command.Parameters.AddWithValue("@id_tarea", _tareaEditando.id_tarea);

                        int ejecutar = command.ExecuteNonQuery();

                        if (ejecutar > 0)
                        {
                            MessageBox.Show("Tarea eliminada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DatosGuardados = true;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar la tarea", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Botón REGRESAR
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            Main mainForm = new Main();
            mainForm.Show();
            this.Close();
        }
    }
}
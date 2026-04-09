using System;
using System.Windows.Forms;
using Proyecto.Clases;
using Proyecto.Modelos;
using Proyecto.Repositorio;
using Proyecto.Forms;


namespace Proyecto.Interfaces
{
    public partial class TareasView : Form
    {
        public TareasView()
        {
            InitializeComponent();
            IniatializateTareasDataGridView();
            InitializeContextMenu();
            Tareas_Settings();
        }

        // Inicializar DataGridView con datos
        private void IniatializateTareasDataGridView()
        {
            try
            {
                var repositorio = new TareaRepository();
                dataGridView1.DataSource = repositorio.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error en los datos: " + ex.ToString());
            }
        }

        // Configurar columnas del DataGridView
        // Configurar columnas del DataGridView
        private void Tareas_Settings()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Columna ID Tarea - AHORA CON NAME
            DataGridViewTextBoxColumn colIdTarea = new DataGridViewTextBoxColumn();
            colIdTarea.Name = "id_tarea";              // ← Nombre interno (para acceder por código)
            colIdTarea.DataPropertyName = "id_tarea";  // ← Propiedad del modelo
            colIdTarea.HeaderText = "ID Tarea";        // ← Lo que ve el usuario
            colIdTarea.Width = 80;
            colIdTarea.ReadOnly = true;
            dataGridView1.Columns.Add(colIdTarea);

            // Columna Título
            DataGridViewTextBoxColumn colTitulo = new DataGridViewTextBoxColumn();
            colTitulo.Name = "titulo";
            colTitulo.DataPropertyName = "titulo";
            colTitulo.HeaderText = "Título";
            colTitulo.Width = 150;
            dataGridView1.Columns.Add(colTitulo);

            // Columna Descripción
            DataGridViewTextBoxColumn colDescripcion = new DataGridViewTextBoxColumn();
            colDescripcion.Name = "descripcion";
            colDescripcion.DataPropertyName = "descripcion";
            colDescripcion.HeaderText = "Descripción";
            colDescripcion.Width = 200;
            dataGridView1.Columns.Add(colDescripcion);

            // Columna Prioridad
            DataGridViewTextBoxColumn colPrioridad = new DataGridViewTextBoxColumn();
            colPrioridad.Name = "prioridad";
            colPrioridad.DataPropertyName = "prioridad";
            colPrioridad.HeaderText = "Prioridad";
            colPrioridad.Width = 100;
            dataGridView1.Columns.Add(colPrioridad);

            // Columna Estado
            DataGridViewTextBoxColumn colEstado = new DataGridViewTextBoxColumn();
            colEstado.Name = "estado";
            colEstado.DataPropertyName = "estado";
            colEstado.HeaderText = "Estado";
            colEstado.Width = 100;
            dataGridView1.Columns.Add(colEstado);

            // Columna Fecha Vencimiento
            DataGridViewTextBoxColumn colFechaVenc = new DataGridViewTextBoxColumn();
            colFechaVenc.Name = "fechaVencimiento";
            colFechaVenc.DataPropertyName = "fechaVencimiento";
            colFechaVenc.HeaderText = "Fecha Vencimiento";
            colFechaVenc.Width = 120;
            dataGridView1.Columns.Add(colFechaVenc);

            // Columna Horas Estimadas
            DataGridViewTextBoxColumn colHoras = new DataGridViewTextBoxColumn();
            colHoras.Name = "horaEstimadas";
            colHoras.DataPropertyName = "horaEstimadas";
            colHoras.HeaderText = "Horas Estimadas";
            colHoras.Width = 100;
            dataGridView1.Columns.Add(colHoras);

            // Configuración visual
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
        }

        // Configurar menú contextual
        private void InitializeContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem editItem = new ToolStripMenuItem("Editar");
            editItem.Click += EditTareaButton_Click;
            contextMenu.Items.Add(editItem);

            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Eliminar");
            deleteItem.Click += DeleteTareaButton_Click;
            contextMenu.Items.Add(deleteItem);

            dataGridView1.ContextMenuStrip = contextMenu;
        }

        // Editar tarea
        // Editar tarea
        // Editar tarea
        private void EditTareaButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una tarea para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridView1.SelectedRows[0];
            if (selectedRow == null) return;

            // Usar el nombre interno de la columna "id_tarea"
            int id_tarea = Convert.ToInt32(selectedRow.Cells["id_tarea"].Value);

            var repo = new TareaRepository();
            var tareaSeleccionada = repo.Find(id_tarea);

            if (tareaSeleccionada == null) return;

            // Abrir el formulario Tareas en modo edición
            btnAgregar formTareas = new btnAgregar(tareaSeleccionada);  // ← Pasar la tarea a editar
            formTareas.ShowDialog();

            IniatializateTareasDataGridView(); // Refrescar después de cerrar
        }


        // Eliminar tarea
        // Eliminar tarea
        private void DeleteTareaButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            var selectedRow = dataGridView1.SelectedRows[0];
            if (selectedRow == null) return;

            // Usar el nombre interno de la columna "id_tarea"
            int id_tarea = Convert.ToInt32(selectedRow.Cells["id_tarea"].Value);

            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar esta tarea?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                var repo = new TareaRepository();
                bool eliminado = repo.Delete(id_tarea);

                if (eliminado)
                {
                    MessageBox.Show("Tarea eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    IniatializateTareasDataGridView();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la tarea.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TareasView_Load(object sender, EventArgs e)
        {
        }

        private void TareasView_Load_1(object sender, EventArgs e)
        {

        }
    }
}
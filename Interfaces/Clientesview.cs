using System;
using System.Windows.Forms;
using Proyecto.Repositorio;
using Proyecto.Controles;

namespace Proyecto.Interfaces
{
    public partial class ClientesView : Form
    {
        public ClientesView()
        {
            InitializeComponent();
            Cliente_Settings();
            IniatializateClientesDataGridView();
            InitializeContextMenu();
        }

        // Configurar columnas del DataGridView
        private void Cliente_Settings()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Columna ID Cliente
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "Id_Cliente";
            colId.DataPropertyName = "Id_Cliente";
            colId.HeaderText = "ID Cliente";
            colId.Width = 80;
            colId.ReadOnly = true;
            dataGridView1.Columns.Add(colId);

            // Columna Nombre
            DataGridViewTextBoxColumn colNombre = new DataGridViewTextBoxColumn();
            colNombre.Name = "Nombre";
            colNombre.DataPropertyName = "Nombre";
            colNombre.HeaderText = "Nombre";
            colNombre.Width = 120;
            dataGridView1.Columns.Add(colNombre);

            // Columna Apellido
            DataGridViewTextBoxColumn colApellido = new DataGridViewTextBoxColumn();
            colApellido.Name = "Apellido";
            colApellido.DataPropertyName = "Apellido";
            colApellido.HeaderText = "Apellido";
            colApellido.Width = 120;
            dataGridView1.Columns.Add(colApellido);

            // Columna Teléfono
            DataGridViewTextBoxColumn colTelefono = new DataGridViewTextBoxColumn();
            colTelefono.Name = "Telefono";
            colTelefono.DataPropertyName = "Telefono";
            colTelefono.HeaderText = "Teléfono";
            colTelefono.Width = 100;
            dataGridView1.Columns.Add(colTelefono);

            // Columna Gmail
            DataGridViewTextBoxColumn colGmail = new DataGridViewTextBoxColumn();
            colGmail.Name = "Gmail";
            colGmail.DataPropertyName = "Gmail";
            colGmail.HeaderText = "Gmail";
            colGmail.Width = 150;
            dataGridView1.Columns.Add(colGmail);

            // Configuración adicional
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
        }

        // Inicializar DataGridView con datos
        private void IniatializateClientesDataGridView()
        {
            try
            {
                var repositorio = new ClientesRepository();
                dataGridView1.DataSource = repositorio.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error en los datos: " + ex.ToString());
            }
        }

        // Configurar menú contextual
        private void InitializeContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem editItem = new ToolStripMenuItem("Editar");
            editItem.Click += EditClienteButton_Click;
            contextMenu.Items.Add(editItem);

            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Eliminar");
            deleteItem.Click += DeleteClienteButton_Click;
            contextMenu.Items.Add(deleteItem);

            dataGridView1.ContextMenuStrip = contextMenu;
        }
        
        // Editar cliente
        private void EditClienteButton_Click(object sender, EventArgs e)
        {
            // Verifica que haya una celda seleccionada
            if (dataGridView1.SelectedCells.Count == 0) return;

            var selectedCell = dataGridView1.SelectedCells[0];
            if (selectedCell == null) return;

            var selectedRow = dataGridView1.Rows[selectedCell.RowIndex];
            if (selectedRow == null) return;

            // Obtiene el ID del cliente usando el nombre de la columna
            int id_cliente = Convert.ToInt32(selectedRow.Cells["Id_Cliente"].Value);

            var repo = new ClientesRepository();
            var selectedCliente = repo.Find(id_cliente);

            if (selectedCliente == null) return;

            // Abre el formulario de edición
            Form form = new Form();
            form.Text = "Editar Cliente";
            form.AutoSize = true;
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;

            ClientesControl3 controlCliente = new ClientesControl3(selectedCliente);
            controlCliente.Dock = DockStyle.Fill;
            form.Controls.Add(controlCliente);
            form.ShowDialog();

            IniatializateClientesDataGridView(); // Actualiza la vista después de editar
        }

        // Eliminar cliente
        private void DeleteClienteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0) return;

            var selectedCell = dataGridView1.SelectedCells[0];
            if (selectedCell == null) return;

            var selectedRow = dataGridView1.Rows[selectedCell.RowIndex];
            if (selectedRow == null) return;

            int id_cliente = Convert.ToInt32(selectedRow.Cells["Id_Cliente"].Value);

            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este cliente?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                var repo = new ClientesRepository();
                bool eliminado = repo.Delete(id_cliente);

                if (eliminado)
                {
                    MessageBox.Show("Cliente eliminado exitosamente", "Hecho", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    IniatializateClientesDataGridView();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Botón para NUEVO cliente (opcional - agrégalo en el diseñador)
        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            form.Text = "Nuevo Cliente";
            form.FormBorderStyle = FormBorderStyle.FixedDialog;  // Borde fijo
            form.StartPosition = FormStartPosition.CenterParent; // Centrar
            form.MaximizeBox = false;  // Deshabilitar maximizar
            form.MinimizeBox = false;  // Deshabilitar minimizar
            form.ControlBox = true;     // Mantener botón cerrar
            form.AutoSize = false;      // No autoajustar

            form.AutoSize = true;
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;

            ClientesControl3 controlCliente = new ClientesControl3();
            controlCliente.Dock = DockStyle.Fill;
            form.Controls.Add(controlCliente);
            form.ShowDialog();
            Close();

            IniatializateClientesDataGridView();
        }

        private void ClientesView_Load(object sender, EventArgs e)
        {

        }
    }
}

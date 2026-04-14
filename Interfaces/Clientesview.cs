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
            IniatializateClientesDataGridView();
            InitializeContextMenu();
        }

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

        private void EditClienteButton_Click(object sender, EventArgs e)
        {
            // Verifica que haya una celda seleccionada
            var selectedCell = dataGridView1.SelectedCells[0];
            if (selectedCell == null) return;

            var selectedRow = dataGridView1.Rows[selectedCell.RowIndex];
            if (selectedRow == null) return;

            int id_cliente = Convert.ToInt32(selectedRow.Cells["Id_Cliente"].Value);

            var repo = new ClientesRepository();
            var selectedCliente = repo.Find(id_cliente);

            if (selectedCliente == null) return;

            // Abre el ClientesControl3 (como UserControl2)
            Form form = new Form();
            form.Text = "Editar Cliente";
            form.AutoSize = true;
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;

            ClientesControl3 controlCliente = new ClientesControl3(selectedCliente);
            form.ClientSize = controlCliente.Size;     
            form.MinimumSize = controlCliente.Size;     
            form.MaximumSize = controlCliente.Size;    
            controlCliente.Dock = DockStyle.Fill;
            form.Controls.Add(controlCliente);
            form.ShowDialog();

            IniatializateClientesDataGridView(); // Actualiza la vista después de editar
        }

        // ELIMINAR Cliente
        private void DeleteClienteButton_Click(object sender, EventArgs e)
        {
            var selectedCell = dataGridView1.SelectedCells[0];
            if (selectedCell == null) return;

            var selectedRow = dataGridView1.Rows[selectedCell.RowIndex];
            if (selectedRow == null) return;

            var repo = new ClientesRepository();
            var selectedCliente = repo.Find(Convert.ToInt32(selectedRow.Cells["Id_Cliente"].Value));

            if (selectedCliente == null) return;

            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este cliente?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                bool isDeleted = new ClientesRepository().Delete(selectedCliente.Id_Cliente);
                if (isDeleted)
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

        private void ClientesView_Load(object sender, EventArgs e)
        {
        }
    }
}
using System;
using System.Windows.Forms;
using Proyecto.Clases;
using Proyecto.Controles;

namespace Proyecto.Forms
{
    public partial class Usuarioview : Form
    {
        public object UsuarioView { get; internal set; }

        public Usuarioview()
        {
            InitializeComponent();

            // Configurar TODO antes de cargar datos
            ConfigureDataGridView();
            InitializeContextMenu();
            LoadUserData();
        }

        // Configuración completa del DataGridView
        private void ConfigureDataGridView()
        {
            // Limpiar cualquier configuración previa
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Agregar columnas MANUALMENTE con nombres exactos
            DataGridViewTextBoxColumn colID = new DataGridViewTextBoxColumn();
            colID.Name = "ID";                    // ← Nombre interno de la columna
            colID.DataPropertyName = "ID";        // ← Propiedad del modelo
            colID.HeaderText = "ID";
            colID.Width = 80;
            colID.ReadOnly = true;
            dataGridView1.Columns.Add(colID);

            DataGridViewTextBoxColumn colNombre = new DataGridViewTextBoxColumn();
            colNombre.Name = "Nombre";
            colNombre.DataPropertyName = "Nombre";
            colNombre.HeaderText = "Nombre";
            colNombre.Width = 150;
            dataGridView1.Columns.Add(colNombre);

            DataGridViewTextBoxColumn colTelefono = new DataGridViewTextBoxColumn();
            colTelefono.Name = "Telefono";
            colTelefono.DataPropertyName = "Telefono";
            colTelefono.HeaderText = "Teléfono";
            colTelefono.Width = 120;
            dataGridView1.Columns.Add(colTelefono);

            DataGridViewTextBoxColumn colGmail = new DataGridViewTextBoxColumn();
            colGmail.Name = "Gmail";
            colGmail.DataPropertyName = "Gmail";
            colGmail.HeaderText = "Gmail";
            colGmail.Width = 200;
            dataGridView1.Columns.Add(colGmail);

            // Configuración visual
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;

            // Verificar que las columnas se crearon
            Console.WriteLine($"Número de columnas: {dataGridView1.Columns.Count}");
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                Console.WriteLine($"Columna: {col.Name} - DataPropertyName: {col.DataPropertyName}");
            }
        }

        // Cargar datos
        private void LoadUserData()
        {
            try
            {
                var repositorio = new UsuarioRepository();
                var usuarios = repositorio.GetAll();

                if (usuarios == null || usuarios.Count == 0)
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("No hay usuarios para mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dataGridView1.DataSource = usuarios;

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        // 2. Editar usuario (menú contextual) - CORREGIDO
        private void EditUsuarioButton_Click(object sender, EventArgs e)
        {
            // Verificar que hay filas
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay usuarios para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar que hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener la fila seleccionada
            var selectedRow = dataGridView1.SelectedRows[0];
            if (selectedRow == null || selectedRow.IsNewRow) return;

            // Verificar que la columna existe
            if (!dataGridView1.Columns.Contains("ID"))
            {
                MessageBox.Show("Error: La columna ID no existe en el DataGridView.", "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtener el ID usando el índice de la columna (más seguro)
            int idColumnIndex = dataGridView1.Columns["ID"].Index;
            int ID = Convert.ToInt32(selectedRow.Cells[idColumnIndex].Value);

            var repo = new UsuarioRepository();
            var selecteduser = repo.Find(ID);

            if (selecteduser == null)
            {
                MessageBox.Show("No se encontró el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Abrir formulario de edición
            Form form = new Form();
            form.Text = "Editar Usuario";
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.MaximizeBox = false;
            form.MinimizeBox = false;

            UserControl1 userControl = new UserControl1(selecteduser);
            form.ClientSize = userControl.Size;
            form.MinimumSize = userControl.Size;
            form.MaximumSize = userControl.Size;
            userControl.Dock = DockStyle.Fill;
            form.Controls.Add(userControl);
            form.ShowDialog();
            Close();

            // Recargar datos después de editar
            LoadUserData();
        }

        // 3. Eliminar usuario (menú contextual) - CORREGIDO
        private void deleteUsuarioButton_Click(object sender, EventArgs e)
        {
            // Verificar que hay filas
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay usuarios para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar que hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener la fila seleccionada
            var selectedRow = dataGridView1.SelectedRows[0];
            if (selectedRow == null || selectedRow.IsNewRow) return;

            // Verificar que la columna existe
            if (!dataGridView1.Columns.Contains("ID"))
            {
                MessageBox.Show("Error: La columna ID no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtener el ID
            int idColumnIndex = dataGridView1.Columns["ID"].Index;
            int ID = Convert.ToInt32(selectedRow.Cells[idColumnIndex].Value);

            // Confirmar eliminación
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este usuario?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                var repo = new UsuarioRepository();
                bool eliminado = repo.Delete(ID);

                if (eliminado)
                {
                    MessageBox.Show("Usuario eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUserData(); // Recargar datos
                }
                else
                {
                    MessageBox.Show("Error al eliminar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 4. Configurar menú contextual
        private void InitializeContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem editItem = new ToolStripMenuItem("Editar");
            editItem.Click += EditUsuarioButton_Click;
            contextMenu.Items.Add(editItem);

            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Eliminar");
            deleteItem.Click += deleteUsuarioButton_Click;
            contextMenu.Items.Add(deleteItem);

            dataGridView1.ContextMenuStrip = contextMenu;
        }

        private void Usuarioview_Load(object sender, EventArgs e)
        {
            // Cualquier lógica adicional
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.ShowDialog();
        }
    }
}
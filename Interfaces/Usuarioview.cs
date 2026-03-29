using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto.Clases;
using Proyecto.Controles;
using Proyecto.Interfaces;
using Proyecto.Modelos;
using Proyecto.Repositorio;
using static Proyecto.Modelos.ProyectosModel;


namespace Proyecto.Forms
{
    public partial class Usuarioview : Form
    {
        public object UsuarioView { get; internal set; }

        public Usuarioview()
        {
            InitializeComponent();
            IniatializateUsuarioDataGridView();
            InitializeContextMenu();
            Usuario_Settings();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void IniatializateUsuarioDataGridView()
        {
            try
            {
                var repositorio = new UsuarioRepository();
                dataGridView1.DataSource = repositorio.GetAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error en los datos: " + ex.ToString());
            }
        }




        private void EditUsuarioButton_Click(object sender, EventArgs e)
        {

            // Verifica que haya una celda seleccionada
            var selectedCell = dataGridView1.SelectedCells[0];
            if (selectedCell == null) return;

            // Obtiene la fila seleccionada

            var selectedRow = dataGridView1.Rows[selectedCell.RowIndex];
            if (selectedRow == null) return;

            // Obtiene el producto seleccionado usando el valor de la celda "code"

            int ID = Convert.ToInt32(selectedRow.Cells["ID"].Value);

            var repo = new UsuarioRepository();
            var selecteduser = repo.Find(ID);

            if (selecteduser == null) return;





            // Abre el usercontrol1
            Form form = new Form();
            form.Text = "Editar Usuario";
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            UserControl1 userControl = new UserControl1(selecteduser);
            userControl.Dock = DockStyle.Fill;
            form.Controls.Add(userControl);
            form.ShowDialog();

            IniatializateUsuarioDataGridView(); // Actualiza la vista después de editar



        }




        private void deleteUsuarioButton_Click(object sender, EventArgs e)
        {
            var selectedCell = dataGridView1.SelectedCells[0];
            if (selectedCell == null) return;

            // Obtiene la fila seleccionada

            var selectedRow = dataGridView1.Rows[selectedCell.RowIndex];
            if (selectedRow == null) return;

            // Obtiene el producto seleccionado usando el valor de la celda "code"

            int ID = Convert.ToInt32(selectedRow.Cells["ID"].Value);


            //busca usuario por ID
            var repo = new UsuarioRepository();
            var selecteduser = repo.Find(ID);

            if (selecteduser == null) return;

            // Abre el formulario de edición con el producto seleccionado
            DialogResult result = MessageBox.Show
                (
                    "¿Estás seguro de que deseas eliminar este usuario?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
            if (result == DialogResult.Yes)
            {
                // bool eliminado = repo.Delete(ID);
                bool eliminado = repo.Delete(ID);
                if (eliminado)
                {
                    MessageBox.Show("Usuario eliminado exitosamente.");
                    IniatializateUsuarioDataGridView(); // Actualiza la vista después de eliminar
                }
                else
                {
                    MessageBox.Show("Error al eliminar el usuario.");
                }
            }
        }

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







        private void Usuario_Settings() // Configura las columnas del DataGridView
        {
            dataGridView1.AutoGenerateColumns = true; // Desactiva la generación automática de columnas
            dataGridView1.Columns.Clear();

            // Asigna al DataGridView
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ID",
                HeaderText = "ID",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Telefono",
                HeaderText = "Telefono",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Gmail",
                HeaderText = "Gmail",
                Width = 100
            });
        }
    }
}

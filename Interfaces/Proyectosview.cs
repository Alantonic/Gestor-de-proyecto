using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Proyecto.Clases;
using Proyecto.Controles;
using Proyecto.Forms;
using Proyecto.Repositorio;
using static Proyecto.Modelos.ProyectosModel;

namespace Proyecto.Interfaces
{
    public partial class Proyectosview : Form
    {
        public object ProyectosView { get; internal set; }
        public Proyectosview()
        {

            InitializeComponent();
            IniatializateProyectosDataGridView();
            InitializeContextMenu();

        }

        private void IniatializateProyectosDataGridView()
        {
            try
            {
                var repositorio = new ProyectoRepository();
                dataGridView1.DataSource = repositorio.GetAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error en los datos: " + ex.ToString());
            }
        }


        private void EditProyectoButton_Click(object sender, EventArgs e)
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

            IniatializateProyectosDataGridView(); // Actualiza la vista después de editar



        }

        /// Este método configura el menú contextual para el DataGridView, permitiendo acciones como editar al hacer clic derecho en una fila.
        private void InitializeContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem editItem = new ToolStripMenuItem("Editar");
            editItem.Click += EditUsuarioButton_Click;
            contextMenu.Items.Add(editItem);

            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Eliminar");
            deleteItem.Click += DeleteProyectoButton_Click;
            contextMenu.Items.Add(deleteItem);

            dataGridView1.ContextMenuStrip = contextMenu;



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

            int id_proyecto = Convert.ToInt32(selectedRow.Cells["ID"].Value);

            var repo = new ProyectoRepository();
            var selecteduser2 = repo.Find(id_proyecto);

            if (selecteduser2 == null) return;





            // Abre el usercontrol1
            Form form = new Form();
            form.Text = "Editar Usuario";
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;

            UserControl2 userControl2 = new UserControl2(selecteduser2);
            form.ClientSize = userControl2.Size; // Ajusta el tamaño del formulario al del UserControl
            form.MinimumSize = userControl2.Size; // Establece el tamaño mínimo para evitar que se reduzca demasiado
            form.MaximumSize = userControl2.Size; // Establece el tamaño máximo para evitar que se agrande demasiado
            userControl2.Dock = DockStyle.Fill;
            form.Controls.Add(userControl2);
            form.ShowDialog();

            IniatializateProyectosDataGridView(); // Actualiza la vista después de editar



        }

        private void DeleteProyectoButton_Click(object sender, EventArgs e)
        {
            var selectedCell = dataGridView1.SelectedCells[0];
            if (selectedCell == null) return;
            var selectedRow = dataGridView1.Rows[selectedCell.RowIndex];
            if (selectedRow == null) return;
            var repo = new ProyectoRepository();
            var selectedProduct = repo.Find(Convert.ToInt32(selectedRow.Cells["ID"].Value));
            if (selectedProduct == null) return;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this product?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (result == DialogResult.Yes)
            {
                var isProductDeleted = new ProyectoRepository().Delete(selectedProduct.id_proyecto);
                if (isProductDeleted)
                {
                    MessageBox.Show(
                        "Product deleted succesfully",
                        "Done",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    IniatializateProyectosDataGridView();
                }
            }
            else
            {
                MessageBox.Show(
                    "Foreign key error. The product cant be deleted ",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        /// Este método se encarga de actualizar la lista de proyectos en el DataGridView, obteniendo los datos más recientes del repositorio.
        private void UpdateDatagridView()
        {
            try
            {
                ProyectoRepository repo = new ProyectoRepository();
                List<ProyectoModel> proyectos = repo.GetAll();

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = proyectos;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proyectos: " + ex.Message);
            }

          

       
        }




        private void Proyectosview_Load(object sender, EventArgs e)
        {

        }
    }
 }

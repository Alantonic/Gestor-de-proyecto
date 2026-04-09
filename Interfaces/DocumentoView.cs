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
using Proyecto.Modelos;
using Proyecto.Repositorio;
using static Proyecto.Modelos.DocumentoModel;

namespace Proyecto.Interfaces
{
    public partial class Documentosview : Form
    {
        public object DocumentosView { get; internal set; }

        public Documentosview()
        {
            InitializeComponent();
            IniatializateDocumentosDataGridView();
            InitializeContextMenu();
        }

        private void IniatializateDocumentosDataGridView()
        {
            try
            {
                var repositorio = new DocumentoRepository();
                dataGridView1.DataSource = repositorio.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error en los datos: " + ex.ToString());
            }
        }

        /// Este método configura el menú contextual para el DataGridView
        private void InitializeContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem editItem = new ToolStripMenuItem("Editar");
            editItem.Click += EditDocumentoButton_Click;
            contextMenu.Items.Add(editItem);

            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Eliminar");
            deleteItem.Click += DeleteDocumentoButton_Click;
            contextMenu.Items.Add(deleteItem);

            dataGridView1.ContextMenuStrip = contextMenu;
        }

        // MÉTODO PARA EDITAR - Abre DocumentosControl (igual que UserControl2 en Proyectosview)
        private void EditDocumentoButton_Click(object sender, EventArgs e)
        {
            // Verifica que haya una celda seleccionada
            if (dataGridView1.SelectedCells.Count == 0) return;

            var selectedCell = dataGridView1.SelectedCells[0];
            if (selectedCell == null) return;

            var selectedRow = dataGridView1.Rows[selectedCell.RowIndex];
            if (selectedRow == null) return;

            // Usa el índice 0 para la columna ID
            int id_documento = Convert.ToInt32(selectedRow.Cells[0].Value);

            var repo = new DocumentoRepository();
            var selectedDocumento = repo.Find(id_documento);

            if (selectedDocumento == null) return;

            Form form = new Form();
            form.Text = "Editar Documento";
            form.FormBorderStyle = FormBorderStyle.FixedDialog;  // Evita redimensionar
            form.StartPosition = FormStartPosition.CenterParent; // Centrar en pantalla
            form.ControlBox = true;  // Mantener botones de cerrar
           
            form.AutoSize = false;
            form.AutoSizeMode = AutoSizeMode.GrowOnly;

            DocumentosControl3 controlDocumento = new DocumentosControl3(selectedDocumento);

            // Ajustar el tamaño del formulario al UserControl
            form.Size = controlDocumento.Size;
            form.MinimumSize = controlDocumento.Size;
            form.MaximumSize = controlDocumento.Size;

            controlDocumento.Dock = DockStyle.Fill;
            form.Controls.Add(controlDocumento);
            form.ShowDialog();
            IniatializateDocumentosDataGridView();


        }

        // MÉTODO PARA ELIMINAR
        private void DeleteDocumentoButton_Click(object sender, EventArgs e)
        {
            var selectedCell = dataGridView1.SelectedCells[0];
            if (selectedCell == null) return;

            var selectedRow = dataGridView1.Rows[selectedCell.RowIndex];
            if (selectedRow == null) return;

            var repo = new DocumentoRepository();
            var selectedDocumento = repo.Find(Convert.ToInt32(selectedRow.Cells["ID"].Value));
            if (selectedDocumento == null) return;

            DialogResult result = MessageBox.Show(
                "¿Está seguro que desea eliminar este documento?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                var isDeleted = new DocumentoRepository().Delete(selectedDocumento.id_documento);
                if (isDeleted)
                {
                    MessageBox.Show(
                        "Documento eliminado correctamente",
                        "Completado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    IniatializateDocumentosDataGridView();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo eliminar el documento. Verifique que no esté siendo utilizado.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        // MÉTODO PARA ACTUALIZAR EL DataGridView
        private void UpdateDataGridView()
        {
            try
            {
                DocumentoRepository repo = new DocumentoRepository();
                List<DocumentoModel> documentos = repo.GetAll();

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = documentos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar documentos: " + ex.Message);
            }
        }

        private void Documentosview_Load(object sender, EventArgs e)
        {

        }

        private void Documentosview_Load_1(object sender, EventArgs e)
        {

        }
    }
}
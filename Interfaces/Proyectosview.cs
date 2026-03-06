using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto.Clases;

namespace Proyecto.Interfaces
{
    public partial class Proyectosview : Form
    {
        public Proyectosview()
        {
            InitializeComponent();
            this.Load += Proyecto_load;        
               }

        private void Proyecto_load(object sender, EventArgs e)
        {
            List<Proyectos> proyectos = null;
            try
            {
                proyectos = Proyectos.Representar();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en Presentar(): " + ex.ToString());
                return;
            }
            if (dataGridView1 == null)
            {
                MessageBox.Show("Error: dataGridView1 es null");
                return;
            }

            // Asigna al DataGridView
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "id_proyecto",
                HeaderText = "id_proyecto",
                Width = 100
            });
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
                DataPropertyName = "descripcion",
                HeaderText = "descripcion",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Fecha_Inicio",
                HeaderText = "Fecha_Inicio",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Fecha_fin",
                HeaderText = "Fecha_fin",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Estado",
                HeaderText = "Estado",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Presupuesto",
                HeaderText = "Presupuesto",
                Width = 100
            });

            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = proyectos;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

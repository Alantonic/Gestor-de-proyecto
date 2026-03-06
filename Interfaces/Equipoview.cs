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
using Proyecto.Repositorio;

namespace Proyecto.Interfaces
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Equipo_Load(object sender, EventArgs e)
        {
            List<Equipo> equipos = null;
            try
            {
                equipos = Equipo.Representar();
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
                DataPropertyName = "ID_equipo",
                HeaderText = "ID_equipo",
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
                DataPropertyName = "descripción",
                HeaderText = "descripción",
                Width = 100
            });
   


            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = equipos;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

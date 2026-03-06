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

namespace Proyecto.Forms
{
    public partial class Usuarioview : Form
    {
        public Usuarioview()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Usuario_Load(object sender, EventArgs e)
        {
            List<Usuario> usuarios = null;
            try
            {
                usuarios = Usuario.Representar();
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
                HeaderText = "ID",
                Width = 100
            });


            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = usuarios;
        }

        private static List<Usuario> Representar()
        {
            throw new NotImplementedException();
        }
    }
    }


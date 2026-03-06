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
using Proyecto.Interfaces;

namespace Proyecto.Forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarioview form = new Usuarioview();
            form.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
          
        }

        private void label6_DoubleClick(object sender, EventArgs e)
        {
            Usuarioview form = new Usuarioview();
            form.ShowDialog();
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            Proyectosview form = new Proyectosview();
            form.ShowDialog();
        }
    }
    }


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
            Close();
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
            Close();
        }

      

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            Proyectosview form = new Proyectosview();
            form.ShowDialog();
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            TareasView form = new TareasView();
            form.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_DoubleClick(object sender, EventArgs e)
        {
            Login form = new Login();
            form.ShowDialog();
            Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            Documentosview form = new Documentosview();
            form.ShowDialog();
        }

        private void label3_DoubleClick(object sender, EventArgs e)
        {
            ClientesView form = new ClientesView();
            form.ShowDialog();
            Close();
        }
    }
    }


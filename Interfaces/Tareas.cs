using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto.Forms;

namespace Proyecto.Interfaces
{
    public partial class Tareas : Form
    {
        public Tareas()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
           
        }

        private void label9_DoubleClick(object sender, EventArgs e)
        {
            Main form = new Main();
            form.ShowDialog();
        }
    }
}

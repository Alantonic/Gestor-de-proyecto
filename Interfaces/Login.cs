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
using Proyecto.Interfaces;
using System.IO;

namespace Proyecto
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Main form = new Main();
            form.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            try
            {
                TextWriter Registro = new StreamWriter(@"C:\Users\alanv\source\repos\Proyecto\Proyecto\bin\Debug\" + TU1.Text + ".txt", true);
                Registro.WriteLine(TC1.Text);
                Iniciar_sesion form = new Iniciar_sesion();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al escribir en el archivo: " + ex.ToString());
                TU1.Clear();
                TC1.Clear();
            }
        }
    }
}

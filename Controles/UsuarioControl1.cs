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
using Proyecto.Forms;
using Proyecto.Modelos;
using Proyecto.Repositorio;

namespace Proyecto.Controles
{
    public partial class UserControl1 : UserControl
    {
        private UsuariosModel _usuarioActual;

        public UserControl1()
        {
            InitializeComponent();
            // Para nuevo usuario
        }

        public UserControl1(UsuariosModel usuario)  // ← CONSTRUCTOR PARA EDITAR
        {
            InitializeComponent();
            _usuarioActual = usuario;
            CargarDatosEnControles();
        }

        private void CargarDatosEnControles()
        {
            txtNombre.Text = _usuarioActual.Nombre;
            txtTelefono.Text = _usuarioActual.Telefono;
            txtGmail.Text = _usuarioActual.Gmail;
            comboBox1.Text = _usuarioActual.ID.ToString(); 
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Aquí va la lógica para guardar
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

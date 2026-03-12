using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto.Base_Datos;
using Proyecto.Clases;
using Proyecto.Forms;

using Proyecto.Repositorio;

namespace Proyecto.Controles
{
    public partial class UserControl1 : UserControl
    {
        private usuario _usuarioActual;

        public UserControl1()
        {
            InitializeComponent();
            // Para nuevo usuario
        }

        public UserControl1(usuario usuario)  // ← CONSTRUCTOR PARA EDITAR
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto.Conexion_Base_Datos;
using Proyecto.Forms;

namespace Proyecto.Controles
{
    public partial class UserControl2 : UserControl
    {
        public bool Datos_guardados { get; private set; } // Propiedad para indicar si los datos se guardaron correctamente
        public UserControl2()
            {
            InitializeComponent();
            this.AutoSize = false;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }

        public UserControl2(Modelos.ProyectosModel.ProyectoModel proyecto)
        {
            InitializeComponent();
            Datos_guardados = false; // Inicialmente, los datos no se han guardado
            CargarDatos(proyecto);
        }

        private void CargarDatos(Modelos.ProyectosModel.ProyectoModel proyecto)
        {
            if (proyecto != null)
            {
                Nombretxt.Text = proyecto.Nombre;
                Descripciontxt.Text = proyecto.descripcion;
                Fecha1txt.Value = proyecto.Fecha_Inicio;
                Fecha2txt.Value = proyecto.Fecha_fin;
                Estadotxt.Text = proyecto.Estado;
                Presupuestotxt.Text = proyecto.Presupuesto.ToString();
            }
        }





        private void Btnguardar_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(Nombretxt.Text))
            {
                MessageBox.Show("El nombre del proyecto es obligatorio");
                return;
            }

            if (string.IsNullOrWhiteSpace(Estadotxt.Text))
            {
                MessageBox.Show("El estado del proyecto es obligatorio");
                return;
            }

            try
            {
                using (SqlConnection conexion = BD.ObtenerConexion())
                {
                    string consulta = @"INSERT INTO Proyecto 
                                        (Nombre, descripcion, Fecha_Inicio, Fecha_fin, Estado, Presupuesto) 
                                        VALUES 
                                        (@Nombre, @descripcion, @Fecha_Inicio, @Fecha_fin, @Estado, @Presupuesto)";

                    SqlCommand command = new SqlCommand(consulta, conexion);
                   
                    command.Parameters.AddWithValue("@Nombre", Nombretxt.Text);
                    command.Parameters.AddWithValue("@descripcion", Descripciontxt.Text);
                    command.Parameters.AddWithValue("@Fecha_Inicio", Fecha1txt.Value);
                    command.Parameters.AddWithValue("@Fecha_fin",  Fecha2txt.Value);
                    command.Parameters.AddWithValue("@Estado", Estadotxt.Text);
                    command.Parameters.AddWithValue("@Presupuesto", decimal.Parse(Presupuestotxt.Text));

                    // Ejecutar la consulta
                    int ejecutar = command.ExecuteNonQuery();



                    // Verificar si la inserción fue exitosa
                    if (ejecutar > 0)
                    {
                        MessageBox.Show("Proyecto guardado exitosamente");
                        Datos_guardados = true; // Indicar que los datos se guardaron correctamente


                        // Cerrar el formulario que contiene este UserControl
                        this.FindForm()?.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar el proyecto");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UserControl2_Load(object sender, EventArgs e)
        {

        }
    }
}
       

    
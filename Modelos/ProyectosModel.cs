using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Modelos
{
    public class ProyectosModel
    {
        public class ProyectoModel
        {
            public int id_proyecto { get; set; }
            public int ID { get; set; }
            public string Nombre { get; set; }
            public string descripcion { get; set; }
            public DateTime Fecha_Inicio { get; set; }
            public DateTime Fecha_fin { get; set; }
            public string Estado { get; set; }
            public decimal Presupuesto { get; set; }
        }
    }
}

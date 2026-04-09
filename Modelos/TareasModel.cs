using System;

namespace Proyecto.Modelos
{
    public class TareasModel
    {
        public int id_tarea { get; set; }      // Primary Key (identity)
        public int id_proyecto { get; set; }    // FK al proyecto
        public int ID { get; set; }             // FK al usuario (quién creó la tarea)
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string prioridad { get; set; }   // Alta, Media, Baja
        public string estado { get; set; }      // Pendiente, En Proceso, Completada
        public DateTime fehca_Inicio { get; set; }  // Nota: tiene typo "fehca" en tu tabla
        public DateTime fechaVencimiento { get; set; }
        public int horaEstimadas { get; set; }
    }
}
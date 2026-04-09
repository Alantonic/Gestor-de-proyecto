using System;

namespace Proyecto.Modelos
{
    public class DocumentoModel
    {
        public int id_documento { get; set; }
        public int id_proyecto { get; set; }
        public string nombre { get; set; }
        public string tipoArchivo { get; set; }
        public string url { get; set; }
        public DateTime fechaSubida { get; set; }
    }
}
using System;
using System.Windows.Forms;
using Proyecto.Clases;
using Proyecto.Forms;

namespace Proyecto
{
    internal static class Program
    {
        [STAThread] 
        static void Main()
        {
            // Clases para la configuarción de windows forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

        

            // 3. Abre el formulario principal
            Application.Run(new Login());
        }
    }
}

//Main
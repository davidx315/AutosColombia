using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosColombia.ModeloDatos.ModeloMensajes
{
    public class Mensaje
    {
        public string Codigo { set; get; }

        public string Mensaje_Generico { set; get; }

        public string Tipo { set; get; }
    }

    public struct Mensaje_Tipo
    {
        public static string INFO = "INFORMACIÓN";

        public static string ERROR = "ERROR";

        public static string SUCCESS = "SUCCESS";

        public static string WARNING = "WARNING";
    }
}

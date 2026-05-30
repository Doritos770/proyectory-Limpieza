using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Auxiliares
{
    public class SolicitudCambiodeContrasena
    {
        public int id { get; set; }
        public string gmail { get; set; } = "0";
        public string Telefono { get; set; } = "1";
    }
    public class NuevaContrasena
    {
        public int id { get; set; }
        public string codigo { get; set; } = "0";
        public string nuevaContrasena { get; set; }
    }
}

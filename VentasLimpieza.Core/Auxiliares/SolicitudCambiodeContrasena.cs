using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Auxiliares
{
    /// <summary>
    /// Modelo auxiliar utilizado para iniciar el proceso de recuperación de contraseña.
    /// </summary>
    public class SolicitudCambiodeContrasena
    {
        /// <summary>
        /// Identificador del usuario (opcional dependiendo del flujo).
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Correo electrónico asociado a la cuenta a recuperar.
        /// </summary>
        public string gmail { get; set; } = "0";

        /// <summary>
        /// Número telefónico asociado a la cuenta, como método alternativo.
        /// </summary>
        public string Telefono { get; set; } = "1";
    }

    /// <summary>
    /// Modelo auxiliar utilizado para concretar el cambio de contraseña.
    /// </summary>
    public class NuevaContrasena
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Código de verificación temporal enviado al usuario.
        /// </summary>
        public string codigo { get; set; } = "0";

        /// <summary>
        /// La nueva contraseña que reemplazará a la actual.
        /// </summary>
        public string nuevaContrasena { get; set; }
    }
}

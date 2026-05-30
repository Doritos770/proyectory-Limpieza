using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Entities
{
    /// <summary>
    /// Entidad ligera que se utiliza exclusivamente para capturar las credenciales durante un inicio de sesión (Login).
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// Nombre de usuario o correo proporcionado.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Contraseña en texto plano proporcionada.
        /// </summary>
        public string Password { get; set; }
    }
}

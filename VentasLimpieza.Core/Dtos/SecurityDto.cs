using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Enum;

namespace VentasLimpieza.Core.Dtos
{
    /// <summary>
    /// Objeto de transferencia de datos para la autenticación y el manejo de sesión de usuarios.
    /// </summary>
    public class SecurityDto
    {
        /// <summary>
        /// Correo o login principal.
        /// </summary>
        public string Login { get; set; } = null!;

        /// <summary>
        /// Contraseña cifrada de acceso.
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Nombre a desplegar del usuario en sesión.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Rol principal asignado (ej. Administrador, Cliente).
        /// </summary>
        public RoleType? Role { get; set; }
    }
}

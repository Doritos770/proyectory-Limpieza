using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Enum;

namespace VentasLimpieza.Core.Entities
{
    /// <summary>
    /// Entidad que representa la seguridad y acceso (Login/Registro) de un usuario administrativo o de alto perfil,
    /// a diferencia del perfil común de clientes.
    /// </summary>
    public partial class Security : BaseEntity
    {
        //public int Id { get; set; }

        /// <summary>
        /// Nombre de usuario o cuenta de correo principal para inicio de sesión.
        /// </summary>
        public string Login { get; set; } = null!;

        /// <summary>
        /// Hash de la contraseña almacenada.
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Nombre visual o display name en el sistema.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Rol del usuario en el sistema basado en los Roles tipificados.
        /// </summary>
        public RoleType Role { get; set; }
    }
}

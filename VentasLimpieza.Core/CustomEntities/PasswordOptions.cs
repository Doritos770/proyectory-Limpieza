using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.CustomEntities
{
    /// <summary>
    /// Configuración para el hashing y verificación de contraseñas.
    /// Habitualmente mapeada desde el appsettings.json.
    /// </summary>
    public class PasswordOptions
    {
        /// <summary>
        /// Tamaño en bytes para la sal generada aleatoriamente.
        /// </summary>
        public int SaltSize { get; set; }

        /// <summary>
        /// Tamaño en bytes de la llave resultante del hashing.
        /// </summary>
        public int KeySize { get; set; }

        /// <summary>
        /// Número de iteraciones aplicadas durante la derivación de la llave (PBKDF2 u otros).
        /// </summary>
        public int Iterations { get; set; }
    }
}

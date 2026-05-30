namespace VentasLimpieza.Core.CustomEntities
{
    /// <summary>
    /// Representa un mensaje de notificación del sistema (informativo, error, advertencia).
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Tipo de mensaje (ej. Error, Info, Warning, Success).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Contenido descriptivo del mensaje.
        /// </summary>
        public string Description { get; set; }
    }
}

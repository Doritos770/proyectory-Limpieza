namespace VentasLimpieza.Core.Entities
{

    /// <summary>
    /// Entidad que representa códigos de validación/seguridad generados para un usuario.
    /// </summary>
    public partial class Codigoseguridad : BaseEntity
    {
        //public int Id { get; set; }

        /// <summary>
        /// ID del usuario propietario del código (Llave Foránea).
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Valor en texto del código de seguridad.
        /// </summary>
        public string Codigo { get; set; } = null!;

        /// <summary>
        /// Fecha en que fue generado en la base de datos.
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha máxima en la que el código sigue siendo válido.
        /// </summary>
        public DateTime FechaExpiracion { get; set; }

        /// <summary>
        /// Bandera que indica si el código ya fue utilizado.
        /// </summary>
        public ulong Usado { get; set; }

        /// <summary>
        /// Referencia de navegación al usuario propietario.
        /// </summary>
        public virtual Usuario Usuario { get; set; } = null!;
    }


}
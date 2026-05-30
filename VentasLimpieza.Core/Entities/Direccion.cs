namespace VentasLimpieza.Core.Entities
{
    /// <summary>
    /// Entidad que representa la dirección física (para entregas) asociada a un usuario.
    /// </summary>
    public partial class Direccion : BaseEntity
    {
     //   public int Id { get; set; }

        /// <summary>
        /// ID del usuario propietario de esta dirección (Llave Foránea).
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Detalles de la dirección (calle, número, etc.).
        /// </summary>
        public string Direccion1 { get; set; } = null!;

        /// <summary>
        /// Nombre de la ciudad.
        /// </summary>
        public string Ciudad { get; set; } = null!;

        /// <summary>
        /// Provincia o estado.
        /// </summary>
        public string Provincia { get; set; } = null!;

        /// <summary>
        /// País de residencia.
        /// </summary>
        public string Pais { get; set; } = null!;

        /// <summary>
        /// Indicador para saber si esta es la dirección por defecto del usuario.
        /// </summary>
        public ulong Principal { get; set; }

        /// <summary>
        /// Propiedad de navegación de EF Core hacia el Usuario propietario.
        /// </summary>
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
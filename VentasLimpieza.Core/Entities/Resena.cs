namespace VentasLimpieza.Core.Entities
{
    /// <summary>
    /// Entidad de dominio que representa los comentarios y valoraciones que dejan los clientes a los productos.
    /// </summary>
    public partial class Resena : BaseEntity
    {
       // public int Id { get; set; }

        /// <summary>
        /// ID del usuario que publica la reseña (Llave Foránea).
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// ID del producto calificado (Llave Foránea).
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary>
        /// Valor numérico que califica el producto (generalmente de 1 a 5).
        /// </summary>
        public int Calificacion { get; set; }

        /// <summary>
        /// Texto de la opinión del usuario.
        /// </summary>
        public string? Comentario { get; set; }

        /// <summary>
        /// Fecha de publicación de la reseña.
        /// </summary>
        public DateOnly Fecha { get; set; }

        /// <summary>
        /// Propiedad de navegación de EF Core hacia el Producto.
        /// </summary>
        public virtual Producto Producto { get; set; } = null!;

        /// <summary>
        /// Propiedad de navegación de EF Core hacia el Usuario.
        /// </summary>
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
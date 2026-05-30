namespace VentasLimpieza.Core.Entities
{

    /// <summary>
    /// Entidad de dominio que representa las clasificaciones de los productos del inventario.
    /// </summary>
    public partial class Categoria : BaseEntity
    {
        //public int Id { get; set; }

        /// <summary>
        /// Nombre descriptivo de la categoría.
        /// </summary>
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Detalles adicionales o descripción.
        /// </summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Ruta de la imagen representativa.
        /// </summary>
        public string? ImagenUrl { get; set; }

        /// <summary>
        /// Colección de productos asociados a esta categoría (Navegación EF Core).
        /// </summary>
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }

}
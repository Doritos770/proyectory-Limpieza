namespace VentasLimpieza.Core.Entities
{

    /// <summary>
    /// Entidad central del dominio que representa los artículos comercializables en la plataforma.
    /// </summary>
    public partial class Producto : BaseEntity
    {
        //public int Id { get; set; }

        /// <summary>
        /// Nombre comercial del producto.
        /// </summary>
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Descripción de las características principales.
        /// </summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// ID de la categoría a la cual pertenece (Llave Foránea).
        /// </summary>
        public int CategoriaId { get; set; }

        /// <summary>
        /// Marca comercial del producto.
        /// </summary>
        public string? Marca { get; set; }

        /// <summary>
        /// Descripción del formato de venta (ej. Botella de 1 litro, 500gr).
        /// </summary>
        public string Presentacion { get; set; } = null!;

        /// <summary>
        /// Precio base o de lista de este producto.
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Enlace a la imagen ilustrativa del producto.
        /// </summary>
        public string? ImagenUrl { get; set; }

        /// <summary>
        /// Fecha en que el producto se dio de alta en el catálogo.
        /// </summary>
        public DateOnly FechaCreacion { get; set; }

        /// <summary>
        /// Propiedad de navegación de EF Core hacia la Categoría.
        /// </summary>
        public virtual Categoria Categoria { get; set; } = null!;

        /// <summary>
        /// Colección de ventas/detalles de pedidos donde se ha incluido este producto.
        /// </summary>
        public virtual ICollection<Detallepedido> Detallepedidos { get; set; } = new List<Detallepedido>();

        /// <summary>
        /// Historial de lotes de ingreso registrados para este producto.
        /// </summary>
        public virtual ICollection<Loteproducto> Loteproductos { get; set; } = new List<Loteproducto>();

        /// <summary>
        /// Reseñas y calificaciones obtenidas de los clientes.
        /// </summary>
        public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
    }

}
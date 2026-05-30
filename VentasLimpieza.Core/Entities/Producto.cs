namespace VentasLimpieza.Core.Entities
{

    public partial class Producto : BaseEntity
    {
        //public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int CategoriaId { get; set; }

        public string? Marca { get; set; }

        public string Presentacion { get; set; } = null!;

        public decimal Precio { get; set; }

        public string? ImagenUrl { get; set; }

        public DateOnly FechaCreacion { get; set; }

        public virtual Categoria Categoria { get; set; } = null!;

        public virtual ICollection<Detallepedido> Detallepedidos { get; set; } = new List<Detallepedido>();

        public virtual ICollection<Loteproducto> Loteproductos { get; set; } = new List<Loteproducto>();

        public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
    }

}
namespace VentasLimpieza.Core.Entities
{

    public partial class Categoria : BaseEntity
    {
        //public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? ImagenUrl { get; set; }

        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }

}
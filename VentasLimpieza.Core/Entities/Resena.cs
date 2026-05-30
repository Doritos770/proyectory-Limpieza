namespace VentasLimpieza.Core.Entities
{
    public partial class Resena : BaseEntity
    {
       // public int Id { get; set; }

        public int UsuarioId { get; set; }

        public int ProductoId { get; set; }

        public int Calificacion { get; set; }

        public string? Comentario { get; set; }

        public DateOnly Fecha { get; set; }

        public virtual Producto Producto { get; set; } = null!;

        public virtual Usuario Usuario { get; set; } = null!;
    }
}
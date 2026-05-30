namespace VentasLimpieza.Core.Entities
{

    public partial class Usuario : BaseEntity
    {
     //   public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string? Telefono { get; set; }

        public DateOnly FechaRegistro { get; set; }

        public ulong IsActive { get; set; }

        public virtual ICollection<Codigoseguridad> Codigoseguridads { get; set; } = new List<Codigoseguridad>();

        public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();

        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
    }

}
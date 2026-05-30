namespace VentasLimpieza.Core.Entities
{

    public partial class Pedido : BaseEntity
    {
       // public int Id { get; set; }

        public int UsuarioId { get; set; }

        public DateTime FechaPedido { get; set; }

        public string Estado { get; set; } = null!;

        public decimal CostoEnvio { get; set; }

        public string MetodoPago { get; set; } = null!;

        public decimal Total { get; set; }

        public virtual ICollection<Detallepedido> Detallepedidos { get; set; } = new List<Detallepedido>();

        public virtual Usuario Usuario { get; set; } = null!;
    }

}
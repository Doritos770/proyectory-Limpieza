namespace VentasLimpieza.Core.Entities
{
    public partial class Direccion : BaseEntity
    {
     //   public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string Direccion1 { get; set; } = null!;

        public string Ciudad { get; set; } = null!;

        public string Provincia { get; set; } = null!;

        public string Pais { get; set; } = null!;

        public ulong Principal { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
    }
}
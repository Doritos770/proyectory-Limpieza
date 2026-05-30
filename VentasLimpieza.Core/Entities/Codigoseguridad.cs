namespace VentasLimpieza.Core.Entities
{

    public partial class Codigoseguridad : BaseEntity
    {
        //public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string Codigo { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaExpiracion { get; set; }

        public ulong Usado { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
    }


}
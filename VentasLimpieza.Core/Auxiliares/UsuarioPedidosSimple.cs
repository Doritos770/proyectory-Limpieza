namespace VentasLimpieza.Core.Auxiliares
{
    public class UsuarioPedidosSimple
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? Telefono { get; set; }
        public int TotalPedidos { get; set; }
        public decimal TotalCompras { get; set; }
    }
}
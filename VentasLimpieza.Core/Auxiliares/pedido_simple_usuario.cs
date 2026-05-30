namespace VentasLimpieza.Core.Auxiliares
{
    public class pedido_simple_usuario
    {
        public int PedidoId { get; set; }
        public string Estado { get; set; } = null!;
        public decimal Total { get; set; }
    }
}

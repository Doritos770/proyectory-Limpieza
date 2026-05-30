namespace VentasLimpieza.Core.Auxiliares
{
    public class pedido_usuario
    {
        public int CantidadPedidos { get; set; }
        public decimal TotalGastado { get; set; }
        public System.Collections.Generic.List<pedido_simple_usuario> Pedidos { get; set; } = new System.Collections.Generic.List<pedido_simple_usuario>();
    }
}

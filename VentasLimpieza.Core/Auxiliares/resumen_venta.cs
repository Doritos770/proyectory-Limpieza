namespace VentasLimpieza.Core.Auxiliares
{
    /// <summary>
    /// Clase auxiliar que consolida las métricas generales de las ventas (dashboard/estadísticas base).
    /// </summary>
    public class resumen_venta
    {
        /// <summary>
        /// Cantidad total de pedidos procesados en el sistema.
        /// </summary>
        public int TotalPedidos { get; set; }

        /// <summary>
        /// Sumatoria monetaria de todos los ingresos generados.
        /// </summary>
        public decimal TotalIngresos { get; set; }
    }
}

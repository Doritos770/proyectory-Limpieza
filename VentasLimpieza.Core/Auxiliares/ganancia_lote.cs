namespace VentasLimpieza.Core.Auxiliares
{
    /// <summary>
    /// Modelo auxiliar que representa el margen de ganancia calculado por cada lote de producto.
    /// Utilizado típicamente en reportes financieros.
    /// </summary>
    public class ganancia_lote
    {
        /// <summary>
        /// Código de identificación del lote.
        /// </summary>
        public string NumeroLote { get; set; } = null!;

        /// <summary>
        /// Nombre del producto asociado al lote.
        /// </summary>
        public string ProductoNombre { get; set; } = null!;

        /// <summary>
        /// Número de unidades que ya fueron vendidas de este lote específico.
        /// </summary>
        public int CantidadVendida { get; set; }

        /// <summary>
        /// Monto total de ganancia monetaria generada por las unidades vendidas de este lote.
        /// </summary>
        public decimal GananciaTotal { get; set; }
    }
}

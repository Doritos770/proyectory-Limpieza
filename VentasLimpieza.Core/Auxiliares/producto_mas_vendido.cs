namespace VentasLimpieza.Core.Auxiliares
{
    /// <summary>
    /// Modelo auxiliar utilizado para rankear los productos más populares.
    /// </summary>
    public class producto_mas_vendido
    {
        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string ProductoNombre { get; set; } = null!;

        /// <summary>
        /// Cantidad de unidades que se han vendido históricamente.
        /// </summary>
        public int TotalVendido { get; set; }
    }
}

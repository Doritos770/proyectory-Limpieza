namespace VentasLimpieza.Core.Auxiliares
{
    public class ganancia_lote
    {
        public string NumeroLote { get; set; } = null!;
        public string ProductoNombre { get; set; } = null!;
        public int CantidadVendida { get; set; }
        public decimal GananciaTotal { get; set; }
    }
}

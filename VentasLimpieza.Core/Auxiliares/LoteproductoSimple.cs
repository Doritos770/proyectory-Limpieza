using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Core.Auxiliares
{
    public class LoteproductoSimple
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } = null!;
        public string NumeroLote { get; set; } = null!;
        public DateOnly FechaFabricacion { get; set; }
        public DateOnly FechaCaducidad { get; set; }
        public int CantidadIngreso { get; set; }
        public int CantidadDisponible { get; set; }
        public decimal PrecioCompra { get; set; }
        public string? UbicacionAlmacen { get; set; }
        public ulong Activo { get; set; }
        public int? DiasRestantes { get; set; }
    }
}

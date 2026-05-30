using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Dtos
{
    public class LoteproductoDto
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }

        public string NumeroLote { get; set; } = null!;

        public string FechaFabricacion { get; set; }

        public string FechaCaducidad { get; set; }

        public int CantidadIngreso { get; set; }

        public int CantidadDisponible { get; set; }

        public decimal PrecioCompra { get; set; }

        public string? UbicacionAlmacen { get; set; }

        public ulong Activo { get; set; } = 1;

    }
}

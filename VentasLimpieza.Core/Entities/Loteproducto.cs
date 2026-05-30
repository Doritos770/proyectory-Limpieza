using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Entities
{
    public partial class Loteproducto : BaseEntity

    {
       // public int Id { get; set; }

        public int ProductoId { get; set; }

        public string NumeroLote { get; set; } = null!;

        public DateOnly FechaFabricacion { get; set; }

        public DateOnly FechaCaducidad { get; set; }

        public int CantidadIngreso { get; set; }

        public int CantidadDisponible { get; set; }

        public decimal PrecioCompra { get; set; }

        public string? UbicacionAlmacen { get; set; }

        public ulong Activo { get; set; }

        public virtual ICollection<Detallepedido> Detallepedidos { get; set; } = new List<Detallepedido>();

        public virtual Producto Producto { get; set; } = null!;
    }
}

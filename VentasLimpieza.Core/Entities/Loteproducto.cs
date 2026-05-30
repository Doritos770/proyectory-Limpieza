using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Entities
{
    /// <summary>
    /// Entidad de dominio que representa un lote de inventario que ingresa al almacén.
    /// </summary>
    public partial class Loteproducto : BaseEntity

    {
       // public int Id { get; set; }

        /// <summary>
        /// ID del producto al que corresponde el lote (Llave Foránea).
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary>
        /// Serial o código identificador del fabricante.
        /// </summary>
        public string NumeroLote { get; set; } = null!;

        /// <summary>
        /// Fecha original de manufactura.
        /// </summary>
        public DateOnly FechaFabricacion { get; set; }

        /// <summary>
        /// Fecha límite para la venta/consumo del lote.
        /// </summary>
        public DateOnly FechaCaducidad { get; set; }

        /// <summary>
        /// Total de unidades que ingresaron.
        /// </summary>
        public int CantidadIngreso { get; set; }

        /// <summary>
        /// Unidades actuales que se encuentran listas para la venta.
        /// </summary>
        public int CantidadDisponible { get; set; }

        /// <summary>
        /// Costo unitario para la empresa al comprar este lote.
        /// </summary>
        public decimal PrecioCompra { get; set; }

        /// <summary>
        /// Pasillo, estante o ubicación en la bodega.
        /// </summary>
        public string? UbicacionAlmacen { get; set; }

        /// <summary>
        /// Estado general del lote (Activo/Inactivo).
        /// </summary>
        public ulong Activo { get; set; }

        /// <summary>
        /// Colección de detalles de pedidos despachados desde este lote.
        /// </summary>
        public virtual ICollection<Detallepedido> Detallepedidos { get; set; } = new List<Detallepedido>();

        /// <summary>
        /// Propiedad de navegación de EF Core hacia el Producto.
        /// </summary>
        public virtual Producto Producto { get; set; } = null!;
    }
}

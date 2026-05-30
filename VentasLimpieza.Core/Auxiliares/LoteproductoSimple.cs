using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Core.Auxiliares
{
    /// <summary>
    /// Clase auxiliar que representa un resumen simplificado de la información de un lote de producto,
    /// utilizado principalmente en consultas rápidas (Dapper).
    /// </summary>
    public class LoteproductoSimple
    {
        /// <summary>
        /// Identificador único del lote.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador del producto asociado.
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary>
        /// Nombre del producto asociado al lote.
        /// </summary>
        public string ProductoNombre { get; set; } = null!;

        /// <summary>
        /// Código o número identificador del lote.
        /// </summary>
        public string NumeroLote { get; set; } = null!;

        /// <summary>
        /// Fecha en que se fabricó el lote.
        /// </summary>
        public DateOnly FechaFabricacion { get; set; }

        /// <summary>
        /// Fecha límite para el consumo o uso del lote.
        /// </summary>
        public DateOnly FechaCaducidad { get; set; }

        /// <summary>
        /// Cantidad de unidades que ingresaron originalmente en este lote.
        /// </summary>
        public int CantidadIngreso { get; set; }

        /// <summary>
        /// Cantidad de unidades que aún están disponibles para la venta.
        /// </summary>
        public int CantidadDisponible { get; set; }

        /// <summary>
        /// Precio unitario al que se compró el producto en este lote.
        /// </summary>
        public decimal PrecioCompra { get; set; }

        /// <summary>
        /// Ubicación física en el almacén donde se encuentra el lote.
        /// </summary>
        public string? UbicacionAlmacen { get; set; }

        /// <summary>
        /// Indica si el lote se encuentra activo en el sistema.
        /// </summary>
        public ulong Activo { get; set; }

        /// <summary>
        /// Días restantes antes de que el lote alcance su fecha de caducidad.
        /// </summary>
        public int? DiasRestantes { get; set; }
    }
}

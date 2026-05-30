using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Auxiliares
{
    /// <summary>
    /// Data Transfer Object (DTO) que representa un producto que no ha registrado ventas,
    /// ideal para reportes de inventario obsoleto o de lenta rotación.
    /// </summary>
    public class ProductoSinVentaDTO
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Marca registrada del producto.
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Formato o presentación del producto (ej. 500ml, 1kg).
        /// </summary>
        public string Presentacion { get; set; }

        /// <summary>
        /// Precio actual del producto.
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Fecha original en la que el producto fue añadido al catálogo.
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Categoría a la que pertenece.
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Sumatoria total de unidades disponibles en stock.
        /// </summary>
        public int StockTotal { get; set; }

        /// <summary>
        /// Cantidad de lotes asociados que aún tienen inventario.
        /// </summary>
        public int CantidadLotes { get; set; }

        /// <summary>
        /// Número de días transcurridos desde que se creó el producto en el sistema.
        /// </summary>
        public int DiasDesdeCreacion { get; set; }
    }
}

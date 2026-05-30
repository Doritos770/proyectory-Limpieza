using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos;

/// <summary>
/// Objeto de transferencia de datos extendido para productos.
/// </summary>
public partial class ProductoDtoPorLote
{
    /// <summary>
    /// Identificador único del producto.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del producto.
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Breve descripción de sus características.
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// ID de la categoría a la que pertenece.
    /// </summary>
    public int CategoriaId { get; set; }

    /// <summary>
    /// Marca del producto.
    /// </summary>
    public string? Marca { get; set; }

    /// <summary>
    /// Presentación o formato (ej. 1L, 500g).
    /// </summary>
    public string Presentacion { get; set; } = null!;

    /// <summary>
    /// Precio de venta general (referencia).
    /// </summary>
    public decimal Precio { get; set; }

    /// <summary>
    /// Enlace a la imagen del producto.
    /// </summary>
    public string? ImagenUrl { get; set; }

    /// <summary>
    /// Fecha de alta en el sistema.
    /// </summary>
    public DateOnly FechaCreacion { get; set; }

}

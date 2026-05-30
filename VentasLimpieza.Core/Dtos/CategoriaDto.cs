using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos;

/// <summary>
/// Objeto de transferencia de datos para la entidad Categoria.
/// </summary>
public partial class CategoriaDto
{
    /// <summary>
    /// Identificador único de la categoría.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la categoría.
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Descripción detallada de la categoría.
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// URL de la imagen representativa de la categoría.
    /// </summary>
    public string? ImagenUrl { get; set; }
    
}

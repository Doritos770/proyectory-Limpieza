using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos;

/// <summary>
/// Objeto de transferencia de datos para valoraciones y comentarios.
/// </summary>
public partial class ResenaDto
{
    /// <summary>
    /// Identificador único de la reseña.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID del usuario autor de la reseña.
    /// </summary>
    public int UsuarioId { get; set; }

    /// <summary>
    /// ID del producto calificado.
    /// </summary>
    public int ProductoId { get; set; }

    /// <summary>
    /// Puntuación otorgada.
    /// </summary>
    public int Calificacion { get; set; }

    /// <summary>
    /// Opinión o detalle.
    /// </summary>
    public string? Comentario { get; set; }

    /// <summary>
    /// Fecha en que se realizó.
    /// </summary>
    public DateOnly Fecha { get; set; }

}

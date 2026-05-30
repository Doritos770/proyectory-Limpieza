using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos;

/// <summary>
/// Objeto de transferencia de datos para la entidad Dirección (física o envío).
/// </summary>
public partial class DireccionDto
{
    /// <summary>
    /// Identificador único de la dirección.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID del usuario propietario de la dirección.
    /// </summary>
    public int UsuarioId { get; set; }

    /// <summary>
    /// Línea principal de la dirección física (Calle, número, depto).
    /// </summary>
    public string Direccion1 { get; set; } = null!;

    /// <summary>
    /// Ciudad de residencia.
    /// </summary>
    public string Ciudad { get; set; } = null!;

    /// <summary>
    /// Provincia, estado o región.
    /// </summary>
    public string Provincia { get; set; } = null!;

    /// <summary>
    /// País de residencia.
    /// </summary>
    public string Pais { get; set; } = null!;

    /// <summary>
    /// Indicador numérico booleano si es la dirección principal (por defecto) del usuario.
    /// </summary>
    public ulong Principal { get; set; }
    
}

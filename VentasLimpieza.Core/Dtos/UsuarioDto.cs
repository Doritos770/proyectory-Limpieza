using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos;

/// <summary>
/// Objeto de transferencia de datos para el perfil general del usuario.
/// </summary>
public partial class UsuarioDto
{
    /// <summary>
    /// ID del usuario.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Correo electrónico.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Contraseña.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Nombres del usuario.
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Apellidos del usuario.
    /// </summary>
    public string Apellido { get; set; } = null!;

    /// <summary>
    /// Número de contacto.
    /// </summary>
    public string? Telefono { get; set; }

    /// <summary>
    /// Fecha de registro en la plataforma.
    /// </summary>
    public DateOnly FechaRegistro { get; set; }

    /// <summary>
    /// Indicador si la cuenta se encuentra activa o suspendida.
    /// </summary>
    public ulong IsActive { get; set; }
}

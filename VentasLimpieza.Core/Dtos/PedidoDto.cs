using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos;

/// <summary>
/// Objeto de transferencia de datos para la entidad Pedido.
/// </summary>
public partial class PedidoDto
{
    /// <summary>
    /// Identificador único del pedido.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID del usuario que realizó la compra.
    /// </summary>
    public int UsuarioId { get; set; }

    /// <summary>
    /// Fecha y hora en la que se generó el pedido.
    /// </summary>
    public DateTime FechaPedido { get; set; }

    /// <summary>
    /// Estado del pedido (Ej. Pendiente, Completado, Cancelado).
    /// </summary>
    public string Estado { get; set; } = null!;

    /// <summary>
    /// Costo total calculado por envío.
    /// </summary>
    public decimal CostoEnvio { get; set; }

    /// <summary>
    /// Método seleccionado para efectuar el pago.
    /// </summary>
    public string MetodoPago { get; set; } = null!;

    /// <summary>
    /// Monto total del pedido, incluyendo productos y envío.
    /// </summary>
    public decimal Total { get; set; }
}

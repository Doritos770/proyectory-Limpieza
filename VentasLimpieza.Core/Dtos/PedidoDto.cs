using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos;

public partial class PedidoDto
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public DateTime FechaPedido { get; set; }

    public string Estado { get; set; } = null!;

    public decimal CostoEnvio { get; set; }

    public string MetodoPago { get; set; } = null!;

    public decimal Total { get; set; }
}

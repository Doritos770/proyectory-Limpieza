using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos
{
    public class CrearPedidoDto
    {
        public int UsuarioId { get; set; }
        public decimal CostoEnvio { get; set; }
        public string MetodoPago { get; set; } = null!;
        public List<CrearDetallePedidoDto> Detalles { get; set; } = new List<CrearDetallePedidoDto>();
    }
}

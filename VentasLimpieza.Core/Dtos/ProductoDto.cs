using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos;

public partial class ProductoDtoPorLote
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int CategoriaId { get; set; }

    public string? Marca { get; set; }

    public string Presentacion { get; set; } = null!;

    public decimal Precio { get; set; }

    public string? ImagenUrl { get; set; }

    public DateOnly FechaCreacion { get; set; }

}

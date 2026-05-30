using System;
using System.Collections.Generic;

namespace VentasLimpieza.core.Dtos;

public partial class ResenaDto
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int ProductoId { get; set; }

    public int Calificacion { get; set; }

    public string? Comentario { get; set; }

    public DateOnly Fecha { get; set; }

}

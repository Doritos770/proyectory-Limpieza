using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Core.Auxiliares
{
    public class ResenaSimple
    {
        public int ResenaId { get; set; }
        public int UsuarioId { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public int Calificacion { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
    }
}

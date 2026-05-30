using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Infrastructure.Queries
{
    public static class sqlLoteproducto
    {
        public static string LotesCaducados = @"
            SELECT 
                l.Id,
                l.ProductoId,
                p.Nombre AS ProductoNombre,
                l.NumeroLote,
                l.FechaFabricacion,
                l.FechaCaducidad,
                l.CantidadIngreso,
                l.CantidadDisponible,
                l.PrecioCompra,
                l.UbicacionAlmacen,
                l.Activo
            FROM Loteproducto l
            INNER JOIN Producto p ON l.ProductoId = p.Id
            WHERE l.FechaCaducidad < @FechaActual 
            AND l.Activo = 1
            ORDER BY l.FechaCaducidad ASC;";

        public static string LotesBajoStock = @"
            SELECT 
                l.Id,
                l.ProductoId,
                p.Nombre AS ProductoNombre,
                l.NumeroLote,
                l.FechaFabricacion,
                l.FechaCaducidad,
                l.CantidadIngreso,
                l.CantidadDisponible,
                l.PrecioCompra,
                l.UbicacionAlmacen,
                l.Activo
            FROM Loteproducto l
            INNER JOIN Producto p ON l.ProductoId = p.Id
            WHERE l.CantidadDisponible <= @StockMinimo 
            AND l.Activo = 1
            ORDER BY l.CantidadDisponible ASC;";

        public static string LotesProximosACaducar = @"
            SELECT 
                l.Id,
                l.ProductoId,
                p.Nombre AS ProductoNombre,
                l.NumeroLote,
                l.FechaFabricacion,
                l.FechaCaducidad,
                l.CantidadIngreso,
                l.CantidadDisponible,
                l.PrecioCompra,
                l.UbicacionAlmacen,
                l.Activo,
                DATEDIFF(DAY, @FechaActual, l.FechaCaducidad) AS DiasRestantes
            FROM Loteproducto l
            INNER JOIN Producto p ON l.ProductoId = p.Id
            WHERE l.FechaCaducidad BETWEEN @FechaActual AND @FechaLimite
            AND l.Activo = 1
            ORDER BY l.FechaCaducidad ASC;";
    }
}
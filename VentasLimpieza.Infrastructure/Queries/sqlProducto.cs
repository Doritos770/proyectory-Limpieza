using System;
using System.Collections.Generic;
using System.Text;

namespace VentasLimpieza.Infrastructure.Queries
{
    public static class sqlProducto
    {
        public static string reportePorLote = @"
        
        SELECT 
            c.Nombre AS Categoria,
            COUNT(DISTINCT p.Id) AS CantidadProductos,
            COUNT(lp.Id) AS CantidadLotes,
            SUM(lp.CantidadDisponible) AS TotalUnidades,
            SUM(lp.PrecioCompra * lp.CantidadDisponible) AS ValorInventario
        FROM Categoria c
        INNER JOIN Producto p ON c.Id = p.CategoriaId
        INNER JOIN LoteProducto lp ON p.Id = lp.ProductoId AND lp.Activo = 1
        GROUP BY c.Id, c.Nombre
        ORDER BY CantidadLotes DESC;
        ";

        public static string productosSinVentas = @"
        SELECT 
            p.Id,
            p.Nombre,
            p.Marca,
            p.Presentacion,
            p.Precio,
            p.FechaCreacion,
            c.Nombre AS Categoria,
            SUM(lp.CantidadDisponible) AS StockTotal,
            COUNT(lp.Id) AS CantidadLotes,
            DATEDIFF(CURDATE(), p.FechaCreacion) AS DiasDesdeCreacion
        FROM Producto p
        INNER JOIN Categoria c ON p.CategoriaId = c.Id
        INNER JOIN LoteProducto lp ON p.Id = lp.ProductoId AND lp.Activo = 1
        LEFT JOIN DetallePedido dp ON p.Id = dp.ProductoId
        WHERE dp.Id IS NULL
        GROUP BY p.Id, p.Nombre, p.Marca, p.Presentacion, p.Precio, p.FechaCreacion, c.Nombre
        ORDER BY DiasDesdeCreacion DESC;
        ";
        public static string estadisticasPorCategoria = @"
        SELECT 
            p.CategoriaId,
            c.Nombre AS CategoriaNombre,
            COUNT(*) AS TotalProductos
        FROM Productos p
        INNER JOIN Categorias c ON p.CategoriaId = c.Id
        GROUP BY p.CategoriaId, c.Nombre
        ORDER BY TotalProductos DESC
        ";
    
    
    
    }
}
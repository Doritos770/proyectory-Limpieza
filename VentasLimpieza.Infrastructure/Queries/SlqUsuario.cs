using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.Core.Entities;

namespace VentasLimpieza.Infrastructure.Queries
{
    public static class slqUsuario
    {
        public static string UsuarioResena = @"
       SELECT
        r.Id AS ResenaId,
        r.UsuarioId,
        r.ProductoId,
        p.Nombre AS ProductoNombre,
        r.Calificacion,
        r.Comentario,
        r.Fecha
        FROM Resena r
        INNER JOIN Producto p ON r.ProductoId = p.Id
        WHERE r.UsuarioId = @UsuarioId
        ORDER BY r.Fecha DESC;";
        public static string UsuariosInactivos = @"
            SELECT 
                Id, 
                Email, 
                Nombre, 
                Apellido, 
                Telefono, 
                FechaRegistro,
                IsActive
            FROM Usuario
            WHERE IsActive = 0
            ORDER BY FechaRegistro DESC;";

        public static string UsuariosConMasPedidos = @"
            SELECT 
                u.Id, 
                u.Email, 
                u.Nombre, 
                u.Apellido, 
                u.Telefono,
                COUNT(p.Id) AS TotalPedidos,
                COALESCE(SUM(p.Total), 0) AS TotalCompras
            FROM Usuario u
            LEFT JOIN Pedido p ON u.Id = p.UsuarioId
            WHERE u.IsActive = 1
            GROUP BY u.Id, u.Email, u.Nombre, u.Apellido, u.Telefono
            ORDER BY TotalPedidos DESC
            LIMIT 10;";
    }
}


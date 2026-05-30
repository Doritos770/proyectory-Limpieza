namespace VentasLimpieza.Infrastructure.Queries
{
    public static class sqlEstadistica
    {
        public static string PedidosPorUsuario = @"
            SELECT 
                COUNT(Id) AS CantidadPedidos, 
                COALESCE(SUM(Total), 0) AS TotalGastado 
            FROM Pedido 
            WHERE UsuarioId = @UsuarioId;";

        public static string ProductosMasVendidos = @"
            SELECT 
                p.Nombre AS ProductoNombre, 
                COALESCE(SUM(dp.Cantidad), 0) AS TotalVendido 
            FROM Detallepedido dp
            INNER JOIN Producto p ON dp.ProductoId = p.Id
            INNER JOIN Pedido ped ON dp.PedidoId = ped.Id
            WHERE ped.Estado != 'Cancelado'
            GROUP BY p.Id, p.Nombre
            ORDER BY TotalVendido DESC
            LIMIT @Limit;";

        public static string ResumenGeneral = @"
            SELECT 
                COUNT(Id) AS TotalPedidos, 
                COALESCE(SUM(Total), 0) AS TotalIngresos 
            FROM Pedido
            WHERE Estado != 'Cancelado';";

        public static string PedidosListaPorUsuario = @"
            SELECT 
                Id AS PedidoId, 
                Estado, 
                Total 
            FROM Pedido 
            WHERE UsuarioId = @UsuarioId
            ORDER BY FechaPedido DESC;";

        public static string GananciasPorLote = @"
            SELECT 
                lp.NumeroLote, 
                p.Nombre AS ProductoNombre,
                SUM(dp.Cantidad) AS CantidadVendida,
                SUM(dp.Cantidad * (dp.PrecioUnitario - lp.PrecioCompra)) AS GananciaTotal
            FROM Detallepedido dp
            INNER JOIN Loteproducto lp ON dp.LoteProductoId = lp.Id
            INNER JOIN Producto p ON lp.ProductoId = p.Id
            INNER JOIN Pedido ped ON dp.PedidoId = ped.Id
            WHERE ped.Estado != 'Cancelado'
            GROUP BY lp.Id, lp.NumeroLote, p.Nombre
            ORDER BY GananciaTotal DESC;";
    }
}

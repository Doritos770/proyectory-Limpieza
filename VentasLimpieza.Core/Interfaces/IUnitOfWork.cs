using System.Data;
using VentasLimpieza.core.Interfaces;
using VentasLimpieza.Core.Entities;


namespace VentasLimpieza.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        
        IBaseRepository<Categoria> CategoriaRepository { get; }
        IBaseRepository<Direccion> DireccionRepository { get; }
        IBaseRepository<Resena> ResenaRepository { get; }
        ISecurityRepository SecurityRepository { get; }

        ILoteproductoRepository LoteproductoRepository { get; }
        IPedidoRepository PedidoRepository { get; }
        IDetallepedidoRepository DetallepedidoRepository { get; }
        ICodigoseguridadRepository CodigoseguridadRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }//darle repository
        IProductoRepository ProductoRepository { get; }


        void SaveChanges();
        Task SaveChangesAsync();

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();

        //Nuevos miembros para Dapper
        IDbConnection? GetDbConnection();
        IDbTransaction? GetDbTransaction();

    }
}

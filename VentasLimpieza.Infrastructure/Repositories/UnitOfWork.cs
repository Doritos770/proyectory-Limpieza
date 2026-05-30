using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using VentasLimpieza.core.Interfaces;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VentasLimpiezaContext _context;
        private readonly IBaseRepository<Categoria> _categoriaRepository;
        private readonly IBaseRepository<Direccion> _direccionRepository;
        private readonly IBaseRepository<Resena> _resenaRepository;

        private readonly IPedidoRepository _pedidoRepository;
        private readonly IDetallepedidoRepository _detallepedidoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly ICodigoseguridadRepository _codigoseguridadRepository;
        private readonly ILoteproductoRepository _loteproductoRepository;
        private readonly ISecurityRepository _securityRepository;

        private readonly IDapperContext _dapper;
        private IDbContextTransaction? _efTransaction;

        public UnitOfWork(VentasLimpiezaContext context,
            IDapperContext dapper)
        {
            _context = context;
            _dapper = dapper;
        }
        public IPedidoRepository PedidoRepository =>
            _pedidoRepository ?? new PedidoRepository(_context, _dapper);

        public IBaseRepository<Categoria> CategoriaRepository =>
            _categoriaRepository ?? new BaseRepository<Categoria>(_context);

        public IBaseRepository<Direccion> DireccionRepository =>
            _direccionRepository ?? new BaseRepository<Direccion>(_context);
        public IBaseRepository<Resena> ResenaRepository =>
            _resenaRepository ?? new BaseRepository<Resena>(_context);

        public ISecurityRepository SecurityRepository =>
            _securityRepository ?? new SecurityRepository(_context);




        public ILoteproductoRepository LoteproductoRepository =>
 _loteproductoRepository ?? new LoteproductoRepository(_context, _dapper);
        public IUsuarioRepository UsuarioRepository =>
            _usuarioRepository ?? new UsuarioRepository(_context, _dapper);
        public ICodigoseguridadRepository CodigoseguridadRepository =>
        _codigoseguridadRepository ?? new CodigoseguridadRepository(_context, _dapper);

        public IProductoRepository ProductoRepository =>
        _productoRepository ?? new ProductoRepository(_context, _dapper);

        public IDetallepedidoRepository DetallepedidoRepository =>
            _detallepedidoRepository ?? new DetallepedidoRepository(_context,_dapper);

        

        public async Task BeginTransactionAsync()
        {
            if (_efTransaction == null)
            {
                _efTransaction = await _context.Database.BeginTransactionAsync();

                //Registrar la coneccion/Tx en DapperContext}
                var conn = _context.Database.GetDbConnection();
                var tx = _efTransaction.GetDbTransaction();
                _dapper.SetAmbientConnection(conn, tx);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_efTransaction != null)
                {
                    await _efTransaction.CommitAsync();
                    _efTransaction.Dispose();
                    _efTransaction = null;
                }
            }
            finally
            {
                _dapper.ClearAmbientConnection();
            }
        }

        public async Task RollbackAsync()
        {
            if (_efTransaction != null)
            {
                await _efTransaction.RollbackAsync();
                _efTransaction.Dispose();
                _efTransaction = null;
            }
            _dapper.ClearAmbientConnection();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _efTransaction?.Dispose();
            }
        }

        public IDbConnection? GetDbConnection()
        {
            // Retornamos la conexión subyacente del DbContext
            return _context.Database.GetDbConnection();
        }

        public IDbTransaction? GetDbTransaction()
        {
            return _efTransaction?.GetDbTransaction();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


















        //en program.cs estara la inyeccion por dependencia
    }
}

using Microsoft.EntityFrameworkCore;
using VentasLimpieza.core.Interfaces;
using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.Dtos;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class DetallepedidoRepository : BaseRepository<Detallepedido>, IDetallepedidoRepository
    {
        private readonly IDapperContext _dapper;

        public DetallepedidoRepository(VentasLimpiezaContext context, IDapperContext dapper)
            : base(context)
        {
            _dapper = dapper;
        }

        //public async Task<Detallepedido> RegistrarPedido(Detallepedido detallepedido)
        //{
        //    int ultimoId = await _entities.MaxAsync(d => (int?)d.Id) ?? 0;
        //    var pedido =new  Detallepedido
        //    {
        //        Id= ultimoId,
                
        //    }
        //}
    }
}

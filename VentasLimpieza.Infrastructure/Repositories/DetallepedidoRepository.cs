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

    }
}

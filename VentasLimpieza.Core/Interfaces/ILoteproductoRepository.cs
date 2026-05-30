using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Interfaces;

namespace VentasLimpieza.Core.Interfaces
{
    public interface ILoteproductoRepository : IBaseRepository<Loteproducto>
    {

        Task<IEnumerable<LoteproductoSimple>> GetLotesCaducados();
        Task<IEnumerable<LoteproductoSimple>> GetLotesBajoStock(int stockMinimo);
        Task<IEnumerable<LoteproductoSimple>> GetLotesProximosACaducar(int diasAntes);


        Task ActualizarEstadoLote(Loteproducto lote);
        Task ReducirStock(int loteId, int cantidad);
        Task<IEnumerable<Loteproducto>> GetLotesActivosPorProducto(int productoId);

 }
}

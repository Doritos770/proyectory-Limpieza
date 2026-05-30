using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.CustomEntities;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.QueryFilter;

namespace VentasLimpieza.Services.Interfaces
{
    public interface ILoteproductoService
    {
        Task<ResponseData> GetAllLoteproductosAsync(LoteproductoQueryFilter? filters = null);
        void ActualizarStock(int stockMin);
        Task<int> DarBajaLotesCaducadosAsync();
        Task<int> MarcarLotesBajoStockAsync(int stockMinimo = 5);


        Task<IEnumerable<LoteproductoSimple>> GetLotesCaducadosAsync();
        Task<IEnumerable<LoteproductoSimple>> GetLotesBajoStockAsync(int stockMinimo = 5);
        Task<IEnumerable<LoteproductoSimple>> GetLotesProximosACaducarAsync(int diasAntes = 30);
        Task<Loteproducto> GetLoteproductoByIdAsync(int id);
        Task RegistrarLoteproducto(Loteproducto loteproducto);
        void UpdateLoteproducto(Loteproducto loteproducto);
        Task DeleteLoteproducto(int id);
    }
}
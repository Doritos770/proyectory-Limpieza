using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Interfaces;

namespace VentasLimpieza.core.Interfaces; 

public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Task<IEnumerable<ResenaSimple>> GetUsuariosResenas(int id);
    Task<IEnumerable<Usuario>> GetUsuariosInactivos();
    Task<IEnumerable<UsuarioPedidosSimple>> GetUsuariosConMasPedidos();
    Task<pedido_usuario> GetEstadisticasPedidos(int usuarioId);
}
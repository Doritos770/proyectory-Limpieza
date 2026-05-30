using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.CustomEntities;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.QueryFilter;

namespace VentasLimpieza.Services.Interfaces
{
    public interface IUsuarioService
    {
        //services va acciones de negocio
        Task<ResponseData> GetAllUsersAsync(UsuarioQueryFilter? filters=null);//a todos
        //Task<IEnumerable<Usuario>> GetAllUsersAsync(UsuarioQueryFilter? filters=null);//a todos
        Task<Usuario> GetUsuarioByIdAsync(int id);//con id
        Task RegistrarUsuario(Usuario usuario);
        void UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(int id);



        Task DesactivarUsuario(int id);
        Task ActivarUsuario(int id);
        Task<IEnumerable<Usuario>> GetUsuariosInactivosAsync();
        Task<IEnumerable<UsuarioPedidosSimple>> GetUsuariosConMasPedidosAsync();
        Task<bool> ActualizarContrasena(NuevaContrasena usuario);
        Task<string> SolicitudCodigo(SolicitudCambiodeContrasena usuario);
        Task<IEnumerable<ResenaSimple>> GetUsuariosResena(int id);
        Task<pedido_usuario> GetEstadisticasPedidosAsync(int usuarioId);
    }
}

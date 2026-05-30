using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using VentasLimpieza.Core.Auxiliares;
using VentasLimpieza.Core.CustomEntities;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Core.Exceptions;
using VentasLimpieza.Core.Helpers;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Core.QueryFilter;
using VentasLimpieza.Services.Interfaces;

namespace VentasLimpieza.Services.Services
{
    public class UsuariosService : IUsuarioService
    {
        
        //public readonly IBaseRepository<Usuario> _usuarioRepository;

        //public UsuariosService(IBaseRepository<Usuario> usuarioRepository)
        //{
        //    _usuarioRepository = usuarioRepository;
        //}
        public readonly IUnitOfWork _unitOfWork;
        public UsuariosService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseData> GetAllUsersAsync(
            UsuarioQueryFilter? filters = null)  //------------------------ ver
        {
            //return await _usuarioRepository.GetAll();
            //    todos los repos  //todas las transacciones //elementos del CRUD
            //return await _unitOfWork.UsuarioRepository.GetAll();
            var usuarios = await _unitOfWork.UsuarioRepository.GetAll();
            if (filters != null)
            {

                if (filters.Email != null)
                {
                    usuarios = usuarios.Where(a => a.Email.ToLower().Contains(filters.Email.ToLower()));
                }
                if (filters.Apellido != null)
                {
                    usuarios = usuarios.Where(a => a.Apellido.ToLower().Contains(filters.Apellido.ToLower()));
                }

                if (filters.Nombre != null)
                {
                    usuarios = usuarios.Where(a => a.Nombre.ToLower().Contains(filters.Nombre.ToLower()));
                }
                if (filters.Telefono != null)
                {
                    usuarios = usuarios.Where(a => a.Telefono == filters.Telefono);
                }
                if (filters.FechaRegistro != null)
                {
                    string fechaAux =
                        Procesos.ParseFechaFlexible(filters.FechaRegistro);
                    if (fechaAux != null)
                    {
                        usuarios = usuarios.Where(x => x.FechaRegistro.ToShortDateString() ==
                        Convert.ToDateTime(fechaAux).ToShortDateString());
                    }
                }
            }
            var pagedPosts = PagedList<object>
                .Create(usuarios, filters.PageNumber, filters.PageSize);

            if (pagedPosts.Any())
            {
                return new ResponseData()
                {
                    Messages = new Message[] { new() { Type = TypeMessage.success.ToString(),
                        Description = "Registros de posts recuperados correctamente" } },
                    Pagination = pagedPosts,
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                return new ResponseData()
                {
                    Messages = new Message[] { new() { Type = TypeMessage.warning.ToString(), Description = "No fue posible recuperar la cantidad de registros" } },
                    Pagination = pagedPosts,
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            //return usuarios;
        }


        //public async Task<IEnumerable<Usuario>> GetAllUsersAsync(
        //    UsuarioQueryFilter? filters=null)  //------------------------ ver
        //{
        //    //return await _usuarioRepository.GetAll();
        //      //    todos los repos  //todas las transacciones //elementos del CRUD
        //    //return await _unitOfWork.UsuarioRepository.GetAll();
        //    var usuarios = await _unitOfWork.UsuarioRepository.GetAll();
        //    if(filters != null)
        //    {

        //        if (filters.Email != null)
        //        {
        //            usuarios = usuarios.Where(a => a.Email.ToLower().Contains(filters.Email.ToLower()));
        //        }
        //        if (filters.Apellido!= null)
        //        {
        //            usuarios = usuarios.Where(a => a.Apellido.ToLower().Contains(filters.Apellido.ToLower()));
        //        }

        //        if (filters.Nombre != null)
        //        {
        //            usuarios = usuarios.Where(a => a.Nombre.ToLower().Contains(filters.Nombre.ToLower()));
        //        } 
        //        if (filters.Telefono != null)
        //        {
        //            usuarios = usuarios.Where(a => a.Telefono == filters.Telefono);
        //        }
        //        if (filters.FechaRegistro != null)
        //        {
        //            string fechaAux =
        //                Procesos.ParseFechaFlexible(filters.FechaRegistro);
        //            if (fechaAux != null)
        //            {
        //                usuarios = usuarios.Where(x => x.FechaRegistro.ToShortDateString() ==
        //                Convert.ToDateTime(fechaAux).ToShortDateString());
        //            }
        //        }
        //    }
        //    return usuarios;
        //}

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            //return await _usuarioRepository.GetById(id);
            return await _unitOfWork.UsuarioRepository.GetById(id);
        }
        public async Task RegistrarUsuario(Usuario usuario)
        {
            await GetUsuarioByEmail(usuario.Email);
            await ValidateNoDuplicate(usuario);
            await ValidateTelefonoYaExistente(usuario.Telefono);
            //var user = await _usuarioRepository.GetById(usuario.Id);
            var user = new Usuario
            { 
            
            };
                

            if (ContainsFobbidenWord(usuario.Apellido))
            {
                throw new BussinesExeption("Apellido no permitido",HttpStatusCode.BadRequest);
            }
            if (ContainsFobbidenWord(usuario.Nombre))
            {
                throw new Exception("Nombre no permitido");
            }

            //await _usuarioRepository.Add(usuario);
            await _unitOfWork.UsuarioRepository.Add(usuario);
            await _unitOfWork.SaveChangesAsync();
        }
        public void UpdateUsuario(Usuario usuario)
        {
            //await _usuarioRepository.Update(usuario);
             _unitOfWork.UsuarioRepository.Update(usuario);
            _unitOfWork.SaveChangesAsync();
     
        }
        public async Task DeleteUsuario(int id)
        {
            //await _usuarioRepository.Delete(id);
            await _unitOfWork.UsuarioRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
        // Agregar estos métodos a tu UsuariosService existente:

        public async Task DesactivarUsuario(int id)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetById(id);
            if (usuario == null)
                throw new BussinesExeption("Usuario no encontrado", HttpStatusCode.NotFound);

            if (usuario.IsActive == 0)
                throw new BussinesExeption("El usuario ya está desactivado", HttpStatusCode.BadRequest);

            usuario.IsActive = 0;
            _unitOfWork.UsuarioRepository.Update(usuario);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ActivarUsuario(int id)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetById(id);
            if (usuario == null)
                throw new BussinesExeption("Usuario no encontrado", HttpStatusCode.NotFound);

            if (usuario.IsActive == 1)
                throw new BussinesExeption("El usuario ya está activo", HttpStatusCode.BadRequest);

            usuario.IsActive = 1;
            _unitOfWork.UsuarioRepository.Update(usuario);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosInactivosAsync()
        {
            return await _unitOfWork.UsuarioRepository.GetUsuariosInactivos();
        }

        public async Task<IEnumerable<UsuarioPedidosSimple>> GetUsuariosConMasPedidosAsync()
        {
            return await _unitOfWork.UsuarioRepository.GetUsuariosConMasPedidos();
        }


        public async Task<string> SolicitudCodigo(SolicitudCambiodeContrasena usuario)
        {
            var usuario1 = await _unitOfWork.UsuarioRepository.GetById(usuario.id);
            if (usuario1 != null)
            {
                if (usuario1.Telefono == usuario.Telefono && usuario1.Email == usuario.gmail)
                {
                    // Regla de negocio: Límite de 2 códigos de seguridad por semana
                    var fechaLimite = DateTime.Now.AddDays(-7);
                    var codigos = await _unitOfWork.CodigoseguridadRepository.GetAll();
                    var codigosEstaSemana = codigos.Count(c => c.UsuarioId == usuario.id && c.FechaCreacion >= fechaLimite);

                    if (codigosEstaSemana >= 2)
                    {
                        throw new BussinesExeption("Has superado el límite de 2 códigos de seguridad por semana", HttpStatusCode.BadRequest);
                    }

                    // Generar el código y guardarlo en la base de datos
                    var codigo = await _unitOfWork.CodigoseguridadRepository.GenerarCodigoSeguridad(usuario.id);
                    await _unitOfWork.SaveChangesAsync();
                    
                    return codigo;
                }
                else
                {
                    throw new BussinesExeption("error en comparacion", HttpStatusCode.BadRequest);
                }
            }
            else
            {
                throw new BussinesExeption("no existe usuario", HttpStatusCode.BadRequest);
            }
        }

        public async Task<bool> ActualizarContrasena(NuevaContrasena usuario)
        {
            var aux = await _unitOfWork.UsuarioRepository.GetById(usuario.id);
            if (aux == null)
                throw new BussinesExeption("Usuario no encontrado", HttpStatusCode.NotFound);

            // Obtener el último código de seguridad para este usuario
            var codigos = await _unitOfWork.CodigoseguridadRepository.GetAll();
            var ultimoCodigo = codigos
                .Where(x => x.UsuarioId == usuario.id)
                .OrderByDescending(x => x.FechaCreacion)
                .FirstOrDefault();

            if (ultimoCodigo == null)
                throw new BussinesExeption("No hay un codigo solicitado", HttpStatusCode.BadRequest);

            if (ultimoCodigo.Usado == 1)
                throw new BussinesExeption("El codigo ya fue utilizado", HttpStatusCode.BadRequest);

            if (ultimoCodigo.FechaExpiracion < DateTime.Now)
                throw new BussinesExeption("Codigo caducado", HttpStatusCode.BadRequest);

            if (ultimoCodigo.Codigo != usuario.codigo)
                throw new BussinesExeption("Codigo incorrecto", HttpStatusCode.BadRequest);

            // Marcar el código como usado
            ultimoCodigo.Usado = 1;
            _unitOfWork.CodigoseguridadRepository.Update(ultimoCodigo);

            // Actualizar la contraseña
            aux.Password = usuario.nuevaContrasena; // ¡Debes hashear!
            _unitOfWork.UsuarioRepository.Update(aux);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ResenaSimple>> GetUsuariosResena(int id)
        {
           return await _unitOfWork.UsuarioRepository.GetUsuariosResenas(id);
        }

        //funciones auxiliares----------------------------------------------------------------------------

        private async Task GetUsuarioByEmail(string email)
        {
            //var usuarios = await _usuarioRepository.GetAll();
            var usuarios = await _unitOfWork.UsuarioRepository.GetAll();
            var existe = usuarios.Any(usuario => usuario.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (existe == true)
            {
                throw new Exception("Esta cuenta ya fue registrada con este email");
            }
        }

        private async Task ValidateNoDuplicate(Usuario usuario)
        {
            //var usuarios = await _usuarioRepository.GetAll();
            var usuarios = await _unitOfWork.UsuarioRepository.GetAll();

            // de aca es
            var existeMismaPersona = usuarios.Any(u =>
                u.Nombre.Equals(usuario.Nombre, StringComparison.OrdinalIgnoreCase) &&
                u.Apellido.Equals(usuario.Apellido, StringComparison.OrdinalIgnoreCase));

            if (existeMismaPersona)
                throw new Exception("Ya existe una cuenta registrada con este nombre y apellido");
        }
        public async Task ValidateTelefonoYaExistente(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return;

            //var usuarios = await _usuarioRepository.GetAll();
            var usuarios = await _unitOfWork.UsuarioRepository.GetAll();
            var existe = usuarios.Any(u => u.Telefono == telefono);

            if (existe)
                throw new Exception("Este número de telefono ya esta registrado");
        }

        private bool ContainsFobbidenWord(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            foreach (var word in ForbbidenWords)
            {
                if (text.Contains(word, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
        public readonly string[] ForbbidenWords =
        {
            "pendejo", "pendeja","maricon", "marica", "culero", 
            "estupido", "estupida", "idiota", "imbecil", 
            "bastardo", "maldito", "maldita", "coño", "boludo", 
            "pelotudo", "concha", "pichula", "weon", "chucha", "mamaguevo",
            "baboso"
        };

        public async Task<pedido_usuario> GetEstadisticasPedidosAsync(int usuarioId)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetById(usuarioId);
            if (usuario == null)
            {
                throw new BussinesExeption("El usuario no existe.", HttpStatusCode.NotFound);
            }

            return await _unitOfWork.UsuarioRepository.GetEstadisticasPedidos(usuarioId);
        }
    }
}

using System.Net;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Exceptions;
using VentasLimpieza.Core.Helpers;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Core.QueryFilter;
using VentasLimpieza.Services.Interfaces;

namespace VentasLimpieza.Services.Services
{
    public class CodigoseguridadService : ICodigoseguridadService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CodigoseguridadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Codigoseguridad> GetCodigoByIdAsync(int id)
        {
            return await _unitOfWork.CodigoseguridadRepository.GetById(id);
        }


        public async Task<IEnumerable<Codigoseguridad>> GetAllCodigosAsync(CodigoseguridadQueryFilter? filters = null)
        {
            var codigos = await _unitOfWork.CodigoseguridadRepository.GetAll();

            if (filters != null)
            {
                if (filters.Id.HasValue)
                {
                    codigos = codigos.Where(x => x.Id == filters.Id.Value);
                }
                if (filters.UsuarioId.HasValue)
                {
                    codigos = codigos.Where(x => x.UsuarioId == filters.UsuarioId.Value);
                }
                if (!string.IsNullOrEmpty(filters.Codigo))
                {
                    codigos = codigos.Where(x => x.Codigo.Contains(filters.Codigo));
                }
                if (!string.IsNullOrEmpty(filters.FechaCreacion))
                {
                    string fechaAux = Procesos.ParseFechaFlexible(filters.FechaCreacion);
                    if (fechaAux != null)
                    {
                        DateTime fechaFiltro = Convert.ToDateTime(fechaAux);
                        codigos = codigos.Where(x => x.FechaCreacion.Date == fechaFiltro.Date);
                    }
                }
                if (!string.IsNullOrEmpty(filters.FechaExpiracion))
                {
                    string fechaAux = Procesos.ParseFechaFlexible(filters.FechaExpiracion);
                    if (fechaAux != null)
                    {
                        DateTime fechaFiltro = Convert.ToDateTime(fechaAux);
                        codigos = codigos.Where(x => x.FechaExpiracion.Date == fechaFiltro.Date);
                    }
                }
                if (filters.Usado.HasValue)
                {
                    codigos = codigos.Where(x => x.Usado == filters.Usado.Value);
                }
            }

            return codigos;
        }

        public async Task<string> Solicitud_Codigo(int usuarioId)
        {
            return await GenerarCodigoSeguridad(usuarioId);
           
        }

        public async Task<bool> VerificarCodigoPorUsuario(int usuarioId, string codigoSeg)
        {
            var codigo = await ObtenerUltimoCodigoPorUsuario(usuarioId);

            if (codigo == null)
            {
                throw new BussinesExeption("No hay un codigo solicitado", HttpStatusCode.BadRequest);
            }

            if (codigo.Usado == 1)
            {
                throw new BussinesExeption("El codigo ya fue utilizado", HttpStatusCode.BadRequest);
            }

            if (codigo.FechaExpiracion < DateTime.Now)
            {
                throw new BussinesExeption("Codigo caducado", HttpStatusCode.BadRequest);
            }

            if (codigo.Codigo != codigoSeg)
            {
                throw new BussinesExeption("Codigo incorrecto", HttpStatusCode.BadRequest);
            }

            // Marcar como usado
            codigo.Usado = 1;
            _unitOfWork.CodigoseguridadRepository.Update(codigo);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }


        private async Task<Codigoseguridad?> ObtenerUltimoCodigoPorUsuario(int usuarioId)
        {
            var codigos = await _unitOfWork.CodigoseguridadRepository.GetAll();

            return codigos
                .Where(x => x.UsuarioId == usuarioId)
                .OrderByDescending(x => x.FechaCreacion)
                .FirstOrDefault();
        }



        private async Task<string> GenerarCodigoSeguridad(int usuarioId)
        {
            var codigo = new Codigoseguridad
            {
                UsuarioId = usuarioId,
                Codigo = GenerarCodigoSeguridadGenerarCodigoAleatorio(),
                FechaCreacion = DateTime.Now,
                FechaExpiracion = DateTime.Now.AddMinutes(12),
                Usado = 0
            };

            await RegistrarCodigo(codigo);

            return codigo.Codigo;
        }



        public async Task DeleteCodigo(int id)
        {
            await _unitOfWork.CodigoseguridadRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }


        private async Task RegistrarCodigo(Codigoseguridad codigo)
        {
            await _unitOfWork.CodigoseguridadRepository.Add(codigo);
            await _unitOfWork.SaveChangesAsync();
        }

        private string GenerarCodigoSeguridadGenerarCodigoAleatorio()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

    }
}

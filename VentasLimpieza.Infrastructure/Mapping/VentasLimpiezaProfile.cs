using AutoMapper;
using VentasLimpieza.core.Dtos;
using VentasLimpieza.Core.Dtos;
using VentasLimpieza.Core.Entities;
using VentasLimpieza.Core.Helpers;

namespace VentasLimpieza.Infrastructure.Mapping
{
    public class VentasLimpiezaProfile : Profile
    {
        public VentasLimpiezaProfile()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioDto, Usuario>();

            CreateMap < Categoria, CategoriaDto>();
            CreateMap<CategoriaDto, Categoria>();

            CreateMap<Direccion, DireccionDto>();
            CreateMap<DireccionDto, Direccion>();

            CreateMap<Producto, ProductoDtoPorLote>();
            CreateMap<ProductoDtoPorLote, ProductoDtoPorLote>();

            CreateMap<Resena, ResenaDto>();
            CreateMap<ResenaDto, Resena>();

            CreateMap<Pedido, PedidoDto>();
            CreateMap<PedidoDto, Pedido>();

            CreateMap<Codigoseguridad, CodigoseguridadDto>();
            CreateMap<CodigoseguridadDto, Codigoseguridad>();

            CreateMap<Detallepedido, DetallepedidoDto>();
            CreateMap<DetallepedidoDto, Detallepedido>();

            //para security
            CreateMap<Security, SecurityDto>().ReverseMap();

            CreateMap<LoteproductoDto, Loteproducto>()
                .ForMember(dest => dest.FechaFabricacion, opt => opt.MapFrom(src =>
               DateOnly.Parse(Procesos.ParseFechaFlexible(src.FechaFabricacion))))
                .ForMember(dest => dest.FechaCaducidad, opt => opt.MapFrom(src =>
               DateOnly.Parse(Procesos.ParseFechaFlexible(src.FechaCaducidad))));

            CreateMap<Loteproducto, LoteproductoDto>()
                .ForMember(dest => dest.FechaFabricacion, opt => opt.MapFrom(src =>
                    src.FechaFabricacion.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.FechaCaducidad, opt => opt.MapFrom(src =>
                    src.FechaCaducidad.ToString("dd/MM/yyyy")));
        }
    }
}

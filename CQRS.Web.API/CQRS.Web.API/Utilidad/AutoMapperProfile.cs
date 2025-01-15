using AutoMapper;
using CQRS.Web.API.Application.ViewModels;
using CQRS.Web.API.Domain;
using CQRS.Web.API.Infrastructure.Commands.Propuestas;

namespace CQRS.Web.API.Utilidad
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PropuestaDTO, crm_Propuestas>()
                .ForMember(dest => dest.IdPropuesta, opt => opt.Ignore())
                .ForMember(dest => dest.FechaHoraCreacion, opt => opt.Ignore())
                .ForMember(dest => dest.FechaHoraModificacion, opt => opt.Ignore());


            CreateMap<crm_Propuestas, PropuestaDTO>()
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.crm_Clientes.IdCliente))
                .ForMember(d => d.NombreCliente, o => o.MapFrom(m => m.crm_Clientes.Nombre));

            CreateMap<UpdatePropuestaCommand, PropuestaDTO>()
                .ForMember(dest => dest.IdPropuesta, opt => opt.MapFrom(src => src.IdPropuesta))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.FormaPago, opt => opt.MapFrom(src => src.FormaPago))
                .ForMember(dest => dest.NumCuotas, opt => opt.MapFrom(src => src.NumCuotas))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
                .ForMember(dest => dest.Aprobada, opt => opt.MapFrom(src => src.Aprobada))
                .ForMember(dest => dest.Rechazada, opt => opt.MapFrom(src => src.Rechazada))
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.FechaHoraModificacion, opt => opt.MapFrom(src => src.FechaHoraModificacion))
                .ForMember(dest => dest.FechaVencimiento, opt => opt.MapFrom(src => src.FechaVencimiento))
                .ForMember(dest => dest.FechaAprobacion, opt => opt.MapFrom(src => src.FechaAprobacion));

            CreateMap<CreatePropuestaCommand, PropuestaDTO>()
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.FormaPago, opt => opt.MapFrom(src => src.FormaPago))
                .ForMember(dest => dest.NumCuotas, opt => opt.MapFrom(src => src.NumCuotas))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
                .ForMember(dest => dest.Aprobada, opt => opt.MapFrom(src => src.Aprobada))
                .ForMember(dest => dest.Rechazada, opt => opt.MapFrom(src => src.Rechazada))
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdCliente));
        }
    }

}

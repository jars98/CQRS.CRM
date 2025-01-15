using CQRS.Web.API.Application.ViewModels;
using CQRS.Web.API.Domain;
using MediatR;

namespace CQRS.Web.API.Infrastructure.Commands.Propuestas
{
    public record CreatePropuestaCommand(string Numero,DateTime Fecha,string Descripcion, string FormaPago,int NumCuotas,
        decimal Total, bool Aprobada, bool Rechazada, int IdCliente, DateTime FechaHoraCreacion) 
        : IRequest<PropuestaDTO>;
}

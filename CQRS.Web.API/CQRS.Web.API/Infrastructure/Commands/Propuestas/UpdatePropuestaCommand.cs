using CQRS.Web.API.Application.ViewModels;
using MediatR;

namespace CQRS.Web.API.Infrastructure.Commands.Propuestas
{
    public record UpdatePropuestaCommand(int IdPropuesta,string Numero, DateTime Fecha, string Descripcion, string FormaPago, int NumCuotas,
        decimal Total, bool Aprobada, bool Rechazada, int IdCliente, DateTime FechaHoraModificacion, DateTime FechaVencimiento, DateTime FechaAprobacion) 
        : IRequest<PropuestaDTO>;
}

using CQRS.Web.API.Application.ViewModels;
using MediatR;

namespace CQRS.Web.API.Infrastructure.Queries
{    
    public record GetPropuestaByIdQuery(int Id) : IRequest<PropuestaDTO>;
}

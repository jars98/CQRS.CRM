using CQRS.Web.API.Application.ViewModels;
using MediatR;

namespace CQRS.Web.API.Infrastructure.Queries
{
    public record GetAllPropuestasQuery : IRequest<IEnumerable<PropuestaDTO>>;

}

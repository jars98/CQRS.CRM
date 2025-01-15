using MediatR;

namespace CQRS.Web.API.Infrastructure.Commands.Propuestas
{
    public record DeletePropuestasCommand(int Id) : IRequest<bool>;

}

using CQRS.Web.API.Application.ViewModels;
using CQRS.Web.API.Infrastructure.Queries;
using CQRS.Web.API.Infrastructure.Services.Contracts;
using MediatR;

namespace CQRS.Web.API.Application.Handlers
{
    public class GetAllPropuestasHandler : IRequestHandler<GetAllPropuestasQuery, IEnumerable<PropuestaDTO>>
    {
        private readonly IPropuestaRepository _propuestaRepository;

        public GetAllPropuestasHandler(IPropuestaRepository propuestaRepository)
        {
            _propuestaRepository = propuestaRepository;
        }

        public async Task<IEnumerable<PropuestaDTO>> Handle(GetAllPropuestasQuery request, CancellationToken cancellationToken)
        {
            var propuestas = await _propuestaRepository.Lista();
            return propuestas;
        }
    }
}

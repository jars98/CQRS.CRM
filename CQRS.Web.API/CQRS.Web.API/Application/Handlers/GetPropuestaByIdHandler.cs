using CQRS.Web.API.Application.ViewModels;
using CQRS.Web.API.Infrastructure.Queries;
using CQRS.Web.API.Infrastructure.Services.Contracts;
using MediatR;

namespace CQRS.Web.API.Application.Handlers
{
    public class GetPropuestaByIdHandler : IRequestHandler<GetPropuestaByIdQuery, PropuestaDTO>
    {
        private readonly IPropuestaRepository _propuestaRepository;

        public GetPropuestaByIdHandler(IPropuestaRepository propuestaRepository)
        {
            _propuestaRepository = propuestaRepository;
        }

        public async Task<PropuestaDTO> Handle(GetPropuestaByIdQuery request, CancellationToken cancellationToken)
        {
            var propuesta = await _propuestaRepository.ObtenerById(request.Id);
            return propuesta;
        }
    }
}

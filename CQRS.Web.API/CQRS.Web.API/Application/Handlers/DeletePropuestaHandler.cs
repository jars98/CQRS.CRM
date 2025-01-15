using CQRS.Web.API.Application.ViewModels;
using CQRS.Web.API.Infrastructure.Commands.Propuestas;
using CQRS.Web.API.Infrastructure.Services.Contracts;
using MediatR;

namespace CQRS.Web.API.Application.Handlers
{
    public class DeletePropuestaHandler : IRequestHandler<DeletePropuestasCommand, bool>
    {
        private readonly IPropuestaRepository _propuestaRepository;

        public DeletePropuestaHandler(IPropuestaRepository propuestaRepository)
        {
            _propuestaRepository = propuestaRepository;
        }

        public async Task<bool> Handle(DeletePropuestasCommand request, CancellationToken cancellationToken)
        {
            var model = await _propuestaRepository.ObtenerById(request.Id);

            if (model == null)
                return false;

            await _propuestaRepository.Eliminar(model.IdPropuesta);
            return true;
        }

    }
}

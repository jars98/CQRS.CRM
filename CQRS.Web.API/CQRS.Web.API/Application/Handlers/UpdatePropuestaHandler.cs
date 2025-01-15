using AutoMapper;
using CQRS.Web.API.Application.ViewModels;
using CQRS.Web.API.Infrastructure.Commands.Propuestas;
using CQRS.Web.API.Infrastructure.Services.Contracts;
using FluentValidation;
using MediatR;

namespace CQRS.Web.API.Application.Handlers
{
    public class UpdatePropuestaHandler : IRequestHandler<UpdatePropuestaCommand, PropuestaDTO>
    {
        private readonly IPropuestaRepository _propuestaRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdatePropuestaCommand> _validator;

        public UpdatePropuestaHandler(IPropuestaRepository propuestaRepository, IMapper mapper, IValidator<UpdatePropuestaCommand> validator)
        {
            _propuestaRepository = propuestaRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PropuestaDTO> Handle(UpdatePropuestaCommand request, CancellationToken cancellationToken)
        {
            // Validar el comando
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);


            var model = await _propuestaRepository.ObtenerById(request.IdPropuesta);

            if (model == null)
                return null;

            _mapper.Map(request, model);

            await _propuestaRepository.Editar(model);
            return _mapper.Map<PropuestaDTO>(model);
        }

    }
}

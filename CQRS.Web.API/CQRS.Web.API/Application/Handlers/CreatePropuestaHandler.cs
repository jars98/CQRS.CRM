using AutoMapper;
using CQRS.Web.API.Application.ViewModels;
using CQRS.Web.API.Domain;
using CQRS.Web.API.Infrastructure.Commands.Propuestas;
using CQRS.Web.API.Infrastructure.Services.Contracts;
using FluentValidation;
using MediatR;

namespace CQRS.Web.API.Application.Handlers
{
    public class CreatePropuestaHandler : IRequestHandler<CreatePropuestaCommand, PropuestaDTO>
    {
        private readonly IPropuestaRepository _propuestaRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePropuestaCommand> _validator;

        public CreatePropuestaHandler(IPropuestaRepository propuestaRepository, IMapper mapper, IValidator<CreatePropuestaCommand> validator)
        {
            _propuestaRepository = propuestaRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PropuestaDTO> Handle(CreatePropuestaCommand request, CancellationToken cancellationToken)
        {
            // Validar el comando
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var propuesta = _mapper.Map<PropuestaDTO>(request);

            _mapper.Map(request, propuesta);
             var model =  await _propuestaRepository.Crear(propuesta);
            return _mapper.Map<PropuestaDTO>(model);
        }
    }
}

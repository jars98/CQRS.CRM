using CQRS.Web.API.Infrastructure.Commands.Propuestas;
using FluentValidation;

namespace CQRS.Web.API.Application.Validator
{
    public class CreatePropuestaCommandvalidator : AbstractValidator<CreatePropuestaCommand>
    {
        public CreatePropuestaCommandvalidator() 
        {
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");

            RuleFor(x => x.Total)
                .GreaterThan(0).WithMessage("El total debe ser mayor a 0.");

            RuleFor(x => x.NumCuotas)
                .GreaterThanOrEqualTo(1).WithMessage("Debe haber al menos una cuota.");

            RuleFor(x => x.IdCliente)
               .GreaterThan(0).WithMessage("Debe seleccionar un cliente.");

        }
    }
}

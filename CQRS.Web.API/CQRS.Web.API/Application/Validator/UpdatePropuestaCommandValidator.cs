using CQRS.Web.API.Infrastructure.Commands.Propuestas;
using FluentValidation;

namespace CQRS.Web.API.Application.Validator
{
    public class UpdatePropuestaCommandValidator : AbstractValidator<UpdatePropuestaCommand>
    {
        public UpdatePropuestaCommandValidator()
        {
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");

            RuleFor(x => x.Total)
                .GreaterThan(0).WithMessage("El total debe ser mayor a 0.");

            RuleFor(x => x.NumCuotas)
                .GreaterThanOrEqualTo(1).WithMessage("Debe haber al menos una cuota.");

            RuleFor(x => x.FechaVencimiento)
                .GreaterThan(DateTime.Now).WithMessage("La fecha de vencimiento debe ser futura.")
                .When(x => x.FechaVencimiento != default);

            RuleFor(x => x.FechaAprobacion)
                .GreaterThanOrEqualTo(x => x.Fecha)
                .WithMessage("La fecha de aprobación debe ser posterior o igual a la fecha de creación.")
                .When(x => x.FechaAprobacion != default);
        }
    }
}
using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class GeneroValidator : AbstractValidator<GeneroModel>
    {
        public GeneroValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não pode ser vazia")

                .NotNull()
                .WithMessage("A entidade não pode ser nula");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome não pode ser vazio")

                .NotNull()
                .WithMessage("O nome não pode ser nulo")
               
                .MaximumLength(60)
                .WithMessage("O nome deve ter no máximo 60 caracteres");
        }
    }
}

using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioModel>
    {
        public UsuarioValidator()
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

                .MinimumLength(3)
                .WithMessage("O nome deve ter no mínimo 3 caracteres")
                .MaximumLength(60)
                .WithMessage("O nome deve ter no máximo 60 caracteres");

            RuleFor(x => x.GeneroId)
               .NotEmpty()
               .WithMessage("O Gênero não pode ser vazio")

               .NotNull()
               .WithMessage("O Gênero não pode ser nulo");
              

            RuleFor(x => x.Email)
                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                .WithMessage("e-mail deve ser válido")
                .NotEmpty()
                .WithMessage("O e-mail não pode ser vazio")

                .NotNull()
                .WithMessage("O e-maill não pode ser nulo")

                .MaximumLength(180)
                .WithMessage("O e-mail deve ter no máximo 180 caracteres");


        }
    }
}

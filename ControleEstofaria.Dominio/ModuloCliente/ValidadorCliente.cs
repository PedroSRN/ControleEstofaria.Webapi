using FluentValidation;

namespace ControleEstofaria.Dominio.ModuloCliente
{
    public class ValidadorCliente : AbstractValidator<Cliente>
    {
        public ValidadorCliente()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("O campo nome é obrigatório")
                .NotEmpty().WithMessage("O campo nome é obrigatório");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O campo email é obrigatório")
                .NotNull().WithMessage("O campo email é obrigatório");


            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O campo telefone é obrigatório")
                .NotNull().WithMessage("O campo telefone é obrigatório");

            RuleFor(x => x.CNPJ)
                .NotEmpty().WithMessage("O campo CNPJ é obrigatório")
                .NotNull().WithMessage("O campo CNPJ é obrigatório");

        }
    }
}

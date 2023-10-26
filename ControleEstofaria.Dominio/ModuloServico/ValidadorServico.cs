using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Dominio.ModuloServico
{
    public class ValidadorServico : AbstractValidator<Servico>
    {
        public ValidadorServico() 
        {
            RuleFor(x => x.NomeServico)
                .NotNull().WithMessage("O campo nome do serviço é obrigatório")
                .NotEmpty().WithMessage("O campo nome do serviço é obrigatório");

            RuleFor(x => x.Descricao)
                .NotNull();

            RuleFor(x => x.DataEntradaServico)
                .NotNull();

            RuleFor(x => x.DataSaidaServico)
               .NotNull()
               .GreaterThanOrEqualTo((x) => DateTime.Now.Date).WithMessage("A data deve ser superior ou igual a data atual.");

            RuleFor(x => x.ValorServico) 
                .GreaterThan(1)
                .NotEmpty();

  
        }
    }
}

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
                //.GreaterThanOrEqualTo((x) => DateTime.Today.Date).WithMessage("A data de entrada deve ser superior ou igual a data atual.");

            //RuleFor(x => x.DataSaidaServico)
            //    .GreaterThanOrEqualTo(x => x.DataEntradaServico)
            //    .WithMessage("A data de saída deve ser superior ou igual a data de entrada");


            RuleFor(x => x.ValorServico) 
                .GreaterThan(1)
                .NotEmpty();

  
        }
    }
}

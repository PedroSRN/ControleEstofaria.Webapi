using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Dominio.ModuloFinanca
{
    public class ValidadorFinanca : AbstractValidator<Financa>
    {
        public ValidadorFinanca()
        {
            RuleFor(x => x.Saldo)
                .NotEmpty()
                .NotNull();

            
        }
    }
}

﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .NotNull().WithMessage("O campo email é obrigatório")
                .NotEqual(x => x.Email); // verificar se não vai bugar a incersão 

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O campo telefone é obrigatório")
                .NotNull().WithMessage("O campo telefone é obrigatório");

            RuleFor(x => x.CNPJ) 
                .NotEmpty().WithMessage("O campo CNPJ é obrigatório")
                .NotNull().WithMessage("O campo CNPJ é obrigatório")
                .NotEqual(x => x.CNPJ); // verificar se não vai bugar a incersão 

        }
    }
}
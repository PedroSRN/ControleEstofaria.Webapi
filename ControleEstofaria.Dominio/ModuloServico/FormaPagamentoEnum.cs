using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Dominio.ModuloServico
{
    public enum FormaPagamentoEnum
    {
        [Description("Cartão")]
        Cartao,

        [Description("Dinheiro")]
        Dinheiro,

        [Description("PIX")]
        PIX
    }
}

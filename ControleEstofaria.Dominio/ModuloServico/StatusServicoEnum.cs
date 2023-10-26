using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Dominio.ModuloServico
{
    public enum StatusServicoEnum
    {
        [Description("Pronto")]
        Pronto,

        [Description("Em Andamento")]
        EmAndamento,

        [Description("Não Iniciado")]
        NaoIniciado,

        [Description("Aguardando o Cliente")]
        AguardandoCliente
    }
}

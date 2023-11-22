using ControleEstofaria.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Dominio.ModuloServico
{
    public interface IRepositorioServico : IRepositorio<Servico>
    {
       List<Servico> SelecionarServicosProntos();

        List<Servico> SelecionarServicosProntosNoPeriodo(DateTime dataInicio, DateTime dataFim);

         decimal SomarValorServicosProntosNoPeriodo(DateTime dataInicio, DateTime dataFim);

         decimal ObterTotalValorServicos();
    }
}

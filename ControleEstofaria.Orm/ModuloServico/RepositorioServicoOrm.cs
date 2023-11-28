using ControleEstofaria.Dominio.Compartilhado;
using ControleEstofaria.Dominio.ModuloServico;
using ControleEstofaria.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Orm.ModuloServico
{
    public class RepositorioServicoOrm : RepositorioBase<Servico>, IRepositorioServico
    {
        public RepositorioServicoOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }

        public override Servico SelecionarPorId(Guid id)
        {
            return registros
                .Include(x => x.Cliente)
                .SingleOrDefault(x => x.Id == id);
        }

        public override List<Servico> SelecionarTodos()
        {
            return registros
                .Include(x => x.Cliente)
                .ToList();
        }

        /// select para manipular serviços prontos
        public List<Servico> SelecionarServicosProntos()
        {
            return registros
                .Where(x => x.StatusServico == StatusServicoEnum.Pronto)
                .Include(x => x.Cliente)
                .ToList();
        }

        public List<Servico> SelecionarServicosProntosNoPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return registros
                .Where(x => x.StatusServico == StatusServicoEnum.Pronto &&
                    x.DataSaidaServico.Date >= dataInicio.Date &&
                    x.DataSaidaServico.Date <= dataFim.Date) 
                .Include(x => x.Cliente)
                .ToList();
            

        }

        private decimal _totalValorServicos; // Variável para armazenar o valor somado

        public decimal SomarValorServicosProntosNoPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            _totalValorServicos = registros
                .Where(x => x.StatusServico == StatusServicoEnum.Pronto &&
                    x.DataSaidaServico.Date >= dataInicio.Date &&
                    x.DataSaidaServico.Date <= dataFim.Date)
                .Sum(x => x.ValorServico);

            return _totalValorServicos;
        }

        public decimal ObterTotalValorServicos()
        {
            return _totalValorServicos;
        }


    }
}

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

        public List<Servico> SelecionarServicosProntos()
        {
            return registros
                .Where(x => x.StatusServico == StatusServicoEnum.Pronto)
                .Include(x => x.Cliente)
                .ToList();
        }

        public override List<Servico> SelecionarTodos()
        {
            return registros
                .Include(x => x.Cliente)
                .ToList();
        }

       
    }
}

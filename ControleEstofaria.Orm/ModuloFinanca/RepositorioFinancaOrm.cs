using ControleEstofaria.Dominio.Compartilhado;
using ControleEstofaria.Dominio.ModuloFinanca;
using ControleEstofaria.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Orm.ModuloFinanca
{
    public class RepositorioFinancaOrm : RepositorioBase<Financa>, IRepositorioFinanca
    {
        public RepositorioFinancaOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }

        public override Financa SelecionarPorId(Guid id)
        {
            return registros
                .Include(x => x.Servico)
                .SingleOrDefault(x => x.Id == id);
        }

        public override List<Financa> SelecionarTodos()
        {
            return registros
                .Include(x => x.Servico)
                .ToList();
        }
    }
}

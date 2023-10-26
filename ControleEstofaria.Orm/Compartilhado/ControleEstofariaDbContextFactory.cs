using ControleEstofaria.Infra.Configs;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Orm.Compartilhado
{
    public class ControleEstofariaDbContextFactory : IDesignTimeDbContextFactory<ControleEstofariaDbContext>
    {
        public ControleEstofariaDbContext CreateDbContext(string[] args)
        {
            var config = new ConfiguracaoAplicacaoControleEstofaria();

            return new ControleEstofariaDbContext(config.ConnectionStrings);
        }
    }
}

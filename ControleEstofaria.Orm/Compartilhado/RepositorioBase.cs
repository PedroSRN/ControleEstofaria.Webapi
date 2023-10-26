using ControleEstofaria.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Orm.Compartilhado
{
    public abstract class RepositorioBase<TEntity> where TEntity : EntidadeBase<TEntity>
    {
        protected DbSet<TEntity> registros;
        private readonly ControleEstofariaDbContext dbContext;

        public RepositorioBase(IContextoPersistencia contextoPersistencia)
        {
            dbContext = (ControleEstofariaDbContext)contextoPersistencia;
            registros = dbContext.Set<TEntity>();
        }
        public virtual void Inserir(TEntity novoRegistro)
        {
            registros.Add(novoRegistro);
        }

        public virtual void Editar(TEntity registro)
        {
            registros.Update(registro);
        }

        public virtual void Excluir(TEntity registro)
        {
            registros.Remove(registro);
        }

        public virtual TEntity SelecionarPorId(Guid id)
        {
            return registros
                .SingleOrDefault(x => x.Id == id);
        }

        public virtual List<TEntity> SelecionarTodos()
        {
            return registros

                .ToList();
        }
    }
}

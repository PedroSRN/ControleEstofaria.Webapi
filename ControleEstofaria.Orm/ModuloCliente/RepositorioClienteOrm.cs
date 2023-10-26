using ControleEstofaria.Dominio.Compartilhado;
using ControleEstofaria.Dominio.ModuloCliente;
using ControleEstofaria.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Orm.ModuloCliente
{
    public class RepositorioClienteOrm : RepositorioBase<Cliente>, IRepositorioCliente
    {
        public RepositorioClienteOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }

        public Cliente SelecionarCNPJ(string cnpj)
        {
            return registros.SingleOrDefault(x => x.CNPJ == cnpj);
        }

        public Cliente SelecionarEmail(string email)
        {
            return registros.SingleOrDefault(x => x.Email == email);
        }

        public override Cliente SelecionarPorId(Guid id)
        {
            return registros
                .Include(x => x.Servicos)
                .SingleOrDefault(x => x.Id == id);
        }


    }
}

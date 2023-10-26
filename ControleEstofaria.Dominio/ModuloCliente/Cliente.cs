using ControleEstofaria.Dominio.Compartilhado;
using ControleEstofaria.Dominio.ModuloServico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Dominio.ModuloCliente
{
    public class Cliente : EntidadeBase<Cliente>
    {
        public Cliente()
        {

        }

        public Cliente(string nome, string telefone, string email, string cnpj)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            CNPJ = cnpj;
        }


        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }

        public override void Atualizar(Cliente registro)
        {
            Id = registro.Id;
            Nome = registro.Nome;
            Telefone = registro.Telefone;
            Email = registro.Email;
            CNPJ = registro.CNPJ;
        }

        public List<Servico> Servicos { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Cliente cliente &&
                   Id.Equals(cliente.Id) &&
                   Nome == cliente.Nome &&
                   Telefone == cliente.Telefone &&
                   Email == cliente.Email &&
                   CNPJ == cliente.CNPJ;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nome, Telefone, Email, CNPJ);
        }
    }
}

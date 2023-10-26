using ControleEstofaria.Dominio.Compartilhado;
using ControleEstofaria.Dominio.ModuloServico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Dominio.ModuloFinanca
{
    public class Financa : EntidadeBase<Financa>
    {
        private Servico _servico;

        public Financa()
        {

        }

        public Financa(decimal saldo, Servico servico)
        {
            Saldo = saldo;
            Servico = servico;
        }

        public decimal Saldo { get; set; }

        public Servico Servico
        {
            get { return _servico; }
            set
            {
                _servico = value;

                if (_servico != null)
                    ServicoId = _servico.Id;
            }
        }
        public Guid ServicoId { get; set; }

        public override void Atualizar(Financa registro)
        {
            Id = registro.Id;
            Saldo = registro.Saldo;
            Servico = registro.Servico;
        }

        public override bool Equals(object? obj)
        {
            return obj is Financa financa &&
                   Id.Equals(financa.Id) &&
                   EqualityComparer<Servico>.Default.Equals(_servico, financa._servico) &&
                   Saldo == financa.Saldo &&
                   EqualityComparer<Servico>.Default.Equals(Servico, financa.Servico) &&
                   ServicoId.Equals(financa.ServicoId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, _servico, Servico, ServicoId);
        }
    }
}

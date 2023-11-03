using ControleEstofaria.Dominio.Compartilhado;
using ControleEstofaria.Dominio.ModuloCliente;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Dominio.ModuloServico
{
    public class Servico : EntidadeBase<Servico>
    {
        private Cliente _cliente;

        public Servico()
        {

        }

        public Servico(string nomeServico, string descricao, DateTime dataEntradaServico, DateTime dataSaidaServico, decimal valorServico, FormaPagamentoEnum formapagamento, StatusServicoEnum statusServico, Cliente cliente)
        {
            NomeServico = nomeServico;
            Descricao = descricao;
            DataEntradaServico = dataEntradaServico;
            DataSaidaServico = dataSaidaServico;
            ValorServico = valorServico;
            FormaPagamento = formapagamento;
            StatusServico = statusServico;
            Cliente = cliente;
        }

        public string NomeServico { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEntradaServico { get; set; }
        public DateTime DataSaidaServico { get; set; }
        public decimal ValorServico { get; set; }
        public FormaPagamentoEnum FormaPagamento { get; set; }
        public StatusServicoEnum StatusServico { get; set; }

        public Cliente Cliente
        {
            get { return _cliente; }
            set
            {
                _cliente = value;

                if (_cliente != null)
                    ClienteId = _cliente.Id;
            }
        }
        public Guid? ClienteId { get; set; }

        public override void Atualizar(Servico registro)
        {
            Id = registro.Id;
            NomeServico = registro.NomeServico;
            Descricao = registro.Descricao;
            DataEntradaServico = registro.DataEntradaServico;
            DataSaidaServico = registro.DataSaidaServico;
            ValorServico = registro.ValorServico;
            FormaPagamento = registro.FormaPagamento;
            StatusServico = registro.StatusServico;
            Cliente = registro.Cliente;
        }

        public override bool Equals(object? obj)
        {
            return obj is Servico servico &&
                   Id.Equals(servico.Id) &&
                   EqualityComparer<Cliente>.Default.Equals(_cliente, servico._cliente) &&
                   NomeServico == servico.NomeServico &&
                   Descricao == servico.Descricao &&
                   DataEntradaServico == servico.DataEntradaServico &&
                   DataSaidaServico == servico.DataSaidaServico &&
                   ValorServico == servico.ValorServico &&
                   FormaPagamento == servico.FormaPagamento &&
                   StatusServico == servico.StatusServico &&
                   EqualityComparer<Cliente>.Default.Equals(Cliente, servico.Cliente);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(_cliente);
            hash.Add(NomeServico);
            hash.Add(Descricao);
            hash.Add(DataEntradaServico);
            hash.Add(DataSaidaServico);
            hash.Add(ValorServico);
            hash.Add(FormaPagamento);
            hash.Add(StatusServico);
            hash.Add(Cliente);
            hash.Add(ClienteId);
            return hash.ToHashCode();
        }
    }
}

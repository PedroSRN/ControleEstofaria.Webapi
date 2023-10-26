using ControleEstofaria.Webapi.ViewModels.ModuloCliente;

namespace ControleEstofaria.Webapi.ViewModels.ModuloServico
{
    public class VisualizarServicoViewModel
    {
        public Guid Id { get; set; }

        public string NomeServico { get; set; }

        public string Descricao { get; set; }

        public string DataEntradaServico { get; set; }

        public string DataSaidaServico { get; set; }

        public string ValorServico { get; set; }

        public string FormaPagamento { get; set; }

        public string StatusServico { get; set; }

        public ListarClienteViewModel Cliente { get; set; }
    }
}

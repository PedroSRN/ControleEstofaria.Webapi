using ControleEstofaria.Webapi.ViewModels.ModuloServico;

namespace ControleEstofaria.Webapi.ViewModels.ModuloCliente
{
    public class VisualizarClienteViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string CNPJ { get; set; }

        public List<ListarServicoViewModel> Servicos { get; set; }
    }
}

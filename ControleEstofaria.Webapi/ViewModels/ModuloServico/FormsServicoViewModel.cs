using ControleEstofaria.Dominio.ModuloServico;
using System.ComponentModel.DataAnnotations;

namespace ControleEstofaria.Webapi.ViewModels.ModuloServico
{
    public class FormsServicoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public string NomeServico { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public DateTime DataEntradaServico { get; set; }

        //[Required(ErrorMessage = "O campo '{0}' é obrigatório")]  /////////////////////////////////////////////////////////////////////////////////
        public DateTime DataSaidaServico { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public decimal ValorServico { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public FormaPagamentoEnum FormaPagamento { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public StatusServicoEnum StatusServico { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public Guid ClienteId { get; set; }
    }
}

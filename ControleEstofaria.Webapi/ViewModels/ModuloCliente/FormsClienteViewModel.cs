using System.ComponentModel.DataAnnotations;

namespace ControleEstofaria.Webapi.ViewModels.ModuloCliente
{
    public class FormsClienteViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public string CNPJ { get; set; }
    }
}

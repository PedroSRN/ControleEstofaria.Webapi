using System.ComponentModel.DataAnnotations;

namespace ControleEstofaria.Webapi.ViewModels.ModuloFinanca
{
    public class FormsFinancaViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public decimal Saldo { get; set; }

        
    }
}

using System.ComponentModel.DataAnnotations;

namespace GestaoObras.Web.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome do Material")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Stock Disponível")]
        public int StockDisponivel { get; set; }
    }
}

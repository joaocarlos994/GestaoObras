using System.ComponentModel.DataAnnotations;

namespace GestaoObras.Web.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do material é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        [Display(Name = "Nome do Material")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "A descrição não pode ter mais de 300 caracteres.")]
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "O stock disponível deve ser um número igual ou superior a 0.")]
        [Display(Name = "Stock Disponível")]
        [Required(ErrorMessage = "O stock disponível é obrigatório.")]
        public int StockDisponivel { get; set; }
    }
}

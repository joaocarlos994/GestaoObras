using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoObras.Web.Models
{
    public class Pagamento
    {
        public int Id { get; set; }

        [Required]
        public int ObraId { get; set; }
        public Obra? Obra { get; set; }

        [Required]
        [Display(Name = "Nome da pessoa")]
        public string NomePessoa { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Valor do pagamento")]
        public decimal Valor { get; set; }

        [Display(Name = "Data/Hora")]
        public DateTime DataRegisto { get; set; } = DateTime.UtcNow;
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoObras.Web.Models
{
    public class RegistoMaoObra
    {
        public int Id { get; set; }

        [Required]
        public int ObraId { get; set; }
        public Obra? Obra { get; set; }

        [Required]
        [Display(Name = "Nome da pessoa")]
        public string NomePessoa { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Horas trabalhadas")]
        public decimal HorasTrabalhadas { get; set; }

        [Display(Name = "Data/Hora")]
        public DateTime DataRegisto { get; set; } = DateTime.UtcNow;
    }
}

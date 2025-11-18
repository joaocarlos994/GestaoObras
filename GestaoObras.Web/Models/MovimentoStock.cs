using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoObras.Web.Models
{
    public class MovimentoStock
    {
        public int Id { get; set; }

        [Required]
        public int ObraId { get; set; }
        public Obra? Obra { get; set; }

        [Required]
        public int MaterialId { get; set; }
        public Material? Material { get; set; }

        [Required]
        [Display(Name = "Operação")]
        public string Operacao { get; set; } = "REMOVE"; // ADD ou REMOVE

        [Required]
        public int Quantidade { get; set; }

        [Display(Name = "Data da Operação")]
        [Column("DataHora")] // 👈 mapeia para a coluna DataHora da BD
        public DateTime DataOperacao { get; set; } = DateTime.UtcNow;
    }
}

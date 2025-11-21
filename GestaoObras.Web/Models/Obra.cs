using System.ComponentModel.DataAnnotations;

namespace GestaoObras.Web.Models
{
    public class Obra
    {
        public int Id { get; set; }

        // Nome da obra
        [Required(ErrorMessage = "O nome da obra é obrigatório.")]
        [StringLength(120, ErrorMessage = "O nome da obra não pode ter mais de 120 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        // Descrição da obra
        [StringLength(500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        // Cliente obrigatório
        [Required(ErrorMessage = "É necessário selecionar um cliente.")]
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        // Morada da Obra
        [Required(ErrorMessage = "A morada da obra é obrigatória.")]
        [StringLength(200, ErrorMessage = "A morada não pode ter mais de 200 caracteres.")]
        public string Morada { get; set; } = string.Empty;

        // Coordenadas
        [Range(-90, 90, ErrorMessage = "A latitude deve estar entre -90 e 90.")]
        public double Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "A longitude deve estar entre -180 e 180.")]
        public double Longitude { get; set; }

        // Estado
        public bool Ativa { get; set; }

        // Relacionamentos
        public ICollection<MovimentoStock> MovimentosStock { get; set; } = new List<MovimentoStock>();
        public ICollection<RegistoMaoObra> RegistosMaoObra { get; set; } = new List<RegistoMaoObra>();
        public ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace GestaoObras.Web.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O NIF é obrigatório.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "O NIF deve ter exatamente 9 dígitos.")]
        public string NIF { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Introduza um email válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [RegularExpression(@"^9\d{8}$", ErrorMessage = "O telefone deve ter 9 dígitos e começar por 9.")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "A morada é obrigatória.")]
        [StringLength(200, ErrorMessage = "A morada não pode ter mais de 200 caracteres.")]
        public string Morada { get; set; } = string.Empty;
    }
}

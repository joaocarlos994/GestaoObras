// Cliente.cs
// Model for Cliente

namespace GestaoObras.Web.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string NIF { get; set; } = string.Empty;
        public string Morada { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }
}
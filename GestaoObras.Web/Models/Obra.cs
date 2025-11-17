namespace GestaoObras.Web.Models
{
    public class Obra
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }   // <--- removido o new Cliente()

        public string Morada { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool Ativa { get; set; }

        public ICollection<MovimentoStock> MovimentosStock { get; set; } = new List<MovimentoStock>();
        public ICollection<RegistoMaoObra> RegistosMaoObra { get; set; } = new List<RegistoMaoObra>();
        public ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
    }
}

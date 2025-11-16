namespace GestaoObras.Web.Models
{
    public class Pagamento
    {
        public int Id { get; set; }

        public int ObraId { get; set; }
        public Obra Obra { get; set; } = new Obra();

        public string NomePessoa { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; }
    }
}

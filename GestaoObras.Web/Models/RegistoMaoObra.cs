namespace GestaoObras.Web.Models
{
    public class RegistoMaoObra
    {
        public int Id { get; set; }

        public int ObraId { get; set; }
        public Obra Obra { get; set; } = new Obra();

        public string NomePessoa { get; set; } = string.Empty;
        public decimal HorasTrabalhadas { get; set; }
        public DateTime DataHora { get; set; }
    }
}

namespace GestaoObras.Web.Models
{
    public class MovimentoStock
    {
        public int Id { get; set; }

        public int ObraId { get; set; }
        public Obra Obra { get; set; } = new Obra();

        public int MaterialId { get; set; }
        public Material Material { get; set; } = new Material();

        public string Operacao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public DateTime DataHora { get; set; }
    }
}

// Material.cs
// Model for Material

using System.Collections.Generic;
using GestaoObras.Web.Models;

namespace GestaoObras.Web.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int StockDisponivel { get; set; }
        public ICollection<MovimentoStock> MovimentosStock { get; set; } = new List<MovimentoStock>();
    }
}
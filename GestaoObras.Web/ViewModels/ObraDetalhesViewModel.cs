using System.Collections.Generic;
using GestaoObras.Web.Models;

namespace GestaoObras.Web.ViewModels
{
    public class ObraDetalhesViewModel
    {
        public Obra Obra { get; set; }
        public IEnumerable<MovimentoStock> MovimentosStock { get; set; }
        public IEnumerable<RegistoMaoObra> RegistosMaoObra { get; set; }
        public IEnumerable<Pagamento> Pagamentos { get; set; }
    }
}
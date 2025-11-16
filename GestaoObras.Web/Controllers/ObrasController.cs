// ObrasController.cs
// Controller logic for managing Obrasusing System.Linq;
using System.Threading.Tasks;
using GestaoObras.Web.Data;
using GestaoObras.Web.Models;
using GestaoObras.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestaoObras.Web.Controllers
{
    public class ObrasController : Controller
    {
        private readonly AppDbContext _context;

        public ObrasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Obras
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Obras.Include(o => o.Cliente);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Obras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var obra = await _context.Obras
                .Include(o => o.Cliente)
                .Include(o => o.MovimentosStock).ThenInclude(m => m.Material)
                .Include(o => o.RegistosMaoObra)
                .Include(o => o.Pagamentos)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obra == null) return NotFound();

            var vm = new ObraDetalhesViewModel
            {
                Obra = obra,
                MovimentosStock = obra.MovimentosStock.OrderByDescending(m => m.DataHora),
                RegistosMaoObra = obra.RegistosMaoObra.OrderByDescending(m => m.DataHora),
                Pagamentos = obra.Pagamentos.OrderByDescending(p => p.DataHora)
            };

            ViewBag.Materiais = await _context.Materiais.ToListAsync();

            return View(vm);
        }

        // POST: Obras/AdicionarMovimento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarMovimento(int obraId, MovimentoStock movimento)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Details), new { id = obraId });

            movimento.ObraId = obraId;
            movimento.DataHora = DateTime.Now;
            movimento.Operacao = "REMOVE"; // material usado na obra

            var material = await _context.Materiais.FindAsync(movimento.MaterialId);
            if (material != null)
            {
                material.StockDisponivel -= movimento.Quantidade;
            }

            _context.MovimentosStock.Add(movimento);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = obraId });
        }

        // POST: Obras/AdicionarMaoObra
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarMaoObra(int obraId, RegistoMaoObra registo)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Details), new { id = obraId });

            registo.ObraId = obraId;
            registo.DataHora = DateTime.Now;

            _context.RegistosMaoObra.Add(registo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = obraId });
        }

        // POST: Obras/AdicionarPagamento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarPagamento(int obraId, Pagamento pagamento)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Details), new { id = obraId });

            pagamento.ObraId = obraId;
            pagamento.DataHora = DateTime.Now;

            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = obraId });
        }

        // … (resto do CRUD Create/Edit/Delete que o scaffolding gerou)
    }
}

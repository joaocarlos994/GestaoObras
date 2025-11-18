using System;
using System.Linq;
using System.Threading.Tasks;
using GestaoObras.Web.Data;
using GestaoObras.Web.Models;
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
            var obras = await _context.Obras
                .Include(o => o.Cliente)
                .OrderBy(o => o.Nome)
                .ToListAsync();

            return View(obras);
        }

        // GET: Obras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var obra = await _context.Obras
                .Include(o => o.Cliente)
                .Include(o => o.MovimentosStock)
                    .ThenInclude(m => m.Material)
                .Include(o => o.RegistosMaoObra)
                .Include(o => o.Pagamentos)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obra == null) return NotFound();

            return View(obra);
        }

        // GET: Obras/RegistarMaterial/5
        public async Task<IActionResult> RegistarMaterial(int id)
        {
            var obra = await _context.Obras
                .Include(o => o.Cliente)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obra == null) return NotFound();

            PopularMateriaisDropDown();

            var movimento = new MovimentoStock
            {
                ObraId = obra.Id,
                Operacao = "REMOVE",
                DataOperacao = DateTime.UtcNow
            };

            ViewBag.ObraNome = obra.Nome;
            ViewBag.ClienteNome = obra.Cliente?.Nome;

            return View(movimento);
        }

        // POST: Obras/RegistarMaterial
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistarMaterial(MovimentoStock movimento)
        {
            ModelState.Remove("Obra");
            ModelState.Remove("Material");

            if (!ModelState.IsValid)
            {
                PopularMateriaisDropDown(movimento.MaterialId);
                return View(movimento);
            }

            var obra = await _context.Obras.FindAsync(movimento.ObraId);
            if (obra == null) return NotFound();

            var material = await _context.Materiais.FindAsync(movimento.MaterialId);
            if (material == null) return NotFound();

            if (movimento.Operacao == "ADD")
            {
                material.StockDisponivel += movimento.Quantidade;
            }
            else if (movimento.Operacao == "REMOVE")
            {
                if (material.StockDisponivel < movimento.Quantidade)
                {
                    ModelState.AddModelError("Quantidade", "Stock insuficiente para remover essa quantidade.");
                    PopularMateriaisDropDown(movimento.MaterialId);
                    return View(movimento);
                }

                material.StockDisponivel -= movimento.Quantidade;
            }

            movimento.DataOperacao = DateTime.UtcNow;

            _context.MovimentosStock.Add(movimento);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = movimento.ObraId });
        }

        // GET: Obras/RegistarMaoObra/5
        public async Task<IActionResult> RegistarMaoObra(int id)
        {
            var obra = await _context.Obras
                .Include(o => o.Cliente)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obra == null) return NotFound();

            var registo = new RegistoMaoObra
            {
                ObraId = obra.Id,
                DataRegisto = DateTime.UtcNow
            };

            ViewBag.ObraNome = obra.Nome;
            ViewBag.ClienteNome = obra.Cliente?.Nome;

            return View(registo);
        }

        // POST: Obras/RegistarMaoObra
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistarMaoObra(RegistoMaoObra registo)
        {
            ModelState.Remove("Obra");

            if (!ModelState.IsValid)
            {
                return View(registo);
            }

            registo.DataRegisto = DateTime.UtcNow;


            _context.RegistosMaoObra.Add(registo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = registo.ObraId });
        }

        // GET: Obras/RegistarPagamento/5
        public async Task<IActionResult> RegistarPagamento(int id)
        {
            var obra = await _context.Obras
                .Include(o => o.Cliente)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obra == null) return NotFound();

            var pagamento = new Pagamento
            {
                ObraId = obra.Id,
                DataRegisto = DateTime.Now
            };

            ViewBag.ObraNome = obra.Nome;
            ViewBag.ClienteNome = obra.Cliente?.Nome;

            return View(pagamento);
        }

        // POST: Obras/RegistarPagamento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistarPagamento(Pagamento pagamento)
        {
            ModelState.Remove("Obra");

            if (!ModelState.IsValid)
            {
                return View(pagamento);
            }

            pagamento.DataRegisto = DateTime.UtcNow;

            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = pagamento.ObraId });
        }

        // GET: Obras/Create
        public IActionResult Create()
        {
            PopularClientesDropDown();
            return View();
        }

        // POST: Obras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Obra obra)
        {
            ModelState.Remove("Cliente");

            if (ModelState.IsValid)
            {
                _context.Add(obra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopularClientesDropDown(obra.ClienteId);
            return View(obra);
        }

        // GET: Obras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var obra = await _context.Obras.FindAsync(id);
            if (obra == null) return NotFound();

            PopularClientesDropDown(obra.ClienteId);
            return View(obra);
        }

        // POST: Obras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Obra obra)
        {
            if (id != obra.Id) return NotFound();

            ModelState.Remove("Cliente");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(obra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObraExists(obra.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            PopularClientesDropDown(obra.ClienteId);
            return View(obra);
        }

        // GET: Obras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var obra = await _context.Obras
                .Include(o => o.Cliente)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obra == null) return NotFound();

            return View(obra);
        }

        // POST: Obras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra != null)
            {
                _context.Obras.Remove(obra);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private void PopularClientesDropDown(int? clienteIdSelecionado = null)
        {
            var clientes = _context.Clientes
                .OrderBy(c => c.Nome)
                .ToList();

            ViewBag.ClienteId = new SelectList(clientes, "Id", "Nome", clienteIdSelecionado);
        }

        private void PopularMateriaisDropDown(int? materialIdSelecionado = null)
        {
            var materiais = _context.Materiais
                .OrderBy(m => m.Nome)
                .ToList();

            ViewBag.MaterialId = new SelectList(
                materiais, "Id", "Nome", materialIdSelecionado
            );
        }

        private bool ObraExists(int id)
        {
            return _context.Obras.Any(e => e.Id == id);
        }
    }
}

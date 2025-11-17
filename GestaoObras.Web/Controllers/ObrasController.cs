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
                .Include(o => o.RegistosMaterial)
                .Include(o => o.RegistosMaoObra)
                .Include(o => o.Pagamentos)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obra == null) return NotFound();

            // mais tarde podes passar um ViewModel com tabs
            return View(obra);
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
            if (!ModelState.IsValid)
            {
                PopularClientesDropDown(obra.ClienteId);
                return View(obra);
            }

            _context.Add(obra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

            if (!ModelState.IsValid)
            {
                PopularClientesDropDown(obra.ClienteId);
                return View(obra);
            }

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

        private bool ObraExists(int id)
        {
            return _context.Obras.Any(e => e.Id == id);
        }
    }
}

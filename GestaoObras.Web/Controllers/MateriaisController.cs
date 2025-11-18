using System.Linq;
using System.Threading.Tasks;
using GestaoObras.Web.Data;
using GestaoObras.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoObras.Web.Controllers
{
    public class MateriaisController : Controller
    {
        private readonly AppDbContext _context;

        public MateriaisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Materiais
        public async Task<IActionResult> Index()
        {
            var materiais = await _context.Materiais
                .OrderBy(m => m.Nome)
                .ToListAsync();

            return View(materiais);
        }

        // GET: Materiais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.Materiais
                .FirstOrDefaultAsync(m => m.Id == id);

            if (material == null) return NotFound();

            return View(material);
        }

        // GET: Materiais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materiais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Material material)
        {
            if (ModelState.IsValid)
            {
                _context.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(material);
        }

        // GET: Materiais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.Materiais.FindAsync(id);
            if (material == null) return NotFound();

            return View(material);
        }

        // POST: Materiais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Material material)
        {
            if (id != material.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Materiais.Any(m => m.Id == material.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(material);
        }

        // GET: Materiais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.Materiais
                .FirstOrDefaultAsync(m => m.Id == id);

            if (material == null) return NotFound();

            return View(material);
        }

        // POST: Materiais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.Materiais.FindAsync(id);
            if (material != null)
            {
                _context.Materiais.Remove(material);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

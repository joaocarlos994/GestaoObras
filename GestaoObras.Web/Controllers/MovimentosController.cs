// MovimentosController.cs
// Controller logic for managing Movimentosusing GestaoObras.Web.Data;
using GestaoObras.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoObras.Web.Controllers
{
    public class MovimentosController : Controller
    {
        private readonly AppDbContext _context;

        public MovimentosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var movimentos = await _context.MovimentosStock
                .Include(m => m.Obra).ThenInclude(o => o.Cliente)
                .Include(m => m.Material)
                .OrderByDescending(m => m.DataHora)
                .ToListAsync();

            return View(movimentos);
        }
    }
}

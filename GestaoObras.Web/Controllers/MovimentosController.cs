using System.Linq;
using System.Threading.Tasks;
using GestaoObras.Web.Data;
using GestaoObras.Web.Models;
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

        // GET: Movimentos
        public async Task<IActionResult> Index()
        {
            var movimentos = await _context.MovimentosStock
                .Include(m => m.Obra)
                    .ThenInclude(o => o.Cliente)
                .Include(m => m.Material)
              .OrderByDescending(m => m.Id)

                .ToListAsync();

            return View(movimentos);
        }
    }
}

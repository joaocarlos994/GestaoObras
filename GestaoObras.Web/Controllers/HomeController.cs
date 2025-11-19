using GestaoObras.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoObras.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

       public async Task<IActionResult> Index()
{
    ViewBag.ObrasAtivas   = await _context.Obras.CountAsync(o => o.Ativa);
    ViewBag.ObrasTotais   = await _context.Obras.CountAsync();
    ViewBag.Clientes      = await _context.Clientes.CountAsync();
    ViewBag.Materiais     = await _context.Materiais.CountAsync();
    ViewBag.Movimentos    = await _context.MovimentosStock.CountAsync();

    ViewBag.UltimasObras = await _context.Obras
        .Include(o => o.Cliente)
        .OrderByDescending(o => o.Id)
        .Take(5)
        .ToListAsync();

    ViewBag.UltimosMovimentos = await _context.MovimentosStock
        .Include(m => m.Material)
        .Include(m => m.Obra)
        .OrderByDescending(m => m.DataOperacao)
        .Take(5)
        .ToListAsync();

    return View();
}

    }
}

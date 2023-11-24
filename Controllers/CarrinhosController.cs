using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMvcMysql.Models;
using WebMvcMysql.Data;

public class CarrinhosController : Controller
{
    private readonly Contexto _context;

    public CarrinhosController(Contexto context)
    {
        _context = context;
    }

    // GET: Carrinhos
    public async Task<IActionResult> Index()
    {
        var carrinhos = await _context.Carrinhos.Include(c => c.Usuario).ToListAsync();
        return View(carrinhos);
    }

    // GET: Carrinhos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carrinho = await _context.Carrinhos
            .Include(c => c.Usuario)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (carrinho == null)
        {
            return NotFound();
        }

        return View(carrinho);
    }

    // GET: Carrinhos/Create
    public IActionResult Create()
    {
        ViewBag.Usuario = _context.Usuario.ToList();
        return View();
    }

    // POST: Carrinhos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,UsuarioId")] Carrinho carrinho)
    {
        if (ModelState.IsValid)
        {
            _context.Add(carrinho);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Usuario = _context.Usuario.ToList();
        return View(carrinho);
    }

    // GET: Carrinhos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carrinho = await _context.Carrinhos.FindAsync(id);
        if (carrinho == null)
        {
            return NotFound();
        }

        ViewBag.Usuario = _context.Usuario.ToList();
        return View(carrinho);
    }

    // POST: Carrinhos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId")] Carrinho carrinho)
    {
        if (id != carrinho.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(carrinho);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrinhoExists(carrinho.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Usuario = _context.Usuario.ToList();
        return View(carrinho);
    }

    // GET: Carrinhos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carrinho = await _context.Carrinhos
            .Include(c => c.Usuario)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (carrinho == null)
        {
            return NotFound();
        }

        return View(carrinho);
    }

    // POST: Carrinhos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var carrinho = await _context.Carrinhos.FindAsync(id);
        _context.Carrinhos.Remove(carrinho);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CarrinhoExists(int id)
    {
        return _context.Carrinhos.Any(e => e.Id == id);
    }
}

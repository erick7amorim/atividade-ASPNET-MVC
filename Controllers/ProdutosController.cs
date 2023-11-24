using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMvcMysql.Data;
using WebMvcMysql.Models;

public class ProdutosController : Controller
{
    private readonly Contexto _context;

    public ProdutosController(Contexto context)
    {
        _context = context;
    }

    // GET: Produtos
    public async Task<IActionResult> Index()
    {
        var produtos = await _context.Produtos.Include(p => p.Categoria).ToListAsync();
        return View(produtos);
    }

    // GET: Produtos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    // GET: Produtos/Create
    public IActionResult Create()
    {
        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
        return View();
    }

    // POST: Produtos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Descricao,PathImagem,Preco,Quantidade,CategoriaId")] Produto produto)
    {
        if (ModelState.IsValid)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", produto.CategoriaId);
        return View(produto);
    }

    // GET: Produtos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }
        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", produto.CategoriaId);
        return View(produto);
    }

    // POST: Produtos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,PathImagem,Preco,Quantidade,CategoriaId")] Produto produto)
    {
        if (id != produto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(produto.Id))
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
        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", produto.CategoriaId);
        return View(produto);
    }

    // GET: Produtos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    // POST: Produtos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProdutoExists(int id)
    {
        return _context.Produtos.Any(e => e.Id == id);
    }
}

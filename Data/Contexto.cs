using Microsoft.EntityFrameworkCore;
using WebMvcMysql.Models;

namespace WebMvcMysql.Data
{
  public class Contexto : DbContext
  {
    public Contexto(DbContextOptions<Contexto> options)
        : base(options)
    { }

    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Carrinho> Carrinhos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

  }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMvcMysql.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string PathImagem { get; set; }

        public decimal Preco { get; set; }

        public int Quantidade { get; set; }

        [ForeignKey("CategoriaID")]
        public virtual Categoria Categoria { get; set; }
    }
}

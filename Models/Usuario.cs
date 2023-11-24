using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMvcMysql.Models
{
  [Table("Usuario")]
  public class Usuario
  {

    [Key]
    [Display(Name = "Id")]
    [Column("Id")]
    public int Id { get; set; }

    [Display(Name = "Login")]
    [Column("Login")]
    public string Login { get; set; }

    [Display(Name = "Password")]
    [Column("Password")]
    public string Password { get; set; }

    [Display(Name = "Email")]
    [Column("Email")]
    public string Email { get; set; }

    [Display(Name = "Carrinhos")]
    public virtual ICollection<Carrinho> Carrinhos { get; set; }

  }
}

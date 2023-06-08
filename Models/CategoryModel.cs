using System.ComponentModel.DataAnnotations;

namespace EstoqueWebApp.Models;

public record CategoryModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(50)]
    [Display(Name = "Nome da Categoria")]
    public string Name { get; set; }

    [Display(Name = "Descrição da Categoria")]
    public string Description { get; set; }

    [Display(Name = "Lista de produtos")]
    public ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
}

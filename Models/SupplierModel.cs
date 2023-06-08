using System.ComponentModel.DataAnnotations;

namespace EstoqueWebApp.Models;

public record SupplierModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(50)]
    [Display(Name = "Nome do Fornecedor")]
    public string Name { get; set; }

    [Display(Name = "Lista de produtos")]
    public ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
}

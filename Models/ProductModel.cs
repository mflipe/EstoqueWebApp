using System.ComponentModel.DataAnnotations;

namespace EstoqueWebApp.Models;

public record ProductModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(50)]
    [Display(Name = "Nome do Produto")]
    public string Name { get; set; } = String.Empty;


    [Display(Name = "Descrição do Produto")]
    public string Description { get; set; } = String.Empty;

    [Required(ErrorMessage = "O quantidade é obrigatória.")]

    [Display(Name = "Quantidade em Estoque")]
    public int Quantity { get; set;} = 0;

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Display(Name = "Preço")]
    public decimal Price { get; set; } = 0;

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    [Display(Name = "Categoria")]
    public CategoryModel Category { get; set; }

    [Required(ErrorMessage = "O fornecedor é obrigatório.")]
    [Display(Name = "Fornecedor")]
    public SupplierModel Supplier { get; set; }
}

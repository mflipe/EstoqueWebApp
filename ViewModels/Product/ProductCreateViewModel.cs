using EstoqueWebApp.Data;
using EstoqueWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace EstoqueWebApp.ViewModels;

public record ProductCreateViewModel
{
    public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> Suppliers { get; set; } = new List<SelectListItem>();

    public ProductCreateViewModel(IEnumerable<SupplierModel> supplierModel, IEnumerable<CategoryModel> categoryModel)
    {
        Suppliers = supplierModel.Select(supplier =>
                                      new SelectListItem
                                      {
                                          Value = supplier.Id.ToString(),
                                          Text = supplier.Name
                                      }).ToList();

        Categories = categoryModel.Select(category =>
                                      new SelectListItem
                                      {
                                          Value = category.Id.ToString(),
                                          Text = category.Name
                                      }).ToList();
    }

    public ProductCreateViewModel() { }

    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(50)]
    [Display(Name = "Nome do Produto")]
    public string Name { get; set; } = String.Empty;

    [Display(Name = "Descrição do Produto")]
    public string Description { get; set; } = String.Empty;

    [Required(ErrorMessage = "O quantidade é obrigatória.")]
    [Display(Name = "Quantidade em Estoque")]
    public int Quantity { get; set; } = 0;

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Display(Name = "Preço")]
    public decimal Price { get; set; } = 0;

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    [Display(Name = "Categoria")]
    [BindProperty]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "O fornecedor é obrigatório.")]
    [Display(Name = "Fornecedor")]
    [BindProperty]
    public int SupplierId { get; set; }
}

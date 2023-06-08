using System.ComponentModel.DataAnnotations;

namespace EstoqueWebApp.Models;

public record ClientModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(50)]
    [Display(Name = "Nome do Cliente")]
    public string Name { get; set; } = string.Empty;

}

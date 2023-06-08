using System.ComponentModel.DataAnnotations;

namespace EstoqueWebApp.Models;

public record LogModel
{
    public int Id { get; set; }

    [Required]
    public string Type { get; set; } = string.Empty;

    [Required]
    public string Message { get; set; }  = string.Empty;

    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

}

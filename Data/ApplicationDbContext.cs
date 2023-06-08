using Microsoft.EntityFrameworkCore;
using EstoqueWebApp.Models;

namespace EstoqueWebApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
    {
    }

    public DbSet<ProductModel> ProductViewModel { get; set; } = default!;

}

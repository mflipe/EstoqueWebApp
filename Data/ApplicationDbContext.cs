using Microsoft.EntityFrameworkCore;
using EstoqueWebApp.Models;

namespace EstoqueWebApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
    {
    }

    public DbSet<ProductModel> ProductModel { get; set; } = default!;

    public DbSet<CategoryModel> CategoryModel { get; set; } = default!;

    public DbSet<EstoqueWebApp.Models.LogModel> LogModel { get; set; } = default!;

    public DbSet<EstoqueWebApp.Models.ClientModel> ClientModel { get; set; } = default!;

    public DbSet<EstoqueWebApp.Models.SupplierModel> SupplierModel { get; set; } = default!;

}

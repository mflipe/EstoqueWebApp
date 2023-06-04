using Microsoft.EntityFrameworkCore;
using System;

namespace EstoqueWebApp.Data;
public class ApplicationDbContext : DbContext
{
   
    /*
    public DbSet<Card> Cards { get; set; }
    public DbSet<Owner> Owners { get; set; }
    */

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
    {
    }

}

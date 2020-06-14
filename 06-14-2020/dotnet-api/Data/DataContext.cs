using Microsoft.EntityFrameworkCore;
using dotnet_api.Models;

namespace dotnet_api.Data {
  public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
  }
}
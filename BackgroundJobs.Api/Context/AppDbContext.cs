using Microsoft.EntityFrameworkCore;

namespace BackgroundJobs.Api.Context;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // Configuração da entidade User
    modelBuilder.Entity<User>(entity =>
    {
      entity.HasKey(e => e.Id);

      entity.Property(e => e.Name)
              .IsRequired()
              .HasMaxLength(100);

      entity.Property(e => e.Email)
              .IsRequired()
              .HasMaxLength(150);

      entity.ToTable("Users");
    });
  }
}

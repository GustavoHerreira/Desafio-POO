using Desafio_POO.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_POO.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Imovel> Imoveis { get; set; }
    public DbSet<Proprietario> Proprietarios { get; set; }
    public DbSet<Inquilino> Inquilinos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Imovel>()
            .HasDiscriminator<string>("TipoImovel") // Define a coluna discriminadora e seu tipo
            .HasValue<Apartamento>("APARTAMENTO")   // Mapeia a classe Apartamento para o valor "APARTAMENTO"
            .HasValue<Casa>("CASA");               // Mapeia a classe Casa para o valor "CASA"
    }
}
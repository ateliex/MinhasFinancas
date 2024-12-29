using Microsoft.EntityFrameworkCore;
using MinhasFinancas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhasFinancas.Data;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {

    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured == false)
        {
            var dataSource = @"C:\temp\MinhasFinancas.db";

            optionsBuilder.UseSqlite($"Data Source={dataSource}");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = "salario", Nome = "Salário", TipoId = TipoFinancaEnum.Receita });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = "compras", Nome = "Compras", TipoId = TipoFinancaEnum.Despesa });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = "mercado", Nome = "Mercado", TipoId = TipoFinancaEnum.Despesa });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = "lanches", Nome = "Lanches", TipoId = TipoFinancaEnum.Despesa });

        modelBuilder.Entity<Financa>().HasData(new Financa { Id = new Guid("6cd6a787-cc87-4c17-8c75-b18ebc4bf901"), Data = new DateTime(2024, 12, 05), Descricao = "Salário", Valor = 3000.00m, TipoId = TipoFinancaEnum.Receita, CategoriaId = "salario" });
        modelBuilder.Entity<Financa>().HasData(new Financa { Id = new Guid("6cd6a787-cc87-4c17-8c75-b18ebc4bf902"), Data = new DateTime(2024, 12, 29), Descricao = "Atacadão", Valor = 30.00m, TipoId = TipoFinancaEnum.Despesa, CategoriaId = "mercado" });
        modelBuilder.Entity<Financa>().HasData(new Financa { Id = new Guid("6cd6a787-cc87-4c17-8c75-b18ebc4bf903"), Data = new DateTime(2024, 12, 29), Descricao = "Mc Donalds", Valor = 45.00m, TipoId = TipoFinancaEnum.Despesa, CategoriaId = "lanches" });

        modelBuilder.Entity<Saldo>().HasKey(s => new { s.CartaoId, s.Data });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Financa> Financas { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
}

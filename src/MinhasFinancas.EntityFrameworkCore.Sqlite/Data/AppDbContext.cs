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

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    var dataSource = @"C:\temp\MinhasFinancas.db";

    //    optionsBuilder.UseSqlite($"Data Source={dataSource}");
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conta>()
            .Property(p => p.Id).ValueGeneratedNever();

        modelBuilder.Entity<Conta>()
            .HasIndex(c => new { c.Numero, c.Banco, c.Tipo, c.Documento }).IsUnique();

        modelBuilder.Entity<Saldo>()
            .HasKey(s => new { s.ContaId, s.Data });

        modelBuilder.Entity<Lancamento>()
            .HasKey(l => l.ProtocoloId);

        modelBuilder.Entity<Lancamento>()
            .Property(l => l.ProtocoloId).HasMaxLength(24);

        modelBuilder.Entity<Lancamento>()
            .OwnsOne(l => l.Protocolo, b => b.Property(p => p.Id).HasMaxLength(24).HasColumnName("ProtocoloId").IsRequired());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Conta> Contas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
}

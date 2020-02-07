using System;
using Microsoft.EntityFrameworkCore;
using Senai.CodeTur.Dominio.Entidades;

namespace Senai.CodeTur.Infra.Data.Contextos
{
    public class CodeTurContext : DbContext
    {
        public DbSet<UsuarioDominio> Usuarios { get; set; }
        public DbSet<PacoteDominio> Pacotes { get; set; }

        public CodeTurContext(DbContextOptions<CodeTurContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS3T;Initial Catalog=CodeTur;User ID=sa;Password=sa132;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioDominio>().HasData(
                new UsuarioDominio()
                {
                    Id = 1,
                    Nome = "Fernando Henrique",
                    Email = "admin@codetur.com",
                    Senha = "Codetur@132",
                    Tipo = "Administrador"
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}





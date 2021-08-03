using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class DataContext : DbContext
    {      
        
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Alunos> Alunos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Turma> Turmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alunos>().ToTable("tabAluno");
            modelBuilder.Entity<Categoria>().ToTable("tabCategoria");
            modelBuilder.Entity<Turma>().ToTable("tabTurma");

        }

    }
}

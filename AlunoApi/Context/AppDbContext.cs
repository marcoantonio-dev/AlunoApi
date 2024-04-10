using AlunoApi.Model;
using Microsoft.EntityFrameworkCore;
using AlunoApi.Service;

namespace AlunoApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {  
        }

        public virtual DbSet<Aluno> Aluno { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Marco Antonio Masson",
                    Email = "marcomasson@gmail.com",
                    Idade = 18
                },
                new Aluno
                {
                    Id = 2,
                    Nome = "Joao da Silva",
                    Email = "joaosilvaD@gmail.com",
                    Idade = 45
                }
            );
        }
    }
}
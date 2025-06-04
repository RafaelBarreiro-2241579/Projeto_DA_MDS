using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Reflection.Emit;

namespace iTasks.models
{
    internal class iTasks
    {
        class iTasksContext : DbContext
        {

            public DbSet<Utilizador> Utilizadores { get; set; }
            public DbSet<Tarefa> Tarefas { get; set; }

            public DbSet<Gestor> Gestores { get; set; }

            public DbSet<Programador> Programadores { get; set; }

            public DbSet<TipoTarefa> TiposTarefa { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                // Garante que o campo Username é único na base de dados
                modelBuilder.Entity<Utilizador>()
                    .HasIndex(u => u.Username)
                    .IsUnique();

                // RELAÇÃO: Programador → Gestor (muitos-para-um)
                // Cada Programador tem um Gestor (IdGestor)
                // Um Gestor pode ter vários Programadores
                modelBuilder.Entity<Programador>()
                    .HasRequired(p => p.Gestor)      // Cada programador TEM UM gestor
                    .WithMany(g => g.Programadores)  // Um gestor TEM MUITOS programadores
                    .HasForeignKey(p => p.IdGestor)  // Chave estrangeira na classe Programador
                    .WillCascadeOnDelete(false);     // Evita apagar os programadores se o gestor for apagado

                // RELAÇÃO: Tarefa → Programador (muitos-para-um)
                // Cada Tarefa tem um Programador responsável
                modelBuilder.Entity<Tarefa>()
                    .HasRequired(t => t.Programador)
                    .WithMany(p => p.Tarefas)
                    .HasForeignKey(t => t.IdProgramador)
                    .WillCascadeOnDelete(false); // Evita apagar tarefas se o programador for apagado

                // RELAÇÃO: Tarefa → Gestor (muitos-para-um)
                // Cada Tarefa é criada por um Gestor
                modelBuilder.Entity<Tarefa>()
                    .HasRequired(t => t.Gestor)
                    .WithMany(g => g.Tarefas)
                    .HasForeignKey(t => t.IdGestor)
                    .WillCascadeOnDelete(false); // Evita apagar tarefas se o gestor for apagado

                base.OnModelCreating(modelBuilder);
            }


        }
    }
}

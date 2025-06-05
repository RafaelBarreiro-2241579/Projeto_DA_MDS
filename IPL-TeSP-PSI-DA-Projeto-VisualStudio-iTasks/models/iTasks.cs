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

        }
    }
}

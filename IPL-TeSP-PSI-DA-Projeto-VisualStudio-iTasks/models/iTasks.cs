using System;
using System.Data.Entity;
using iTasks.controllers;

namespace iTasks.models
{
    internal class iTasksBD
    {
        public iTasksContext Context { get; private set; }

        public TipoTarefasController TipoTarefaController { get; private set; }
        public TarefaController TarefaController { get; private set; }

        public GestorController GestorController { get; private set; }

        public ProgramadorController ProgramadorController { get; private set; }

        public iTasksBD()
        {
            try
            {
                Context = new iTasksContext();

                // Testa apenas se consegue conectar
                Console.WriteLine("A testar conexão...");
                bool exists = Context.Database.Exists();
                Console.WriteLine($"BD existe: {exists}");

                // NÃO carregues nada por agora
                // Context.Utilizadores.Load();
                // etc...

                TipoTarefaController = new TipoTarefasController(Context);
                TarefaController = new TarefaController(Context);
                GestorController = new GestorController(Context);
                ProgramadorController = new ProgramadorController(Context);

                Console.WriteLine("Inicialização completa!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public class iTasksContext : DbContext
        {
            public DbSet<Utilizador> Utilizadores { get; set; }
            public DbSet<Tarefa> Tarefas { get; set; }
            public DbSet<Gestor> Gestores { get; set; }
            public DbSet<Programador> Programadores { get; set; }
            public DbSet<TipoTarefa> TipoTarefa { get; set; }

            public iTasksContext() : base("iTasksContext")
            {
                Database.SetInitializer<iTasksContext>(
                    new CreateDatabaseIfNotExists<iTasksContext>());

                // Para ver onde está o ficheiro da BD
                Console.WriteLine("BD Path: " + this.Database.Connection.ConnectionString);

                this.Database.Initialize(false);
            }
        }
    }
}

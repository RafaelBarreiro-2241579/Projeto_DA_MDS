using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.models
{
    class TipoTarefa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Tarefa> Tarefas { get; set; }

        // OBRIGATÓRIO: Construtor sem parâmetros para Entity Framework
        public TipoTarefa()
        {
        }

        // Construtor com parâmetros (mantém a funcionalidade original)
        public TipoTarefa(string nome)
        {
            Nome = nome;
        }

        public override string ToString()
        {
            return $"[{Id}] - {Nome}";
        }
    }
}
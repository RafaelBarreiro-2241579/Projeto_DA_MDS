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

        public Tarefa IdTarefa { get; set; }


        public List<Tarefa> Tarefas { get; set; }

    }
}

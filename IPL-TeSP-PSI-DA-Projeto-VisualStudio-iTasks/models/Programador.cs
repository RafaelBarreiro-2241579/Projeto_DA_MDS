using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTasks.models;

namespace iTasks.models
{
    public enum NivelExperiencia
    {
        Junior,
        Senior
    }


    class Programador : Utilizador
    {

        public int IdGestor { get; set; }
        public Gestor Gestor { get; set; }

        public NivelExperiencia NivelExperiencia { get; set; }
        public List<Tarefa> Tarefas { get; set; }

    }

}

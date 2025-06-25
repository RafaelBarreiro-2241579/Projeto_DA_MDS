using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTasks.models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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


        public Programador()
        {

        }

        public Programador (string nome, string username, string password, NivelExperiencia nivelExperiencia, Gestor idGestor)
        {
            this.Nome = nome;
            this.Username = username;
            this.Password = password;
            this.NivelExperiencia = nivelExperiencia;
            this.IdGestor = idGestor.Id;
        }

        public override string ToString()
        {
            return $"{Nome} | Username: {Username} | Experiencia: {NivelExperiencia}";
        }
    }
}

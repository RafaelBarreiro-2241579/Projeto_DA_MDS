using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.models
{

    public enum Departamento
    {
        IT,
        Marketing,
        Administração
    }


    class Gestor : Utilizador
    {

        public Departamento Departamento { get; set; }

        public bool GereUtilizadores { get; set; }

        public List<Programador> Programadores { get; set; }
        //   public List<Tarefa> Tarefas { get; set; }


        public Gestor()
        {
        }
        public Gestor(string nome, string username, string password, Departamento departamento, bool gereUtilizadores)
        {
            Nome = nome;
            Username = username;
            Password = password;
            Departamento = departamento;
            GereUtilizadores = gereUtilizadores;
        }
        public override string ToString()
        {
            return $"{Nome} | Username: {Username} | Departamento: {Departamento}";
        }

    }
}

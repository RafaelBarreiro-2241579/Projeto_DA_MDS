using iTasks.models;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace iTasks.controllers
{
    internal class GestorController
    {
        // Contexto da base de dados da aplicação
        private iTasksBD.iTasksContext db;

        // Construtor que recebe o contexto da base de dados
        public GestorController(iTasksBD.iTasksContext context)
        {
            db = context;
        }

        // Obtém o primeiro gestor da base de dados (pode ser usado como o gestor atualmente ativo)
        public Gestor ObterGestorAtual()
        {
            return db.Gestores.FirstOrDefault();
        }

        // Grava (cria) um novo gestor na base de dados
        public void GravarGestor(string nome, string username, string password, Departamento departamento, bool gereUtilizadores)
        {
            db.Utilizadores.Add(new Gestor(nome, username, password, departamento, gereUtilizadores));
            db.SaveChanges();
        }

        // Edita os dados de um gestor já existente
        public void EditarGestor(Gestor gestorSelecionado, string nome, string username, string password, Departamento departamento, bool gereUtilizadores)
        {
            if (gestorSelecionado != null)
            {
                gestorSelecionado.Nome = nome;
                gestorSelecionado.Username = username;
                gestorSelecionado.Password = password;
                gestorSelecionado.Departamento = departamento;
                gestorSelecionado.GereUtilizadores = gereUtilizadores;

                db.SaveChanges();
            }
        }

        // Elimina um gestor da base de dados
        public void EliminarGestor(Gestor gestorSelecionado)
        {
            if (gestorSelecionado != null)
            {
                db.Gestores.Remove(gestorSelecionado);
                db.SaveChanges();
            }
        }

        // Retorna a lista de todos os gestores existentes na base de dados
        public List<Gestor> ObterTodosGestores()
        {
            return db.Gestores.ToList();
        }
    }
}

 using iTasks.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.controllers
{
    internal class ProgramadorController
    {
        // Contexto da base de dados da aplicação
        private iTasksBD.iTasksContext db;

        // Construtor que recebe o contexto da base de dados
        public ProgramadorController(iTasksBD.iTasksContext context)
        {
            db = context;
        }

        // Gera um identificador sequencial para um novo programador com base na contagem atual
        public string IncrementarProgramador()
        {
            int contarProgramador = 0;
            // Conta quantos programadores existem atualmente
            contarProgramador = db.Programadores.Count();

            // Retorna o próximo número como string (contagem + 1)
            return (contarProgramador + 1).ToString();
        }

        // Grava (cria) um novo programador na base de dados
        public void GravarProgramador(string nome, string username, string password, NivelExperiencia nivelExperiencia, Gestor gestorId)
        {
            db.Utilizadores.Add(new Programador(nome, username, password, nivelExperiencia, gestorId));
            db.SaveChanges();
        }

        // Edita os dados de um programador já existente
        public void EditarProgramador(Programador programadorSelecionado, string nome, string username, string password, NivelExperiencia nivelExperiencia, Gestor gestorId)
        {
            if (programadorSelecionado != null)
            {
                programadorSelecionado.Nome = nome;
                programadorSelecionado.Username = username;
                programadorSelecionado.Password = password;
                programadorSelecionado.NivelExperiencia = nivelExperiencia;
                programadorSelecionado.IdGestor = gestorId.Id;

                db.SaveChanges();
            }
        }

        // Elimina um programador da base de dados, se ele não tiver tarefas atribuídas
        public void EliminarProgramador(Programador programadorSelecionado)
        {
            if (programadorSelecionado != null)
            {
                // Verifica se o programador tem tarefas atribuídas
                if (db.Tarefas.Any(t => t.Programador.Id == programadorSelecionado.Id))
                {
                    // Mostra mensagem de aviso se tiver tarefas
                    MessageBox.Show("Este programador tem tarefas atribuídas. Não pode ser eliminado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    // Remove o programador se não tiver tarefas
                    db.Programadores.Remove(programadorSelecionado);
                    db.SaveChanges();
                }
            }
        }

        // Retorna a lista de todos os programadores existentes
        public List<Programador> MostrarProgramadores()
        {
            return db.Programadores.ToList();
        }
    }

}
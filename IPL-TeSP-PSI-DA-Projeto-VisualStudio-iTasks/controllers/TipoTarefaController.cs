using iTasks.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.controllers
{
    internal class TipoTarefasController
    {
        // Contexto da base de dados da aplicação
        private iTasksBD.iTasksContext db;

        // Construtor que recebe o contexto da base de dados
        public TipoTarefasController(iTasksBD.iTasksContext context)
        {
            db = context;
        }

        // Incrementa o contador de tipos de tarefa, retornando o próximo número como string
        public string IncrementarContadorTipoTarefa()
        {
            int contarTipoTarefa = 0;
            // Conta quantos tipos de tarefa existem atualmente
            contarTipoTarefa = db.TipoTarefa.Count();

            // Retorna a contagem atual + 1 como string
            return (contarTipoTarefa + 1).ToString();
        }

        // Adiciona um novo tipo de tarefa à base de dados
        public void GravarTipoTarefa(string Nome)
        {
            db.TipoTarefa.Add(new TipoTarefa(Nome));
            db.SaveChanges();
        }

        // Edita um tipo de tarefa existente
        public void EditarTipoTarefa(TipoTarefa tipoTarefaSelecionada, string nomeTarefa)
        {
            // Procura o tipo de tarefa pelo Id
            TipoTarefa tipoTarefa = db.TipoTarefa.Find(tipoTarefaSelecionada.Id);

            // Atualiza o nome do tipo de tarefa
            tipoTarefa.Nome = nomeTarefa;

            db.SaveChanges();
        }

        // Remove um tipo de tarefa da base de dados
        public void RemoverTipoTarefa(TipoTarefa tipoTarefaSelecionada)
        {
            // Procura o tipo de tarefa pelo Id
            TipoTarefa tipoTarefa = db.TipoTarefa.Find(tipoTarefaSelecionada.Id);
            if (tipoTarefa != null)
            {
                // Remove o tipo de tarefa e guarda as alterações
                db.TipoTarefa.Remove(tipoTarefa);
                db.SaveChanges();
            }
        }

        // Retorna uma lista com todos os tipos de tarefa existentes
        public List<TipoTarefa> MostrarTiposTarefas()
        {
            return db.TipoTarefa.ToList();
        }
    }


}

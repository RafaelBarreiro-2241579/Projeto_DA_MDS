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
        private iTasksBD.iTasksContext db;

        public TipoTarefasController(iTasksBD.iTasksContext context)
        {
            db = context;
        }
        //        private TipoTarefasController tipoTarefasController;

        public string IncrementarContadorTipoTarefa()
        {
            int contarTipoTarefa = 0;
            // Conta o número de TipoTarefa e adiciona 1, começando em 1 se não houver nenhum
            contarTipoTarefa = db.TipoTarefa.Count();

            return (contarTipoTarefa + 1).ToString();


        }

        //adicionar tipo de tarefa 
        public void GravarTipoTarefa(string Nome)
        {
            db.TipoTarefa.Add(new TipoTarefa(Nome));


            db.SaveChanges();
        }

        //mostrar todos os tipos de tarefa
        public List<TipoTarefa> MostrarTiposTarefas()
        {
            return db.TipoTarefa.ToList();
        }


        //editar um tipo de tarefa
        public void EditarTipoTarefa(TipoTarefa tipoTarefaSelecionada, string nomeTarefa)

        {
            TipoTarefa tipoTarefa = db.TipoTarefa.Find(tipoTarefaSelecionada.Id);

            tipoTarefa.Nome = nomeTarefa;


            db.SaveChanges();

        }

        //remover tipo de tarefa
        public void RemoverTipoTarefa(TipoTarefa tipoTarefaSelecionada)
        {
            TipoTarefa tipoTarefa = db.TipoTarefa.Find(tipoTarefaSelecionada.Id);
            if (tipoTarefa != null)
            {
                db.TipoTarefa.Remove(tipoTarefa);
                db.SaveChanges();
            }
            // Caso não encontre, não faz nada (poderia lançar exceção se desejar)
        }


    }

}

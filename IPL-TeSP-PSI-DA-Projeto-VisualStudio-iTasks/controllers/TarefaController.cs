using iTasks.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.controllers
{
    internal class TarefaController
    {

        private iTasksBD.iTasksContext db;

        public TarefaController(iTasksBD.iTasksContext context)
        {
            db = context;
        }
        //        private TipoTarefasController tipoTarefasController;

        public string IncrementarContadorTipoTarefa()
        {
            int contarTarefa = 0;
            // Conta o número de TipoTarefa e adiciona 1, começando em 1 se não houver nenhum
            contarTarefa = db.Tarefas.Count();

            return (contarTarefa + 1).ToString();


        }

        //guarda a tarefa
        public void GravarTarefa(string descricao,   Gestor IdGestor, Programador IdProgramador, int ordemExecucao, DateTime dataPrevistaInicio, DateTime dataPrevistaFim,
            TipoTarefa tipoDeTarefa, int storyPoints, DateTime dataRealInicio, DateTime dataRealFim, DateTime dataCriacao, EstadoAtual estadoAtual)
        {
            db.Tarefas.Add(new Tarefa
            {
                Descricao = descricao,
                Gestor = IdGestor,
                IdGestor = IdGestor.Id,
                Programador = IdProgramador,
                IdProgramador = IdProgramador.Id,
                OrdemExecucao = ordemExecucao,
                DataPrevistaInicio = dataPrevistaInicio,
                DataPrevistaFim = dataPrevistaFim,
                TipoDeTarefa = tipoDeTarefa,
                IdTipoDeTarefa = tipoDeTarefa.Id,
                StoryPoints = storyPoints,
                DataRealInicio = dataRealInicio,
                DataRealFim = dataRealFim,
                DataCriacao = dataCriacao,
                EstadoAtual = estadoAtual
            });

            db.SaveChanges();
        }

        // Mostra todas as tarefas
        public List<Tarefa> MostrarTarefas()
        {
            return db.Tarefas.ToList();
        }

        //edita a tarefa
        public void EditarTarefa(Tarefa tarefaSelecionada, string descricao, Gestor gestor, Programador programador, int ordemExecucao, DateTime? dataPrevistaInicio, DateTime dataPrevistaFim,
            TipoTarefa tipoDeTarefa, int storyPoints)
        {
            //var tarefa = db.Tarefas.Find(id);
            if (tarefaSelecionada != null)
            {
                tarefaSelecionada.Descricao = descricao;
                tarefaSelecionada.Gestor = gestor;
                tarefaSelecionada.IdGestor = gestor.Id;
                tarefaSelecionada.Programador = programador;
                tarefaSelecionada.IdProgramador = programador.Id;
                tarefaSelecionada.OrdemExecucao = ordemExecucao;
                tarefaSelecionada.DataPrevistaInicio = dataPrevistaInicio;
                tarefaSelecionada.DataPrevistaFim = dataPrevistaFim;
                tarefaSelecionada.TipoDeTarefa = tipoDeTarefa;
                tarefaSelecionada.IdTipoDeTarefa = tipoDeTarefa.Id;
                tarefaSelecionada.StoryPoints = storyPoints;

                db.SaveChanges();
            }
        }


        public List<Tarefa> MostrarTarefasToDo()
        {
            return db.Tarefas.Where(t => t.EstadoAtual == EstadoAtual.ToDo).ToList();
        }

        public List<Tarefa> MostrarTarefasDoing()
        {
            return db.Tarefas.Where(t => t.EstadoAtual == EstadoAtual.Doing).ToList();
        }

        public List<Tarefa> MostrarTarefasDone()
        {
            return db.Tarefas.Where(t => t.EstadoAtual == EstadoAtual.Done).ToList();
        }






        public void MudarEstadoTarefa(Tarefa tarefaSelecionada, EstadoAtual estado, Utilizador utilizadorRecebido)
        {
            // Valida se a tarefa selecionada é nula
            if (tarefaSelecionada == null)
                throw new Exception("Nenhuma tarefa selecionada.");

            /* if (tarefaSelecionada.IdProgramador != utilizadorRecebido.Id)
                throw new Exception("Apenas o programador responsável pela tarefa pode alterar o seu estado.");*/

            // Atualiza o estado da tarefa selecionada
            tarefaSelecionada.EstadoAtual = estado;

            switch (estado)
            {
                case EstadoAtual.Done:
                    tarefaSelecionada.DataRealFim = DateTime.Now;
                    break;
                case EstadoAtual.Doing:
                    tarefaSelecionada.DataRealInicio = DateTime.Now;
                    break;
                case EstadoAtual.ToDo:
                    tarefaSelecionada.DataRealInicio = null;
                    tarefaSelecionada.DataRealFim = null;
                    break;
                default:
                    throw new Exception("Estado inválido. Deve ser ToDo, Doing ou Done.");
            }

            // Salva as alterações na base de dados
            db.SaveChanges();
        }



        // Elimina a tarefa
        public void EliminarTarefa(Tarefa tarefaSelecionada)
        {

            db.Tarefas.Remove(tarefaSelecionada);
            db.SaveChanges();

            
        }



    }

}


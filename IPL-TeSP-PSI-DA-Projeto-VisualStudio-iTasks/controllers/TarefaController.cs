using iTasks.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace iTasks.controllers
{
    
    class TarefaController
    {
        // Contexto da base de dados da aplicação
        private iTasksBD.iTasksContext db;

        // Construtor que recebe o contexto da base de dados
        public TarefaController(iTasksBD.iTasksContext context)
        {
            db = context;
        }

        // Incrementa o contador de tarefas, retornando o próximo número como string
        public string IncrementarContadorTarefa()
        {
            int contarTarefa = 0;
            // Conta quantas tarefas existem atualmente
            contarTarefa = db.Tarefas.Count();

            // Retorna a contagem atual + 1 como string
            return (contarTarefa + 1).ToString();
        }

        // Grava uma nova tarefa na base de dados com todos os seus detalhes
        public void GravarTarefa(string descricao, Gestor IdGestor, Programador IdProgramador, int ordemExecucao, DateTime dataPrevistaInicio, DateTime dataPrevistaFim,
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

        // Edita os dados de uma tarefa existente
        public void EditarTarefa(Tarefa tarefaSelecionada, string descricao, Gestor gestor, Programador programador, int ordemExecucao, DateTime? dataPrevistaInicio, DateTime dataPrevistaFim,
            TipoTarefa tipoDeTarefa, int storyPoints)
        {
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

        // Remove uma tarefa da base de dados
        public void EliminarTarefa(Tarefa tarefaSelecionada)
        {
            db.Tarefas.Remove(tarefaSelecionada);
            db.SaveChanges();
        }

        // Retorna uma lista com todas as tarefas
        public List<Tarefa> MostrarTarefas()
        {
            return db.Tarefas.ToList();
        }

        // Retorna uma lista com todas as tarefas no estado ToDo
        public List<Tarefa> MostrarTarefasToDo()
        {
            return db.Tarefas.Where(t => t.EstadoAtual == EstadoAtual.ToDo).ToList();
        }

        // Retorna uma lista com todas as tarefas no estado Doing
        public List<Tarefa> MostrarTarefasDoing()
        {
            return db.Tarefas.Where(t => t.EstadoAtual == EstadoAtual.Doing).ToList();
        }

        // Retorna uma lista com todas as tarefas no estado Done
        public List<Tarefa> MostrarTarefasDone()
        {
            return db.Tarefas.Where(t => t.EstadoAtual == EstadoAtual.Done).ToList();
        }

        // Altera o estado de uma tarefa, aplicando regras de negócio e validações
        public void MudarEstadoTarefa(Tarefa tarefaSelecionada, EstadoAtual estado, Utilizador utilizadorRecebido)
        {
            if (tarefaSelecionada == null)
                return;

            // Impede alterações se a tarefa já estiver no estado Done e tentar mudar para outro estado
            if (tarefaSelecionada.EstadoAtual == EstadoAtual.Done && estado != EstadoAtual.Done)
            {
                MessageBox.Show("Tarefas no estado 'Done' não podem ser alteradas para outro estado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bloqueia a mudança para Doing ou Done se existirem tarefas anteriores a fazer
            if (estado == EstadoAtual.Doing || estado == EstadoAtual.Done)
            {
                var anterioresPorFazer = db.Tarefas.Where(t =>
                    t.IdProgramador == tarefaSelecionada.IdProgramador &&
                    t.OrdemExecucao < tarefaSelecionada.OrdemExecucao &&
                    t.EstadoAtual == EstadoAtual.ToDo).ToList();

                if (anterioresPorFazer.Any())
                {
                    return;
                }
            }

            var doingTarefas = db.Tarefas.Where(t =>
                t.IdGestor == tarefaSelecionada.IdGestor &&
                t.EstadoAtual == EstadoAtual.Doing).ToList();

            // Atualiza o estado da tarefa e define datas reais conforme o estado
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
                    MessageBox.Show("Estado inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            db.SaveChanges();
        }

        // Mostra tarefas no estado ToDo atribuídas a um programador específico
        public List<Tarefa> MostrarTarefasToDoPorProgramador(Utilizador utilizador)
        {
            if (utilizador is Programador programador)
            {
                return db.Tarefas
                    .Where(t => t.IdProgramador == programador.Id && t.EstadoAtual == EstadoAtual.ToDo)
                    .ToList();
            }

            // Se não for programador, retorna lista vazia
            return new List<Tarefa>();
        }

        // Mostra tarefas no estado Doing atribuídas a um programador específico
        public List<Tarefa> MostrarTarefasDoingPorProgramador(Utilizador utilizador)
        {
            if (utilizador is Programador programador)
            {
                return db.Tarefas
                    .Where(t => t.IdProgramador == programador.Id && t.EstadoAtual == EstadoAtual.Doing)
                    .ToList();
            }

            return new List<Tarefa>();
        }

        // Mostra tarefas no estado Done atribuídas ao utilizador (programador ou gestor)
        public List<Tarefa> MostrarTarefasDonePorUtilizador(Utilizador utilizador)
        {
            var query = db.Tarefas
                .Include("Gestor")
                .Include("Programador")
                .Include("TipoDeTarefa")
                .Where(t => t.EstadoAtual == EstadoAtual.Done);

            if (utilizador is Programador programador)
            {
                query = query.Where(t => t.IdProgramador == programador.Id);
            }
            else if (utilizador is Gestor gestor)
            {
                query = query.Where(t => t.IdGestor == gestor.Id);
            }

            return query.ToList();
        }

        // Mostra tarefas em curso (não Done) para um gestor específico, ordenadas pelo estado
        public List<Tarefa> MostrarTarefasEmCursoPorGestor(int idGestor)
        {
            return db.Tarefas
                .Include("Gestor")
                .Include("Programador")
                .Include("TipoDeTarefa")
                .Where(t => t.EstadoAtual != EstadoAtual.Done && t.Gestor != null && t.IdGestor == idGestor)
                .OrderBy(t => t.EstadoAtual)
                .ToList();
        }

        // Exporta as tarefas concluídas de um gestor para um ficheiro CSV no caminho especificado
        public void ExportarTarefasConcluidasParaCSV(int idGestor, string caminhoFicheiro)
        {
            var tarefasConcluidas = db.Tarefas
                .Include("Programador")
                .Include("TipoDeTarefa")
                .Where(t => t.IdGestor == idGestor && t.EstadoAtual == EstadoAtual.Done)
                .ToList();

            using (var writer = new StreamWriter(caminhoFicheiro, false, new UTF8Encoding(true))) // Com BOM para Excel
            {
                // Escreve a linha de cabeçalho do CSV
                writer.WriteLine("Programador;Descricao;DataPrevistaInicio;DataPrevistaFim;TipoTarefa;DataRealInicio;DataRealFim");

                foreach (Tarefa tarefa in tarefasConcluidas)
                {
                    // Prepara a linha com os dados da tarefa, separando por ponto e vírgula
                    string linha = string.Join(";",
                        tarefa.Programador?.Nome ?? "Sem Programador",
                        tarefa.Descricao,
                        tarefa.DataPrevistaInicio?.ToString("dd/MM/yyyy") ?? "",
                        tarefa.DataPrevistaFim.ToString("dd/MM/yyyy"),
                        tarefa.TipoDeTarefa?.Nome ?? "Sem Tipo",
                        tarefa.DataRealInicio?.ToString("dd/MM/yyyy") ?? "",
                        tarefa.DataRealFim?.ToString("dd/MM/yyyy") ?? ""
                    );

                    // Escreve a linha no ficheiro
                    writer.WriteLine(linha);
                }
            }
        }

        public List<Tarefa> CalcularPrevisaoTarefasToDo()
        {
            var tarefasConcluidas = MostrarTarefasDone();

            var temposPorSP = new Dictionary<int, List<double>>();
            foreach (var tarefa in tarefasConcluidas)
            {
                if (tarefa.DataRealInicio.HasValue && tarefa.DataRealFim.HasValue)
                {
                    double horas = (tarefa.DataRealFim.Value - tarefa.DataRealInicio.Value).TotalHours;
                    if (!temposPorSP.ContainsKey(tarefa.StoryPoints))
                        temposPorSP[tarefa.StoryPoints] = new List<double>();
                    temposPorSP[tarefa.StoryPoints].Add(horas);
                }
            }

            var mediaPorSP = temposPorSP.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Average()
            );

            var tarefasToDo = MostrarTarefasToDo();

            foreach (var tarefa in tarefasToDo)
            {
                double mediaHoras;
                if (mediaPorSP.ContainsKey(tarefa.StoryPoints))
                {
                    mediaHoras = mediaPorSP[tarefa.StoryPoints];
                }
                else if (mediaPorSP.Count > 0)
                {
                    var spMaisProximo = mediaPorSP.Keys.OrderBy(sp => Math.Abs(sp - tarefa.StoryPoints)).First();
                    mediaHoras = mediaPorSP[spMaisProximo];
                }
                else
                {
                    mediaHoras = 0;
                }

                tarefa.PrevisaoHoras = mediaHoras; // esta propriedade tem de existir no model
            }

            return tarefasToDo;
        }

    }
}


using iTasks.controllers;
using iTasks.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmConsultarTarefasConcluidas : Form
    {
        // Instância da base de dados (contexto para aceder aos dados)
        private iTasksBD db;

        // Guarda o utilizador que foi passado para este formulário
        private Utilizador utilizadorRecebido;

        // Construtor do formulário que recebe um utilizador
        public frmConsultarTarefasConcluidas(Utilizador utilizadorRecebido)
        {
            InitializeComponent();  // Inicializa os componentes visuais do formulário
            db = new iTasksBD();    // Cria uma nova instância do contexto da base de dados

            this.utilizadorRecebido = utilizadorRecebido; // Guarda o utilizador recebido na variável local

            // Chama o método para mostrar as tarefas concluídas deste utilizador
            VerTarefasConcluidas(utilizadorRecebido);
        }

        // Método que carrega e mostra as tarefas concluídas de um determinado utilizador
        public void VerTarefasConcluidas(Utilizador utilizador)
        {
            try
            {
                // Obtém a lista de tarefas concluídas associadas ao utilizador
                var tarefas = db.TarefaController.MostrarTarefasDonePorUtilizador(utilizador);

                // Define a fonte de dados do DataGridView 'gvTarefasConcluidas' com uma lista anônima 
                // que contém informações formatadas das tarefas para melhor visualização
                gvTarefasConcluidas.DataSource = tarefas.Select(t => new
                {
                    ID = t.Id,
                    Descrição = t.Descricao,
                    // Informações do Gestor formatadas numa única string
                    Gestor = $"{t.Gestor?.Nome} | Username: {t.Gestor?.Username} | Departamento: {t.Gestor?.Departamento}",
                    // Informações do Programador formatadas numa única string
                    Programador = $"{t.Programador?.Nome} | Username: {t.Programador?.Username} | Experiência: {t.Programador?.NivelExperiencia}",
                    // Tipo da tarefa
                    TipoTarefa = $"{t.TipoDeTarefa?.Id} | Nome: {t.TipoDeTarefa?.Nome}",
                    Ordem = t.OrdemExecucao,
                    StoryPoints = t.StoryPoints.ToString(),
                    // Formata datas para o formato "dd/MM/yyyy", tratando possíveis valores nulos com operador ?.
                    DataPrevistaInício = t.DataPrevistaInicio?.ToString("dd/MM/yyyy"),
                    DataPrevistaFim = t.DataPrevistaFim.ToString("dd/MM/yyyy"),
                    Início = t.DataRealInicio?.ToString("dd/MM/yyyy"),
                    Fim = t.DataRealFim?.ToString("dd/MM/yyyy"),
                    DataCriação = t.DataCriacao?.ToString("dd/MM/yyyy"),
                    // Calcula a duração em dias da tarefa com base na diferença entre as datas reais de fim e início
                    DuraçãoDias = (int)(t.DataRealFim.Value - t.DataRealInicio.Value).TotalDays
                }).ToList(); // Converte para lista para ligação com o DataGridView
            }
            catch (Exception ex)
            {
                // Em caso de erro, mostra uma mensagem de erro ao utilizador
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento do clique no botão "Fechar"
        private void btFechar_Click(object sender, EventArgs e)
        {
            // Cria e abre o formulário frmKanban passando o utilizador atual
            frmKanban frmKanban = new frmKanban(utilizadorRecebido);
            frmKanban.Show();

            // Fecha o formulário atual
            this.Close();
        }
    }
}

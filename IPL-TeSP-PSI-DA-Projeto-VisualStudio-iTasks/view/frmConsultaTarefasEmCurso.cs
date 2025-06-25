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
    public partial class frmConsultaTarefasEmCurso : Form
    {
        // Instância para acesso à base de dados
        private iTasksBD db;

        // Guarda o utilizador recebido no construtor
        private Utilizador utilizadorRecebido;

        // Guarda o gestor, caso o utilizador recebido seja um gestor
        private Gestor gestor;

        // Construtor que recebe um utilizador selecionado
        public frmConsultaTarefasEmCurso(Utilizador utilizadorSelecionado)
        {
            InitializeComponent();    // Inicializa os componentes visuais do formulário
            db = new iTasksBD();      // Cria a instância do contexto da base de dados
            utilizadorRecebido = utilizadorSelecionado;  // Guarda o utilizador recebido

            // Verifica se o utilizador é um Gestor usando pattern matching
            if (utilizadorSelecionado is Gestor g)
            {
                gestor = g;          // Atribui o gestor local
                VerTarefasEmCurso(); // Chama o método para mostrar tarefas em curso para este gestor
            }
            else
            {
                // Se não for gestor, mostra uma mensagem e fecha o formulário
                MessageBox.Show("Apenas gestores podem consultar as tarefas em curso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        // Método que carrega e mostra as tarefas em curso para o gestor
        private void VerTarefasEmCurso()
        {
            try
            {
                // Obtém as tarefas em curso associadas ao ID do gestor
                var tarefas = db.TarefaController.MostrarTarefasEmCursoPorGestor(gestor.Id);

                // Cria uma lista anônima formatada para mostrar as tarefas
                var lista = tarefas.Select(t => new
                {
                    ID = t.Id,
                    Descrição = t.Descricao,
                    // Nome do Gestor ou "Sem atribuição" caso nulo
                    Gestor = t.Gestor?.Nome ?? "Sem atribuição",
                    // Nome do Programador ou "Sem atribuição" caso nulo
                    Programador = t.Programador?.Nome ?? "Sem atribuição",
                    // Nome do tipo da tarefa ou "Sem tipo" caso nulo
                    TipoTarefa = t.TipoDeTarefa?.Nome ?? "Sem tipo",
                    Ordem = t.OrdemExecucao,
                    StoryPoints = t.StoryPoints.ToString(),
                    // Datas previstas e reais formatadas, tratando valores nulos
                    DataPrevistaInício = t.DataPrevistaInicio.HasValue ? t.DataPrevistaInicio.Value.ToString("dd/MM/yyyy") : "Sem data",
                    DataPrevistaFim = t.DataPrevistaFim.ToString("dd/MM/yyyy"),
                    DataRealInicio = t.DataRealInicio.HasValue ? t.DataRealInicio.Value.ToString("dd/MM/yyyy") : "Não iniciado",
                    DataRealFim = t.DataRealFim.HasValue ? t.DataRealFim.Value.ToString("dd/MM/yyyy") : "Não terminado",
                    Estado = t.EstadoAtual.ToString(),
                    // Calcula tempo que falta até data prevista de fim, mostra em dias
                    TempoEmFalta = t.DataPrevistaFim > DateTime.Now ? (t.DataPrevistaFim - DateTime.Now).Days + " dias" : "0 dias",
                    // Calcula atraso caso data prevista fim seja passada
                    Atraso = t.DataPrevistaFim < DateTime.Now ? (DateTime.Now - t.DataPrevistaFim).Days + " dias" : "-"
                }).ToList();

                // Atribui a lista ao DataGridView para mostrar ao utilizador
                gvTarefasEmCurso.DataSource = lista;
            }
            catch (Exception ex)
            {
                // Caso haja erro, mostra mensagem ao utilizador
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento do clique no botão "Fechar"
        private void btFechar_Click(object sender, EventArgs e)
        {
            // Abre o formulário frmKanban com o utilizador recebido
            frmKanban frmKanban = new frmKanban(utilizadorRecebido);
            frmKanban.Show();

            // Fecha o formulário atual
            this.Close();
        }
    }
}

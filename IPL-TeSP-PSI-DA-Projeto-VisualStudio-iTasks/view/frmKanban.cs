using iTasks.controllers;
using iTasks.models;
using iTasks.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmKanban : Form
    {
        private iTasksBD db; // Acesso à base de dados

        public Utilizador utilizadorRecebido { get; private set; } // Utilizador autenticado

        public frmKanban()
        {
            InitializeComponent(); // Inicialização dos componentes visuais
            db = new iTasksBD();   // Criação da instância da base de dados
            AtualizaListas();      // Preenche as listas ToDo, Doing, Done
        }

        public frmKanban(Utilizador utilizador)
        {
            InitializeComponent();
            db = new iTasksBD();

            this.utilizadorRecebido = utilizador;

            // Apresenta o nome do utilizador
            if (utilizadorRecebido != null && !string.IsNullOrEmpty(utilizadorRecebido.Nome))
                label1.Text = "Bem-vindo: " + utilizadorRecebido.Nome;
            else
                label1.Text = "Bem-vindo!";

            AtualizaListas(); // Preenche listas
            Permisssoes();    // Ajusta interface com base no tipo de utilizador
        }

        // Botão para criar nova tarefa
        private void btNova_Click(object sender, EventArgs e)
        {
            frmDetalhesTarefa novaTarefa = new frmDetalhesTarefa();
            novaTarefa.Show();
            this.Hide();
        }

        // Vai para a gestão de utilizadores
        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGereUtilizadores gereUtilizadores = new frmGereUtilizadores();
            gereUtilizadores.Show();
            this.Hide();
        }

        // Vai para a gestão de tipos de tarefas
        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGereTiposTarefas gereTipos = new frmGereTiposTarefas();
            gereTipos.Show();
            this.Hide();
        }

        // Abre tarefas concluídas
        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultarTarefasConcluidas frm = new frmConsultarTarefasConcluidas(utilizadorRecebido);
            frm.Show();
            this.Hide();
        }

        // Abre tarefas em curso
        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaTarefasEmCurso frm = new frmConsultaTarefasEmCurso(utilizadorRecebido);
            frm.Show();
            this.Hide();
        }

        // Atualiza as listas do kanban
        private void AtualizaListas()
        {
            try
            {
                if (utilizadorRecebido is Programador)
                {
                    // Filtrar tarefas por programador
                    lstTodo.DataSource = db.TarefaController.MostrarTarefasToDoPorProgramador(utilizadorRecebido);
                    lstTodo.DisplayMember = "Descricao";

                    lstDoing.DataSource = db.TarefaController.MostrarTarefasDoingPorProgramador(utilizadorRecebido);
                    lstDoing.DisplayMember = "Descricao";

                    lstDone.DataSource = db.TarefaController.MostrarTarefasDonePorUtilizador(utilizadorRecebido);
                    lstDone.DisplayMember = "Descricao";
                }
                else
                {
                    // Gestor vê todas
                    lstTodo.DataSource = db.TarefaController.MostrarTarefasToDo();
                    lstDoing.DataSource = db.TarefaController.MostrarTarefasDoing();
                    lstDone.DataSource = db.TarefaController.MostrarTarefasDone();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar listas: {ex.Message}");
            }
        }

        // Mudar estado para "Doing"
        private void btSetDoing_Click(object sender, EventArgs e)
        {
            var tarefa = lstTodo.SelectedItem as Tarefa;
            if (tarefa == null)
            {
                MessageBox.Show("Nenhuma tarefa selecionada!");
                return;
            }

            db.TarefaController.MudarEstadoTarefa(tarefa, EstadoAtual.Doing, utilizadorRecebido);
            MessageBox.Show("Estado alterado para 'Doing'!");
            AtualizaListas();
        }

        // Mudar estado para "ToDo"
        private void btSetTodo_Click(object sender, EventArgs e)
        {
            var tarefa = lstDoing.SelectedItem as Tarefa;
            if (tarefa == null)
            {
                MessageBox.Show("Nenhuma tarefa selecionada!");
                return;
            }

            db.TarefaController.MudarEstadoTarefa(tarefa, EstadoAtual.ToDo, utilizadorRecebido);
            MessageBox.Show("Estado alterado para 'ToDo'!");
            AtualizaListas();
        }

        // Mudar estado para "Done"
        private void btSetDone_Click(object sender, EventArgs e)
        {
            var tarefa = lstDoing.SelectedItem as Tarefa;
            if (tarefa == null)
            {
                MessageBox.Show("Nenhuma tarefa selecionada!");
                return;
            }

            db.TarefaController.MudarEstadoTarefa(tarefa, EstadoAtual.Done, utilizadorRecebido);
            MessageBox.Show("Estado alterado para 'Done'!");
            MessageBox.Show("Não pode ser movida após estar em 'Done'.");
            AtualizaListas();
        }

        // Duplo clique nas tarefas ToDo
        private void lstTodo_DoubleClick(object sender, EventArgs e)
        {
            var tarefa = lstTodo.SelectedItem as Tarefa;
            if (tarefa == null) return;

            var frm = new frmDetalhesTarefa(tarefa, utilizadorRecebido);
            if (frm.ShowDialog() == DialogResult.OK)
                AtualizaListas();
        }

        // Duplo clique nas tarefas Doing
        private void lstDoing_DoubleClick(object sender, EventArgs e)
        {
            var tarefa = lstDoing.SelectedItem as Tarefa;
            if (tarefa == null) return;

            var frm = new frmDetalhesTarefa(tarefa, utilizadorRecebido);
            if (frm.ShowDialog() == DialogResult.OK)
                AtualizaListas();
        }

        // Duplo clique nas tarefas Done
        private void lstDone_DoubleClick(object sender, EventArgs e)
        {
            var tarefa = lstDone.SelectedItem as Tarefa;
            if (tarefa == null) return;

            var frm = new frmDetalhesTarefa(tarefa, utilizadorRecebido);
            if (frm.ShowDialog() == DialogResult.OK)
                AtualizaListas();
        }

        // Define permissões com base no tipo de utilizador
        private void Permisssoes()
        {
            if (utilizadorRecebido is Programador)
            {
                utilizadoresToolStripMenuItem.Enabled = false;
                exportarParaCSVToolStripMenuItem.Enabled = false;
                btNova.Enabled = false;
                tarefasEmCursoToolStripMenuItem.Enabled = false;
                btPrevisao.Enabled = false; // Programadores não podem ver previsões
            }
            else if (utilizadorRecebido is Gestor gestor)
            {
                if (!gestor.GereUtilizadores)
                {
                    gerirUtilizadoresToolStripMenuItem.Enabled = false;
                }
            }
        }

        // Terminar a aplicação
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Ao carregar o formulário
        private void frmKanban_Load_1(object sender, EventArgs e)
        {
            if (utilizadorRecebido != null)
                label1.Text = "Bem-vindo: " + utilizadorRecebido.Nome;
            else
                label1.Text = "Bem-vindo!";
        }

        // Exportar tarefas concluídas para CSV
        private void exportarParaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (utilizadorRecebido is Gestor gestorLogado)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "CSV files (*.csv)|*.csv";
                dialog.FileName = "TarefasConcluidas.csv";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    db.TarefaController.ExportarTarefasConcluidasParaCSV(gestorLogado.Id, dialog.FileName);
                    MessageBox.Show("Exportação concluída com sucesso!");
                }
            }
            else
            {
                MessageBox.Show("Apenas gestores podem exportar tarefas.");
            }
        }

        private void btPrevisao_Click(object sender, EventArgs e)
        {
            // ir para o formulário de previsão
            if (utilizadorRecebido is Gestor gestor)
            {
                FrmDetalhesPrevisao frmPrevisao = new FrmDetalhesPrevisao(gestor);
                frmPrevisao.Show();
            }
            else
            {
                MessageBox.Show("Apenas gestores podem visualizar a previsão de conclusão das tarefas.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}

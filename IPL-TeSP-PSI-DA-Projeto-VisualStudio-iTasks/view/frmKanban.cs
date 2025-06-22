using iTasks.controllers;
using iTasks.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmKanban : Form
    {

        private iTasksBD db;

        public Utilizador utilizadorRecebido { get; private set; }


        public frmKanban()
        {
            this.utilizadorRecebido = utilizadorRecebido;

            InitializeComponent();
            db = new iTasksBD();
            //label1.Text = "Bem-Vindo, " + utilizadorRecebido.Nome;
            AtualizaListas();

        }

        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ir para a vista de gestao de utilizadores
            frmGereUtilizadores gereUtilizadores = new frmGereUtilizadores();
            gereUtilizadores.Show();
            this.Hide();
        }

        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ir para a vista de gestao de tarefas
            frmGereTiposTarefas gereTiposDeTarefas = new frmGereTiposTarefas();
            gereTiposDeTarefas.Show();
            this.Hide();
        }

        private void btNova_Click(object sender, EventArgs e)
        {
            //ir para a vista de criar nova tarefa
            frmDetalhesTarefa novaTarefa = new frmDetalhesTarefa();
            novaTarefa.Show();
            this.Hide();

        }

        //apresentar a tarefa criada na lista das tarefas

private void AtualizaListas()
        {
            try
            {
                // Atualizar lista ToDo
                lstTodo.DataSource = null;
                lstTodo.DataSource = db.TarefaController.MostrarTarefasToDo();
                lstTodo.DisplayMember = "Descricao"; // ou a propriedade que quiser mostrar

                // Atualizar lista Doing
                lstDoing.DataSource = null;
                lstDoing.DataSource = db.TarefaController.MostrarTarefasDoing();
                lstDoing.DisplayMember = "Descricao";

                // Atualizar lista Done
                lstDone.DataSource = null;
                lstDone.DataSource = db.TarefaController.MostrarTarefasDone();
                lstDone.DisplayMember = "Descricao";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar listas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstTodo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var tarefaSelecionada = lstTodo.SelectedItem as Tarefa;

                if (tarefaSelecionada == null)
                {
                    MessageBox.Show("Nenhuma tarefa selecionada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Criar formulário de detalhes com a tarefa
                frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(tarefaSelecionada);

                // Mostrar como diálogo modal
                DialogResult result = detalhesTarefa.ShowDialog(this);

                // Se o utilizador gravou ou eliminou, atualizar lista
                if (result == DialogResult.OK)
                {
                    AtualizaListas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btSetDoing_Click(object sender, EventArgs e)
        {
            try
            {
                // Buscar a tarefa selecionada
                var tarefaSelecionada = lstTodo.SelectedItem as Tarefa;
                if (tarefaSelecionada == null)
                {
                    MessageBox.Show("Nenhuma tarefa selecionada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Muda o estado da tarefa para Doing
                db.TarefaController.MudarEstadoTarefa(tarefaSelecionada, EstadoAtual.Doing, utilizadorRecebido);

                MessageBox.Show("Estado da tarefa alterado para 'Doing' com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Atualizar as listas/interface se necessário
                AtualizaListas(); // descomente se tiver este método
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao alterar estado da tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btSetTodo_Click(object sender, EventArgs e)
        {
            try
            {
                // Buscar a tarefa selecionada
                var tarefaSelecionada = lstDoing.SelectedItem as Tarefa;
                if (tarefaSelecionada == null)
                {
                    MessageBox.Show("Nenhuma tarefa selecionada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Muda o estado da tarefa para Doing
                db.TarefaController.MudarEstadoTarefa(tarefaSelecionada, EstadoAtual.ToDo, utilizadorRecebido);

                MessageBox.Show("Estado da tarefa alterado para 'ToDo' com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Atualizar as listas/interface se necessário
                AtualizaListas(); // descomente se tiver este método
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao alterar estado da tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btSetDone_Click(object sender, EventArgs e)
        {
            try
            {
                // Buscar a tarefa selecionada
                var tarefaSelecionada = lstDoing.SelectedItem as Tarefa;
                if (tarefaSelecionada == null)
                {
                    MessageBox.Show("Nenhuma tarefa selecionada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Muda o estado da tarefa para Doing
                db.TarefaController.MudarEstadoTarefa(tarefaSelecionada, EstadoAtual.Done, utilizadorRecebido);

                MessageBox.Show("Estado da tarefa alterado para 'Done' com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Atualizar as listas/interface se necessário
                AtualizaListas(); // descomente se tiver este método
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao alterar estado da tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDoing_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var tarefaSelecionada = lstDoing.SelectedItem as Tarefa;

                if (tarefaSelecionada == null)
                {
                    MessageBox.Show("Nenhuma tarefa selecionada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Criar formulário de detalhes com a tarefa
                frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(tarefaSelecionada);

                // Mostrar como diálogo modal
                DialogResult result = detalhesTarefa.ShowDialog(this);

                // Se o utilizador gravou ou eliminou, atualizar lista
                if (result == DialogResult.OK)
                {
                    AtualizaListas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

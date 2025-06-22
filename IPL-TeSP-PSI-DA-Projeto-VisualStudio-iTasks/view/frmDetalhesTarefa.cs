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
    public partial class frmDetalhesTarefa : Form
    {
        private iTasksBD db;

        private Tarefa tarefa;


        Tarefa tarefaSelecionada;


        // Construtor normal (para nova tarefa)
        public frmDetalhesTarefa()
        {
            InitializeComponent();
            db = new iTasksBD();
            IniciarComboBox();

            txtId.Text = db.TarefaController.IncrementarContadorTipoTarefa();
        }

        internal frmDetalhesTarefa(Tarefa tarefaSelecionada)
        {
            InitializeComponent();
            db = new iTasksBD();
            this.tarefaSelecionada = tarefaSelecionada; // Corrigido para usar a mesma variável

            IniciarComboBox();

            PreencherCamposImutaveis();
            PreencherCamposMutaveis();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (tarefaSelecionada != null) // Usando a variável correta
                {
                    // Obter dados atualizados do formulário
                    Programador programador = cbProgramador.SelectedItem as Programador;
                    Gestor gestor = db.GestorController.ObterGestorAtual();
                    TipoTarefa tipoTarefa = cbTipoTarefa.SelectedItem as TipoTarefa;

                    db.TarefaController.EditarTarefa(
                        tarefaSelecionada,
                        txtDesc.Text,
                        gestor,
                        programador,
                        int.Parse(txtOrdem.Text),
                        dtInicio.Value,
                        dtFim.Value,
                        tipoTarefa,
                        int.Parse(txtStoryPoints.Text)
                    );

                    MessageBox.Show("Tarefa atualizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao editar a tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void IniciarComboBox()
        {
            // Preencher o ComboBox com os tipos de tarefa
            cbTipoTarefa.DisplayMember = "Descricao";
            cbTipoTarefa.ValueMember = "Id";
            cbTipoTarefa.DataSource = db.TipoTarefaController.MostrarTiposTarefas();

            // Preencher o ComboBox com os programadores
            cbProgramador.DisplayMember = "Nome";
            cbProgramador.ValueMember = "Id";
            cbProgramador.DataSource = db.ProgramadorController.MostrarProgramadores();


        }

        private void btGravar_Click_1(object sender, EventArgs e)
        {
            if (txtDesc.Text == "")
            {
                MessageBox.Show("Por favor, preencha a descrição da tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbTipoTarefa.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione o tipo de tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbProgramador.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione um programador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ordem;
            if (!int.TryParse(txtOrdem.Text, out ordem))
            {
                MessageBox.Show("Por favor, preencha a ordem da tarefa corretamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int storyPoints;
            if (!int.TryParse(txtStoryPoints.Text, out storyPoints))
            {
                MessageBox.Show("Por favor, preencha os story points corretamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtInicio.Value < DateTime.Today || dtFim.Value < DateTime.Today)
            {
                MessageBox.Show("Datas devem ser hoje ou futuras.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Captura os objetos
                string descricao = txtDesc.Text;
                var tipoTarefa = (TipoTarefa)cbTipoTarefa.SelectedItem;
                var programador = (Programador)cbProgramador.SelectedItem;
                var gestor = db.GestorController.ObterGestorAtual();

                db.TarefaController.GravarTarefa(
                     descricao,
                    gestor,
                    programador,
                    ordem,
                    dtInicio.Value,
                    dtFim.Value,
                    tipoTarefa,
                    storyPoints,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now,
                    EstadoAtual.ToDo
                );

                MessageBox.Show("Tarefa gravada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gravar tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void PreencherCamposImutaveis()
        {
            if (tarefaSelecionada == null) return; // Usando a variável correta

            txtId.Text = tarefaSelecionada.Id.ToString();
            txtEstado.Text = tarefaSelecionada.EstadoAtual.ToString();
            txtDataCriacao.Text = tarefaSelecionada.DataCriacao?.ToString("dd/MM/yyyy") ?? "N/A";

            txtDataRealini.Text = tarefaSelecionada.DataRealInicio?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
            txtdataRealFim.Text = tarefaSelecionada.DataRealFim?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
        }

        private void PreencherCamposMutaveis()
        {
            if (tarefaSelecionada == null) return; // Usando a variável correta

            txtDesc.Text = tarefaSelecionada.Descricao;
            cbTipoTarefa.SelectedValue = tarefaSelecionada.TipoDeTarefa?.Id ?? -1;

            cbProgramador.SelectedValue = tarefaSelecionada.Programador?.Id ?? -1;
            txtOrdem.Text = tarefaSelecionada.OrdemExecucao.ToString();
            txtStoryPoints.Text = tarefaSelecionada.StoryPoints.ToString();
            dtInicio.Value = tarefaSelecionada.DataPrevistaInicio ?? DateTime.Today;
            dtFim.Value = tarefaSelecionada.DataPrevistaFim;

        }


        private void btFechar_Click(object sender, EventArgs e)
        {
            //ir para o form de kanban
            frmKanban frm = new frmKanban();
            this.Hide();
             frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tarefaSelecionada != null) // Usando a variável correta
                {
                    db.TarefaController.EliminarTarefa(tarefaSelecionada);
                    MessageBox.Show("Tarefa eliminada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao eliminar a tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}

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
        // Instância da base de dados para manipular dados
        private iTasksBD db;

        // Variável para guardar a tarefa que foi selecionada para visualizar/editar
        Tarefa tarefaSelecionada;

        // Guarda o utilizador que abriu este formulário (pode ser gestor ou programador)
        private Utilizador utilizadorRecebido;

        // Construtor padrão para criar nova tarefa
        public frmDetalhesTarefa(Utilizador utilizador)
        {
            InitializeComponent();
            db = new iTasksBD();
            this.utilizadorRecebido = utilizador;
            IniciarComboBox();
            txtId.Text = db.TarefaController.IncrementarContadorTarefa();
        }

        // Construtor que recebe uma tarefa existente e um utilizador, para visualizar/editar
        internal frmDetalhesTarefa(Tarefa tarefaSelecionada, Utilizador utilizadorRecebido)
        {
            InitializeComponent();
            db = new iTasksBD();

            this.tarefaSelecionada = tarefaSelecionada;
            this.utilizadorRecebido = utilizadorRecebido;

            IniciarComboBox();    // Preenche combobox

            CamposNaoAlteraveis(); // Preenche campos que não podem ser alterados (Id, datas, estado)
            CamposAlteraveis();    // Preenche campos que podem ser alterados (descrição, programador, etc)

            // Se o utilizador for Programador, torna os campos apenas leitura (não editáveis)
            if (utilizadorRecebido is Programador)
            {
                ReadOnly();
            }
        }

        // Evento do botão de editar (button1)
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (tarefaSelecionada != null) // Só editar se a tarefa existir
                {
                    // Obtem os dados atualizados dos controles do formulário
                    Programador programador = cbProgramador.SelectedItem as Programador;
                    Gestor gestor = db.GestorController.ObterGestorAtual();
                    TipoTarefa tipoTarefa = cbTipoTarefa.SelectedItem as TipoTarefa;

                    // Chama método para editar a tarefa no banco de dados, passando os dados novos
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

        // Evento do botão gravar nova tarefa (btGravar)
        private void btGravar_Click_1(object sender, EventArgs e)
        {
            // Validações de campos obrigatórios e formatos corretos
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

            if (!int.TryParse(txtOrdem.Text, out int ordem))
            {
                MessageBox.Show("Por favor, preencha a ordem da tarefa corretamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtStoryPoints.Text, out int storyPoints))
            {
                MessageBox.Show("Por favor, preencha os story points corretamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validação para as datas serem hoje ou futuras
            if (dtInicio.Value < DateTime.Today || dtFim.Value < DateTime.Today)
            {
                MessageBox.Show("Datas devem ser hoje ou futuras.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Obtem os objetos selecionados nos comboboxes
                string descricao = txtDesc.Text;
                var tipoTarefa = (TipoTarefa)cbTipoTarefa.SelectedItem;
                var programador = (Programador)cbProgramador.SelectedItem;
                var gestor = db.GestorController.ObterGestorAtual();

                // Verifica se já existe uma tarefa com a mesma ordem para o programador selecionado
                if (db.TarefaController.MostrarTarefas().Any(t => t.IdProgramador == programador.Id && t.OrdemExecucao == ordem))
                {
                    MessageBox.Show("Já existe uma tarefa com essa mesma ordem para o programador selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Grava a nova tarefa na base de dados
                db.TarefaController.GravarTarefa(
                    descricao,
                    gestor,
                    programador,
                    ordem,
                    dtInicio.Value,
                    dtFim.Value,
                    tipoTarefa,
                    storyPoints,
                    DateTime.Now,    // Data criação
                    DateTime.Now,    // Data real início (inicialmente igual à criação)
                    DateTime.Now,    // Data real fim (inicialmente igual à criação)
                    EstadoAtual.ToDo // Estado inicial da tarefa
                );

                MessageBox.Show("Tarefa gravada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gravar tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento do botão eliminar tarefa (button2)
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tarefaSelecionada != null) // Só elimina se existir tarefa selecionada
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

        // Método para preencher os ComboBoxes (tipos de tarefa e programadores)
        public void IniciarComboBox()
        {
            // Preenche ComboBox dos tipos de tarefa, definindo propriedade que mostra e que identifica
            cbTipoTarefa.DisplayMember = "Descricao";
            cbTipoTarefa.ValueMember = "Id";
            cbTipoTarefa.DataSource = db.TipoTarefaController.MostrarTiposTarefas();

            // Preenche ComboBox dos programadores
            cbProgramador.DisplayMember = "Nome";  // Aqui assumo que "Nome" é o campo a mostrar
            cbProgramador.ValueMember = "Id";
            cbProgramador.DataSource = db.ProgramadorController.MostrarProgramadores();
        }

        // Preenche os campos que não podem ser alterados pelo utilizador
        private void CamposNaoAlteraveis()
        {
            if (tarefaSelecionada == null) return;

            txtId.Text = tarefaSelecionada.Id.ToString();
            txtEstado.Text = tarefaSelecionada.EstadoAtual.ToString();
            txtDataCriacao.Text = tarefaSelecionada.DataCriacao?.ToString("dd/MM/yyyy") ?? "N/A";

            txtDataRealini.Text = tarefaSelecionada.DataRealInicio?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
            txtdataRealFim.Text = tarefaSelecionada.DataRealFim?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
        }

        // Preenche os campos que podem ser alterados pelo utilizador
        private void CamposAlteraveis()
        {
            if (tarefaSelecionada == null) return;

            txtDesc.Text = tarefaSelecionada.Descricao;

            // Seleciona o tipo de tarefa no ComboBox
            cbTipoTarefa.SelectedValue = tarefaSelecionada.IdTipoDeTarefa;

            // Configura o ComboBox dos programadores para mostrar o nome e identificar pelo Id
            cbProgramador.ValueMember = "Id";
            cbProgramador.DisplayMember = "Nome";

            // Seleciona o programador da tarefa
            cbProgramador.SelectedValue = tarefaSelecionada.IdProgramador;

            txtOrdem.Text = tarefaSelecionada.OrdemExecucao.ToString();
            txtStoryPoints.Text = tarefaSelecionada.StoryPoints.ToString();

            // Define as datas previstas, se existirem; senão usa hoje para início
            dtInicio.Value = tarefaSelecionada.DataPrevistaInicio ?? DateTime.Today;
            dtFim.Value = tarefaSelecionada.DataPrevistaFim;
        }

        // Torna campos e botões apenas leitura (para programadores, por exemplo)
        private void ReadOnly()
        {
            txtDesc.ReadOnly = true;
            cbTipoTarefa.Enabled = false;
            cbProgramador.Enabled = false;
            txtOrdem.ReadOnly = true;
            txtStoryPoints.ReadOnly = true;
            dtInicio.Enabled = false;
            dtFim.Enabled = false;

            // Desativa botões de gravação, edição e eliminação
            btGravar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        // Evento do botão fechar que volta para o formulário Kanban
        private void btFechar_Click(object sender, EventArgs e)
        {
            try
            {
                // Mostra o nome do utilizador (debug/aviso)
                //MessageBox.Show("Utilizador: " + (utilizadorRecebido?.Nome ?? "null"));

                // Abre o formulário Kanban com o utilizador atual e fecha este
                frmKanban frm = new frmKanban(utilizadorRecebido);
                this.Hide();
                frm.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao fechar e abrir Kanban: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}

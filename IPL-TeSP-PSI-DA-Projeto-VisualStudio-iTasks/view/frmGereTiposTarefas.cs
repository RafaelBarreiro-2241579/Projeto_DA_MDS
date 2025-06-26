using iTasks.controllers;
using iTasks.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmGereTiposTarefas : Form
    {
        private iTasksBD db; // Instância da base de dados

        private Utilizador utilizadorRecebido; // Utilizador autenticado

        public frmGereTiposTarefas()
        {
            InitializeComponent(); // Inicializa os componentes visuais do formulário
            db = new iTasksBD();   // Inicializa a base de dados

            db.Context.TipoTarefa.Load(); // Carrega os dados da tabela TipoTarefa

            txtId.Text = db.TipoTarefaController.IncrementarContadorTipoTarefa(); // Mostra o próximo ID disponível

            lstLista.DataSource = db.TipoTarefaController.MostrarTiposTarefas(); // Preenche a lista com os tipos existentes

            lstLista.ClearSelected(); // Nenhum item fica selecionado inicialmente
        }

        public frmGereTiposTarefas(Utilizador utilizador)
        {
            InitializeComponent();
            db = new iTasksBD();
            this.utilizadorRecebido = utilizador;

            db.Context.TipoTarefa.Load(); // Carrega os dados da tabela TipoTarefa

            txtId.Text = db.TipoTarefaController.IncrementarContadorTipoTarefa(); // Mostra o próximo ID disponível

            lstLista.DataSource = db.TipoTarefaController.MostrarTiposTarefas(); // Preenche a lista com os tipos existentes

            lstLista.ClearSelected(); // Nenhum item fica selecionado inicialmente
        }


        private void btGravar_Click(object sender, EventArgs e)
        {
            // Verifica se a descrição está vazia
            if (string.IsNullOrWhiteSpace(txtDesc.Text))
            {
                MessageBox.Show("Por favor, preencha a descrição do tipo de tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                db.TipoTarefaController.GravarTipoTarefa(txtDesc.Text); // Grava novo tipo de tarefa

                MessageBox.Show("Tipo de tarefa gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshList(); // Atualiza a lista
                ClearForm();   // Limpa os campos do formulário
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gravar tipo de tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se foi selecionado algum item na lista
                if (lstLista.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, selecione um tipo de tarefa para editar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verifica se a descrição está preenchida
                if (string.IsNullOrWhiteSpace(txtDesc.Text))
                {
                    MessageBox.Show("Por favor, preencha a descrição do tipo de tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtém o item selecionado
                TipoTarefa tipoTarefaSelecionada = (TipoTarefa)lstLista.SelectedItem;

                // Atualiza o tipo de tarefa com a nova descrição
                db.TipoTarefaController.EditarTipoTarefa(tipoTarefaSelecionada, txtDesc.Text);

                MessageBox.Show("Tipo de tarefa editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshList(); // Atualiza a lista
                ClearForm();   // Limpa os campos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao editar tipo de tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se algum item está selecionado
                if (lstLista.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, selecione um tipo de tarefa para remover.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Confirma a eliminação com o utilizador
                DialogResult resultado = MessageBox.Show("Tem certeza que deseja remover este tipo de tarefa?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado != DialogResult.Yes)
                {
                    return;
                }

                // Obtém o tipo de tarefa selecionado
                TipoTarefa tipoTarefaSelecionada = (TipoTarefa)lstLista.SelectedItem;

                // Remove o tipo de tarefa
                db.TipoTarefaController.RemoverTipoTarefa(tipoTarefaSelecionada);

                MessageBox.Show("Tipo de tarefa removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshList(); // Atualiza a lista
                ClearForm();   // Limpa os campos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover tipo de tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Quando um item é selecionado, atualiza os campos de ID e descrição
            if (lstLista.SelectedItem is TipoTarefa tarefa)
            {
                txtId.Text = tarefa.Id.ToString();
                txtDesc.Text = tarefa.Nome;
            }
            else
            {
                // Se nada estiver selecionado, gera novo ID e limpa a descrição
                txtId.Text = db.TipoTarefaController.IncrementarContadorTipoTarefa();
                txtDesc.Text = "";
            }
        }

        // Atualiza a lista com os tipos de tarefa atuais
        private void RefreshList()
        {
            lstLista.DataSource = null;
            lstLista.DataSource = db.TipoTarefaController.MostrarTiposTarefas();
        }

        // Limpa o formulário e define próximo ID
        private void ClearForm()
        {
            txtDesc.Text = "";
            txtId.Text = db.TipoTarefaController.IncrementarContadorTipoTarefa();
            lstLista.ClearSelected();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Abre o formulário Kanban e fecha o atual
            frmKanban kanbanForm = new frmKanban(utilizadorRecebido);
            kanbanForm.Show();
            this.Close();
        }
    }
}
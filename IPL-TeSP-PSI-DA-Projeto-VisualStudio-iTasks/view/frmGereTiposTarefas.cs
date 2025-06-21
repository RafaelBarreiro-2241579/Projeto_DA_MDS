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
        private iTasksBD db;

        public frmGereTiposTarefas()
        {
            InitializeComponent();
            db = new iTasksBD();

            db.Context.TipoTarefa.Load();

            txtId.Text = db.TipoTarefaController.IncrementarContadorTipoTarefa();

            lstLista.DataSource = db.TipoTarefaController.MostrarTiposTarefas();

            lstLista.ClearSelected();

        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDesc.Text))
            {
                MessageBox.Show("Por favor, preencha a descrição do tipo de tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Save the new TipoTarefa
                db.TipoTarefaController.GravarTipoTarefa(txtDesc.Text);

                MessageBox.Show("Tipo de tarefa gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh list
                RefreshList();

                // Clear the form
                ClearForm();
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
                // Verifica se há um item selecionado na lista
                if (lstLista.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, selecione um tipo de tarefa para editar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verifica se a descrição não está vazia
                if (string.IsNullOrWhiteSpace(txtDesc.Text))
                {
                    MessageBox.Show("Por favor, preencha a descrição do tipo de tarefa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtém o tipo de tarefa selecionado
                TipoTarefa tipoTarefaSelecionada = (TipoTarefa)lstLista.SelectedItem;

                // Atualiza o nome do tipo de tarefa
                db.TipoTarefaController.EditarTipoTarefa(tipoTarefaSelecionada, txtDesc.Text);

                MessageBox.Show("Tipo de tarefa editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh list
                RefreshList();

                // Clear form
                ClearForm();
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
                // Verifica se há um item selecionado na lista
                if (lstLista.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, selecione um tipo de tarefa para remover.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Confirmação antes de remover
                DialogResult resultado = MessageBox.Show("Tem certeza que deseja remover este tipo de tarefa?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado != DialogResult.Yes)
                {
                    return;
                }

                // Obtém o tipo de tarefa selecionado
                TipoTarefa tipoTarefaSelecionada = (TipoTarefa)lstLista.SelectedItem;

                // Remove o tipo de tarefa usando o controller
                db.TipoTarefaController.RemoverTipoTarefa(tipoTarefaSelecionada);

                MessageBox.Show("Tipo de tarefa removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh list
                RefreshList();

                // Clear form
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover tipo de tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lstLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstLista.SelectedItem is TipoTarefa tarefa)
            {
                // Quando seleciona um item, mostra o ID e nome do item selecionado
                txtId.Text = tarefa.Id.ToString();
                txtDesc.Text = tarefa.Nome;
            }
            else
            {
                // Quando não há seleção, mostra o próximo ID disponível
                txtId.Text = db.TipoTarefaController.IncrementarContadorTipoTarefa();
                txtDesc.Text = "";
            }
        }

        // Helper methods to avoid code duplication
        private void RefreshList()
        {
            lstLista.DataSource = null; // Clear existing binding
            lstLista.DataSource = db.TipoTarefaController.MostrarTiposTarefas();
        }

        private void ClearForm()
        {
            txtDesc.Text = "";
            // Atualizar para o próximo ID disponível
            txtId.Text = db.TipoTarefaController.IncrementarContadorTipoTarefa();
            lstLista.ClearSelected(); // Clear selection
        }


    }
}
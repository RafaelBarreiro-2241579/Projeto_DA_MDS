using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTasks.controllers;
using iTasks.models;

namespace iTasks
{
    public partial class frmLogin : Form
    {
        private iTasksBD iTasksBD;
        private UserController userController;

        public frmLogin()
        {
            InitializeComponent();
            iTasksBD = new iTasksBD(); // guarda instância global
            userController = new UserController(new iTasksBD.iTasksContext());

            try
            {
                userController.addAdmin();
                // Adiciona o utilizador admin se não existir
            }
            catch (Exception ex)
            {
                // Se ocorrer um erro ao adicionar o administrador, mostra uma mensagem de erro
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Guarda os dados do utilizador
                Utilizador user = userController.loginUtilizador(txtUsername.Text, txtPassword.Text);

                //Verifica se o utilizador existe e se a password está correta
                if (user != null)
                {
                    // Se o utilizador existir e a password estiver correta, abre o formulário principal
                    this.Hide();
                    frmKanban kanban = new frmKanban(user);
                    kanban.FormClosed += (s, args) => this.Close(); // Fecha o login só depois do Kanban fechar
                    kanban.Show();
                }
                else
                {
                    MessageBox.Show("Username ou password incorretos.", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Se ocorrer um erro ao fazer login, mostra uma mensagem de erro
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
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
    public partial class frmGereUtilizadores : Form
    {
        private iTasksBD db; // Instância da base de dados

        public frmGereUtilizadores()
        {
            InitializeComponent();

            db = new iTasksBD(); // Inicializa a ligação à base de dados

            AtualizarInformacao(); // Preenche os dados visuais

            // Define os próximos IDs para programador e gestor
            txtIdProg.Text = db.ProgramadorController.IncrementarProgramador();
            txtIdGestor.Text = db.ProgramadorController.IncrementarProgramador(); // Reutilização do método
        }

        // Botão para gravar um novo programador
        private void btGravarProg_Click(object sender, EventArgs e)
        {
            // Validações básicas
            string nome = txtNomeProg.Text;
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Por favor, preencha o nome do programador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string username = txtUsernameProg.Text;
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Por favor, preencha o username do programador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string password = txtPasswordProg.Text;
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, preencha a password do programador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nivelExperienciaStr = cbNivelProg.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(nivelExperienciaStr))
            {
                MessageBox.Show("Por favor, selecione o nível de experiência do programador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string gestorIdStr = cbGestorProg.Text;
            if (string.IsNullOrWhiteSpace(gestorIdStr))
            {
                MessageBox.Show("Por favor, selecione o gestor do programador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Chama o controller para gravar
                db.ProgramadorController.GravarProgramador(
                    txtNomeProg.Text,
                    txtUsernameProg.Text,
                    txtPasswordProg.Text,
                    (NivelExperiencia)cbNivelProg.SelectedItem,
                    (Gestor)cbGestorProg.SelectedItem
                );

                AtualizarInformacao(); // Atualiza listas e limpa campos

                MessageBox.Show("Programador gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gravar programador: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Atualiza dados dos formulários (listas e comboboxes)
        private void AtualizarInformacao()
        {
            // Limpa campos de programador
            txtIdProg.Clear();
            txtNomeProg.Clear();
            txtUsernameProg.Clear();
            txtPasswordProg.Clear();

            // Combobox de gestores para programador
            cbGestorProg.DisplayMember = "Nome";
            cbGestorProg.ValueMember = "Id";
            cbGestorProg.DataSource = db.Context.Gestores.ToList();

            // Combo de nível de experiência
            cbNivelProg.DataSource = Enum.GetValues(typeof(NivelExperiencia));

            // Combo de departamento
            cbDepartamento.DataSource = Enum.GetValues(typeof(Departamento));

            // Lista de gestores
            lstListaGestores.DataSource = null;
            lstListaGestores.DataSource = db.Context.Gestores.ToList();
            lstListaGestores.DisplayMember = ""; // Mostrar ToString() ou outra propriedade

            // Lista de programadores
            lstListaProgramadores.DataSource = db.Context.Programadores.ToList();
        }

        // Botão para gravar gestor
        private void btGravarGestor_Click(object sender, EventArgs e)
        {
            string nome = txtNomeGestor.Text;
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Por favor, preencha o nome do gestor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string username = txtUsernameGestor.Text;
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Por favor, preencha o username do gestor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string password = txtPasswordGestor.Text;
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, preencha a password do gestor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbDepartamento.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione o departamento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                db.GestorController.GravarGestor(
                    nome,
                    username,
                    password,
                    (Departamento)cbDepartamento.SelectedItem,
                    chkGereUtilizadores.Checked
                );

                MessageBox.Show("Gestor gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarInformacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gravar gestor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botão editar gestor
        private void button1_Click(object sender, EventArgs e)
        {
            Gestor gestorSelecionado = lstListaGestores.SelectedItem as Gestor;

            if (gestorSelecionado != null)
            {
                db.GestorController.EditarGestor(
                    gestorSelecionado,
                    txtNomeGestor.Text,
                    txtUsernameGestor.Text,
                    txtPasswordGestor.Text,
                    (Departamento)cbDepartamento.SelectedItem,
                    chkGereUtilizadores.Checked);

                MessageBox.Show("Gestor atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarInformacao();
            }
        }

        // Botão eliminar gestor
        private void button2_Click(object sender, EventArgs e)
        {
            db.GestorController.EliminarGestor(lstListaGestores.SelectedItem as Gestor);

            MessageBox.Show("Gestor eliminado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            AtualizarInformacao();
        }

        // Botão editar programador
        private void button3_Click(object sender, EventArgs e)
        {
            Programador programadorSelecionado = lstListaProgramadores.SelectedItem as Programador;

            db.ProgramadorController.EditarProgramador(
                programadorSelecionado,
                txtNomeProg.Text,
                txtUsernameProg.Text,
                txtPasswordProg.Text,
                (NivelExperiencia)cbNivelProg.SelectedItem,
                (Gestor)cbGestorProg.SelectedItem
            );

            MessageBox.Show("Programador atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AtualizarInformacao();
        }

        // Botão eliminar programador
        private void button4_Click(object sender, EventArgs e)
        {
            Programador programadorSelecionado = lstListaProgramadores.SelectedItem as Programador;

            db.ProgramadorController.EliminarProgramador(programadorSelecionado);

            AtualizarInformacao();
        }

        // Quando seleciona gestor, preenche os campos
        private void lstListaGestores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstListaGestores.SelectedItem is Gestor gestorSelecionado)
            {
                txtIdGestor.Text = gestorSelecionado.Id.ToString();
                txtNomeGestor.Text = gestorSelecionado.Nome;
                txtUsernameGestor.Text = gestorSelecionado.Username;
                txtPasswordGestor.Text = gestorSelecionado.Password;
                cbDepartamento.SelectedItem = gestorSelecionado.Departamento;
                chkGereUtilizadores.Checked = gestorSelecionado.GereUtilizadores;
            }
            else
            {
                txtIdGestor.Clear();
                txtNomeGestor.Clear();
                txtUsernameGestor.Clear();
                txtPasswordGestor.Clear();
                cbDepartamento.SelectedIndex = -1;
                chkGereUtilizadores.Checked = false;
            }
        }

        // Quando seleciona programador, preenche os campos
        private void lstListaProgramadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstListaProgramadores.SelectedItem is Programador programadorSelecionado)
            {
                txtIdProg.Text = programadorSelecionado.Id.ToString();
                txtNomeProg.Text = programadorSelecionado.Nome;
                txtUsernameProg.Text = programadorSelecionado.Username;
                txtPasswordProg.Text = programadorSelecionado.Password;
                cbNivelProg.SelectedItem = programadorSelecionado.NivelExperiencia;
                cbGestorProg.SelectedValue = programadorSelecionado.IdGestor;
            }
            else
            {
                txtIdProg.Clear();
                txtNomeProg.Clear();
                txtUsernameProg.Clear();
                txtPasswordProg.Clear();
                cbNivelProg.SelectedIndex = -1;
                cbGestorProg.SelectedIndex = -1;
            }
        }

        // Botão voltar ao Kanban
        private void button5_Click(object sender, EventArgs e)
        {
            frmKanban kanbanForm = new frmKanban();
            kanbanForm.Show();
            this.Hide();
        }
    }
}

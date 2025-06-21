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
    public partial class frmKanban : Form
    {
        public frmKanban()
        {
            InitializeComponent();
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
    }
}

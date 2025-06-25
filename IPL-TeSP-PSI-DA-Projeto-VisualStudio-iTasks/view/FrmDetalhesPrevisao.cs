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

namespace iTasks.view
{
    public partial class FrmDetalhesPrevisao : Form
    {
        private iTasksBD db;
        private Utilizador utilizadorRecebido;

        public FrmDetalhesPrevisao(Utilizador utilizador)
        {
            InitializeComponent();
            db = new iTasksBD();
            utilizadorRecebido = utilizador;

            // Só permite se for Gestor
            if (!(utilizadorRecebido is Gestor))
            {
                MessageBox.Show("Apenas gestores podem visualizar a previsão de conclusão das tarefas.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            CarregarPrevisao();
        }

        private void CarregarPrevisao()
        {
            var previsoes = db.TarefaController.CalcularPrevisaoTarefasToDo();

            double somaPrevisaoHoras = previsoes.Sum(p => p.PrevisaoHoras);
            var detalhes = previsoes.Select(p =>
                $"{p.Id} |Tarefa: {p.Descricao} | StoryPoints: {p.StoryPoints} | Previsão: {p.PrevisaoHoras:F2} Horas"
            ).ToList();

            lstDetalhesPrevisao.DataSource = detalhes;
            lblPrevisaoTotal.Text = $"Total Previsto para Conclusão:  {somaPrevisaoHoras:F2}  Horas";
        }

        private void btn_FecharFrmDetalhesPrevisao_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

namespace iTasks.view
{
    partial class FrmDetalhesPrevisao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPrevisaoTotal = new System.Windows.Forms.Label();
            this.btn_FecharFrmDetalhesPrevisao = new System.Windows.Forms.Button();
            this.lstDetalhesPrevisao = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lblPrevisaoTotal
            // 
            this.lblPrevisaoTotal.AutoSize = true;
            this.lblPrevisaoTotal.Location = new System.Drawing.Point(48, 493);
            this.lblPrevisaoTotal.Name = "lblPrevisaoTotal";
            this.lblPrevisaoTotal.Size = new System.Drawing.Size(64, 16);
            this.lblPrevisaoTotal.TabIndex = 5;
            this.lblPrevisaoTotal.Text = "Previsao:";
            // 
            // btn_FecharFrmDetalhesPrevisao
            // 
            this.btn_FecharFrmDetalhesPrevisao.Location = new System.Drawing.Point(454, 503);
            this.btn_FecharFrmDetalhesPrevisao.Name = "btn_FecharFrmDetalhesPrevisao";
            this.btn_FecharFrmDetalhesPrevisao.Size = new System.Drawing.Size(120, 49);
            this.btn_FecharFrmDetalhesPrevisao.TabIndex = 4;
            this.btn_FecharFrmDetalhesPrevisao.Text = "Fechar";
            this.btn_FecharFrmDetalhesPrevisao.UseVisualStyleBackColor = true;
            this.btn_FecharFrmDetalhesPrevisao.Click += new System.EventHandler(this.btn_FecharFrmDetalhesPrevisao_Click);
            // 
            // lstDetalhesPrevisao
            // 
            this.lstDetalhesPrevisao.FormattingEnabled = true;
            this.lstDetalhesPrevisao.ItemHeight = 16;
            this.lstDetalhesPrevisao.Location = new System.Drawing.Point(51, 37);
            this.lstDetalhesPrevisao.Name = "lstDetalhesPrevisao";
            this.lstDetalhesPrevisao.Size = new System.Drawing.Size(523, 420);
            this.lstDetalhesPrevisao.TabIndex = 3;
            // 
            // FrmDetalhesPrevisao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 588);
            this.Controls.Add(this.lblPrevisaoTotal);
            this.Controls.Add(this.btn_FecharFrmDetalhesPrevisao);
            this.Controls.Add(this.lstDetalhesPrevisao);
            this.Name = "FrmDetalhesPrevisao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmDetalhesPrevisao";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPrevisaoTotal;
        private System.Windows.Forms.Button btn_FecharFrmDetalhesPrevisao;
        private System.Windows.Forms.ListBox lstDetalhesPrevisao;
    }
}
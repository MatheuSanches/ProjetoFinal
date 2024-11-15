namespace ProjetoFinal
{
    partial class FrmRelatorioVenda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRelatorioVenda));
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.lblData = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.cbxCliente = new System.Windows.Forms.ComboBox();
            this.DgvRelVenda = new System.Windows.Forms.DataGridView();
            this.mskTxtDataIni = new System.Windows.Forms.MaskedTextBox();
            this.txtValorTotal = new System.Windows.Forms.TextBox();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.mskTxtDataFim = new System.Windows.Forms.MaskedTextBox();
            this.lblDataFim = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvRelVenda)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnPesquisa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisa.Location = new System.Drawing.Point(693, 33);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 52;
            this.btnPesquisa.Text = "Pesquisar";
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(12, 39);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(63, 13);
            this.lblData.TabIndex = 50;
            this.lblData.Text = "Data Inicial:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(434, 38);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(42, 13);
            this.lblUsuario.TabIndex = 59;
            this.lblUsuario.Text = "Cliente:";
            // 
            // cbxCliente
            // 
            this.cbxCliente.FormattingEnabled = true;
            this.cbxCliente.Location = new System.Drawing.Point(484, 35);
            this.cbxCliente.Name = "cbxCliente";
            this.cbxCliente.Size = new System.Drawing.Size(178, 21);
            this.cbxCliente.TabIndex = 58;
            // 
            // DgvRelVenda
            // 
            this.DgvRelVenda.AllowUserToAddRows = false;
            this.DgvRelVenda.AllowUserToDeleteRows = false;
            this.DgvRelVenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvRelVenda.Location = new System.Drawing.Point(39, 82);
            this.DgvRelVenda.Name = "DgvRelVenda";
            this.DgvRelVenda.ReadOnly = true;
            this.DgvRelVenda.Size = new System.Drawing.Size(729, 345);
            this.DgvRelVenda.TabIndex = 60;
            // 
            // mskTxtDataIni
            // 
            this.mskTxtDataIni.Location = new System.Drawing.Point(75, 36);
            this.mskTxtDataIni.Mask = "00/00/0000";
            this.mskTxtDataIni.Name = "mskTxtDataIni";
            this.mskTxtDataIni.Size = new System.Drawing.Size(122, 20);
            this.mskTxtDataIni.TabIndex = 61;
            this.mskTxtDataIni.ValidatingType = typeof(System.DateTime);
            // 
            // txtValorTotal
            // 
            this.txtValorTotal.Enabled = false;
            this.txtValorTotal.Location = new System.Drawing.Point(668, 437);
            this.txtValorTotal.Name = "txtValorTotal";
            this.txtValorTotal.Size = new System.Drawing.Size(100, 20);
            this.txtValorTotal.TabIndex = 63;
            // 
            // lblValorTotal
            // 
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorTotal.Location = new System.Drawing.Point(515, 440);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(147, 13);
            this.lblValorTotal.TabIndex = 62;
            this.lblValorTotal.Text = "Valor Total Vendido(R$):";
            // 
            // mskTxtDataFim
            // 
            this.mskTxtDataFim.Location = new System.Drawing.Point(285, 36);
            this.mskTxtDataFim.Mask = "00/00/0000";
            this.mskTxtDataFim.Name = "mskTxtDataFim";
            this.mskTxtDataFim.Size = new System.Drawing.Size(122, 20);
            this.mskTxtDataFim.TabIndex = 65;
            this.mskTxtDataFim.ValidatingType = typeof(System.DateTime);
            // 
            // lblDataFim
            // 
            this.lblDataFim.AutoSize = true;
            this.lblDataFim.Location = new System.Drawing.Point(221, 39);
            this.lblDataFim.Name = "lblDataFim";
            this.lblDataFim.Size = new System.Drawing.Size(58, 13);
            this.lblDataFim.TabIndex = 64;
            this.lblDataFim.Text = "Data Final:";
            // 
            // FrmRelatorioVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 469);
            this.Controls.Add(this.mskTxtDataFim);
            this.Controls.Add(this.lblDataFim);
            this.Controls.Add(this.txtValorTotal);
            this.Controls.Add(this.lblValorTotal);
            this.Controls.Add(this.mskTxtDataIni);
            this.Controls.Add(this.DgvRelVenda);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.cbxCliente);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.lblData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRelatorioVenda";
            this.Text = "Relatorio de Venda";
            ((System.ComponentModel.ISupportInitialize)(this.DgvRelVenda)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox cbxCliente;
        private System.Windows.Forms.DataGridView DgvRelVenda;
        private System.Windows.Forms.MaskedTextBox mskTxtDataIni;
        private System.Windows.Forms.TextBox txtValorTotal;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.MaskedTextBox mskTxtDataFim;
        private System.Windows.Forms.Label lblDataFim;
    }
}
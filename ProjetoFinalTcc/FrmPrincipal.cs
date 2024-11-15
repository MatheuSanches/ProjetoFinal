using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjetoFinal
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }
        public void CarregaDgvPriPedido()
        {
            SqlConnection con = Conexao.obterConexao();
            String query = "select * from cartaovenda";

            SqlCommand cmd = new SqlCommand(query, con);
            Conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable cartao = new DataTable();
            da.Fill(cartao);
            DgvPriPedido.DataSource = cartao;

            DataGridViewButtonColumn fechar = new DataGridViewButtonColumn();
            fechar.Name = "FecharConta";
            fechar.HeaderText = "FEchar Conta";
            fechar.Text = "Fechar Conta";
            fechar.UseColumnTextForButtonValue = true;
            int columnIndex = 4;
            DgvPriPedido.Columns.Insert(columnIndex, fechar);
            Conexao.fecharConexao();

            DgvPriPedido.CellClick += DgvPriPedido_CellClick;
            int colunas = DgvPriPedido.Columns.Count;
            if(colunas > 5)
            {
                DgvPriPedido.Columns.Remove("FecharConta");
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrudCliente cli = new FrmCrudCliente();
            cli.Show();
        }

        private void testarBancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                String query = "select * from cliente";
                SqlCommand cmd = new SqlCommand(query, con);
                Conexao.obterConexao();
                DataSet ds = new DataSet();
                MessageBox.Show("Conectado ao Banco de Dados com Sucesso!", "Teste de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Information) ;
                Conexao.fecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrudProduto pro = new FrmCrudProduto();
            pro.Show();
        }

        private void vendasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmVenda ven = new FrmVenda();
            ven.Show();
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrudUsuario usu = new FrmCrudUsuario();
            usu.Show();
        }

        private void cartãoVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrudCartaoVenda card = new FrmCrudCartaoVenda();
            card.Show();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            CarregaDgvPriPedido();
        }

        private void DgvPriPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == DgvPriPedido.Columns["FecharConta"].Index)
                {
                    if (Application.OpenForms["FrmVenda"]==null)
                    {
                        FrmVenda ven = new FrmVenda();
                        ven.Show();
                    }
                }
            }
            catch
            { 
            }
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPedido ped = new FrmPedido();
            ped.Show();
        }
    }
}

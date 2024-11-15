using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoFinal
{
    public partial class FrmRelatorioVenda : Form
    {
        SqlConnection con = Conexao.obterConexao();
        public FrmRelatorioVenda()
        {
            InitializeComponent();
            CarregacbxCliente();
        }
        public void CarregacbxCliente()
        {
            string cli = "SELECT cpf, nome FROM cliente";
            SqlCommand cmd = new SqlCommand(cli, con);
            Conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cli, con);
            DataSet ds = new DataSet();
            cbxCliente.Items.Add("");
            da.Fill(ds, "nome");
            cbxCliente.ValueMember = "cpf";
            cbxCliente.DisplayMember = "nome";
            cbxCliente.DataSource = ds.Tables["nome"];
            Conexao.fecharConexao();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                string dataIniConv = mskTxtDataIni.Text;
                DateTime dataIniNova = DateTime.Parse(dataIniConv);//Aqui vou converter para DateTime
                string dataFimConv = mskTxtDataFim.Text;
                DateTime dataFimNova = DateTime.Parse(dataFimConv);
                if (cbxCliente.Text=="") {
                    cmd.CommandText = "LocalizarVendaRelatorio2";   
                }
                else
                {
                    cmd.CommandText = "LocalizarVendaRelatorio";
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@data_ini", dataIniNova);
                cmd.Parameters.AddWithValue("@data_fim", dataFimNova);
                cmd.Parameters.AddWithValue("@nome_cliente", cbxCliente.Text);
                Conexao.obterConexao();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DgvRelVenda.ReadOnly = true;
                    DgvRelVenda.DataSource = ds.Tables[0];
                    Conexao.fecharConexao();
                }
                else
                {
                    MessageBox.Show("Nenhum venda encontrada nessa data!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Conexao.fecharConexao();
                }
                decimal soma = 0;
                foreach (DataGridViewRow dr in DgvRelVenda.Rows)
                {
                    soma += Convert.ToDecimal(dr.Cells[1].Value);
                    txtValorTotal.Text = Convert.ToString(soma);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro" + erro);
            }
        }
    }
}

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
    public partial class FrmCrudCartaoVenda : Form
    {
        SqlConnection con = Conexao.obterConexao();
        public FrmCrudCartaoVenda()
        {
            InitializeComponent();
        }

        private void FrmCrudCartaoVenda_Load(object sender, EventArgs e)
        {
            CarregaDgvCartaoVenda();
            CarregacbxUsuario();
        }
        public void CarregaDgvCartaoVenda()
        {
            SqlConnection con = Conexao.obterConexao();
            String query = "SELECT * FROM cartaovenda";
            SqlCommand cmd = new SqlCommand(query, con);
            Conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable card = new DataTable();
            da.Fill(card);
            DgvCartaoVenda.DataSource = card;
            Conexao.fecharConexao();
        }

        public void CarregacbxUsuario()
        {
            string cli = "SELECT login FROM usuario";
            SqlCommand cmd = new SqlCommand(cli, con);
            Conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cli, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "login");
            cbxUsuario.DisplayMember = "login";
            cbxUsuario.DataSource = ds.Tables["login"];
            Conexao.fecharConexao();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "LocalizarCartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                Conexao.obterConexao();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtId.Text = rd["Id"].ToString();
                    txtNumero.Text = rd["numero"].ToString();
                    Conexao.fecharConexao();
                    CarregacbxUsuario();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado!", "Sem registro!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Conexao.fecharConexao();
                    CarregacbxUsuario();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "InserirCartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@numero", txtNumero.Text.Trim());
                cmd.Parameters.AddWithValue("@usuario", cbxUsuario.Text.Trim());
                Conexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvCartaoVenda();
                FrmPrincipal obj= (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregaDgvPriPedido();
                MessageBox.Show("Cartão de venda cadastrado com sucesso!", "Incluir Cadastro", MessageBoxButtons.OK);
                Conexao.fecharConexao();
                txtId.Text = "";
                txtNumero.Text = "";
                cbxUsuario.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "AtualizarCartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", this.txtId.Text);
                cmd.Parameters.AddWithValue("@numero", this.txtNumero.Text.Trim());
                cmd.Parameters.AddWithValue("@usuario", this.cbxUsuario.Text.Trim());
                Conexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvCartaoVenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregaDgvPriPedido();
                MessageBox.Show("Cartão de venda atualizado com sucesso!", "Atualizar Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Conexao.fecharConexao();
                txtId.Text = "";
                txtNumero.Text = "";
                cbxUsuario.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "ExcluirCartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                Conexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvCartaoVenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregaDgvPriPedido();
                MessageBox.Show("Cartão de venda excluido com sucesso!", "Excluir Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Conexao.fecharConexao();
                txtId.Text = "";
                txtNumero.Text = "";
                cbxUsuario.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DgvCartaoVenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.DgvCartaoVenda.Rows[e.RowIndex];
                txtId.Text = row.Cells[0].Value.ToString();
                txtNumero.Text = row.Cells[1].Value.ToString();
                cbxUsuario.Text = row.Cells[2].Value.ToString();
            }
        }
    }
}

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
    public partial class FrmCrudProduto : Form
    {
        public FrmCrudProduto()
        {
            InitializeComponent();
        }

        public void CarregaDgvProduto()
        {
            SqlConnection con = Conexao.obterConexao();
            String query = "SELECT * FROM produto";
            SqlCommand cmd = new SqlCommand(query, con);
            Conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable produto = new DataTable();
            da.Fill(produto);
            DgvProduto.DataSource = produto;
            Conexao.fecharConexao();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "InserirProduto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nome", txtNome.Text.Trim());
                cmd.Parameters.AddWithValue("@tipo", txtTipo.Text.Trim());
                cmd.Parameters.AddWithValue("@quantidade", txtQuantidade.Text);
                cmd.Parameters.Add("@valor", SqlDbType.Decimal,3).Value = txtValor.Text;
                Conexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvProduto();
                MessageBox.Show("Produto cadastrado com sucesso!", "Incluir Cadastro", MessageBoxButtons.OK);
                Conexao.fecharConexao();
                txtId.Text = "";
                txtNome.Text = "";
                txtTipo.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
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
                cmd.CommandText = "AtualizarProduto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                cmd.Parameters.AddWithValue("@nome", this.txtNome.Text.Trim());
                cmd.Parameters.AddWithValue("@tipo", this.txtTipo.Text.Trim());
                cmd.Parameters.AddWithValue("@quantidade", this.txtQuantidade.Text);
                cmd.Parameters.Add("@valor", SqlDbType.Decimal, 3).Value = txtValor.Text;
                Conexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvProduto();
                MessageBox.Show("Produto atualizado com sucesso!", "Atualizar Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Conexao.fecharConexao();
                txtId.Text = "";
                txtNome.Text = "";
                txtTipo.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
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
                cmd.CommandText = "ExcluirProduto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                Conexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvProduto();
                MessageBox.Show("Produto excluido com sucesso!", "Excluir Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Conexao.fecharConexao();
                txtId.Text = "";
                txtNome.Text = "";
                txtTipo.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "LocalizarProduto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                Conexao.obterConexao();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtId.Text = rd["Id"].ToString();
                    txtNome.Text = rd["nome"].ToString();
                    txtTipo.Text = rd["tipo"].ToString();
                    txtQuantidade.Text = rd["quantidade"].ToString();
                    txtValor.Text = rd["valor"].ToString();
                    Conexao.fecharConexao();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado!", "Sem registro!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Conexao.fecharConexao();
                }
            }
            finally
            {
            }
        }

        private void FrmCrudProduto_Load(object sender, EventArgs e)
        {
            CarregaDgvProduto();
        }

        private void DgvProduto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.DgvProduto.Rows[e.RowIndex];
                txtId.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                txtTipo.Text = row.Cells[2].Value.ToString();
                txtQuantidade.Text = row.Cells[3].Value.ToString();
                txtValor.Text = row.Cells[4].Value.ToString();
            }
        }
    }
}

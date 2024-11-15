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
            CarregacbxUsuario();
        }
        public void CarregaDgvCartaoVenda()
        {
            SqlConnection con = Conexao.obterConexao();
            String query = "select * from cartaovenda";
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
            string cli = "select login from usuario";
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
                cmd.CommandText = "Localizar_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                Conexao.obterConexao();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtId.Text = rd["Id"].ToString();
                    txtNumero.Text = rd["numero"].ToString();
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

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Inserir_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@numero", txtNumero.Text);
                cmd.Parameters.AddWithValue("@usuario", cbxUsuario.Text);
                Conexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvCartaoVenda();
                FrmPrincipal obj= (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregaDgvPriPedido();
                MessageBox.Show("Registro inserido com sucesso!", "Cadastro", MessageBoxButtons.OK);
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
                cmd.CommandText = "Atualizar_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", this.txtId.Text);
                cmd.Parameters.AddWithValue("@numero", this.txtNumero.Text);
                cmd.Parameters.AddWithValue("@usuario", this.cbxUsuario.Text);
                Conexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvCartaoVenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregaDgvPriPedido();
                MessageBox.Show("Registro atualizado com sucesso!", "Atualizar Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                cmd.CommandText = "Excluir_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                Conexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvCartaoVenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregaDgvPriPedido();
                MessageBox.Show("Registro apagado com sucesso!", "Excluir Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}

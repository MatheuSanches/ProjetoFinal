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
    public partial class FrmLogin : Form
    {
        //Aqui inicialiaza a conexão com o banco pela classe "Conexao"
        SqlConnection con = Conexao.obterConexao();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            SqlConnection con = Conexao.obterConexao();
            string usu = "SELECT login,senha FROM usuario WHERE login=@login AND senha=@senha";
            SqlCommand cmd = new SqlCommand(usu, con);
            cmd.Parameters.AddWithValue("@login", SqlDbType.VarChar).Value = txtLogin.Text.Trim();//Aqui e abaixo vai atribuir os valores passado no txt para o campo que selicionamos no WHERE
            cmd.Parameters.AddWithValue("@senha", SqlDbType.VarChar).Value = txtSenha.Text.Trim();
            Conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataReader usuario = cmd.ExecuteReader();
            if (usuario.HasRows)
            {
                this.Hide();
                FrmPrincipal pri = new FrmPrincipal();
                pri.Show();
                Conexao.fecharConexao();
            } 
            else
            {
                MessageBox.Show("Login ou Senha informados incorretos!, /nTente novamente!!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLogin.Text = "";
                txtSenha.Text = "";
                con.Close();
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtLogin.Focus();
        }
    }
}

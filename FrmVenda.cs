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
    public partial class FrmVenda : Form
    {
        SqlConnection con = Conexao.obterConexao();
        public FrmVenda()
        {
            InitializeComponent();
        }

        public FrmVenda(string numero)
        {
            InitializeComponent();
            cbxCartao.Text = Convert.ToString(numero);
        }

        public void CarregacbxCartao()
        {
            string cartao = "SELECT * FROM cartaovenda";
            SqlCommand cmd = new SqlCommand(cartao, con);
            Conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cartao, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "numero");
            cbxCartao.ValueMember = "Id";
            cbxCartao.DisplayMember = "numero";
            cbxCartao.DataSource = ds.Tables["numero"];
            ds.Dispose();
            Conexao.fecharConexao();
        }

        public void CarregacbxCliente()
        {
            string cli = "SELECT cpf, nome FROM cliente";
            SqlCommand cmd = new SqlCommand(cli, con);
            Conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cli, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "nome");
            cbxCliente.ValueMember = "cpf";
            cbxCliente.DisplayMember = "nome";
            cbxCliente.DataSource = ds.Tables["nome"];
            Conexao.fecharConexao();
        }

        public void CarregacbxProduto()
        {
            string pro = "SELECT Id, nome FROM produto";
            SqlCommand cmd = new SqlCommand(pro, con);
            Conexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(pro, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "nome");
            cbxProduto.ValueMember = "Id";
            cbxProduto.DisplayMember = "nome";
            cbxProduto.DataSource = ds.Tables["nome"];
            ds.Dispose();
            Conexao.fecharConexao();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmVenda_Load(object sender, EventArgs e)
        {
            if (cbxCartao.Text == "")
            {
                CarregacbxCartao();
                CarregacbxProduto();
                CarregacbxCliente();
            }
            else
            {
                int num_cartao = Convert.ToInt32(cbxCartao.Text);
                SqlConnection con = Conexao.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "LocalizarVendaGrid";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@numero", num_cartao);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtUsuario.Text = reader[1].ToString();
                    reader.Close();
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgvVenda.DataSource = ds.Tables[0];
                    ds.Dispose();
                    Conexao.fecharConexao();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado!", "Sem registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ds.Dispose();
                    Conexao.fecharConexao();
                    cbxCartao.Text = "";
                    CarregacbxCartao();
                }
                CarregacbxCliente();
                CarregacbxProduto();
                decimal soma = 0;
                foreach (DataGridViewRow dr in dgvVenda.Rows)
                {
                    soma += Convert.ToDecimal(dr.Cells[5].Value);
                    txtValorTotal.Text = Convert.ToString(soma);
                }
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            int num_cartao = Convert.ToInt32(cbxCartao.Text);
            SqlConnection con = Conexao.obterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "LocalizarVendaGrid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@numero", num_cartao);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvVenda.DataSource = ds.Tables[0];
                ds.Dispose();
                Conexao.fecharConexao();
            }
            else
            {
                MessageBox.Show("Nenhum registro encontrado!", "Sem registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvVenda.DataSource = null;
                Conexao.fecharConexao();
                cbxCartao.Text = "";
                CarregacbxCartao();
            }
            decimal soma = 0;
            foreach (DataGridViewRow dr in dgvVenda.Rows)
            {
                soma += Convert.ToDecimal(dr.Cells[5].Value);
                txtValorTotal.Text = Convert.ToString(soma);
            }
            CarregacbxProduto();
        }

        private void cbxProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = Conexao.obterConexao();
            SqlCommand cmd = new SqlCommand("LocalizarProduto", con);
            cmd.Parameters.AddWithValue("@Id", cbxProduto.SelectedValue);
            cmd.CommandType = CommandType.StoredProcedure;
            Conexao.obterConexao();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                txtValor.Text = rd["valor"].ToString();
                txtId.Text = rd["Id"].ToString();
                Conexao.fecharConexao();
            } else
            {
                MessageBox.Show("Nenhum registro encontrado!", "Erro de Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Conexao.fecharConexao();
            }
        }

        private void btnNovoItem_Click(object sender, EventArgs e)
        {
            int num_cartao = Convert.ToInt32(cbxCartao.Text);
            SqlConnection con = Conexao.obterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "LocalizarIDCartao";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@numero", num_cartao);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            Int32 cartao = 0;
            if (reader.Read())
            {
                cartao = reader.GetInt32(0);
                reader.Close();
            }
            SqlCommand pedidos = new SqlCommand("InserirPedidos", con);
            pedidos.CommandType = CommandType.StoredProcedure;
            pedidos.Parameters.AddWithValue("@id_cartaovenda", SqlDbType.Int).Value = cartao;
            pedidos.Parameters.AddWithValue("@id_produto", SqlDbType.Int).Value = cbxProduto.SelectedValue;
            pedidos.Parameters.AddWithValue("@usuario", SqlDbType.VarChar).Value = txtUsuario.Text;
            pedidos.Parameters.AddWithValue("@quantidade", SqlDbType.Int).Value = Convert.ToInt32(txtQuantidade.Text);
            pedidos.Parameters.AddWithValue("@dia_hora", SqlDbType.DateTime).Value = DateTime.Now;
            pedidos.Parameters.AddWithValue("@valor", SqlDbType.Int).Value = Convert.ToDecimal(txtValor.Text);
            pedidos.Parameters.AddWithValue("@total", SqlDbType.Int).Value = Convert.ToDecimal(txtQuantidade.Text) * Convert.ToDecimal(txtValor.Text);
            Conexao.obterConexao();
            pedidos.ExecuteNonQuery();
            Conexao.fecharConexao();
            MessageBox.Show("Produto inserido na venda!", "Atualizar Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandText = "LocalizarVendaGrid";
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@numero", num_cartao);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dgvVenda.DataSource = ds.Tables[0];
                ds.Dispose();
                Conexao.fecharConexao();
            }
            cbxProduto.Text = "";
            txtValor.Text = "";
            txtQuantidade.Text = "";
            //Realizar a soma usando um contador...
            decimal soma = 0;
            foreach (DataGridViewRow dr in dgvVenda.Rows)
                soma += Convert.ToDecimal(dr.Cells[4].Value);
            txtValorTotal.Text = Convert.ToString(soma);
        }

        private void dgvVenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgvVenda.Rows[e.RowIndex];
            cbxProduto.Text = row.Cells[2].Value.ToString();
            txtQuantidade.Text = row.Cells[3].Value.ToString();
            txtValor.Text = row.Cells[4].Value.ToString();
            int linha = dgvVenda.CurrentRow.Index;
        }

        private void btnEditarItem_Click(object sender, EventArgs e)
        {
            int linha = dgvVenda.CurrentRow.Index;
            dgvVenda.Rows[linha].Cells[0].Value = txtId.Text;
            dgvVenda.Rows[linha].Cells[2].Value = cbxProduto.Text;
            dgvVenda.Rows[linha].Cells[3].Value = txtQuantidade.Text;
            dgvVenda.Rows[linha].Cells[4].Value = txtValor.Text;
            dgvVenda.Rows[linha].Cells[5].Value = Convert.ToDecimal(txtValor.Text) * Convert.ToDecimal(txtQuantidade.Text);
            decimal soma = 0;
            foreach (DataGridViewRow dr in dgvVenda.Rows)
                soma += Convert.ToDecimal(dr.Cells[5].Value);
            int num_cartao = Convert.ToInt32(cbxCartao.Text);
            SqlConnection con = Conexao.obterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "LocalizarIDCartao";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@numero", num_cartao);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            Int32 cartao = 0;
            if (reader.Read())
            {
                cartao = reader.GetInt32(0);
                reader.Close();
            }
            foreach (DataGridViewRow lin in dgvVenda.Rows)
            {
                SqlCommand pedidos = new SqlCommand("AtualizarPedidos", con);
                pedidos.CommandType = CommandType.StoredProcedure;
                pedidos.Parameters.AddWithValue("@id_cartaovenda", SqlDbType.Int).Value = cartao;
                pedidos.Parameters.AddWithValue("@id_produto", SqlDbType.Int).Value = Convert.ToInt32(lin.Cells[0].Value);
                pedidos.Parameters.AddWithValue("@usuario", SqlDbType.VarChar).Value = txtUsuario.Text;
                pedidos.Parameters.AddWithValue("@quantidade", SqlDbType.Int).Value = Convert.ToInt32(lin.Cells[3].Value);
                pedidos.Parameters.AddWithValue("@dia_hora", SqlDbType.DateTime).Value = DateTime.Now;
                pedidos.Parameters.AddWithValue("@valor", SqlDbType.Int).Value = Convert.ToDecimal(lin.Cells[4].Value);
                pedidos.Parameters.AddWithValue("@total", SqlDbType.Int).Value = Convert.ToDecimal(lin.Cells[5].Value);
                Conexao.obterConexao();
                pedidos.ExecuteNonQuery();
                Conexao.fecharConexao();
            }
            MessageBox.Show("Venda atualizada!", "Atualizar Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtValorTotal.Text = Convert.ToString(soma);
            cbxProduto.Text = "";
            txtQuantidade.Text = "";
            txtValor.Text = "";
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            int linha = dgvVenda.CurrentRow.Index;
            dgvVenda.Rows.RemoveAt(linha);
            int num_cartao = Convert.ToInt32(cbxCartao.Text);
            SqlConnection con = Conexao.obterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "LocalizarIDCartao";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@numero", num_cartao);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            Int32 cartao = 0;
            if (reader.Read())
            {
                cartao = reader.GetInt32(0);
                reader.Close();
            }
            SqlCommand pedidos = new SqlCommand("ExcluirProdutoPedido", con);
            pedidos.CommandType = CommandType.StoredProcedure;
            pedidos.Parameters.AddWithValue("@idcartao", SqlDbType.Int).Value = cartao;
            pedidos.Parameters.AddWithValue("@idproduto", SqlDbType.Int).Value = Convert.ToInt32(dgvVenda.CurrentRow.Cells[0].Value);
            Conexao.obterConexao();
            pedidos.ExecuteNonQuery();
            Conexao.fecharConexao();
            MessageBox.Show("Produto Apagado!", "Excluir Produto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvVenda.Refresh();
            decimal soma = 0;
            foreach (DataGridViewRow dr in dgvVenda.Rows)
                soma += Convert.ToDecimal(dr.Cells[5].Value);
            txtValorTotal.Text = Convert.ToString(soma);
            cbxProduto.Text = "";
            txtQuantidade.Text = "";
            txtValor.Text = "";
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            SqlConnection con = Conexao.obterConexao();
            SqlCommand cmd = new SqlCommand("InserirVenda", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@valor_pago", SqlDbType.Decimal).Value = Convert.ToDecimal(txtValorTotal.Text);
            cmd.Parameters.AddWithValue("@cartao", SqlDbType.VarChar).Value = cbxCartao.Text;
            cmd.Parameters.AddWithValue("@dia_hora", SqlDbType.VarChar).Value = DateTime.Now;
            cmd.Parameters.AddWithValue("@usuario", SqlDbType.VarChar).Value = txtUsuario.Text;
            cmd.Parameters.AddWithValue("@cliente_nome", SqlDbType.VarChar).Value = cbxCliente.Text;
            cmd.Parameters.AddWithValue("@cliente_cpf", SqlDbType.VarChar).Value = cbxCliente.SelectedValue;
            cmd.ExecuteNonQuery();
            string idvenda = "SELECT IDENT_CURRENT('venda') as id_venda";
            SqlCommand cmdvenda = new SqlCommand(idvenda, con);
            Int32 idvenda2 = Convert.ToInt32(cmdvenda.ExecuteScalar());
            foreach (DataGridViewRow dr in dgvVenda.Rows)
            {
                SqlCommand cmditens = new SqlCommand("InserirItens", con);
                cmditens.CommandType = CommandType.StoredProcedure;
                //aqui começo a subtrair do meu estoque após finalizar o processo
                string ven = "UPDATE produto SET quantidade = (quantidade - @quantidade2) FROM produto WHERE Id = @id_produto2";
                SqlCommand cmditemvenda = new SqlCommand(ven, con);
                cmditemvenda.Parameters.AddWithValue("@quantidade2", SqlDbType.Int).Value = Convert.ToInt32(dr.Cells[3].Value);
                cmditemvenda.Parameters.AddWithValue("@id_produto2", SqlDbType.Int).Value = Convert.ToInt32(dr.Cells[0].Value);
                //termina a atualização do estoque
                cmditens.Parameters.AddWithValue("@quantidade", SqlDbType.Int).Value = Convert.ToInt32(dr.Cells[3].Value);
                cmditens.Parameters.AddWithValue("@id_produto", SqlDbType.Int).Value = Convert.ToInt32(dr.Cells[0].Value);
                cmditens.Parameters.AddWithValue("@id_venda", SqlDbType.Int).Value = idvenda2;
                cmditens.Parameters.AddWithValue("@valor", SqlDbType.Decimal).Value = Convert.ToDecimal(dr.Cells[4].Value);
                cmditens.Parameters.AddWithValue("@valor_total", SqlDbType.Decimal).Value = Convert.ToDecimal(dr.Cells[5].Value);
                Conexao.obterConexao();
                cmditens.ExecuteNonQuery();
                cmditemvenda.ExecuteNonQuery();
                SqlCommand idcartao = con.CreateCommand();
                idcartao.CommandText = "LocalizarIDCartao";
                idcartao.CommandType = CommandType.StoredProcedure;
                int num_cartao = Convert.ToInt32(cbxCartao.Text);
                idcartao.Parameters.AddWithValue("@numero", num_cartao);
                SqlDataReader reader;
                reader = idcartao.ExecuteReader();
                Int32 cartao = 0;
                if (reader.Read())
                {
                    cartao = reader.GetInt32(0);
                    reader.Close();
                }
                SqlCommand clone = new SqlCommand("InserirPedidos2", con);
                clone.CommandType = CommandType.StoredProcedure;
                clone.Parameters.AddWithValue("@id_cartaovenda", SqlDbType.Int).Value = cartao;
                clone.ExecuteNonQuery();
                SqlCommand excluir = new SqlCommand("ExcluirProdutoPedido2", con);
                excluir.CommandType = CommandType.StoredProcedure;
                excluir.Parameters.AddWithValue("@idcartao", SqlDbType.Int).Value = cartao;
                excluir.ExecuteNonQuery();
                //Atualizo o Status do Cartão para false novamente, sinalizando que foi finalizado o processo da venda
                string status = "UPDATE cartaovenda SET status = 'False' WHERE numero = @numero";
                SqlCommand cmdstatus = new SqlCommand(status, con);
                cmdstatus.CommandType = CommandType.Text;
                cmdstatus.Parameters.AddWithValue("@numero", SqlDbType.Int).Value = Convert.ToInt32(cbxCartao.Text);
                cmdstatus.ExecuteNonQuery();
                //Regarrego os grids do form principal
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregaDgvPriPedido();
                obj.CarregaDgvPriVenda();
                Conexao.fecharConexao();
            }
            MessageBox.Show("Venda realizada com sucesso!", "Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cbxProduto.Text = "";
            txtQuantidade.Text = "";
            txtValor.Text = "";
            cbxProduto.Enabled = false;
            txtQuantidade.Enabled = false;
            txtValor.Enabled = false;
            txtValorTotal.Enabled = false;
            btnNovoItem.Enabled = false;
            btnEditarItem.Enabled = false;
            btnExcluirItem.Enabled = false;
            btnFinalizar.Enabled = false;
            dgvVenda.Enabled = false;
            //limpando o DataGridView
            dgvVenda.DataSource = null;
            dgvVenda.Rows.Clear();
            dgvVenda.Refresh();
        }

        private void txtQuantidade_Leave(object sender, EventArgs e)
        {
            SqlConnection con = Conexao.obterConexao();
            SqlCommand cmd = new SqlCommand("LocalizarProduto", con);
            cmd.Parameters.AddWithValue("@Id", cbxProduto.SelectedValue);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = cmd.ExecuteReader();
            int valor1 = 0;
            bool conversaoSucedida = int.TryParse(txtQuantidade.Text, out valor1);
            if (rd.Read())
            {
                int valor2 = Convert.ToInt32(rd["quantidade"].ToString());
                if (valor1 > valor2)
                {
                    MessageBox.Show("Não há quantidade suficiente em estoque!", "Estoque", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Conexao.fecharConexao();
                    txtQuantidade.Text = "";
                    txtQuantidade.Focus();
                }
            }
        }

        private void cbxCartao_SelectedIndexChanged(object sender, EventArgs e)
        {
            int num_cartao = Convert.ToInt32(cbxCartao.Text);
            SqlConnection con = Conexao.obterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "LocalizarUsuarioCartao";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@numero", num_cartao);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtUsuario.Text = reader[2].ToString();
                reader.Close();
                con.Close();
            }
        }
    }
}

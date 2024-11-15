using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoFinal
{
    class Conexao
    {
        //private static string str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Usuario\\Desktop\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=30";
        private static string str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Matheus\\Desktop\\Projeto\\Shop-in-Csharp-Win-Forms-master\\DbLoja.mdf;Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=30\";\\DbLoja.mdf;Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=30";
        private static SqlConnection con = null;
        public static SqlConnection obterConexao()
        {
            con = new SqlConnection(str);
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
            }
            catch (SqlException sqle)
            {
                con = null;
            }
            return con;
        }
        public static void fecharConexao()
        {
            if(con != null)
            {
                con.Close();
            }
        }
    }
}

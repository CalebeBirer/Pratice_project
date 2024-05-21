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
using System.Configuration;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pratice_project
{
    public partial class cadastrar_usuario : Form
    {
        public cadastrar_usuario()
        {
            InitializeComponent();
        }
        public string nome;

        private void button1_Click(object sender, EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection con = new MySqlConnection(conn);
            con.Open();
            string comando = "Select * from morador where nome = @nome";
            MySqlCommand com = new MySqlCommand(comando, con);
            com.Parameters.Add("@nome", MySqlDbType.VarChar).Value = textBox1.Text;

            try
            {
                if (textBox1.Text == "")
                {
                    throw new Exception("Você esqueceu de digitar um nome...");
                }
                MySqlDataReader cs = com.ExecuteReader();
                if (cs.HasRows == false)
                    throw new Exception("Nome não encontrado...");
                else
                {
                    cs.Read();
                    textBox1.Text = Convert.ToString(cs["nome"]);
                    textBox2.Text = Convert.ToString(cs["apartamento"]);
                    textBox3.Text = Convert.ToString(cs["id_morador"]);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void cadastrar_usuario_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection conexao = new MySqlConnection(conn);
            try
            {

                conexao.Open();
                MessageBox.Show("conexao foi criada.");
                MySqlCommand comando = new MySqlCommand();
                comando = conexao.CreateCommand();
                comando.CommandText = "insert into morador (nome, apartamento) values(@vNome, @vApartamento); ";

                comando.Parameters.AddWithValue("vNome", textBox1.Text.Trim());
                comando.Parameters.AddWithValue("vApartamento", textBox2.Text.Trim());

                int valorRetorno = comando.ExecuteNonQuery();
                if (valorRetorno < 1)
                    MessageBox.Show("erro ao inserir");
                else
                    MessageBox.Show("inserido com sucesso");
            }
            catch (MySqlException msqle)
            {
                MessageBox.Show("Erro de acesso ao mysql...." + msqle.Message, "Erro");
            }
            finally
            {
                conexao.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection conexao = new MySqlConnection(conn);
            try
            {
                conexao.Open();
                MessageBox.Show("conexao foi criada.");
                MySqlCommand comando = new MySqlCommand();
                comando = conexao.CreateCommand();
                comando.CommandText = "update morador set nome=@vNome, apartamento=@vApartamento where id_morador = @vId; ";

                comando.Parameters.AddWithValue("vNome", textBox1.Text);
                comando.Parameters.AddWithValue("vApartamento", textBox2.Text.Trim());
                comando.Parameters.AddWithValue("vId", textBox3.Text.Trim());

                int valorRetorno = comando.ExecuteNonQuery();
                if (valorRetorno < 1)
                    MessageBox.Show("erro ao atualizar");
                else
                    MessageBox.Show("atualizado com sucesso");
            }
            catch (MySqlException msqle)
            {
                MessageBox.Show("Erro de acesso ao mysql...." + msqle.Message, "Erro");
            }
            finally
            {
                conexao.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection conexao = new MySqlConnection(conn);
            try
            {
                // int i;
                conexao.Open();
                MessageBox.Show("conexao foi criada.");
                MySqlCommand comando = new MySqlCommand();
                comando = conexao.CreateCommand();
                comando.CommandText = "delete from morador where id_morador =@vId;";
                comando.Parameters.AddWithValue("vId", textBox3.Text.Trim());
                MySqlDataReader reader = comando.ExecuteReader();
                MessageBox.Show("Morador excluido com sucesso");

            }
            // Agora altere a comando Catch
            catch (MySqlException msqle)
            {
                MessageBox.Show("Erro de Acesso ao Mysql..." + msqle.Message, "Erro");
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}

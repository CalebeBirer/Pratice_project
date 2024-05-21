using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pratice_project
{
    public partial class agendamento_salao : Form
    {
        public agendamento_salao()
        {
            InitializeComponent();
        }

        public string nome;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void agendamento_salao_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            using (MySqlConnection conexao = new MySqlConnection(conn))
            {

                try
                {
                    conexao.Open();
                    MessageBox.Show("Conexão foi criada.");

                    // Exibir o valor de textBox2 para depuração
                    DateTime dataEvento = monthCalendar1.SelectionStart;
                    string dataString = dataEvento.ToString("yyyy-MM-dd");

                    MessageBox.Show("Valor de data: " + dataString);                  

                    // Verificar se a data já está cadastrada
                    string selectData = "SELECT * FROM evento WHERE data = @data";
                    using (MySqlCommand comData = new MySqlCommand(selectData, conexao))
                    {
                        comData.Parameters.Add("@data", MySqlDbType.Date).Value = dataEvento;

                        using (MySqlDataReader csData = comData.ExecuteReader())
                        {
                            if (!csData.HasRows)
                            {
                                MessageBox.Show("Nao ha enventos cadastrado nesse dia");
                                return;
                            }
                            else
                            {
                                csData.Read();
                                textBox1.Text = Convert.ToString(csData["nome_evento"]);
                                textBox2.Text = Convert.ToString(csData["id_morador"]);
                                comboBox1.Text = Convert.ToString(csData["tipo_evento"]);
                                textBox3.Text = Convert.ToString(csData["id_evento"]);
                            }
                        }
                    }
                                                           
                }
                catch (MySqlException msqle)
                {
                    MessageBox.Show("Erro de acesso ao MySQL: " + msqle.Message, "Erro");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            using (MySqlConnection conexao = new MySqlConnection(conn))
            {
                int id_morador = 0;

                try
                {
                    conexao.Open();
                    MessageBox.Show("Conexão foi criada.");

                    // Exibir o valor de textBox2 para depuração
                    string nomeMorador = textBox2.Text.Trim();
                    DateTime dataEvento = monthCalendar1.SelectionStart;
                    string dataString = dataEvento.ToString("yyyy-MM-dd");

                    MessageBox.Show("Valor de data: " + dataString);

                    // Obter o ID do morador
                    string selectMorador = "SELECT id_morador FROM morador WHERE apartamento = @apartamento";
                    using (MySqlCommand comMorador = new MySqlCommand(selectMorador, conexao))
                    {
                        comMorador.Parameters.Add("@apartamento", MySqlDbType.VarChar).Value = nomeMorador;

                        using (MySqlDataReader cs = comMorador.ExecuteReader())
                        {
                            if (!cs.HasRows)
                            {
                                MessageBox.Show("Morador não cadastrado");
                                return;
                            }

                            cs.Read();
                            id_morador = Convert.ToInt32(cs["id_morador"]);
                        }
                    }

                    // Verificar se a data já está cadastrada
                    string selectData = "SELECT data FROM evento WHERE data = @data";
                    using (MySqlCommand comData = new MySqlCommand(selectData, conexao))
                    {
                        comData.Parameters.Add("@data", MySqlDbType.Date).Value = dataEvento;

                        using (MySqlDataReader csData = comData.ExecuteReader())
                        {
                            if (csData.HasRows)
                            {
                                MessageBox.Show("Evento já cadastrado");
                                return;
                            }
                            if (dataEvento < DateTime.Today)
                            {
                                MessageBox.Show("Evento nao pode ser cadastrado pois data inferir ao dia de hoje");
                                return;
                            }
                        }
                    }

                    string tipoEvento = comboBox1.Text.Trim();
                    string nomeEvento = textBox1.Text.Trim();
                    MessageBox.Show("Valores para inserção - ID Morador: " + id_morador + ", Data: " + dataString + ", Tipo de Evento: " + tipoEvento + ", Nome de Evento: " + nomeEvento);

                    // Inserir o evento
                    string insert = "INSERT INTO evento (id_morador, data, tipo_evento, nome_evento) VALUES (@id_morador, @vData, @vTipoEvento, @vNomeEvento)";
                    using (MySqlCommand comando = new MySqlCommand(insert, conexao))
                    {
                        comando.Parameters.Add("@id_morador", MySqlDbType.Int32).Value = id_morador;
                        comando.Parameters.Add("@vData", MySqlDbType.Date).Value = dataEvento;
                        comando.Parameters.Add("@vTipoEvento", MySqlDbType.VarChar).Value = tipoEvento;
                        comando.Parameters.Add("@vNomeEvento", MySqlDbType.VarChar).Value = nomeEvento;

                        int valorRetorno = comando.ExecuteNonQuery();
                        if (valorRetorno < 1)
                        {
                            MessageBox.Show("Erro ao inserir");
                        }
                        else
                        {
                            MessageBox.Show("Inserido com sucesso");
                        }
                    }
                }
                catch (MySqlException msqle)
                {
                    MessageBox.Show("Erro de acesso ao MySQL: " + msqle.Message, "Erro");
                }
                finally
                {
                    conexao.Close();
                }
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
                comando.CommandText = "update evento set data=@vData, tipo_evento=@vtipoEvento, nome_evento=@vnomeEvento where id_evento = @vidEvento";
                      

                comando.Parameters.AddWithValue("vData", monthCalendar1.SelectionStart.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("vtipoEvento", comboBox1.Text.Trim());
                comando.Parameters.AddWithValue("vnomeEvento", textBox1.Text.Trim());
                comando.Parameters.AddWithValue("vidEvento", textBox3.Text.Trim());


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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

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
                comando.CommandText = "delete from evento where id_evento =@vId;";
                comando.Parameters.AddWithValue("vId", textBox3.Text.Trim());
                MySqlDataReader reader = comando.ExecuteReader();
                MessageBox.Show("Evento excluido com sucesso");

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



using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pratice_project
{
    public partial class listaMorador : Form
    {
        public listaMorador()
        {
            InitializeComponent();
        }

        private void listaMorador_Load(object sender, EventArgs e)
        {

        }

        private MySqlDataAdapter da;
        BindingSource bsource = new BindingSource();

        DataSet ds = null;

        string sql;

        private void CarregarDados()
        {
            string conn = "Server=localhost;Database=hopevillage;Uid=root;Pwd=z*mon5xut; Connect Timeout=30;";
            MySqlConnection conexao = new MySqlConnection(conn);
            sql = "Select * from morador";
            da = new MySqlDataAdapter(sql, conexao);
            conexao.Open();
            ds = new DataSet();
            new MySqlCommandBuilder(da);
            da.Fill(ds, "morador");
            bsource.DataSource = ds.Tables["morador"];
            dataGridView1.DataSource = bsource;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CarregarDados();
        }
    }
}

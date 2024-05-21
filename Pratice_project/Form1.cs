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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form cadastro = new cadastrar_usuario();
            cadastro.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form agendar = new agendamento_salao();
            agendar.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form lista = new listarEventos();
            lista.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form lista_morador = new listaMorador();
            lista_morador.Show();
        }
    }
}

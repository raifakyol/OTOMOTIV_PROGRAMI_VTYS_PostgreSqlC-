using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odev
{
    public partial class RandevuForm : Form
    {
        public RandevuForm()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglan = new NpgsqlConnection("server=localHost; port=5432; Database = proje; user ID = postgres; password=0024003");
        private void RandevuForm_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from randevu";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServisRandevuForm yeniSayfa = new ServisRandevuForm();
            this.Hide();
            yeniSayfa.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TestSurusRandevuForm yeniSayfa = new TestSurusRandevuForm();
            this.Hide();
            yeniSayfa.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from randevu";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}

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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PersonelForm yeniSayfa = new PersonelForm();
            this.Hide();
            yeniSayfa.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServisForm yeniSayfa = new ServisForm();
            this.Hide();
            yeniSayfa.ShowDialog();
            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RandevuForm yeniSayfa = new RandevuForm();
            this.Hide();
            yeniSayfa.ShowDialog();
            this.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString();
            label2.Text = DateTime.Now.ToLongTimeString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            AraclarForm yeniSayfa = new AraclarForm();
            this.Hide();
            yeniSayfa.ShowDialog();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StoklarForm yeniSayfa = new StoklarForm();
            this.Hide();
            yeniSayfa.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GelirGiderForm yeniSayfa = new GelirGiderForm();
            this.Hide();
            yeniSayfa.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BayiForm yeniSayfa = new BayiForm();
            this.Hide();
            yeniSayfa.ShowDialog();
            this.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SatislarForm yeniSayfa = new SatislarForm();
            this.Hide();
            yeniSayfa.ShowDialog();
            this.Show();
        }
    }
}

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
    public partial class GelirGiderForm : Form
    {
        public void txtboxTemizle()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tbox = (TextBox)item;
                    tbox.Clear();

                }
            }
        }
        NpgsqlConnection baglan = new NpgsqlConnection("server=localHost; port=5432; Database = proje; user ID = postgres; password=0024003");
        public GelirGiderForm()
        {
            InitializeComponent();
        }

        private void GelirGiderForm_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from gelirgider";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtid.Text))
            {
                baglan.Open();
                string ara = "SELECT * from gelirgider where id=@p1";
                NpgsqlCommand komut4 = new NpgsqlCommand(ara, baglan);
                komut4.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(komut4);
                DataSet dst1 = new DataSet();
                da1.Fill(dst1);
                NpgsqlDataReader oku = komut4.ExecuteReader();
                if (oku.Read())
                {
                    dataGridView1.DataSource = dst1.Tables[0];
                }
                else { MessageBox.Show("Kayıt Bulunamadı Tekrar Deneyiniz...!"); }
                baglan.Close();
                txtboxTemizle();
            }
            else { MessageBox.Show("Lütfen ID Giriniz..!"); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult onaysil = MessageBox.Show("Kayıt Silmek İstediğinizden Emin Misiniz?", "Silme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onaysil == DialogResult.Yes)
            {
                baglan.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("Delete from gelirgider where id=@p1", baglan);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                if (komut2.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    txtboxTemizle();
                    MessageBox.Show("Silme İşlemi Başarılı");
                }
                else { MessageBox.Show("Silme İşlemi Başarısız"); }
                baglan.Close();
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from gelirgider";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtid.Text) & !string.IsNullOrWhiteSpace(txtaylikgelir.Text) & !string.IsNullOrWhiteSpace(txtyillikgelir.Text) & !string.IsNullOrWhiteSpace(txtaylıkgider.Text) & !string.IsNullOrWhiteSpace(txtyıllıkgider.Text) & !string.IsNullOrWhiteSpace(txtaylıknetkar.Text) & !string.IsNullOrWhiteSpace(txtyıllıknetkar.Text))
            {
                baglan.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into gelirgider(id,aylikgelir,yillikgelir,aylikgider,yillikgider,ayliknetkar,yilliknetkar) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglan);
                komut1.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                komut1.Parameters.AddWithValue("@p2", int.Parse(txtaylikgelir.Text));
                komut1.Parameters.AddWithValue("@p3", int.Parse(txtyillikgelir.Text));
                komut1.Parameters.AddWithValue("@p4", int.Parse(txtaylıkgider.Text));
                komut1.Parameters.AddWithValue("@p5", int.Parse(txtyıllıkgider.Text));
                komut1.Parameters.AddWithValue("@p6", int.Parse(txtaylıknetkar.Text));
                komut1.Parameters.AddWithValue("@p7", int.Parse(txtyıllıknetkar.Text));
                if (komut1.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    MessageBox.Show("Gelir/Gider Kaydı Eklendi ..!");
                    txtboxTemizle();
                }
                else { MessageBox.Show("Gelir/Gider Kaydı Ekleme Başarısız ..!"); }
            }
            else { MessageBox.Show("Tüm Alanları Doldurunuz ..!"); }
            baglan.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult onayguncelle = MessageBox.Show("Kayıt Guncellemek İstediğinizden Emin Misiniz?", "Güncelleme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onayguncelle == DialogResult.Yes)
            {
                baglan.Open();
                string kayit2 = "update gelirgider set id=@p1,aylikgelir=@p2,yillikgelir=@p3,aylikgider=@p4,yillikgider=@p5,ayliknetkar=@p6,yilliknetkar=@p7 where id=@p1";
                NpgsqlCommand komut5 = new NpgsqlCommand(kayit2, baglan);
                komut5.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                komut5.Parameters.AddWithValue("@p2", int.Parse(txtaylikgelir.Text));
                komut5.Parameters.AddWithValue("@p3", int.Parse(txtyillikgelir.Text));
                komut5.Parameters.AddWithValue("@p4", int.Parse(txtaylıkgider.Text));
                komut5.Parameters.AddWithValue("@p5", int.Parse(txtyıllıkgider.Text));
                komut5.Parameters.AddWithValue("@p6", int.Parse(txtaylıknetkar.Text));
                komut5.Parameters.AddWithValue("@p7", int.Parse(txtyıllıknetkar.Text));
                if (komut5.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    txtboxTemizle();
                    MessageBox.Show("Gelir/Gider Kaydı Güncellendi ..!");
                }
                else { MessageBox.Show("Günceleme Başarısız"); }
                baglan.Close();
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
        }
    }
}

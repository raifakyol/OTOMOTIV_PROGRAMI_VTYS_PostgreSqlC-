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
    public partial class BayiForm : Form
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
        public BayiForm()
        {
            InitializeComponent();
        }

        private void BayiForm_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from bayi";
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
                string ara = "SELECT * from bayi where id=@p1";
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
                NpgsqlCommand komut2 = new NpgsqlCommand("Delete from bayi where id=@p1", baglan);
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
            string sorgu = "select * from bayi";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtid.Text) & !string.IsNullOrWhiteSpace(txtaracno.Text) & !string.IsNullOrWhiteSpace(txtstokno.Text) & !string.IsNullOrWhiteSpace(txtsatislarno.Text) & !string.IsNullOrWhiteSpace(txtpersonelid.Text) & !string.IsNullOrWhiteSpace(txtrandvno.Text) & !string.IsNullOrWhiteSpace(txtsehir.Text) & !string.IsNullOrWhiteSpace(txtbayibölgeno.Text) & !string.IsNullOrWhiteSpace(txtbayibölgead.Text) & !string.IsNullOrWhiteSpace(txtbayiad.Text))
            {
                baglan.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into bayi(id,arac_no,stok_no,satislar_no,personel_id,randevu_no,sehir,bayi_bolge_no,bayi_bolge_ad,bayi_ad) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", baglan);
                komut1.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                komut1.Parameters.AddWithValue("@p2", int.Parse(txtaracno.Text));
                komut1.Parameters.AddWithValue("@p3", int.Parse(txtstokno.Text));
                komut1.Parameters.AddWithValue("@p4", int.Parse(txtsatislarno.Text));
                komut1.Parameters.AddWithValue("@p5", int.Parse(txtpersonelid.Text));
                komut1.Parameters.AddWithValue("@p6", int.Parse(txtrandvno.Text));
                komut1.Parameters.AddWithValue("@p7", txtsehir.Text);
                komut1.Parameters.AddWithValue("@p8", int.Parse(txtbayibölgeno.Text));
                komut1.Parameters.AddWithValue("@p9", txtbayibölgead.Text);
                komut1.Parameters.AddWithValue("@p10", txtbayiad.Text);
                if (komut1.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    MessageBox.Show("Bayi Kaydı Eklendi ..!");
                    txtboxTemizle();
                }
                else { MessageBox.Show("Bayi Kaydı Ekleme Başarısız ..!"); }
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
                string kayit2 = "update bayi set id=@p1,arac_no=@p2,stok_no=@p3,satislar_no=@p4,personel_id=@p5,randevu_no=@p6,sehir=@p7,bayi_bolge_no=@p8,bayi_bolge_ad=@p9,bayi_ad=@p10 where id=@p1";
                NpgsqlCommand komut5 = new NpgsqlCommand(kayit2, baglan);
                komut5.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
                komut5.Parameters.AddWithValue("@p2", int.Parse(txtaracno.Text));
                komut5.Parameters.AddWithValue("@p3", int.Parse(txtstokno.Text));
                komut5.Parameters.AddWithValue("@p4", int.Parse(txtsatislarno.Text));
                komut5.Parameters.AddWithValue("@p5", int.Parse(txtpersonelid.Text));
                komut5.Parameters.AddWithValue("@p6", int.Parse(txtrandvno.Text));
                komut5.Parameters.AddWithValue("@p7", txtsehir.Text);
                komut5.Parameters.AddWithValue("@p8", int.Parse(txtbayibölgeno.Text));
                komut5.Parameters.AddWithValue("@p9", txtbayibölgead.Text);
                komut5.Parameters.AddWithValue("@p10", txtbayiad.Text);
                if (komut5.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    txtboxTemizle();
                    MessageBox.Show("Bayi Kaydı Güncellendi ..!");
                }
                else { MessageBox.Show("Günceleme Başarısız"); }
                baglan.Close();
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
        }
    }
}

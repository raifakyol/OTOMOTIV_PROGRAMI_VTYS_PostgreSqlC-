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
    public partial class ServisRandevuForm : Form
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
        public ServisRandevuForm()
        {
            InitializeComponent();
        }

        private void ServisRandevuForm_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from servisrandevu";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServisRandevuForm rnd = new ServisRandevuForm();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtrandevuno.Text))
            {
                baglan.Open();
                string ara = "SELECT * from servisrandevu where randevu_no=@p1";
                NpgsqlCommand komut4 = new NpgsqlCommand(ara, baglan);
                komut4.Parameters.AddWithValue("@p1", int.Parse(txtrandevuno.Text));
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
            else { MessageBox.Show("Lütfen Randevu No Giriniz..!"); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult onaysil = MessageBox.Show("Kayıt Silmek İstediğinizden Emin Misiniz?", "Silme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onaysil == DialogResult.Yes)
            {
                baglan.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("Delete from servisrandevu where randevu_no=@p1", baglan);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtrandevuno.Text));
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
            string sorgu = "select * from servisrandevu";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtrandevuno.Text) & !string.IsNullOrWhiteSpace(txtaracno.Text) & !string.IsNullOrWhiteSpace(txtmarkaad.Text) & !string.IsNullOrWhiteSpace(txtmodelad.Text) & !string.IsNullOrWhiteSpace(txtad.Text) & !string.IsNullOrWhiteSpace(txtsoyad.Text) & !string.IsNullOrWhiteSpace(txttelefonno.Text) & !string.IsNullOrWhiteSpace(txtaracplaka.Text) & !string.IsNullOrWhiteSpace(txtrandevutarih.Text))
            {
                baglan.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into servisrandevu(randevu_no,arac_no,marka_ad,model_ad,ad,soyad,telefon_numarasi,arac_plaka,servis_randevu_tarih) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", baglan);
                komut1.Parameters.AddWithValue("@p1", int.Parse(txtrandevuno.Text));
                komut1.Parameters.AddWithValue("@p2", int.Parse(txtaracno.Text));
                komut1.Parameters.AddWithValue("@p3", txtmarkaad.Text);
                komut1.Parameters.AddWithValue("@p4", txtmodelad.Text);
                komut1.Parameters.AddWithValue("@p5", txtad.Text);
                komut1.Parameters.AddWithValue("@p6", txtsoyad.Text);
                komut1.Parameters.AddWithValue("@p7", txttelefonno.Text);
                komut1.Parameters.AddWithValue("@p8", txtaracplaka.Text);
                komut1.Parameters.AddWithValue("@p9", txtrandevutarih.Text);
                if (komut1.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    MessageBox.Show("Servis Randevu Kaydı Eklendi ..!");
                    txtboxTemizle();
                }
                else { MessageBox.Show("Servis Randevu Kaydı Ekleme Başarısız ..!"); }
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
                string kayit2 = "update servisrandevu set randevu_no=@p1,arac_no=@p2,marka_ad=@p3,model_ad=@p4,ad=@p5,soyad=@p6,telefon_numarasi=@p7,arac_plaka=@p8,servis_randevu_tarih=@p9 where randevu_no=@p1";
                NpgsqlCommand komut5 = new NpgsqlCommand(kayit2, baglan);
                komut5.Parameters.AddWithValue("@p1", int.Parse(txtrandevuno.Text));
                komut5.Parameters.AddWithValue("@p2", int.Parse(txtaracno.Text));
                komut5.Parameters.AddWithValue("@p3", txtmarkaad.Text);
                komut5.Parameters.AddWithValue("@p4", txtmodelad.Text);
                komut5.Parameters.AddWithValue("@p5", txtad.Text);
                komut5.Parameters.AddWithValue("@p6", txtsoyad.Text);
                komut5.Parameters.AddWithValue("@p7", txttelefonno.Text);
                komut5.Parameters.AddWithValue("@p8", txtaracplaka.Text);
                komut5.Parameters.AddWithValue("@p9", txtrandevutarih.Text);
                if (komut5.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    txtboxTemizle();
                    MessageBox.Show("Servis Randevu Kaydı Güncellendi ..!");
                }
                else { MessageBox.Show("Günceleme Başarısız"); }
                baglan.Close();
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
        }
    }
}

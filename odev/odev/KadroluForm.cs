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
    public partial class KadroluForm : Form
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
        public KadroluForm()
        {
            InitializeComponent();
        }

        private void KadroluForm_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from kadrolu";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtpersonelid.Text))
            {
                baglan.Open();
                string ara = "SELECT * from kadrolu where personel_id=@p1";
                NpgsqlCommand komut4 = new NpgsqlCommand(ara, baglan);
                komut4.Parameters.AddWithValue("@p1", int.Parse(txtpersonelid.Text));
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
            else { MessageBox.Show("Lütfen Personel ID Giriniz..!"); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KadroluForm prsn = new KadroluForm();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult onaysil = MessageBox.Show("Kayıt Silmek İstediğinizden Emin Misiniz?", "Silme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onaysil == DialogResult.Yes)
            {
                baglan.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("Delete from kadrolu where personel_id=@p1", baglan);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtpersonelid.Text));
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
            string sorgu = "select * from kadrolu";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult onayguncelle = MessageBox.Show("Kayıt Guncellemek İstediğinizden Emin Misiniz?", "Güncelleme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onayguncelle == DialogResult.Yes)
            {
                baglan.Open();
                string kayit2 = "update kadrolu set personel_id=@p1,ad=@p2,soyad=@p3,personel_tip_no=@p4,personel_tip_ad=@p5,ucret=@p6,kadro_no=@p7 where personel_id=@p1";
                NpgsqlCommand komut5 = new NpgsqlCommand(kayit2, baglan);
                komut5.Parameters.AddWithValue("@p1", int.Parse(txtpersonelid.Text));
                komut5.Parameters.AddWithValue("@p2", txtpersonelad.Text);
                komut5.Parameters.AddWithValue("@p3", txtpersonelsoyad.Text);
                komut5.Parameters.AddWithValue("@p4", int.Parse(txtpersoneltipno.Text));
                komut5.Parameters.AddWithValue("@p5", txtpersoneltipad.Text);
                komut5.Parameters.AddWithValue("@p6", txtpersonelucret.Text);
                komut5.Parameters.AddWithValue("@p7", int.Parse(txtkadrono.Text));
                if (komut5.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    txtboxTemizle();
                    MessageBox.Show("Personel Kaydı Güncellendi ..!");
                }
                else { MessageBox.Show("Günceleme Başarısız"); }
                baglan.Close();
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtpersonelid.Text) & !string.IsNullOrWhiteSpace(txtpersonelad.Text) & !string.IsNullOrWhiteSpace(txtpersonelsoyad.Text) & !string.IsNullOrWhiteSpace(txtpersoneltipno.Text) & !string.IsNullOrWhiteSpace(txtpersoneltipad.Text) & !string.IsNullOrWhiteSpace(txtpersonelucret.Text) & !string.IsNullOrWhiteSpace(txtkadrono.Text))
            {
                baglan.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into kadrolu(personel_id,ad,soyad,personel_tip_no,personel_tip_ad,ucret,kadro_no) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglan);
                komut1.Parameters.AddWithValue("@p1", int.Parse(txtpersonelid.Text));
                komut1.Parameters.AddWithValue("@p2", txtpersonelad.Text);
                komut1.Parameters.AddWithValue("@p3", txtpersonelsoyad.Text);
                komut1.Parameters.AddWithValue("@p4", int.Parse(txtpersoneltipno.Text));
                komut1.Parameters.AddWithValue("@p5", txtpersoneltipad.Text);
                komut1.Parameters.AddWithValue("@p6", txtpersonelucret.Text);
                komut1.Parameters.AddWithValue("@p7", int.Parse(txtkadrono.Text));
                if (komut1.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    MessageBox.Show("Personel Kaydı Eklendi ..!");
                    txtboxTemizle();
                }
                else { MessageBox.Show("Personel Kaydı Ekleme Başarısız ..!"); }
            }
            else { MessageBox.Show("Tüm Alanları Doldurunuz ..!"); }
            baglan.Close();
        }
    }
}

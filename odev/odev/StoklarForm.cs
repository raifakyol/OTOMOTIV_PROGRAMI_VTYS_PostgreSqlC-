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
    public partial class StoklarForm : Form
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
        public StoklarForm()
        {
            InitializeComponent();
        }

        private void StoklarForm_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from stok";
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
            if (!string.IsNullOrWhiteSpace(txtstokno.Text))
            {
                baglan.Open();
                string ara = "SELECT * from stok where stok_no=@no";
                NpgsqlCommand komut4 = new NpgsqlCommand(ara, baglan);
                komut4.Parameters.AddWithValue("@no", int.Parse(txtstokno.Text));
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
            else { MessageBox.Show("Lütfen Stok No Giriniz..!"); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from stok";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult onaysil = MessageBox.Show("Kayıt Silmek İstediğinizden Emin Misiniz?", "Silme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onaysil == DialogResult.Yes)
            {
                baglan.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("Delete from stok where stok_no=@p1", baglan);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtstokno.Text));
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtstokno.Text) & !string.IsNullOrWhiteSpace(txtaracno.Text) & !string.IsNullOrWhiteSpace(txtaracturad.Text) & !string.IsNullOrWhiteSpace(txtmarkaad.Text) & !string.IsNullOrWhiteSpace(txtmodelad.Text) & !string.IsNullOrWhiteSpace(txtvitestipi.Text) & !string.IsNullOrWhiteSpace(txtmodelyılı.Text) & !string.IsNullOrWhiteSpace(txtstokmiktarı.Text) & !string.IsNullOrWhiteSpace(txtaracfiyat.Text) & !string.IsNullOrWhiteSpace(txtstokgnctarihi.Text))
            {
                baglan.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into stok(stok_no,arac_no,arac_tur_no,marka_ad,model_ad,vites_tipi,model_yili,stok_miktari,arac_fiyat,stok_guncelleme_tarihi) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", baglan);
                komut1.Parameters.AddWithValue("@p1", int.Parse(txtstokno.Text));
                komut1.Parameters.AddWithValue("@p2", int.Parse(txtaracno.Text));
                komut1.Parameters.AddWithValue("@p3", txtaracturad.Text);
                komut1.Parameters.AddWithValue("@p4", txtmarkaad.Text);
                komut1.Parameters.AddWithValue("@p5", txtmodelad.Text);
                komut1.Parameters.AddWithValue("@p6", txtvitestipi.Text);
                komut1.Parameters.AddWithValue("@p7", txtmodelyılı.Text);
                komut1.Parameters.AddWithValue("@p8", int.Parse(txtstokmiktarı.Text));
                komut1.Parameters.AddWithValue("@p9", txtaracfiyat.Text);
                komut1.Parameters.AddWithValue("@p10", txtstokgnctarihi.Text);
                if (komut1.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    MessageBox.Show("Stok Kaydı Eklendi ..!");
                    txtboxTemizle();
                }
                else { MessageBox.Show("Stok Kaydı Ekleme Başarısız ..!"); }
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
                string kayit2 = "update stok set stok_no=@p1,arac_no=@p2,arac_tur_no=@p3,marka_ad=@p4,model_ad=@p5,vites_tipi=@p6,model_yili=@p7,stok_miktari=@p8,arac_fiyat=@p9,stok_guncelleme_tarihi=@p10 where stok_no=@p1";
                NpgsqlCommand komut5 = new NpgsqlCommand(kayit2, baglan);
                komut5.Parameters.AddWithValue("@p1", int.Parse(txtstokno.Text));
                komut5.Parameters.AddWithValue("@p2", int.Parse(txtaracno.Text));
                komut5.Parameters.AddWithValue("@p3", txtaracturad.Text);
                komut5.Parameters.AddWithValue("@p4", txtmarkaad.Text);
                komut5.Parameters.AddWithValue("@p5", txtmodelad.Text);
                komut5.Parameters.AddWithValue("@p6", txtvitestipi.Text);
                komut5.Parameters.AddWithValue("@p7", txtmodelyılı.Text);
                komut5.Parameters.AddWithValue("@p8", int.Parse(txtstokmiktarı.Text));
                komut5.Parameters.AddWithValue("@p9", txtaracfiyat.Text);
                komut5.Parameters.AddWithValue("@p10", txtstokgnctarihi.Text);
                if (komut5.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    txtboxTemizle();
                    MessageBox.Show("Stok Kaydı Güncellendi ..!");
                }
                else { MessageBox.Show("Günceleme Başarısız"); }
                baglan.Close();
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
        }
    }
}
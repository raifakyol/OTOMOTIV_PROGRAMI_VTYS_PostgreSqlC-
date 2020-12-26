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
    public partial class AraclarForm : Form
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
        public AraclarForm()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglan = new NpgsqlConnection("server=localHost; port=5432; Database = proje; user ID = postgres; password=0024003");

        private void AraclarForm_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from arac";
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

        private void button7_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from arac";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtaracno.Text) & !string.IsNullOrWhiteSpace(txtaracturno.Text) & !string.IsNullOrWhiteSpace(txtyakitno.Text) & !string.IsNullOrWhiteSpace(txtmarkano.Text) & !string.IsNullOrWhiteSpace(txtmodelno.Text) & !string.IsNullOrWhiteSpace(txtmodelad.Text) & !string.IsNullOrWhiteSpace(txtkoltuksayisi.Text) & !string.IsNullOrWhiteSpace(txtagirlik.Text) & !string.IsNullOrWhiteSpace(txtvitestipi.Text) & !string.IsNullOrWhiteSpace(txtrenk.Text) & !string.IsNullOrWhiteSpace(txturetimyeri.Text) & !string.IsNullOrWhiteSpace(txtaracfiyat.Text) & !string.IsNullOrWhiteSpace(txturetimtarihi.Text) & !string.IsNullOrWhiteSpace(txtgumruktarihi.Text) & !string.IsNullOrWhiteSpace(txtmodelyılı2.Text))
            {
                baglan.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into arac(arac_no,arac_tur_no,yakit_no,marka_no,model_no,model_ad,koltuk_sayisi,agirlik,vites_tipi,renk,uretim_yeri,arac_fiyat,uretim_tarihi,gumruk_giris_tarihi,model_yil) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15)", baglan);
                komut1.Parameters.AddWithValue("@p1", int.Parse(txtaracno.Text));
                komut1.Parameters.AddWithValue("@p2", int.Parse(txtaracturno.Text));
                komut1.Parameters.AddWithValue("@p3", int.Parse(txtyakitno.Text));
                komut1.Parameters.AddWithValue("@p4", int.Parse(txtmarkano.Text));
                komut1.Parameters.AddWithValue("@p5", int.Parse(txtmodelno.Text));
                komut1.Parameters.AddWithValue("@p6", txtmodelad.Text);
                komut1.Parameters.AddWithValue("@p7", txtkoltuksayisi.Text);
                komut1.Parameters.AddWithValue("@p8", int.Parse(txtagirlik.Text));
                komut1.Parameters.AddWithValue("@p9", txtvitestipi.Text);
                komut1.Parameters.AddWithValue("@p10", txtrenk.Text);
                komut1.Parameters.AddWithValue("@p11", txturetimyeri.Text);
                komut1.Parameters.AddWithValue("@p12", txtaracfiyat.Text);
                komut1.Parameters.AddWithValue("@p13", txturetimtarihi.Text);
                komut1.Parameters.AddWithValue("@p14", txtgumruktarihi.Text);
                komut1.Parameters.AddWithValue("@p15", int.Parse(txtmodelyılı2.Text));
                if (komut1.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    MessageBox.Show("Arac Kaydı Eklendi ..!");
                    txtboxTemizle();
                }
                else { MessageBox.Show("Arac Kaydı Ekleme Başarısız ..!"); }
            }
            else { MessageBox.Show("Tüm Alanları Doldurunuz ..!"); }
            baglan.Close();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtaracno.Text))
            {
                baglan.Open();
                string ara = "SELECT * from arac where arac_no=@no";
                NpgsqlCommand komut4 = new NpgsqlCommand(ara, baglan);
                komut4.Parameters.AddWithValue("@no", int.Parse(txtaracno.Text));
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
            else { MessageBox.Show("Lütfen Arac No Giriniz..!"); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult onayguncelle = MessageBox.Show("Kayıt Guncellemek İstediğinizden Emin Misiniz?", "Güncelleme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onayguncelle == DialogResult.Yes)
            {
                baglan.Open();
                string kayit2 = "update arac set arac_no=@p1,arac_tur_no=@p2,yakit_no=@p3,marka_no=@p4,model_no=@p5,model_ad=@p6,koltuk_sayisi=@p7,agirlik=@p8,vites_tipi=@p9,renk=@p10,uretim_yeri=@p11,arac_fiyat=@p12,uretim_tarihi=@p13,gumruk_giris_tarihi=@p14,model_yil=@p15 where arac_no=@p1";
                NpgsqlCommand komut5 = new NpgsqlCommand(kayit2, baglan);
                komut5.Parameters.AddWithValue("@p1", int.Parse(txtaracno.Text));
                komut5.Parameters.AddWithValue("@p2", int.Parse(txtaracturno.Text));
                komut5.Parameters.AddWithValue("@p3", int.Parse(txtyakitno.Text));
                komut5.Parameters.AddWithValue("@p4", int.Parse(txtmarkano.Text));
                komut5.Parameters.AddWithValue("@p5", int.Parse(txtmodelno.Text));
                komut5.Parameters.AddWithValue("@p6", txtmodelad.Text);
                komut5.Parameters.AddWithValue("@p7", txtkoltuksayisi.Text);
                komut5.Parameters.AddWithValue("@p8", int.Parse(txtagirlik.Text));
                komut5.Parameters.AddWithValue("@p9", txtvitestipi.Text);
                komut5.Parameters.AddWithValue("@p10", txtrenk.Text);
                komut5.Parameters.AddWithValue("@p11", txturetimyeri.Text);
                komut5.Parameters.AddWithValue("@p12", txtaracfiyat.Text);
                komut5.Parameters.AddWithValue("@p13", txturetimtarihi.Text);
                komut5.Parameters.AddWithValue("@p14", txtgumruktarihi.Text);
                komut5.Parameters.AddWithValue("@p15", int.Parse(txtmodelyılı2.Text));
                if (komut5.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    txtboxTemizle();
                    MessageBox.Show("Arac Kaydı Güncellendi ..!");
                }
                else { MessageBox.Show("Günceleme Başarısız"); }
                baglan.Close();
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult onaysil = MessageBox.Show("Kayıt Silmek İstediğinizden Emin Misiniz?", "Silme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onaysil == DialogResult.Yes)
            {
                baglan.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("Delete from arac where arac_no=@p1", baglan);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtaracno.Text));
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
    }
}

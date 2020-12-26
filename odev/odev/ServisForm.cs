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
    public partial class ServisForm : Form
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
        public ServisForm()
        {
            InitializeComponent();
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
            string sorgu = "select * from servis";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void ServisForm_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from servis";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtservisno.Text))
            {
                baglan.Open();
                string ara = "SELECT * from servis where servis_no=@no";
                NpgsqlCommand komut4 = new NpgsqlCommand(ara, baglan);
                komut4.Parameters.AddWithValue("@no", int.Parse(txtservisno.Text));
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
            else { MessageBox.Show("Lütfen Servis No Giriniz..!"); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult onaysil = MessageBox.Show("Kaydı Silmek İstediğinizden Emin Misiniz?", "Silme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onaysil == DialogResult.Yes)
            {
                baglan.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("Delete from servis where servis_no=@p1", baglan);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtservisno.Text));
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
            if (!string.IsNullOrWhiteSpace(txtservisno.Text) & !string.IsNullOrWhiteSpace(txtaracno.Text) & !string.IsNullOrWhiteSpace(txtmarkaad.Text) & !string.IsNullOrWhiteSpace(txtaracturad.Text) & !string.IsNullOrWhiteSpace(txtmodel.Text) & !string.IsNullOrWhiteSpace(txtrenk.Text) & !string.IsNullOrWhiteSpace(txtyakitad.Text) & !string.IsNullOrWhiteSpace(txtplaka.Text) & !string.IsNullOrWhiteSpace(txtarackm.Text) & !string.IsNullOrWhiteSpace(txtmodelyılı.Text) & !string.IsNullOrWhiteSpace(txtsucrttipno.Text) & !string.IsNullOrWhiteSpace(txtsucrttipad.Text) & !string.IsNullOrWhiteSpace(txtservisucret.Text) & !string.IsNullOrWhiteSpace(txthasarkaydioranı.Text))
            {
                baglan.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into servis(servis_no,arac_no,marka_ad,arac_tur_ad,model,renk,yakit_ad,plaka,arac_km_bilgisi,model_yili,servis_ucret_tip_no,servis_ucret_tip_ad,servis_ucret,hasar_kaydi_orani) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14)", baglan);
                komut1.Parameters.AddWithValue("@p1", int.Parse(txtservisno.Text));
                komut1.Parameters.AddWithValue("@p2", int.Parse(txtaracno.Text));
                komut1.Parameters.AddWithValue("@p3", txtmarkaad.Text);
                komut1.Parameters.AddWithValue("@p4", txtaracturad.Text);
                komut1.Parameters.AddWithValue("@p5", txtmodel.Text);
                komut1.Parameters.AddWithValue("@p6", txtrenk.Text);
                komut1.Parameters.AddWithValue("@p7", txtyakitad.Text);
                komut1.Parameters.AddWithValue("@p8", txtplaka.Text);
                komut1.Parameters.AddWithValue("@p9", txtarackm.Text);
                komut1.Parameters.AddWithValue("@p10", txtmodelyılı.Text);
                komut1.Parameters.AddWithValue("@p11", int.Parse(txtsucrttipno.Text));
                komut1.Parameters.AddWithValue("@p12", txtsucrttipad.Text);
                komut1.Parameters.AddWithValue("@p13", int.Parse(txtservisucret.Text));
                komut1.Parameters.AddWithValue("@p14", int.Parse(txthasarkaydioranı.Text));
                if (komut1.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    MessageBox.Show("Servis Kaydı Eklendi ..!");
                    txtboxTemizle();
                }
                else { MessageBox.Show("Servis Kaydı Ekleme Başarısız ..!"); }
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
                string kayit2 = "update servis set servis_no=@p1,arac_no=@p2,marka_ad=@p3,arac_tur_ad=@p4,model=@p5,renk=@p6,yakit_ad=@p7,plaka=@p8,arac_km_bilgisi=@p9,model_yili=@p10,servis_ucret_tip_no=@p11,servis_ucret_tip_ad=@p12,servis_ucret=@p13,hasar_kaydi_orani=@p14 where servis_no=@p1";
                NpgsqlCommand komut5 = new NpgsqlCommand(kayit2, baglan);
                komut5.Parameters.AddWithValue("@p1", int.Parse(txtservisno.Text));
                komut5.Parameters.AddWithValue("@p2", int.Parse(txtaracno.Text));
                komut5.Parameters.AddWithValue("@p3", txtmarkaad.Text);
                komut5.Parameters.AddWithValue("@p4", txtaracturad.Text);
                komut5.Parameters.AddWithValue("@p5", txtmodel.Text);
                komut5.Parameters.AddWithValue("@p6", txtrenk.Text);
                komut5.Parameters.AddWithValue("@p7", txtyakitad.Text);
                komut5.Parameters.AddWithValue("@p8", txtplaka.Text);
                komut5.Parameters.AddWithValue("@p9", txtarackm.Text);
                komut5.Parameters.AddWithValue("@p10", txtmodelyılı.Text);
                komut5.Parameters.AddWithValue("@p11", int.Parse(txtsucrttipno.Text));
                komut5.Parameters.AddWithValue("@p12", txtsucrttipad.Text);
                komut5.Parameters.AddWithValue("@p13", int.Parse(txtservisucret.Text));
                komut5.Parameters.AddWithValue("@p14", int.Parse(txthasarkaydioranı.Text));
                if (komut5.ExecuteNonQuery() > 0)
                {
                    baglan.Close();
                    txtboxTemizle();
                    MessageBox.Show("Servis Kaydı Güncellendi ..!");
                }
                else { MessageBox.Show("Günceleme Başarısız"); }
                baglan.Close();
            }
            else { MessageBox.Show("İşlem İptal Edildi"); }
        }
    }
}

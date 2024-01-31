using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HalıSahaOtomasyonu
{
    public partial class BasariliGiris : Form
    {
        // SQL Bağlantısı
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=OtomasyonVeriTabanı;Integrated Security=True");

        // Veri temizleme motodu
        void Temizle()
        {
            TxtId.Text = "";
            TxtAd.Text = "";
            TxtTelNumara.Text = "";
            cmbSaat.Text = "";
            dtTarih.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            TxtSaha.Text = "";
            TxtEkstra.Text = "";
            TxtForma.Text = "";
        }

        public BasariliGiris()
        {
            InitializeComponent();
        }

        // Form ekranını kapatma
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Form ekranı açıldığına veri tabanındaki tabloların getirildiği yer. Hata kontrolünü yaparak çağırdım.
        private void BasariliGiris_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'otomasyonVeriTabanıDataSet.Tablo_Otomasyon' tablosuna veri yükler.
            try
            {
                this.tablo_OtomasyonTableAdapter.Fill(this.otomasyonVeriTabanıDataSet.Tablo_Otomasyon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            // Listele butonuna tıklandığında hata kontrolünü yaparak verileri getiriyor.
            try
            {
                this.tablo_OtomasyonTableAdapter.Fill(this.otomasyonVeriTabanıDataSet.Tablo_Otomasyon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Araçların içerisinin boş olup olmamasını kontrol eder.
        private bool VeriGirisKontrol()
        {
            // TextBox'ların boş olup olmadığını kontrol et
            if (string.IsNullOrEmpty(TxtId.Text) || string.IsNullOrEmpty(TxtAd.Text) || string.IsNullOrEmpty(TxtTelNumara.Text) || string.IsNullOrEmpty(TxtSaha.Text))
            {
                MessageBox.Show("Veri giriş alanı boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // ComboBox'ların boş olup olmadığını kontrol et
            if (cmbSaat.SelectedItem == null || dtTarih.SelectedItem == null)
            {
                MessageBox.Show("Veri giriş alanı boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // RadioButton'ların boş olup olmadığını kontrol et
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Veri giriş alanı boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Tüm kontroller geçildiyse, true döndür
            return true;
        }

        public int Kontrol()
        {
            // Metodun temel amacı SQL de kayıtlı olan veriler ile araçlara yazdığın verilerin aynı mı kontrol eder.
            SqlCommand tekrar = new SqlCommand("SELECT COUNT(*) FROM [Tablo_Otomasyon] WHERE [Id] = @Id AND [Saat] = @Saat AND [Tarih] = @Tarih AND [Saha] = @Saha", baglanti);
            tekrar.Parameters.AddWithValue("@Id", TxtId.Text);
            tekrar.Parameters.AddWithValue("@Saat", cmbSaat.Text);
            tekrar.Parameters.AddWithValue("@Tarih", dtTarih.Text);
            tekrar.Parameters.AddWithValue("@Saha", TxtSaha.Text);
            int count = (int)tekrar.ExecuteScalar();
            return count;
        }

        private void BtnKaydet_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            Kontrol();
            int count = Kontrol();
            baglanti.Close(); // Bağlantıyı kapat

            // Veri giriş kontrolünü yap
            if (!VeriGirisKontrol())
            {
                return;
            }

            // Kaydetme veya güncelleme işlemini burada yapın

            if (count > 0)
            {
                MessageBox.Show("Kayıt yapılamıyor, veritabanında aynı veri zaten var.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Kayıt işlemini burada yapılıyor.
                baglanti.Open(); // Bağlantıyı aç
                SqlCommand komut = new SqlCommand("insert into Tablo_Otomasyon (Id,Ad,TelefonNumarası,Saat,Tarih,Ücret,Saha,Ekstralar) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", baglanti);
                komut.Parameters.AddWithValue("@p1", TxtId.Text);
                komut.Parameters.AddWithValue("@p2", TxtAd.Text);
                komut.Parameters.AddWithValue("@p3", TxtTelNumara.Text);
                komut.Parameters.AddWithValue("@p4", cmbSaat.Text);
                komut.Parameters.AddWithValue("@p5", dtTarih.Text);
                komut.Parameters.AddWithValue("@P6", label8.Text); // Ödeme durumu için oluşturulmuş.
                komut.Parameters.AddWithValue("@p7", TxtSaha.Text);
                komut.Parameters.AddWithValue("@p8", TxtEkstra.Text);

                komut.ExecuteNonQuery(); //Sorguyu çalıştırıyor.
                baglanti.Close(); // Bağlantıyı kapat

                MessageBox.Show("Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label8.Text = "True";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label8.Text = "False";
            }
        }

        // Veri temizleme
        private void BtnTemizle_Click_1(object sender, EventArgs e)
        {
            Temizle();
        }

        // Veri çekme
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilen = dataGridView1.SelectedCells[0].RowIndex;

            //Ekstralar kısmına da buraya mutlaka eklemem gerek !
            TxtId.Text = dataGridView1.Rows[seçilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[seçilen].Cells[1].Value.ToString();
            TxtTelNumara.Text = dataGridView1.Rows[seçilen].Cells[2].Value.ToString();
            cmbSaat.Text = dataGridView1.Rows[seçilen].Cells[3].Value.ToString();
            dtTarih.Text = dataGridView1.Rows[seçilen].Cells[4].Value.ToString();
            label8.Text = dataGridView1.Rows[seçilen].Cells[5].Value.ToString();
            TxtSaha.Text = dataGridView1.Rows[seçilen].Cells[6].Value.ToString();
            TxtEkstra.Text = dataGridView1.Rows[seçilen].Cells[7].Value.ToString();
            //EKSTRALAR.Text = dataGridView1.Rows[seçilen].Cells[7].Value.ToString();
        }

        // Ücret kontrolü
        private void label8_TextChanged(object sender, EventArgs e)
        {
            if  (label8.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if (label8.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }

        // Silme Butonu
        private void BtnSil_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("Delete From Tablo_Otomasyon Where Ad=@k1", baglanti);
            komutsil.Parameters.AddWithValue("@k1",TxtAd.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Sildindi!.","Bilgi!",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Güncelleme Butonu
        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            // Veri giriş kontrolünü yap
            if (!VeriGirisKontrol())
            {
                return;
            }

            baglanti.Open();
            Kontrol();
            int count = Kontrol();

            if (count > 0)
            {
                MessageBox.Show("Güncelleme yapılamıyor, veritabanında aynı veri zaten var.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Güncelleme işlemi burada yapılıyor. Veri çakışması yoksa burası çalışıyor.
                SqlCommand komutGuncelle = new SqlCommand("Update Tablo_Otomasyon Set Ad=@a1,TelefonNumarası=@a2,Saat=@a3,Tarih=@a4,Ücret=@a5,Saha=@a6,Ekstralar=@a8 where Id=@a7", baglanti);
                komutGuncelle.Parameters.AddWithValue("@a7", TxtId.Text);
                komutGuncelle.Parameters.AddWithValue("@a1", TxtAd.Text);
                komutGuncelle.Parameters.AddWithValue("@a2", TxtTelNumara.Text);
                komutGuncelle.Parameters.AddWithValue("@a3", cmbSaat.Text);
                komutGuncelle.Parameters.AddWithValue("@a4", dtTarih.Text);
                komutGuncelle.Parameters.AddWithValue("@a5", label8.Text); // Ödeme durumu için oluşturulmuş.
                komutGuncelle.Parameters.AddWithValue("@a6", TxtSaha.Text);
                komutGuncelle.Parameters.AddWithValue("@a8", TxtEkstra.Text);

                komutGuncelle.ExecuteNonQuery();

                MessageBox.Show("Bilgiler Güncellendi!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();
        }

        // Kiralama ve satın alma (Default olarak halısaha ücretini 450 TL olarak atadım)
        private void BtnHesapla_Click_1(object sender, EventArgs e)
        {
            int forma = 15 * int.Parse(TxtForma.Text);
            int eldiven = 10 * int.Parse(TxtEldiven.Text);
            int krampon = 10 * int.Parse(TxtKrampon.Text);
            int cay = 5 * int.Parse(TxtCay.Text);
            int su = 3 * int.Parse(TxtSu.Text);
            int kola = 12 * int.Parse(TxtKola.Text);

            int total = 450 + (forma + eldiven + krampon + cay + su + kola);

            TxtEkstra.Text = total.ToString();
        }

        //TextBoxlara sadece sayı girişi yapılabilmesini sağlıyor
        //
        static void TxtKontrol(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtForma_KeyPress(object sender, KeyPressEventArgs e)
        {
            TxtKontrol(sender, e);
        }

        private void TxtEldiven_KeyPress(object sender, KeyPressEventArgs e)
        {
            TxtKontrol(sender, e);
        }

        private void TxtKrampon_KeyPress(object sender, KeyPressEventArgs e)
        {
            TxtKontrol(sender, e);
        }

        private void TxtCay_KeyPress(object sender, KeyPressEventArgs e)
        {
            TxtKontrol(sender, e);
        }

        private void TxtSu_KeyPress(object sender, KeyPressEventArgs e)
        {
            TxtKontrol(sender, e);
        }

        private void TxtKola_KeyPress(object sender, KeyPressEventArgs e)
        {
            TxtKontrol(sender, e);
        }

        private void TxtEkstra_KeyPress(object sender, KeyPressEventArgs e)
        {
            TxtKontrol(sender, e);
        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btnİstatistik_Click(object sender, EventArgs e)
        {
            İstatistik istatistik = new İstatistik();
            istatistik.Show();
        }

        private void BtnHavaDurumu_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://weather.com/tr-TR");
        }
        //
    }
}

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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HalıSahaOtomasyonu
{
    public partial class İstatistik : Form
    {
        //SQL Bağlantısı
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=OtomasyonVeriTabanı;Integrated Security=True");
        string connectionString = "Data Source=.;Initial Catalog=OtomasyonVeriTabanı;Integrated Security=True";

        public İstatistik()
        {
            InitializeComponent();
        }

        // İstatistik için SQL sorguları //

        private void İstatistik_Load(object sender, EventArgs e)
        {
            // Id sorgusu yaparak tabloda ne kadar veri olduğunu buluyor.
            string query = "SELECT COUNT(Id) FROM [Tablo_Otomasyon]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                Lblid.Text = count.ToString();
            }

            // Önceden ödenmiş ücretlerin sayısını getiriyor.
            string query1 = "SELECT COUNT(*) FROM [Tablo_Otomasyon] WHERE Ücret = 1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query1, connection);
                connection.Open();
                int count1 = (int)command.ExecuteScalar();
                label4.Text = count1.ToString();
            }

            //En çok tercih edilen saat.
            string query2 = "SELECT Saat, COUNT(*) as Sayi FROM [Tablo_Otomasyon] GROUP BY Saat ORDER BY Sayi DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string enCokTercihEdilenSaat = reader["Saat"].ToString();
                    label8.Text = enCokTercihEdilenSaat;
                }
            }

            //En az tercih edilen saat.
            string query3 = "SELECT Saat, COUNT(*) as Sayi FROM [Tablo_Otomasyon] GROUP BY Saat ORDER BY Sayi ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query3, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string enAzTercihEdilenSaat = reader["Saat"].ToString();
                    label6.Text = enAzTercihEdilenSaat;
                }
            }

            // En çok tercih edilen tarihi bulma
            string query4 = "SELECT Tarih, COUNT(*) as Sayi FROM [Tablo_Otomasyon] GROUP BY Tarih ORDER BY Sayi DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query4, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string enCokTercihEdilenTarih = reader["Tarih"].ToString();
                    label12.Text = enCokTercihEdilenTarih;
                }
            }

            // En az tercih edilen tarihi bulma
            string query5 = "SELECT Tarih, COUNT(*) as Sayi FROM [Tablo_Otomasyon] GROUP BY Tarih ORDER BY Sayi ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query5, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string enAzTercihEdilenTarih = reader["Tarih"].ToString();
                    label11.Text = enAzTercihEdilenTarih;
                }
            }

            // En çok tercih edilen saha bilgisini bulma
            string query6 = "SELECT Saha, COUNT(*) as Sayi FROM [Tablo_Otomasyon] GROUP BY Saha ORDER BY Sayi DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query6, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string enCokTercihEdilenSaha = reader["Saha"].ToString();
                    label16.Text = enCokTercihEdilenSaha;
                }
            }

            // En az tercih edilen saha bilgisini bulma
            string query7 = "SELECT Saha, COUNT(*) as Sayi FROM [Tablo_Otomasyon] GROUP BY Saha ORDER BY Sayi ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query7, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string enAzTercihEdilenSaha = reader["Saha"].ToString();
                    label15.Text = enAzTercihEdilenSaha;
                }
            }
        }

        // Butona tıklandığında combobox daki veriyi alıp SQL deki verilerle kıyaslıyor. Kıyaslama sonucunda bir veya birden çok veri bulursa bu verileri toplayıp label a yazdırıyor.
        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            string secilenTarih = dtTarih.SelectedItem != null ? dtTarih.SelectedItem.ToString() : string.Empty;

            if (!string.IsNullOrEmpty(secilenTarih))
            {
                string query8 = "SELECT Ekstralar FROM [Tablo_Otomasyon] WHERE Tarih = @Tarih";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query8, connection);
                    command.Parameters.AddWithValue("@Tarih", secilenTarih);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    int toplamKazanc = 0;
                    while (reader.Read())
                    {
                        string ekstra = reader["Ekstralar"].ToString();
                        int ekstraDeger;
                        if (int.TryParse(ekstra, out ekstraDeger))
                        {
                            toplamKazanc += ekstraDeger;
                        }
                    }
                    label18.Text = toplamKazanc.ToString();
                }
            }
            else
            {
                // ComboBox'tan bir tarih seçilmediğinde yapılacak işlemler
                MessageBox.Show("Bir Tarih Seçilmedi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Form ekranını kapatma.
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

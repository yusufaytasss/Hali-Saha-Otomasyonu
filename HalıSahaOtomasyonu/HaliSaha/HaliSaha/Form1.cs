using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HalıSahaOtomasyonu.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using HalıSahaOtomasyonu.SQL;

namespace HalıSahaOtomasyonu
{
    public partial class Form1 : Form
    {
        //Veritabanından referans alınan yer
        WindowsFormEntitiesConnectionDB db = new WindowsFormEntitiesConnectionDB(); 
        public Form1()
        {
            InitializeComponent();
        }

        // Uygulamayı sonlandırma.
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static int Id;

        private void btngiris_Click_1(object sender, EventArgs e)
        {
            // SQL deki verileri çekip karşılaştırıyor.
            var Durum = db.GirisTablo.FirstOrDefault(x => x.FirstName == bunifuTextBox1.Text && x.Password == bunifuTextBox2.Text);
            if (Durum != null)
            {
                // Progress bar'ın görünürlüğünü değiştirme
                bunifuProgressBar1.Visible = true;

                // Duraksama.
                Thread thread = new Thread(() =>
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            bunifuProgressBar1.Value = i;
                        });
                        Thread.Sleep(40); // Progress barı 4 saniyede dolduruyor
                    }
                    // yarım saniye sonra giriş başarılı mesajı getirir
                    Thread.Sleep(500);
                    this.Invoke((MethodInvoker)delegate
                    {
                        // Giriş başarılı mesajı.
                        DialogResult result = MessageBox.Show("Giriş Başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (result == DialogResult.OK)
                        {
                            // OK butonuna tıklandığında yapılacak işlemler (BasariliGiris formuna yönlendiriyor.)
                            Id = Durum.Id;
                            BasariliGiris bg = new BasariliGiris();
                            bg.ShowDialog();
                        }
                    });
                });
                thread.Start();
            }
            else
            {
                // Progress bar'ın görünürlüğünü değiştirme
                bunifuProgressBar1.Visible = true;

                // Kod tekrarı //
                Thread thread = new Thread(() =>
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            bunifuProgressBar1.Value = i;
                        });
                        Thread.Sleep(40); // Progress barı 4 saniyede dolduruyor
                    }
                    // yarım saniye sonra giriş başarılı mesajı getirir
                    Thread.Sleep(500);
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("Hatalı Giriş", "Bilgi Uyuşmazlığı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                });
                thread.Start();
            }
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {
            // Şifre girerken yıldız(*) gösterir.
            bunifuTextBox2.PasswordChar = '*';
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Maksimum izin verilen karakter uzunluğu.
            bunifuTextBox1.MaxLength = 15;
        }

        // Şifre görünürlüğü.
        bool isPasswordVisible = false;

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            // Eğer şifre görünürse, onu gizle
            if (isPasswordVisible)
            {
                bunifuTextBox2.PasswordChar = '*';
                isPasswordVisible = false;
            }
            // Eğer şifre gizliyse, onu görünür yap
            else
            {
                bunifuTextBox2.PasswordChar = '\0';
                isPasswordVisible = true;
            }
        }

        // Şifre sıfırlama formuna yönlendirme.
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SifremiUnuttum Su = new SifremiUnuttum();
            Su.ShowDialog();
        }
    }
}

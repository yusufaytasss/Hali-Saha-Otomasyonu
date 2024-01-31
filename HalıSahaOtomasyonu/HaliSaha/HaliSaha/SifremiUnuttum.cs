using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalıSahaOtomasyonu.SQL;

namespace HalıSahaOtomasyonu
{
    public partial class SifremiUnuttum : Form
    {
        public SifremiUnuttum()
        {
            InitializeComponent();
        }

        MailGonder Mg = new MailGonder();

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private void bunifuButton21_Click_1(object sender, EventArgs e)
        {
            Mg.Microsoft(bunifuTextBox1.Text, bunifuTextBox2.Text, bunifuTextBox1.Text);
            MessageBox.Show("Şifre Sıfırlama Talebiniz İletildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {
            // Şifre girerken yıldız(*) gösterir.
            bunifuTextBox2.PasswordChar = '*';
        }
    }
}

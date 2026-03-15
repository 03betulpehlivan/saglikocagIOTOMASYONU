using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SaglikOcagiOtomasyon
{
    public class LoginForm : Form
    {
        TextBox txtKullanici;
        TextBox txtSifre;
        Button btnGiris;

        public LoginForm()
        {
            FormuHazirla();
            KontrolleriOlustur();
        }

        void FormuHazirla()
        {
            this.Text = "Giriş";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 350;
            this.Height = 220;
        }

        void KontrolleriOlustur()
        {
            this.Controls.Add(new Label { Text = "Kullanıcı Adı:", Location = new Point(20, 30), AutoSize = true });
            txtKullanici = new TextBox { Location = new Point(120, 28), Width = 180 };
            this.Controls.Add(txtKullanici);

            this.Controls.Add(new Label { Text = "Şifre:", Location = new Point(20, 70), AutoSize = true });
            txtSifre = new TextBox { Location = new Point(120, 68), Width = 180, PasswordChar = '*' };
            this.Controls.Add(txtSifre);

            btnGiris = new Button { Text = "Giriş Yap", Location = new Point(120, 110), Width = 100 };
            btnGiris.Click += BtnGiris_Click;
            this.Controls.Add(btnGiris);
        }

        void BtnGiris_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(@"
                    SELECT Yetki
                    FROM Kullanici
                    WHERE KullaniciAd=@a AND Sifre=@s", conn);

                cmd.Parameters.AddWithValue("@a", txtKullanici.Text);
                cmd.Parameters.AddWithValue("@s", txtSifre.Text);

                conn.Open();
                object sonuc = cmd.ExecuteScalar();

                if (sonuc == null)
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı");
                    return;
                }

                int yetki = Convert.ToInt32(sonuc);
                this.Hide();
                new Form1(yetki).Show();
            }
        }
    }
}

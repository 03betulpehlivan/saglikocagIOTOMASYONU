using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SaglikOcagiOtomasyon
{
    public class KullaniciForm : Form
    {
        TextBox txtKullanici, txtSifre;
        ComboBox cbYetki;
        Button btnKaydet;

        public KullaniciForm()
        {
            FormuHazirla();
            KontrolleriOlustur();
        }

        void FormuHazirla()
        {
            this.Text = "Yeni Kullanıcı Ekle";
            this.Width = 350;
            this.Height = 260;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        void KontrolleriOlustur()
        {
            this.Controls.Add(new Label { Text = "Kullanıcı Adı:", Location = new Point(20, 30) });
            txtKullanici = new TextBox { Location = new Point(130, 28), Width = 160 };
            this.Controls.Add(txtKullanici);

            this.Controls.Add(new Label { Text = "Şifre:", Location = new Point(20, 70) });
            txtSifre = new TextBox { Location = new Point(130, 68), Width = 160 };
            this.Controls.Add(txtSifre);

            this.Controls.Add(new Label { Text = "Yetki:", Location = new Point(20, 110) });
            cbYetki = new ComboBox
            {
                Location = new Point(130, 108),
                Width = 160,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbYetki.Items.Add("Admin");
            cbYetki.Items.Add("Standart");
            cbYetki.SelectedIndex = 1;
            this.Controls.Add(cbYetki);

            btnKaydet = new Button
            {
                Text = "Kaydet",
                Location = new Point(130, 160),
                Width = 100
            };
            btnKaydet.Click += BtnKaydet_Click;
            this.Controls.Add(btnKaydet);
        }

        void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (txtKullanici.Text == "" || txtSifre.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurun");
                return;
            }

            int yetki = cbYetki.SelectedIndex == 0 ? 1 : 0;

            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Kullanici (KullaniciAd, Sifre, Yetki)
                    VALUES (@k, @s, @y)", conn);

                cmd.Parameters.AddWithValue("@k", txtKullanici.Text);
                cmd.Parameters.AddWithValue("@s", txtSifre.Text);
                cmd.Parameters.AddWithValue("@y", yetki);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Kullanıcı eklendi");
            this.Close();
        }
    }
}

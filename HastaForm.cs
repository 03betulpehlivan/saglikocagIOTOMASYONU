using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SaglikOcagiOtomasyon
{
    public class HastaForm : Form
    {
        TextBox txtDosyaNo, txtAd, txtSoyad, txtTc, txtKurum;
        Button btnKaydet;

        string _dosyaNo;

        public HastaForm(string dosyaNo)
        {
            _dosyaNo = dosyaNo;

            FormuHazirla();
            KontrolleriOlustur();
        }

        void FormuHazirla()
        {
            this.Text = "Yeni Hasta Kaydı";
            this.Width = 350;
            this.Height = 320;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        void KontrolleriOlustur()
        {
            this.Controls.Add(new Label { Text = "Dosya No:", Location = new Point(20, 20) });
            txtDosyaNo = new TextBox
            {
                Location = new Point(120, 18),
                Width = 150,
                ReadOnly = true,
                Text = _dosyaNo
            };
            this.Controls.Add(txtDosyaNo);

            this.Controls.Add(new Label { Text = "Ad:", Location = new Point(20, 60) });
            txtAd = new TextBox { Location = new Point(120, 58), Width = 150 };
            this.Controls.Add(txtAd);

            this.Controls.Add(new Label { Text = "Soyad:", Location = new Point(20, 100) });
            txtSoyad = new TextBox { Location = new Point(120, 98), Width = 150 };
            this.Controls.Add(txtSoyad);

            this.Controls.Add(new Label { Text = "TC Kimlik:", Location = new Point(20, 140) });
            txtTc = new TextBox { Location = new Point(120, 138), Width = 150 };
            this.Controls.Add(txtTc);

            this.Controls.Add(new Label { Text = "Kurum Adı:", Location = new Point(20, 180) });
            txtKurum = new TextBox { Location = new Point(120, 178), Width = 150 };
            this.Controls.Add(txtKurum);

            btnKaydet = new Button
            {
                Text = "Kaydet",
                Location = new Point(120, 220),
                Width = 100
            };
            btnKaydet.Click += BtnKaydet_Click;
            this.Controls.Add(btnKaydet);
        }

        void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "")
            {
                MessageBox.Show("Ad ve Soyad boş olamaz");
                return;
            }

            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Hasta (DosyaNo, Ad, Soyad, TCKIMLIKNO, KurumAdi)
                    VALUES (@d, @a, @s, @tc, @k)", conn);

                cmd.Parameters.AddWithValue("@d", txtDosyaNo.Text);
                cmd.Parameters.AddWithValue("@a", txtAd.Text);
                cmd.Parameters.AddWithValue("@s", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@tc", txtTc.Text);
                cmd.Parameters.AddWithValue("@k", txtKurum.Text); // Kurum bilgisi artık eklendi

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Hasta kaydedildi");
            this.Close();
        }
    }
}

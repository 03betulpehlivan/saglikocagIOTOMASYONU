using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SaglikOcagiOtomasyon
{
    public class DoktorForm : Form
    {
        TextBox txtKod, txtAd, txtSoyad, txtUnvan;
        Button btnKaydet;

        public DoktorForm()
        {
            this.Text = "Doktor Tanıtma";
            this.Width = 350;
            this.Height = 300;
            this.StartPosition = FormStartPosition.CenterParent;

            this.Controls.Add(new Label { Text = "Dr Kodu:", Location = new Point(20, 20) });
            txtKod = new TextBox { Location = new Point(120, 18), Width = 150 };
            this.Controls.Add(txtKod);

            this.Controls.Add(new Label { Text = "Ad:", Location = new Point(20, 60) });
            txtAd = new TextBox { Location = new Point(120, 58), Width = 150 };
            this.Controls.Add(txtAd);

            this.Controls.Add(new Label { Text = "Soyad:", Location = new Point(20, 100) });
            txtSoyad = new TextBox { Location = new Point(120, 98), Width = 150 };
            this.Controls.Add(txtSoyad);

            this.Controls.Add(new Label { Text = "Ünvan:", Location = new Point(20, 140) });
            txtUnvan = new TextBox { Location = new Point(120, 138), Width = 150 };
            this.Controls.Add(txtUnvan);

            btnKaydet = new Button
            {
                Text = "Kaydet",
                Location = new Point(120, 190),
                Width = 100
            };
            btnKaydet.Click += BtnKaydet_Click;
            this.Controls.Add(btnKaydet);

            // 🔹 TAB & FOCUS AYARLARI EN SON
            txtKod.TabIndex = 0;
            txtAd.TabIndex = 1;
            txtSoyad.TabIndex = 2;
            txtUnvan.TabIndex = 3;
            btnKaydet.TabIndex = 4;

            this.Shown += (s, e) => txtKod.Focus();
            txtKod.KeyDown += TxtKod_KeyDown;
        }

        void TxtKod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT Ad,Soyad,Unvan FROM Doktor WHERE DrKodu=@k", conn);
                cmd.Parameters.AddWithValue("@k", txtKod.Text);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtAd.Text = dr["Ad"].ToString();
                    txtSoyad.Text = dr["Soyad"].ToString();
                    txtUnvan.Text = dr["Unvan"].ToString();
                }
                else
                {
                    MessageBox.Show("Kayıt yok, ekleyebilirsiniz");
                }
            }
        }

        void BtnKaydet_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(@"
IF EXISTS (SELECT 1 FROM Doktor WHERE DrKodu=@k)
    UPDATE Doktor SET Ad=@a,Soyad=@s,Unvan=@u WHERE DrKodu=@k
ELSE
    INSERT INTO Doktor (DrKodu,Ad,Soyad,Unvan)
    VALUES (@k,@a,@s,@u)", conn);

                cmd.Parameters.AddWithValue("@k", txtKod.Text);
                cmd.Parameters.AddWithValue("@a", txtAd.Text);
                cmd.Parameters.AddWithValue("@s", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@u", txtUnvan.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Doktor kaydedildi");
        }
    }
}

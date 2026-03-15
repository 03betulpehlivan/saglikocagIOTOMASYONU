using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SaglikOcagiOtomasyon
{
    public class PoliklinikForm : Form
    {
        TextBox txtPoliklinikAd;
        CheckBox chkAktif;
        Button btnKaydet, btnSil;

        public PoliklinikForm()
        {
            FormuHazirla();
            KontrolleriOlustur();
        }

        void FormuHazirla()
        {
            this.Text = "Poliklinik Tanıtma";
            this.Width = 360;
            this.Height = 240;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        void KontrolleriOlustur()
        {
            this.Controls.Add(new Label
            {
                Text = "Poliklinik Adı:",
                Location = new Point(20, 30),
                AutoSize = true
            });

            txtPoliklinikAd = new TextBox
            {
                Location = new Point(140, 28),
                Width = 170
            };
            txtPoliklinikAd.KeyDown += TxtPoliklinikAd_KeyDown;
            this.Controls.Add(txtPoliklinikAd);

            chkAktif = new CheckBox
            {
                Text = "Aktif",
                Location = new Point(140, 65),
                Checked = true
            };
            this.Controls.Add(chkAktif);

            btnKaydet = new Button
            {
                Text = "Kaydet / Güncelle",
                Location = new Point(40, 120),
                Width = 130
            };
            btnKaydet.Click += BtnKaydet_Click;
            this.Controls.Add(btnKaydet);

            btnSil = new Button
            {
                Text = "Sil",
                Location = new Point(190, 120),
                Width = 80
            };
            btnSil.Click += BtnSil_Click;
            this.Controls.Add(btnSil);
        }

        // ENTER ile poliklinik var mı kontrolü
        void TxtPoliklinikAd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT Durum FROM Poliklinik WHERE PoliklinikAd=@ad", conn);
                cmd.Parameters.AddWithValue("@ad", txtPoliklinikAd.Text);

                conn.Open();
                object sonuc = cmd.ExecuteScalar();

                if (sonuc != null)
                {
                    chkAktif.Checked = Convert.ToBoolean(sonuc);
                }
                else
                {
                    DialogResult dr = MessageBox.Show(
                        "Poliklinik bulunamadı. Eklemek ister misiniz?",
                        "Bilgi",
                        MessageBoxButtons.YesNo);

                    if (dr == DialogResult.No)
                        txtPoliklinikAd.Clear();
                }
            }
        }

        // Insert / Update
        void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (txtPoliklinikAd.Text == "")
            {
                MessageBox.Show("Poliklinik adı boş olamaz");
                return;
            }

            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(@"
IF EXISTS (SELECT 1 FROM Poliklinik WHERE PoliklinikAd=@ad)
    UPDATE Poliklinik SET Durum=@d WHERE PoliklinikAd=@ad
ELSE
    INSERT INTO Poliklinik (PoliklinikAd, Durum)
    VALUES (@ad, @d)", conn);

                cmd.Parameters.AddWithValue("@ad", txtPoliklinikAd.Text);
                cmd.Parameters.AddWithValue("@d", chkAktif.Checked);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Kayıt işlemi tamamlandı");
        }

        // Delete
        void BtnSil_Click(object sender, EventArgs e)
        {
            if (txtPoliklinikAd.Text == "") return;

            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Poliklinik WHERE PoliklinikAd=@ad", conn);
                cmd.Parameters.AddWithValue("@ad", txtPoliklinikAd.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Poliklinik silindi");
            txtPoliklinikAd.Clear();
        }
    }
}

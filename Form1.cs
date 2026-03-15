// ======================= Form1.cs =======================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SaglikOcagiOtomasyon
{
    public class Form1 : Form
    {
        int _yetki;
        int _sevkID; // seçilen sevk için işlemler

        TextBox txtDosyaNo, txtSevkTarihi, txtSiraNo, txtKayitSaati, txtToplamTutar;
        ComboBox cbPoliklinik;
        Button btnKaydet, btnYeni, btnTaburcu, btnGecmis;
        Button btnPoliklinik;
        Button btnHastaBul;
        Button btnYazdir;
        DataGridView dgvAktif, dgvTaburcu, dgvRapor, dgvIslemler;
        Button btnDoktor;
        Button btnGunlukRapor, btnIslemEkle, btnIslemSil;
        TextBox txtIslem, txtDrKodu, txtMiktar, txtBirimFiyat;
        Panel pnlTanimlar;


        public Form1(int yetki)
        {
            _yetki = yetki;

            FormuHazirla();
            KontrolleriOlustur();
            PoliklinikleriGetir();

            txtSevkTarihi.Text = DateTime.Now.ToString("dd.MM.yyyy");

            AktifSevkleriListele();
            TaburcuSevkleriListele();
            YetkiyeGoreAyarla();
        }

        void FormuHazirla()
        {
            this.Text = "Sağlık Ocağı Hasta Takip Sistemi";
            this.Width = 1200;
            this.Height = 1000;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

        }

        void YetkiyeGoreAyarla()
        {
            if (_yetki == 0)
            {
                btnTaburcu.Visible = false;
                btnIslemEkle.Visible = false;
                btnIslemSil.Visible = false;
            }
        }

        void KontrolleriOlustur()
        {
            // ===== ÜST TANIM PANELİ =====
            pnlTanimlar = new Panel
            {
                Location = new Point(0, 0),
                Width = this.Width,
                Height = 55,
                BackColor = Color.LightGray
            };
            this.Controls.Add(pnlTanimlar);

            btnPoliklinik = new Button
            {
                Text = "Poliklinik Tanıtma",
                Location = new Point(20, 12),
                Width = 150
            };
            btnPoliklinik.Click += BtnPoliklinik_Click;
            pnlTanimlar.Controls.Add(btnPoliklinik);

            btnDoktor = new Button
            {
                Text = "Doktor Tanıtma",
                Location = new Point(180, 12),
                Width = 150
            };
            btnDoktor.Click += BtnDoktor_Click;
            pnlTanimlar.Controls.Add(btnDoktor);

            int ofs = 60; // +60 kaydırma

            // ===== DOSYA NO =====
            this.Controls.Add(new Label { Text = "Dosya No:", Location = new Point(20, 20 + ofs) });
            txtDosyaNo = new TextBox { Location = new Point(120, 18 + ofs), Width = 150 };
            txtDosyaNo.KeyDown += TxtDosyaNo_KeyDown;
            this.Controls.Add(txtDosyaNo);

            btnHastaBul = new Button
            {
                Text = "Hasta Bul",
                Location = new Point(280, 16 + ofs),
                Width = 100
            };
            btnHastaBul.Click += BtnHastaBul_Click;
            this.Controls.Add(btnHastaBul);

            // ===== SEVK TARİHİ =====
            this.Controls.Add(new Label { Text = "Sevk Tarihi:", Location = new Point(20, 60 + ofs) });
            txtSevkTarihi = new TextBox { Location = new Point(120, 58 + ofs), Width = 150 };
            this.Controls.Add(txtSevkTarihi);

            // ===== POLİKLİNİK =====
            this.Controls.Add(new Label { Text = "Poliklinik:", Location = new Point(20, 100 + ofs) });
            cbPoliklinik = new ComboBox
            {
                Location = new Point(120, 98 + ofs),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbPoliklinik.SelectedIndexChanged += CbPoliklinik_SelectedIndexChanged;
            this.Controls.Add(cbPoliklinik);

            // ===== SIRA NO =====
            this.Controls.Add(new Label { Text = "Sıra No:", Location = new Point(20, 140 + ofs) });
            txtSiraNo = new TextBox { Location = new Point(120, 138 + ofs), Width = 100, ReadOnly = true };
            this.Controls.Add(txtSiraNo);

            // ===== SAAT =====
            this.Controls.Add(new Label { Text = "Kayıt Saati:", Location = new Point(20, 180 + ofs) });
            txtKayitSaati = new TextBox { Location = new Point(120, 178 + ofs), Width = 150, ReadOnly = true };
            this.Controls.Add(txtKayitSaati);

            // ===== İŞLEM DETAY =====
            int yStart = 260 + ofs;


            int x = 20;
            int gap = 10;
            int labelW = 90;
            int boxW = 100;

            this.Controls.Add(new Label { Text = "Yapılan İşlem:", Location = new Point(x, yStart) });
            txtIslem = new TextBox
            {
                Location = new Point(x + labelW, yStart - 2),
                Width = boxW,
                Height = 23,
                Multiline = false
            };

            this.Controls.Add(txtIslem);

            x += labelW + boxW + gap;

            this.Controls.Add(new Label { Text = "Dr Kodu:", Location = new Point(x, yStart) });
            txtDrKodu = new TextBox { Location = new Point(x + 60, yStart - 2), Width = 70 };
            this.Controls.Add(txtDrKodu);
            txtDrKodu.KeyPress += TxtSayi_KeyPress;
            txtDrKodu.BringToFront();


            x += 60 + 70 + gap;

            this.Controls.Add(new Label { Text = "Miktar:", Location = new Point(x, yStart) });
            txtMiktar = new TextBox { Location = new Point(x + 50, yStart - 2), Width = 60, Text = "1" };
            this.Controls.Add(txtMiktar);
            txtMiktar.KeyPress += TxtSayi_KeyPress;
            txtMiktar.BringToFront();


            x += 50 + 60 + gap;

            // ===== Birim Fiyat Label =====
            Label lblBirimFiyat = new Label
            {
                Text = "Birim Fiyat:",
                Location = new Point(x, yStart), // Form üzerindeki konumu
                AutoSize = true
            };
            this.Controls.Add(lblBirimFiyat);

            // ===== Birim Fiyat Panel =====
            Panel pnlBirimFiyat = new Panel
            {
                Location = new Point(x + lblBirimFiyat.Width + 5, yStart - 2), // Label ile aradaki boşluk
                Width = 100,
                Height = 28,
                BorderStyle = BorderStyle.FixedSingle
            };

            // ===== TextBox =====
            txtBirimFiyat = new TextBox
            {
                Location = new Point(3, 3), // Panel içindeki boşluk
                Width = pnlBirimFiyat.Width - 6,
                Height = pnlBirimFiyat.Height - 6,
                BorderStyle = BorderStyle.None, // Panel zaten kenarlık içeriyor
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular),
                TextAlign = HorizontalAlignment.Left
            };

            pnlBirimFiyat.Controls.Add(txtBirimFiyat);

            // Paneli Form’a ekle
            this.Controls.Add(pnlBirimFiyat);





            btnIslemEkle = new Button { Text = "Ekle", Location = new Point(710, yStart - 5), Width = 80 };
            btnIslemEkle.Click += BtnIslemEkle_Click;
            this.Controls.Add(btnIslemEkle);

            btnIslemSil = new Button { Text = "Seç-Sil", Location = new Point(800, yStart - 5), Width = 80 };
            btnIslemSil.Click += BtnIslemSil_Click;
            this.Controls.Add(btnIslemSil);

            this.Controls.Add(new Label { Text = "Toplam Tutar:", Location = new Point(900, yStart) });
            txtToplamTutar = new TextBox { Location = new Point(990, yStart - 2), Width = 80, ReadOnly = true };
            this.Controls.Add(txtToplamTutar);

            // ===== ALT BUTONLAR =====
            btnKaydet = new Button { Text = "Kaydet Sevk", Location = new Point(20, yStart + 40), Width = 100 };
            btnKaydet.Click += BtnKaydet_Click;
            this.Controls.Add(btnKaydet);

            btnYeni = new Button { Text = "Yeni", Location = new Point(140, yStart + 40), Width = 100 };
            btnYeni.Click += BtnYeni_Click;
            this.Controls.Add(btnYeni);

            btnGecmis = new Button { Text = "Hasta Geçmişi", Location = new Point(260, yStart + 40), Width = 140 };
            btnGecmis.Click += BtnGecmis_Click;
            this.Controls.Add(btnGecmis);

            btnYazdir = new Button { Text = "Yazdır", Location = new Point(420, yStart + 40), Width = 100 };
            btnYazdir.Click += BtnYazdir_Click;
            this.Controls.Add(btnYazdir);

            // ===== AKTİF =====
            this.Controls.Add(new Label
            {
                Text = "Aktif Hastalar",
                Location = new Point(20, yStart + 90),
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            });

            dgvAktif = new DataGridView
            {
                Location = new Point(20, yStart + 120),
                Width = 1050,
                Height = 180,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvAktif.SelectionChanged += DgvAktif_SelectionChanged;
            this.Controls.Add(dgvAktif);

            btnTaburcu = new Button
            {
                Text = "Seçili Hastayı Taburcu Et",
                Location = new Point(20, yStart + 310),
                Width = 220
            };
            btnTaburcu.Click += BtnTaburcu_Click;
            this.Controls.Add(btnTaburcu);

            // ===== TABURCU =====
            this.Controls.Add(new Label
            {
                Text = "Taburcu Edilen Hastalar",
                Location = new Point(20, yStart + 350),
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            });

            dgvTaburcu = new DataGridView
            {
                Location = new Point(20, yStart + 380),
                Width = 1050,
                Height = 160,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgvTaburcu);

            // ===== RAPOR =====
            btnGunlukRapor = new Button
            {
                Text = "Günlük Poliklinik Raporu",
                Location = new Point(20, yStart + 560),
                Width = 220
            };
            btnGunlukRapor.Click += BtnGunlukRapor_Click;
            this.Controls.Add(btnGunlukRapor);

            dgvRapor = new DataGridView
            {
                Location = new Point(260, yStart + 550),
                Width = 810,
                Height = 200,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgvRapor);

            dgvIslemler = new DataGridView
            {
                Location = new Point(20, yStart + 90 + 180 + 5),
                Width = 1050,
                Height = 100,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgvIslemler);
        }



        // =================== METOTLAR ===================

        void CbPoliklinik_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPoliklinik.SelectedItem == null) return;
            var secilen = (ComboboxItem)cbPoliklinik.SelectedItem;

            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(@"
                    SELECT ISNULL(MAX(SiraNo),0)
                    FROM Sevk
                    WHERE PoliklinikID=@p
                    AND CAST(SevkTarihi AS DATE)=CAST(GETDATE() AS DATE)
                    AND Taburcu=0", conn);

                cmd.Parameters.AddWithValue("@p", secilen.Value);
                conn.Open();

                int sira = Convert.ToInt32(cmd.ExecuteScalar());
                txtSiraNo.Text = (sira + 1).ToString();
                txtKayitSaati.Text = DateTime.Now.ToString("HH:mm");
            }
        }

        void TxtDosyaNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT Ad,Soyad FROM Hasta WHERE DosyaNo=@d", conn);
                cmd.Parameters.AddWithValue("@d", txtDosyaNo.Text);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (!dr.Read())
                    new HastaForm(txtDosyaNo.Text).ShowDialog();
            }
        }

        void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (cbPoliklinik.SelectedItem == null || txtDosyaNo.Text == "") return;
            var secilen = (ComboboxItem)cbPoliklinik.SelectedItem;

            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Sevk
                    (DosyaNo,SevkTarihi,PoliklinikID,SiraNo,Saat,Taburcu,ToplamTutar)
                    VALUES (@d,@t,@p,@s,@sa,0,0);
                    SELECT SCOPE_IDENTITY();", conn);

                cmd.Parameters.AddWithValue("@d", txtDosyaNo.Text);
                cmd.Parameters.AddWithValue("@t", DateTime.Parse(txtSevkTarihi.Text));
                cmd.Parameters.AddWithValue("@p", secilen.Value);
                cmd.Parameters.AddWithValue("@s", txtSiraNo.Text);
                cmd.Parameters.AddWithValue("@sa", txtKayitSaati.Text);

                conn.Open();
                _sevkID = Convert.ToInt32(cmd.ExecuteScalar());
            }

            AktifSevkleriListele();
            dgvIslemler.DataSource = null;
            txtToplamTutar.Text = "0";
        }


        void BtnYazdir_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Hasta Sevk Bilgileri Yazdırıldı (Simülasyon)",
                "Yazdır",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }




        void BtnTaburcu_Click(object sender, EventArgs e)
        {
            if (dgvAktif.SelectedRows.Count == 0) return;
            string dosyaNo = dgvAktif.SelectedRows[0].Cells["DosyaNo"].Value.ToString();

            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Sevk SET Taburcu=1, CikisSaati=@c WHERE DosyaNo=@d AND Taburcu=0", conn);
                cmd.Parameters.AddWithValue("@d", dosyaNo);
                cmd.Parameters.AddWithValue("@c", DateTime.Now.ToString("HH:mm"));
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            AktifSevkleriListele();
            TaburcuSevkleriListele();
        }

        void BtnHastaBul_Click(object sender, EventArgs e)
        {
            HastaAraForm f = new HastaAraForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                txtDosyaNo.Text = f.SecilenDosyaNo.ToString();
            }
        }



        void BtnYeni_Click(object sender, EventArgs e)
        {
            txtDosyaNo.Clear();
            txtSiraNo.Clear();
            txtKayitSaati.Clear();
            cbPoliklinik.SelectedIndex = -1;

            dgvIslemler.DataSource = null;
            txtIslem.Clear();
            txtDrKodu.Clear();
            txtMiktar.Text = "1";
            txtBirimFiyat.Clear();
            txtToplamTutar.Text = "0";
            _sevkID = 0;
        }

        void BtnGecmis_Click(object sender, EventArgs e)
        {
            if (txtDosyaNo.Text == "")
            {
                MessageBox.Show("Dosya No giriniz");
                return;
            }

            new HastaGecmisForm(txtDosyaNo.Text).ShowDialog();
        }

        void PoliklinikleriGetir()
        {
            cbPoliklinik.Items.Clear();
            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT PoliklinikID, PoliklinikAd FROM Poliklinik WHERE Durum=1", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbPoliklinik.Items.Add(new ComboboxItem
                    {
                        Text = dr["PoliklinikAd"].ToString(),
                        Value = dr["PoliklinikID"]
                    });
                }
            }
        }

        void AktifSevkleriListele() => Listele("WHERE s.Taburcu=0", dgvAktif);
        void TaburcuSevkleriListele() => Listele("WHERE s.Taburcu=1", dgvTaburcu);

        void Listele(string kosul, DataGridView dgv)
        {
            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlDataAdapter da = new SqlDataAdapter($@"
                    SELECT h.DosyaNo,
                           h.Ad+' '+h.Soyad AS Hasta,
                           p.PoliklinikAd,
                           s.SiraNo,
                           s.Saat,
                           s.SevkID
                    FROM Sevk s
                    JOIN Hasta h ON s.DosyaNo=h.DosyaNo
                    JOIN Poliklinik p ON s.PoliklinikID=p.PoliklinikID
                    {kosul}
                    ORDER BY s.SiraNo", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
                dgv.Columns["SevkID"].Visible = false;
            }
        }

        void DgvAktif_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAktif.SelectedRows.Count == 0) return;
            _sevkID = Convert.ToInt32(dgvAktif.SelectedRows[0].Cells["SevkID"].Value);
            IslemListele();
        }

        void BtnGunlukRapor_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT p.PoliklinikAd AS Poliklinik,
                           COUNT(*) AS HastaSayisi,
                           SUM(s.ToplamTutar) AS ToplamGelir
                    FROM Sevk s
                    JOIN Poliklinik p ON s.PoliklinikID=p.PoliklinikID
                    WHERE CAST(s.SevkTarihi AS DATE)=CAST(GETDATE() AS DATE)
                    GROUP BY p.PoliklinikAd", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvRapor.DataSource = dt;
            }
        }

        void BtnIslemEkle_Click(object sender, EventArgs e)
        {
            if (_sevkID == 0)
            {
                MessageBox.Show("Önce sevk kaydı yapın");
                return;
            }

            if (txtIslem.Text == "" || txtDrKodu.Text == "" || txtMiktar.Text == "" || txtBirimFiyat.Text == "")
            {
                MessageBox.Show("Tüm işlem alanlarını doldurun");
                return;
            }

            int miktar = int.Parse(txtMiktar.Text);
            decimal birimFiyat = decimal.Parse(txtBirimFiyat.Text);
            decimal tutar = miktar * birimFiyat;

            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO SevkIslemleri
                    (SevkID,YapilanIslem,DrKodu,Miktar,BirimFiyat,IslemSaati)
                    VALUES (@sid,@i,@d,@m,@b,@s)", conn);

                cmd.Parameters.AddWithValue("@sid", _sevkID);
                cmd.Parameters.AddWithValue("@i", txtIslem.Text);
                cmd.Parameters.AddWithValue("@d", int.Parse(txtDrKodu.Text));
                cmd.Parameters.AddWithValue("@m", miktar);
                cmd.Parameters.AddWithValue("@b", birimFiyat);
                cmd.Parameters.AddWithValue("@s", DateTime.Now.ToString("HH:mm"));

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            IslemListele();
            txtIslem.Clear();
            txtDrKodu.Clear();
            txtMiktar.Text = "1";
            txtBirimFiyat.Clear();
        }


        void BtnPoliklinik_Click(object sender, EventArgs e)
        {
            PoliklinikForm f = new PoliklinikForm();
            f.ShowDialog(this);


            // Poliklinikler güncellenmiş olabilir
            PoliklinikleriGetir();
        }



        void BtnIslemSil_Click(object sender, EventArgs e)
        {
            if (dgvIslemler.SelectedRows.Count == 0) return;
            int islemID = Convert.ToInt32(dgvIslemler.SelectedRows[0].Cells["IslemID"].Value);

            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM SevkIslemleri WHERE IslemID=@i", conn);
                cmd.Parameters.AddWithValue("@i", islemID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            IslemListele();
        }


        void BtnDoktor_Click(object sender, EventArgs e)
        {
            DoktorForm f = new DoktorForm();
            f.ShowDialog(this);

        }


        void TxtSayi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }


        void IslemListele()
        {
            if (_sevkID == 0) return;
            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT IslemID,YapilanIslem,DrKodu,Miktar,BirimFiyat,IslemSaati
                    FROM SevkIslemleri
                    WHERE SevkID=@sid", conn);
                da.SelectCommand.Parameters.AddWithValue("@sid", _sevkID);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvIslemler.DataSource = dt;

                decimal toplam = 0;
                foreach (DataRow row in dt.Rows)
                {
                    toplam += Convert.ToDecimal(row["Miktar"]) * Convert.ToDecimal(row["BirimFiyat"]);
                }

                txtToplamTutar.Text = toplam.ToString();

                using (SqlConnection conn2 = VeriBaglantisi.BaglantiAl())
                {
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Sevk SET ToplamTutar=@t WHERE SevkID=@id", conn2);
                    cmd.Parameters.AddWithValue("@t", toplam);
                    cmd.Parameters.AddWithValue("@id", _sevkID);
                    conn2.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    // Combobox için helper class
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public override string ToString() => Text;
    }
}

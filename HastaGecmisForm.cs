using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SaglikOcagiOtomasyon
{ 
    public class HastaGecmisForm : Form
    {
        string _dosyaNo;
        DataGridView dgv;

        public HastaGecmisForm(string dosyaNo)
        {
            _dosyaNo = dosyaNo;

            this.Text = "Hasta Geçmişi";
            this.Width = 700;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterParent;

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            this.Controls.Add(dgv);
            Listele();
        }

        void Listele()
        {
            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT 
                        s.SevkTarihi,
                        p.PoliklinikAd,
                        s.SiraNo,
                        s.Saat,
                        CASE WHEN s.Taburcu = 1 THEN 'Evet' ELSE 'Hayır' END AS Taburcu
                    FROM Sevk s
                    JOIN Poliklinik p ON s.PoliklinikID = p.PoliklinikID
                    WHERE s.DosyaNo = @d
                    ORDER BY s.SevkTarihi DESC", conn);

                da.SelectCommand.Parameters.AddWithValue("@d", _dosyaNo);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SaglikOcagiOtomasyon
{
    public class HastaAraForm : Form
    {
        public int SecilenDosyaNo { get; private set; }
        DataGridView dgv;

        public HastaAraForm()
        {
            this.Text = "Hasta Arama";
            this.Width = 600;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterParent;

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgv.DoubleClick += Dgv_DoubleClick;
            this.Controls.Add(dgv);

            Listele();
        }

        void Listele()
        {
            using (SqlConnection conn = VeriBaglantisi.BaglantiAl())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT DosyaNo, Ad, Soyad, TCKIMLIKNO FROM Hasta", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }

        void Dgv_DoubleClick(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;

            SecilenDosyaNo = Convert.ToInt32(
                dgv.SelectedRows[0].Cells["DosyaNo"].Value);

            this.DialogResult = DialogResult.OK;
        }
    }
}

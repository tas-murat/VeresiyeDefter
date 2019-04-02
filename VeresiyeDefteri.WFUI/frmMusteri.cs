using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VeresiyeDefteri.BLL.Repository;
using VeresiyeDefteri.BOL.Entities;

namespace VeresiyeDefteri.WFUI
{
    public partial class frmMusteri : Form
    {
        SqlRepository<Musteri> repoMusteri = new SqlRepository<Musteri>();
        IQueryable<Musteri> musteriler;
        int MID = 0;
        public frmMusteri()
        {
            InitializeComponent();
        }

        private void frmMusteri_Load(object sender, EventArgs e)
        {
            yukle();
            bool kontrol = Form1.kontrol;//form1den gelen istek düzenlememi yoksa yeni kayıt eklememi kontrolu...
            if (kontrol)
            {
                btnKaydet.Text = "Güncelle";
                btnMBul.Visible = true;
                btnSil.Visible = true;
            }


        }
        void yukle()
        {
            musteriler = repoMusteri.Listele();
            dataGridView1.DataSource = musteriler.Select(s => new { s.ID, s.Ad, s.Soyad, s.Lakap, s.CepTelefon }).ToList();
            dataGridView1.Columns["ID"].Visible = false;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (btnKaydet.Text == "Kaydet")
            {//kaydetme işlemi
                repoMusteri.Insert(new Musteri
                {
                    Ad = txtAd.Text,
                    Soyad = txtSoyad.Text,
                    Lakap = txtLakap.Text,
                    EvTelefon = txtEvtel.Text,
                    CepTelefon = txtCepTel.Text,
                    Fax = txtFax.Text,
                    Email = txtEmail.Text,
                    TC = txtTcno.Text,
                    Aciklama = txtAciklama.Text,
                    Adres = txtAdres.Text
                });
                MessageBox.Show("Müşteri Bilgileri Kaydedildi");
            }
            else
            {//güncelleme işlemi
                if (MID != 0)
                {
                    if (MessageBox.Show("Bu Kaydı Güncellemek İstediğinize Emin Misiniz?", "Güncelleme Onay İşlemi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Musteri m = repoMusteri.Listele(w => w.ID == MID).First();
                        m.Ad = txtAd.Text;
                        m.Soyad = txtSoyad.Text;
                        m.Lakap = txtLakap.Text;
                        m.EvTelefon = txtEvtel.Text;
                        m.CepTelefon = txtCepTel.Text;
                        m.Fax = txtFax.Text;
                        m.Email = txtEmail.Text;
                        m.TC = txtTcno.Text;
                        m.Aciklama = txtAciklama.Text;
                        m.Adres = txtAdres.Text;
                        repoMusteri.Update(m);
                        MessageBox.Show("Müşteri Bilgileri Güncellendi");
                    }
                }
                else MessageBox.Show("Önce Bir Müşteri Bul");
            }
            MID = 0;
            temizle();
            yukle();
        }

        void temizle()
        {
            foreach (Control control in groupBox1.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textTemizle = (TextBox)control;
                    textTemizle.Text = "";
                }
                else if (control is MaskedTextBox)
                {
                    MaskedTextBox maskeTemizle = (MaskedTextBox)control;
                    maskeTemizle.Text = "";
                }
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (repoMusteri.Listele(w => w.ID == MID).Any())//böyle bir müşteri varsa sil...
            {
                if (MessageBox.Show("Bu Kaydı Silmek İstediğinize Emin Misiniz?", "Silme Onay İşlemi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Musteri m = repoMusteri.Listele(w => w.ID == MID).First();
                    repoMusteri.Delete(m);
                }

            }
            else MessageBox.Show("Önce Bir Müşteri Bul");
            MID = 0;
            temizle();
            yukle();
        }

        private void btnMBul_Click(object sender, EventArgs e)
        {//bulma ekranını gösterme
            panelMusteri.Visible = false;
            pnlBulma.Visible = true;

        }

        private void btnSec_Click(object sender, EventArgs e)
        {//düzenlenecek kayıt seçildiğinde  verileri alma
            MID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
            if (repoMusteri.Listele(w => w.ID == MID).Any())
            {
                Musteri m = repoMusteri.Listele(w => w.ID == MID).First();
                txtAd.Text = m.Ad;
                txtSoyad.Text = m.Soyad;
                txtLakap.Text = m.Lakap;
                txtCepTel.Text = m.CepTelefon;
                txtEvtel.Text = m.EvTelefon;
                txtEmail.Text = m.Email;
                txtFax.Text = m.Fax;
                txtTcno.Text = m.TC;
                txtAciklama.Text = m.Aciklama;
                txtAdres.Text = m.Adres;
            }
            panelMusteri.Visible = true;
            pnlBulma.Visible = false;
        }
        private void txtBAd_TextChanged(object sender, EventArgs e)
        {//ad,soyad ve lakaba göre arama
            dataGridView1.DataSource = musteriler.Where(w => w.Ad.Contains(txtBAd.Text) && w.Soyad.Contains(txtBSoyad.Text) && w.Lakap.Contains(txtBLakap.Text)).Select(s => new { s.ID, s.Ad, s.Soyad, s.Lakap, s.CepTelefon }).ToList();
        }
    }
}

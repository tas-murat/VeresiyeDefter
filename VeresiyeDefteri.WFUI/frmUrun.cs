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
    public partial class frmUrun : Form
    {
        SqlRepository<Urun> repoUrun = new SqlRepository<Urun>();
        IQueryable<Urun> urunler;
        bool kontrol = false;
        int id = 0;
        int selecIndex = 0;
        public frmUrun()
        {
            InitializeComponent();
        }

        private void frmUrun_Load(object sender, EventArgs e)
        {
            yukle();
            kontrol = Form1.Ukontrol;//form1den gelen istek düzenlememi yoksa yeni kayıt eklememi kontrolu...
            if (kontrol)
            {
                btnKaydet.Text = "Güncelle";
                btnSil.Visible = true;
                txtAraBarkod.Visible = true;
                txtaraAd.Visible = true;
                if (repoUrun.Listele().Any())
                {
                    Urun urun = repoUrun.Listele().First();
                    doldur(urun);
                }
            }
            cmbBirim.DataSource = Enum.GetNames(typeof(EBirim));
        }



        void yukle()
        {
            urunler = repoUrun.Listele();
            dataGridView1.DataSource = urunler.ToList();
            dataGridView1.Columns["ID"].Visible = false;
        }

        private void txtAFiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (btnKaydet.Text == "Kaydet")
            {//kaydetme işlemi
                repoUrun.Insert(new Urun
                {
                    Ad = txtAd.Text,
                    Barkod = txtBarkod.Text,
                    Birim = (EBirim)Enum.Parse(typeof(EBirim), cmbBirim.Text),
                    Miktar = Convert.ToInt32(txtMiktar.Text),
                    AFiyat = Convert.ToDecimal(txtAFiyat.Text),
                    SFiyat = Convert.ToDecimal(txtSFiyat.Text),
                    ATarih = Convert.ToDateTime(dtpATarih.Text),
                    SKTarih = Convert.ToDateTime(dtpSKTarih.Text),

                });
                MessageBox.Show("Ürün Bilgileri Kaydedildi");
                temizle();
            }
            {//güncelleme işlemi
                if (id != 0)
                {
                    if (MessageBox.Show("Bu Kaydı Güncellemek İstediğinize Emin Misiniz?", "Güncelleme Onay İşlemi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Urun ur = repoUrun.Listele(w => w.ID == id).First();
                        ur.Ad = txtAd.Text;
                        ur.Barkod = txtBarkod.Text;
                        ur.Miktar = Convert.ToInt32(txtMiktar.Text);
                        ur.Birim = (EBirim)Enum.Parse(typeof(EBirim), cmbBirim.Text);
                        ur.AFiyat = Convert.ToDecimal(txtAFiyat.Text);
                        ur.SFiyat = Convert.ToDecimal(txtSFiyat.Text);
                        ur.ATarih = Convert.ToDateTime(dtpATarih.Text);
                        ur.SKTarih = Convert.ToDateTime(dtpSKTarih.Text);

                        repoUrun.Update(ur);
                        MessageBox.Show("Ürün Bilgileri Güncellendi");
                    }
                    
                }
            }
           
            yukle();
            dataGridView1.SelectedRows[2].Selected = true;
            // dataGridView1.SelectedRows = dataGridView1.CurrentRow.Cells[0].Value;
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

        private void btnSil_Click(object sender, EventArgs e)
        {

        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAraBarkod_TextChanged(object sender, EventArgs e)
        {//arama kısmı
            string ad = txtaraAd.Text,barkod=txtAraBarkod.Text;
            if (txtaraAd.Text == "Adına göre ara")
                ad = "";
            if (txtaraAd.Text == "barkoda göre ara")
                barkod = "";
            dataGridView1.DataSource = urunler.Where(w => w.Ad.Contains(ad) && w.Barkod.Contains(barkod)).ToList();
           
        }
        void doldur(Urun u)
        {
            txtAd.Text = u.Ad;
            txtBarkod.Text = u.Barkod;
            txtMiktar.Text = u.Miktar.ToString();
            dtpATarih.Text = u.ATarih.ToString();
            dtpSKTarih.Text = u.SKTarih.ToString();
            txtAFiyat.Text = u.AFiyat.ToString();
            txtSFiyat.Text = u.SFiyat.ToString();
            cmbBirim.Text = Enum.GetName(typeof(EBirim), u.Birim);

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            selecIndex = dataGridView1.CurrentCell.RowIndex;
            if (kontrol)
            {
                 id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
                if (repoUrun.Listele().Where(w => w.ID == id).Any())
                {
                    Urun u = repoUrun.Listele().Where(w => w.ID == id).First();
                    doldur(u);

                }
            }
        }


        private void txtaraAd_Enter(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;
            if (text.Text == "barkoda göre ara" || text.Text == "Adına göre ara")
                text.Text = "";
          //  if (txtaraAd.Text == "") txtaraAd.Text = "Adına göre ara";
        }
    }
}

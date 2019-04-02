using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeresiyeDefteri.WFUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static bool kontrol { get; set; }
        public static bool Ukontrol { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnMusteriEkle_Click(object sender, EventArgs e)
        {
            frmMusteri frm = new frmMusteri();
            kontrol = false;
            frm.ShowDialog();
        }

        private void btnMusteriDuzenle_Click(object sender, EventArgs e)
        {
            frmMusteri frm = new frmMusteri();
            kontrol = true;
            frm.ShowDialog();
        }

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            Ukontrol = false;
            frmUrun frm = new frmUrun();
            frm.ShowDialog();
        }

        private void btnUDuzenle_Click(object sender, EventArgs e)
        {
            Ukontrol = true;
            frmUrun frm = new frmUrun();
            frm.ShowDialog();
        }
    }
}

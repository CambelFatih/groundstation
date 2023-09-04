using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KazganYerIstasyonu
{
    public partial class GirisForm : Form
    {
        string kullanici = "Kazgan";
        string sifre = "123";
        private Move_Panelcs _move;
        public GirisForm()
        {
            InitializeComponent();
            _move = new Move_Panelcs(this);
            _move.SetMovable(solpanel);
            _move.SetMovable(sagPanel);
            _move.SetMovable(altpanel);
            _move.SetMovable(ustPanel);
            _move.SetMovable(lblAnamenu);
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.StartPosition = FormStartPosition.CenterScreen;
            btnkapat.ForeColor = Color.White;
            lblAnamenu.ForeColor = Color.White;
            ustPanel.BackColor = Color.FromArgb(51, 51, 76);
            sagPanel.BackColor = Color.FromArgb(51, 51, 76);
            altpanel.BackColor = Color.FromArgb(51, 51, 76);
            solpanel.BackColor = Color.FromArgb(51, 51, 76);
            this.BackColor = Color.FromArgb(160, 225, 137);
        }

        private void GirisForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void kaydet_button_Click(object sender, EventArgs e)
        {
            if(string.Compare(kullanici, 0, KullanicitextBox.Text, 0, kullanici.Length) == 0)
            {
                if(string.Compare(sifre, 0, sifretextBox.Text, 0, sifre.Length) == 0)
                {
                    this.Hide();
                    form1 anaform = new form1();
                    anaform.Show();
                }
                else
                {
                    MessageBox.Show("Şifre Yanlış.", "Kazgan Yer İstasyonu Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Kullanıcı Adı Yanlış.", "Kazgan Yer İstasyonu Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnkapat_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

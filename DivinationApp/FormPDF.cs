using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DivinationApp
{
    public partial class FormPDF : Form
    {
        public FormPDF()
        {
            InitializeComponent();

        }

        private void FormPDF_Load(object sender, EventArgs e)
        {
            SetNowYearMonth();

            chkShukumeiAndKoutenun.Checked = true;
            chkKyoki.Checked = true;
            chkTaiun.Checked = true;
            chkNenun.Checked = true;
            chkGetuun.Checked = true;
            chkShugosin.Checked = false;
            chkKonkihou.Checked = false;

            chkDispGetuun.Checked = true;

            chkGogyou.Checked = false;
            chkGotoku.Checked = true;


            chkRefrectSigou.Checked = false;
            chkRefrectHankai.Checked = false;
            chkRefrectKangou.Checked = false;
            chkRefrectHousani.Checked = false;
            chkRefrectSangouKaikyoku.Checked = false;

            chkSangouKaikyoku.Checked = true;
            chkZougan.Checked = false;
            chkJuniSinkanHou.Checked = false;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frmMaqin = FormMain.GetFormMain();
            Person person = frmMaqin.GetActiveFormPerson();

            PDFOutput.Parameter param = new PDFOutput.Parameter();
            param.person = person;
            param.bShukumeiAndKoutenun = chkShukumeiAndKoutenun.Checked;
            param.bKyoki = chkKyoki.Checked;
            param.bTaiunHyou = chkTaiun.Checked;
            param.bNenunHyou = chkNenun.Checked;
            param.bGetuunHyou = chkGetuun.Checked;
            param.bShugosinHou = chkShugosin.Checked;
            param.bKonkiHou = chkKonkihou.Checked;

            param.year = int.Parse(txtYear.Text);
            param.month = int.Parse(txtMonth.Text);

            param.bDispGetuun = chkDispGetuun.Checked;
            param.bGogyou = chkGogyou.Checked;
            param.bGotoku = chkGotoku.Checked;

            param.bSangouKaikyoku = chkSangouKaikyoku.Checked;
            param.bZougan = chkZougan.Checked;
            param.bJuniSinkanHou = chkJuniSinkanHou.Checked;

            //宿命、後天運図への反映項目、
            person.bRefrectSigou = chkRefrectSigou.Checked;
            person.bRefrectHankai = chkRefrectHankai.Checked;
            person.bRefrectKangou = chkRefrectKangou.Checked;
            person.bRefrectHousani = chkRefrectHousani.Checked;
            person.bRefrectSangouKaikyoku = chkRefrectSangouKaikyoku.Checked;



            PDFOutput pdf = new PDFOutput(param);
            pdf.WritePDF(@"c:\temp\01_Hello.pdf");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetNowYearMonth();
        }
        private void SetNowYearMonth()
        {
            DateTime dt = DateTime.Now;
            txtYear.Text = dt.Year.ToString();
            txtMonth.Text = dt.Month.ToString();
        }
    }
}

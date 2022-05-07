﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DivinationApp
{
    public partial class FormPDF : ModelessBase
    {
        Persons personList;

        public FormPDF(Persons persons)
        {
            InitializeComponent();

            personList = persons;

        }


        private void FormPDF_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.MinimumSize = this.Size;
            this.MaximumSize = new Size(this.Size.Width, 2048);

            var frmMain = FormMain.GetFormMain();
            Person person = frmMain.GetActiveFormPerson();
            Group group = frmMain.GetActiveFormGroup();


            SetNowYearMonth();

            chkShukumeiAndKoutenun.Checked = true;
            chkKyoki.Checked = true;
            chkTaiunNenun.Checked = true;
            chkGetuun.Checked = false;
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

            //グループコンボボックス設定
            Common.SetGroupCombobox(personList, cmbGroup, group.groupName);

            //初回のみ現在のメンバーのチェックマークをONする
            foreach(ListViewItem item in lstPerson.Items)
            {
                if(item.Text == person.name)
                {
                    item.Checked = true;
                }
            }



        }

        bool bStop = false;
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "PDF出力")
            {
                string folderPath = Path.Combine(FormMain.GetExePath(), "PDF");
                Directory.CreateDirectory(folderPath);
                button2.Text = "停止";
                bStop = false;
                lstPerson.Enabled = false;
                cmbGroup.Enabled = false;

                //人名一覧でチェックの付いている人をすべてPDF化
                List<Person> lstPdfPersons = new List<Person>();
                foreach (ListViewItem lvItem in lstPerson.Items)
                {
                    if (!lvItem.Checked) continue;
                    lstPdfPersons.Add((Person)lvItem.Tag);
                }

                Task.Run(() =>
                {
                    //人名一覧でチェックの付いている人をすべてPDF化
                    foreach (var person in lstPdfPersons)
                    {
                        if (bStop) break;

                        Invoke((MethodInvoker)(() =>
                        {
                            foreach (ListViewItem lvItem in lstPerson.Items)
                            {
                                Person p = (Person)lvItem.Tag;
                                if ( p.name == person.name && p.group == person.group)
                                {
                                    lvItem.Selected = true;
                                    lvItem.EnsureVisible();
                                }
                            }

                        }));


                        OutputPDF(person, folderPath);
                    }
                    Invoke((MethodInvoker)(() =>
                    {
                        button2.Text = "PDF出力";
                        lstPerson.Enabled = true;
                        cmbGroup.Enabled = true;

                    }));

                });

            }
            else
            {
                bStop = true;
            }
        }

        private void OutputPDF( Person person, string outputFolderPath )
        {
            PDFOutput.Parameter param = new PDFOutput.Parameter();

            //Person情報を一部書き換えるので、ここでクローンを作成してparamに設定
            param.person = person.Clone(); 

            param.bShukumeiAndKoutenun = chkShukumeiAndKoutenun.Checked;
            param.bKyoki = chkKyoki.Checked;
            param.bTaiunHyouAndNenunHyou = chkTaiunNenun.Checked;
            if (chkTaiunNenun.Checked)
            {
                param.bGetuunHyou = chkGetuun.Checked;
            }else
            {
                param.bGetuunHyou = false;
            }
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

            param.bInsenYousenExplanation = chkInsenYousenExplanation.Checked;

            //宿命、後天運図への反映項目、
            param.person.bRefrectSigou = chkRefrectSigou.Checked;
            param.person.bRefrectHankai = chkRefrectHankai.Checked;
            param.person.bRefrectKangou = chkRefrectKangou.Checked;
            param.person.bRefrectHousani = chkRefrectHousani.Checked;
            param.person.bRefrectSangouKaikyoku = chkRefrectSangouKaikyoku.Checked;

            PDFOutput pdf = new PDFOutput(param);
            pdf.WritePDF( Path.Combine(outputFolderPath, string.Format("{0}.pdf", person.name)) );


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

        private void chkTaiunNenun_CheckedChanged(object sender, EventArgs e)
        {
            chkGetuun.Enabled = chkTaiunNenun.Checked;
        }


        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<Person> persons = null;
            if (cmbGroup.SelectedIndex == 0)
            {
                //全て
                persons = personList.GetPersonList();
            }
            else
            {
                var item = (Group)cmbGroup.SelectedItem;
                persons = item.members;

            }
            lstPerson.Items.Clear();
            foreach (var item in persons)
            {
                var lvItem = lstPerson.Items.Add(item.name);
                lvItem.Tag = item;

            }
        }

        private void chkGogyou_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGotoku.Checked == false) return;
            chkGotoku.CheckedChanged -= chkGotoku_CheckedChanged;
            chkGotoku.Checked = false;
            chkGotoku.CheckedChanged += chkGotoku_CheckedChanged;
        }

        private void chkGotoku_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGotoku.Checked == false) return;
            chkGogyou.CheckedChanged -= chkGogyou_CheckedChanged;
            chkGogyou.Checked = false;
            chkGogyou.CheckedChanged += chkGogyou_CheckedChanged;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in lstPerson.Items)
            {
                lvItem.Checked = checkBox1.Checked;
            }

        }
    }
}


namespace DivinationApp
{
    partial class FormPDF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstPerson = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chkGetuun = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkTaiunNenun = new System.Windows.Forms.CheckBox();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.chkKyoki = new System.Windows.Forms.CheckBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.chkInsenYousenExplanation = new System.Windows.Forms.CheckBox();
            this.chkKonkihou = new System.Windows.Forms.CheckBox();
            this.chkShugosin = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDispGetuun = new System.Windows.Forms.CheckBox();
            this.grpGogyouGotoku = new System.Windows.Forms.GroupBox();
            this.chkRefrectKangou = new System.Windows.Forms.CheckBox();
            this.chkRefrectSangouKaikyoku = new System.Windows.Forms.CheckBox();
            this.chkRefrectHankai = new System.Windows.Forms.CheckBox();
            this.chkRefrectSigou = new System.Windows.Forms.CheckBox();
            this.chkRefrectHousani = new System.Windows.Forms.CheckBox();
            this.chkJuniSinkanHou = new System.Windows.Forms.CheckBox();
            this.chkSangouKaikyoku = new System.Windows.Forms.CheckBox();
            this.chkGotoku = new System.Windows.Forms.CheckBox();
            this.chkGogyou = new System.Windows.Forms.CheckBox();
            this.chkZougan = new System.Windows.Forms.CheckBox();
            this.chkShukumeiAndKoutenun = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chkViewPDF = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.grpGogyouGotoku.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstPerson
            // 
            this.lstPerson.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstPerson.CheckBoxes = true;
            this.lstPerson.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstPerson.FullRowSelect = true;
            this.lstPerson.GridLines = true;
            this.lstPerson.HideSelection = false;
            this.lstPerson.Location = new System.Drawing.Point(9, 52);
            this.lstPerson.MultiSelect = false;
            this.lstPerson.Name = "lstPerson";
            this.lstPerson.Size = new System.Drawing.Size(147, 235);
            this.lstPerson.TabIndex = 3;
            this.lstPerson.UseCompatibleStateImageBehavior = false;
            this.lstPerson.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "氏名";
            this.columnHeader1.Width = 123;
            // 
            // cmbGroup
            // 
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(8, 7);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(148, 20);
            this.cmbGroup.TabIndex = 1;
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(394, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 22);
            this.button1.TabIndex = 13;
            this.button1.Text = "今日";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkGetuun
            // 
            this.chkGetuun.AutoSize = true;
            this.chkGetuun.Location = new System.Drawing.Point(188, 111);
            this.chkGetuun.Name = "chkGetuun";
            this.chkGetuun.Size = new System.Drawing.Size(60, 16);
            this.chkGetuun.TabIndex = 9;
            this.chkGetuun.Text = "月運表";
            this.chkGetuun.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(330, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 95;
            this.label2.Text = "年";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(370, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 95;
            this.label1.Text = "月";
            // 
            // chkTaiunNenun
            // 
            this.chkTaiunNenun.AutoSize = true;
            this.chkTaiunNenun.Location = new System.Drawing.Point(162, 91);
            this.chkTaiunNenun.Name = "chkTaiunNenun";
            this.chkTaiunNenun.Size = new System.Drawing.Size(90, 16);
            this.chkTaiunNenun.TabIndex = 8;
            this.chkTaiunNenun.Text = "大運・年運表";
            this.chkTaiunNenun.UseVisualStyleBackColor = true;
            this.chkTaiunNenun.CheckedChanged += new System.EventHandler(this.chkTaiunNenun_CheckedChanged);
            // 
            // txtMonth
            // 
            this.txtMonth.Location = new System.Drawing.Point(348, 6);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(22, 19);
            this.txtMonth.TabIndex = 12;
            this.txtMonth.Text = "12";
            // 
            // chkKyoki
            // 
            this.chkKyoki.AutoSize = true;
            this.chkKyoki.Location = new System.Drawing.Point(162, 71);
            this.chkKyoki.Name = "chkKyoki";
            this.chkKyoki.Size = new System.Drawing.Size(72, 16);
            this.chkKyoki.TabIndex = 7;
            this.chkKyoki.Text = "虚気変化";
            this.chkKyoki.UseVisualStyleBackColor = true;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(291, 6);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(38, 19);
            this.txtYear.TabIndex = 11;
            this.txtYear.Text = "2022";
            // 
            // chkInsenYousenExplanation
            // 
            this.chkInsenYousenExplanation.AutoSize = true;
            this.chkInsenYousenExplanation.Location = new System.Drawing.Point(162, 149);
            this.chkInsenYousenExplanation.Name = "chkInsenYousenExplanation";
            this.chkInsenYousenExplanation.Size = new System.Drawing.Size(126, 16);
            this.chkInsenYousenExplanation.TabIndex = 10;
            this.chkInsenYousenExplanation.Text = "陽占・陰占特徴説明";
            this.chkInsenYousenExplanation.UseVisualStyleBackColor = true;
            // 
            // chkKonkihou
            // 
            this.chkKonkihou.AutoSize = true;
            this.chkKonkihou.Location = new System.Drawing.Point(162, 29);
            this.chkKonkihou.Name = "chkKonkihou";
            this.chkKonkihou.Size = new System.Drawing.Size(60, 16);
            this.chkKonkihou.TabIndex = 5;
            this.chkKonkihou.Text = "根気法";
            this.chkKonkihou.UseVisualStyleBackColor = true;
            // 
            // chkShugosin
            // 
            this.chkShugosin.AutoSize = true;
            this.chkShugosin.Location = new System.Drawing.Point(162, 49);
            this.chkShugosin.Name = "chkShugosin";
            this.chkShugosin.Size = new System.Drawing.Size(72, 16);
            this.chkShugosin.TabIndex = 6;
            this.chkShugosin.Text = "守護神法";
            this.chkShugosin.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(162, 252);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 35);
            this.button2.TabIndex = 85;
            this.button2.Text = "PDF出力";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDispGetuun);
            this.groupBox1.Controls.Add(this.grpGogyouGotoku);
            this.groupBox1.Controls.Add(this.chkJuniSinkanHou);
            this.groupBox1.Controls.Add(this.chkSangouKaikyoku);
            this.groupBox1.Controls.Add(this.chkGotoku);
            this.groupBox1.Controls.Add(this.chkGogyou);
            this.groupBox1.Controls.Add(this.chkZougan);
            this.groupBox1.Location = new System.Drawing.Point(291, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 214);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "宿命・後天運表示オプション";
            // 
            // chkDispGetuun
            // 
            this.chkDispGetuun.AutoSize = true;
            this.chkDispGetuun.Checked = true;
            this.chkDispGetuun.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDispGetuun.Location = new System.Drawing.Point(17, 22);
            this.chkDispGetuun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkDispGetuun.Name = "chkDispGetuun";
            this.chkDispGetuun.Size = new System.Drawing.Size(100, 16);
            this.chkDispGetuun.TabIndex = 1;
            this.chkDispGetuun.Text = "月運も表示する";
            this.chkDispGetuun.UseVisualStyleBackColor = true;
            // 
            // grpGogyouGotoku
            // 
            this.grpGogyouGotoku.Controls.Add(this.chkRefrectKangou);
            this.grpGogyouGotoku.Controls.Add(this.chkRefrectSangouKaikyoku);
            this.grpGogyouGotoku.Controls.Add(this.chkRefrectHankai);
            this.grpGogyouGotoku.Controls.Add(this.chkRefrectSigou);
            this.grpGogyouGotoku.Controls.Add(this.chkRefrectHousani);
            this.grpGogyouGotoku.Location = new System.Drawing.Point(15, 59);
            this.grpGogyouGotoku.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpGogyouGotoku.Name = "grpGogyouGotoku";
            this.grpGogyouGotoku.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpGogyouGotoku.Size = new System.Drawing.Size(118, 88);
            this.grpGogyouGotoku.TabIndex = 4;
            this.grpGogyouGotoku.TabStop = false;
            // 
            // chkRefrectKangou
            // 
            this.chkRefrectKangou.AutoSize = true;
            this.chkRefrectKangou.Location = new System.Drawing.Point(6, 36);
            this.chkRefrectKangou.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkRefrectKangou.Name = "chkRefrectKangou";
            this.chkRefrectKangou.Size = new System.Drawing.Size(48, 16);
            this.chkRefrectKangou.TabIndex = 3;
            this.chkRefrectKangou.Text = "干合";
            this.chkRefrectKangou.UseVisualStyleBackColor = true;
            // 
            // chkRefrectSangouKaikyoku
            // 
            this.chkRefrectSangouKaikyoku.AutoSize = true;
            this.chkRefrectSangouKaikyoku.Location = new System.Drawing.Point(6, 58);
            this.chkRefrectSangouKaikyoku.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkRefrectSangouKaikyoku.Name = "chkRefrectSangouKaikyoku";
            this.chkRefrectSangouKaikyoku.Size = new System.Drawing.Size(72, 16);
            this.chkRefrectSangouKaikyoku.TabIndex = 5;
            this.chkRefrectSangouKaikyoku.Text = "三合会局";
            this.chkRefrectSangouKaikyoku.UseVisualStyleBackColor = true;
            // 
            // chkRefrectHankai
            // 
            this.chkRefrectHankai.AutoSize = true;
            this.chkRefrectHankai.Location = new System.Drawing.Point(56, 16);
            this.chkRefrectHankai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkRefrectHankai.Name = "chkRefrectHankai";
            this.chkRefrectHankai.Size = new System.Drawing.Size(48, 16);
            this.chkRefrectHankai.TabIndex = 2;
            this.chkRefrectHankai.Text = "半会";
            this.chkRefrectHankai.UseVisualStyleBackColor = true;
            // 
            // chkRefrectSigou
            // 
            this.chkRefrectSigou.AutoSize = true;
            this.chkRefrectSigou.Location = new System.Drawing.Point(6, 16);
            this.chkRefrectSigou.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkRefrectSigou.Name = "chkRefrectSigou";
            this.chkRefrectSigou.Size = new System.Drawing.Size(48, 16);
            this.chkRefrectSigou.TabIndex = 1;
            this.chkRefrectSigou.Text = "支合";
            this.chkRefrectSigou.UseVisualStyleBackColor = true;
            // 
            // chkRefrectHousani
            // 
            this.chkRefrectHousani.AutoSize = true;
            this.chkRefrectHousani.Location = new System.Drawing.Point(56, 36);
            this.chkRefrectHousani.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkRefrectHousani.Name = "chkRefrectHousani";
            this.chkRefrectHousani.Size = new System.Drawing.Size(60, 16);
            this.chkRefrectHousani.TabIndex = 4;
            this.chkRefrectHousani.Text = "方三位";
            this.chkRefrectHousani.UseVisualStyleBackColor = true;
            // 
            // chkJuniSinkanHou
            // 
            this.chkJuniSinkanHou.AutoSize = true;
            this.chkJuniSinkanHou.Location = new System.Drawing.Point(21, 187);
            this.chkJuniSinkanHou.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkJuniSinkanHou.Name = "chkJuniSinkanHou";
            this.chkJuniSinkanHou.Size = new System.Drawing.Size(84, 16);
            this.chkJuniSinkanHou.TabIndex = 7;
            this.chkJuniSinkanHou.Text = "十二親干法";
            this.chkJuniSinkanHou.UseVisualStyleBackColor = true;
            // 
            // chkSangouKaikyoku
            // 
            this.chkSangouKaikyoku.Checked = true;
            this.chkSangouKaikyoku.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSangouKaikyoku.Location = new System.Drawing.Point(21, 152);
            this.chkSangouKaikyoku.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkSangouKaikyoku.Name = "chkSangouKaikyoku";
            this.chkSangouKaikyoku.Size = new System.Drawing.Size(115, 19);
            this.chkSangouKaikyoku.TabIndex = 5;
            this.chkSangouKaikyoku.Text = "三合会局・方三位";
            this.chkSangouKaikyoku.UseVisualStyleBackColor = true;
            // 
            // chkGotoku
            // 
            this.chkGotoku.AutoSize = true;
            this.chkGotoku.Location = new System.Drawing.Point(71, 45);
            this.chkGotoku.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkGotoku.Name = "chkGotoku";
            this.chkGotoku.Size = new System.Drawing.Size(48, 16);
            this.chkGotoku.TabIndex = 3;
            this.chkGotoku.Text = "五徳";
            this.chkGotoku.UseVisualStyleBackColor = true;
            this.chkGotoku.CheckedChanged += new System.EventHandler(this.chkGotoku_CheckedChanged);
            // 
            // chkGogyou
            // 
            this.chkGogyou.AutoSize = true;
            this.chkGogyou.Location = new System.Drawing.Point(20, 45);
            this.chkGogyou.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkGogyou.Name = "chkGogyou";
            this.chkGogyou.Size = new System.Drawing.Size(48, 16);
            this.chkGogyou.TabIndex = 2;
            this.chkGogyou.Text = "五行";
            this.chkGogyou.UseVisualStyleBackColor = true;
            this.chkGogyou.CheckedChanged += new System.EventHandler(this.chkGogyou_CheckedChanged);
            // 
            // chkZougan
            // 
            this.chkZougan.AutoSize = true;
            this.chkZougan.Location = new System.Drawing.Point(21, 170);
            this.chkZougan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkZougan.Name = "chkZougan";
            this.chkZougan.Size = new System.Drawing.Size(48, 16);
            this.chkZougan.TabIndex = 6;
            this.chkZougan.Text = "蔵元";
            this.chkZougan.UseVisualStyleBackColor = true;
            // 
            // chkShukumeiAndKoutenun
            // 
            this.chkShukumeiAndKoutenun.AutoSize = true;
            this.chkShukumeiAndKoutenun.Location = new System.Drawing.Point(162, 9);
            this.chkShukumeiAndKoutenun.Name = "chkShukumeiAndKoutenun";
            this.chkShukumeiAndKoutenun.Size = new System.Drawing.Size(104, 16);
            this.chkShukumeiAndKoutenun.TabIndex = 4;
            this.chkShukumeiAndKoutenun.Text = "宿命、後天運図";
            this.chkShukumeiAndKoutenun.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 33);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(69, 16);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "全て選択";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chkViewPDF
            // 
            this.chkViewPDF.AutoSize = true;
            this.chkViewPDF.Checked = true;
            this.chkViewPDF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewPDF.Location = new System.Drawing.Point(315, 262);
            this.chkViewPDF.Name = "chkViewPDF";
            this.chkViewPDF.Size = new System.Drawing.Size(121, 16);
            this.chkViewPDF.TabIndex = 96;
            this.chkViewPDF.Text = "作成したPDFを表示";
            this.chkViewPDF.UseVisualStyleBackColor = true;
            // 
            // FormPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 297);
            this.Controls.Add(this.chkViewPDF);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.lstPerson);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkGetuun);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkTaiunNenun);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.chkKyoki);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.chkInsenYousenExplanation);
            this.Controls.Add(this.chkKonkihou);
            this.Controls.Add(this.chkShugosin);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkShukumeiAndKoutenun);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPDF";
            this.Text = "PDF出力";
            this.Load += new System.EventHandler(this.FormPDF_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpGogyouGotoku.ResumeLayout(false);
            this.grpGogyouGotoku.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkShukumeiAndKoutenun;
        private System.Windows.Forms.CheckBox chkJuniSinkanHou;
        private System.Windows.Forms.CheckBox chkGotoku;
        private System.Windows.Forms.CheckBox chkZougan;
        private System.Windows.Forms.CheckBox chkGogyou;
        private System.Windows.Forms.GroupBox grpGogyouGotoku;
        private System.Windows.Forms.CheckBox chkRefrectKangou;
        private System.Windows.Forms.CheckBox chkRefrectSangouKaikyoku;
        private System.Windows.Forms.CheckBox chkRefrectHankai;
        private System.Windows.Forms.CheckBox chkRefrectSigou;
        private System.Windows.Forms.CheckBox chkRefrectHousani;
        private System.Windows.Forms.CheckBox chkSangouKaikyoku;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chkShugosin;
        private System.Windows.Forms.CheckBox chkKonkihou;
        private System.Windows.Forms.CheckBox chkKyoki;
        private System.Windows.Forms.CheckBox chkTaiunNenun;
        private System.Windows.Forms.CheckBox chkGetuun;
        private System.Windows.Forms.CheckBox chkDispGetuun;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMonth;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.CheckBox chkInsenYousenExplanation;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.ListView lstPerson;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox chkViewPDF;
    }
}
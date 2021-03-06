
namespace DivinationApp
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPatternCondFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSerch = new System.Windows.Forms.ToolStripMenuItem();
            this.パターン条件検索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOption = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExcelPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolAdd = new System.Windows.Forms.ToolStripButton();
            this.toolFind = new System.Windows.Forms.ToolStripButton();
            this.toolFindCustom = new System.Windows.Forms.ToolStripButton();
            this.toolOutputPDF = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new DivinationApp.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.mnuPatternCondFind,
            this.mnuSetting,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1312, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(67, 20);
            this.toolStripMenuItem2.Text = "ファイル(&F)";
            // 
            // mnuOpen
            // 
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(143, 22);
            this.mnuOpen.Text = "名簿を開く(&O)";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuPatternCondFind
            // 
            this.mnuPatternCondFind.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddTab,
            this.toolStripSeparator1,
            this.mnuSerch,
            this.パターン条件検索ToolStripMenuItem});
            this.mnuPatternCondFind.Name = "mnuPatternCondFind";
            this.mnuPatternCondFind.Size = new System.Drawing.Size(59, 20);
            this.mnuPatternCondFind.Text = "機能(&U)";
            // 
            // mnuAddTab
            // 
            this.mnuAddTab.Name = "mnuAddTab";
            this.mnuAddTab.Size = new System.Drawing.Size(158, 22);
            this.mnuAddTab.Text = "タブ追加";
            this.mnuAddTab.Click += new System.EventHandler(this.mnuAddTab_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // mnuSerch
            // 
            this.mnuSerch.Name = "mnuSerch";
            this.mnuSerch.Size = new System.Drawing.Size(158, 22);
            this.mnuSerch.Text = "大運・年運検索";
            this.mnuSerch.Click += new System.EventHandler(this.mnuSerch_Click);
            // 
            // パターン条件検索ToolStripMenuItem
            // 
            this.パターン条件検索ToolStripMenuItem.Name = "パターン条件検索ToolStripMenuItem";
            this.パターン条件検索ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.パターン条件検索ToolStripMenuItem.Text = "パターン条件検索";
            this.パターン条件検索ToolStripMenuItem.Click += new System.EventHandler(this.mnuPatternCondFind_Click);
            // 
            // mnuSetting
            // 
            this.mnuSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOption});
            this.mnuSetting.Name = "mnuSetting";
            this.mnuSetting.Size = new System.Drawing.Size(43, 20);
            this.mnuSetting.Text = "設定";
            // 
            // mnuOption
            // 
            this.mnuOption.Name = "mnuOption";
            this.mnuOption.Size = new System.Drawing.Size(118, 22);
            this.mnuOption.Text = "オプション";
            this.mnuOption.Click += new System.EventHandler(this.mnuOption_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExcelPicture,
            this.mnuPDF});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // mnuExcelPicture
            // 
            this.mnuExcelPicture.Name = "mnuExcelPicture";
            this.mnuExcelPicture.Size = new System.Drawing.Size(171, 22);
            this.mnuExcelPicture.Text = "Excel画像読み込み";
            this.mnuExcelPicture.Click += new System.EventHandler(this.mnuExcelPicture_Click);
            // 
            // mnuPDF
            // 
            this.mnuPDF.Name = "mnuPDF";
            this.mnuPDF.Size = new System.Drawing.Size(171, 22);
            this.mnuPDF.Text = "PDF出力";
            this.mnuPDF.Click += new System.EventHandler(this.mnuPDF_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpen,
            this.toolStripSeparator2,
            this.toolAdd,
            this.toolFind,
            this.toolFindCustom,
            this.toolOutputPDF});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1312, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolOpen
            // 
            this.toolOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolOpen.Image")));
            this.toolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOpen.Name = "toolOpen";
            this.toolOpen.Size = new System.Drawing.Size(23, 22);
            this.toolOpen.Text = "名簿を開く";
            this.toolOpen.Click += new System.EventHandler(this.toolOpen_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolAdd
            // 
            this.toolAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolAdd.Image")));
            this.toolAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAdd.Name = "toolAdd";
            this.toolAdd.Size = new System.Drawing.Size(23, 22);
            this.toolAdd.Text = "タブ追加";
            this.toolAdd.ToolTipText = "タブ追加";
            this.toolAdd.Click += new System.EventHandler(this.toolAdd_Click);
            // 
            // toolFind
            // 
            this.toolFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolFind.Image = ((System.Drawing.Image)(resources.GetObject("toolFind.Image")));
            this.toolFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFind.Name = "toolFind";
            this.toolFind.Size = new System.Drawing.Size(23, 22);
            this.toolFind.Text = "大運・年運検索";
            this.toolFind.Click += new System.EventHandler(this.toolFind_Click);
            // 
            // toolFindCustom
            // 
            this.toolFindCustom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolFindCustom.Image = ((System.Drawing.Image)(resources.GetObject("toolFindCustom.Image")));
            this.toolFindCustom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFindCustom.Name = "toolFindCustom";
            this.toolFindCustom.Size = new System.Drawing.Size(23, 22);
            this.toolFindCustom.Text = "パターン条件検索";
            this.toolFindCustom.ToolTipText = "パターン条件検索";
            this.toolFindCustom.Click += new System.EventHandler(this.toolFindCustom_Click);
            // 
            // toolOutputPDF
            // 
            this.toolOutputPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolOutputPDF.Image = ((System.Drawing.Image)(resources.GetObject("toolOutputPDF.Image")));
            this.toolOutputPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOutputPDF.Name = "toolOutputPDF";
            this.toolOutputPDF.Size = new System.Drawing.Size(23, 22);
            this.toolOutputPDF.Text = "PDF出力";
            this.toolOutputPDF.Click += new System.EventHandler(this.toolOutputPDF_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 52);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1312, 732);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(1304, 704);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(1304, 704);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 781);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMain";
            this.Text = "占いソフト";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControlEx tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuPatternCondFind;
        private System.Windows.Forms.ToolStripMenuItem mnuAddTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuSerch;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolAdd;
        private System.Windows.Forms.ToolStripButton toolFind;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripButton toolOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuExcelPicture;
        private System.Windows.Forms.ToolStripButton toolFindCustom;
        private System.Windows.Forms.ToolStripMenuItem パターン条件検索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting;
        private System.Windows.Forms.ToolStripMenuItem mnuOption;
        private System.Windows.Forms.ToolStripMenuItem mnuPDF;
        private System.Windows.Forms.ToolStripButton toolOutputPDF;
    }
}
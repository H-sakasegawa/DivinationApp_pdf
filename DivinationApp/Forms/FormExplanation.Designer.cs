
namespace DivinationApp
{
    partial class FormExplanation 
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
            this.picExplanation = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lblPage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lstKeys = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lstMainKey = new System.Windows.Forms.ListBox();
            this.lstSubKey = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.picExplanation)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // picExplanation
            // 
            this.picExplanation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picExplanation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picExplanation.Cursor = System.Windows.Forms.Cursors.Default;
            this.picExplanation.Location = new System.Drawing.Point(23, 20);
            this.picExplanation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picExplanation.Name = "picExplanation";
            this.picExplanation.Size = new System.Drawing.Size(300, 164);
            this.picExplanation.TabIndex = 0;
            this.picExplanation.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(122, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = ">";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(35, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(22, 22);
            this.button2.TabIndex = 1;
            this.button2.Text = "<";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(29, 22);
            this.button3.TabIndex = 0;
            this.button3.Text = "|<";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(153, 0);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(29, 22);
            this.button4.TabIndex = 3;
            this.button4.Text = ">|";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // lblPage
            // 
            this.lblPage.Location = new System.Drawing.Point(56, 2);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(66, 19);
            this.lblPage.TabIndex = 7;
            this.lblPage.Text = "nn/nn";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.lblPage);
            this.panel1.Location = new System.Drawing.Point(3, 301);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(184, 22);
            this.panel1.TabIndex = 4;
            // 
            // lstKeys
            // 
            this.lstKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstKeys.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstKeys.FormattingEnabled = true;
            this.lstKeys.IntegralHeight = false;
            this.lstKeys.ItemHeight = 15;
            this.lstKeys.Location = new System.Drawing.Point(13, 96);
            this.lstKeys.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstKeys.Name = "lstKeys";
            this.lstKeys.Size = new System.Drawing.Size(69, 103);
            this.lstKeys.TabIndex = 3;
            this.lstKeys.SelectedIndexChanged += new System.EventHandler(this.lstKeys_SelectedIndexChanged);
            this.lstKeys.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstKeys_KeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 38);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.picExplanation);
            this.splitContainer1.Size = new System.Drawing.Size(603, 256);
            this.splitContainer1.SplitterDistance = 267;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 10;
            // 
            // splitContainer2
            // 
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 10);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lstKeys);
            this.splitContainer2.Size = new System.Drawing.Size(267, 241);
            this.splitContainer2.SplitterDistance = 166;
            this.splitContainer2.TabIndex = 11;
            // 
            // splitContainer3
            // 
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(3, 28);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lstMainKey);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.lstSubKey);
            this.splitContainer3.Size = new System.Drawing.Size(160, 196);
            this.splitContainer3.SplitterDistance = 83;
            this.splitContainer3.TabIndex = 11;
            // 
            // lstMainKey
            // 
            this.lstMainKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstMainKey.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstMainKey.FormattingEnabled = true;
            this.lstMainKey.IntegralHeight = false;
            this.lstMainKey.ItemHeight = 15;
            this.lstMainKey.Location = new System.Drawing.Point(3, 68);
            this.lstMainKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstMainKey.Name = "lstMainKey";
            this.lstMainKey.Size = new System.Drawing.Size(74, 89);
            this.lstMainKey.TabIndex = 1;
            this.lstMainKey.SelectedIndexChanged += new System.EventHandler(this.lstMainKey_SelectedIndexChanged);
            this.lstMainKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstMainKey_KeyDown);
            // 
            // lstSubKey
            // 
            this.lstSubKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstSubKey.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstSubKey.FormattingEnabled = true;
            this.lstSubKey.IntegralHeight = false;
            this.lstSubKey.ItemHeight = 15;
            this.lstSubKey.Location = new System.Drawing.Point(3, 68);
            this.lstSubKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstSubKey.Name = "lstSubKey";
            this.lstSubKey.Size = new System.Drawing.Size(70, 89);
            this.lstSubKey.TabIndex = 2;
            this.lstSubKey.SelectedIndexChanged += new System.EventHandler(this.lstSubKey_SelectedIndexChanged);
            this.lstSubKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstSubKey_KeyDown);
            // 
            // FormExplanation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 324);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormExplanation";
            this.Text = "説明";
            this.Load += new System.EventHandler(this.FormExplanation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormExplanation_KeyDown);
            this.Resize += new System.EventHandler(this.FormExplanation_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picExplanation)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picExplanation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lstKeys;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstMainKey;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListBox lstSubKey;
    }
}
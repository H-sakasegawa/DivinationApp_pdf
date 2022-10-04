
namespace DivinationApp
{
    partial class ModelessBase
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
            this.SuspendLayout();
            // 
            // ModelessBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(168, 160);
            this.KeyPreview = true;
            this.Name = "ModelessBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ModelessBase2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelessBase2_FormClosing);
            this.Load += new System.EventHandler(this.ModelessBase_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ModelessBase_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
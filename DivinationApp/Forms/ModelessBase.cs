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
    public partial class ModelessBase : Form
    {
        public event Common.CloseHandler OnClose = null;

        public ModelessBase()
        {
            InitializeComponent();


         }
        private void ModelessBase_Load(object sender, EventArgs e)
        {
            var frmMain = FormMain.GetFormMain();
            if (frmMain == null) return;
            Size parentSz = FormMain.GetFormMain().Size;
            Size sz = new Size((frmMain.Size.Width - Size.Width) / 2, (frmMain.Size.Height - Size.Height) / 2);

            this.Location = new Point(frmMain.Location.X + sz.Width, frmMain.Location.Y + sz.Height);

        }

        private void ModelessBase2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null) OnClose(this);

        }

        private void ModelessBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

    }
}

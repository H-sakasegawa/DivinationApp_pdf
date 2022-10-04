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

        public ModelessBase(Form frmParent)
        {
            InitializeComponent();


         }
        private void ModelessBase_Load(object sender, EventArgs e)
        {
            //Parent = frmParent;
            Size parentSz = FormMain.GetFormMain().Size;
            Size sz = this.Size;
            sz = parentSz-sz;
            sz.Width = sz.Width / 2;
            sz.Height = sz.Height / 2;

            this.Location = new Point(FormMain.GetFormMain().Location.X + sz.Width, FormMain.GetFormMain().Location.Y + sz.Height);

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

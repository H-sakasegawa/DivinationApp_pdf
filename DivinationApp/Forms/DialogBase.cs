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
    public partial class DialogBase : Form
    {
        public event Common.CloseHandler OnClose = null;

        string filePath = "";
        string entryName;
        public DialogBase()
        {
            InitializeComponent();
            entryName = GetType().Name;
            filePath = System.IO.Path.Combine(FormMain.GetExePath(), Const.windowPosFileName);

        }
        private void DialogBase_Load(object sender, EventArgs e)
        {

            IniFile iniFile = new IniFile(filePath);

            string value = iniFile.GetString(Const.SECTION_WINDOWPOS, entryName);
            if (!string.IsNullOrEmpty(value))
            {
                var values = value.Split(',');

                int x = int.Parse(values[0]);
                int y = int.Parse(values[1]);
                int width = int.Parse(values[2]);
                int height = int.Parse(values[3]);

                System.Windows.Forms.Screen sz = System.Windows.Forms.Screen.FromControl(this);
                if (x + this.Width > sz.Bounds.Width)
                {
                    x = sz.Bounds.Width - this.Width;
                }
                if (y + this.Height > sz.Bounds.Height)
                {
                    y = sz.Bounds.Height - this.Height;
                }

                if (x < 0) x = 0;
                if (y < 0) y = 0;


                this.Location = new Point(x, y);
                this.Width = width;
                this.Height = height;
            }
            else
            {
                //初期は親フォーム中央
                var frmMain = FormMain.GetFormMain();
                if (frmMain == null) return;
                Size parentSz = FormMain.GetFormMain().Size;
                Size sz = new Size((frmMain.Size.Width - Size.Width) / 2, (frmMain.Size.Height - Size.Height) / 2);

                this.Location = new Point(frmMain.Location.X + sz.Width, frmMain.Location.Y + sz.Height);

            }

        }

        private void DialogBase2_FormClosing(object sender, FormClosingEventArgs e)
        {
            IniFile iniFile = new IniFile(filePath);
            iniFile.WriteString(Const.SECTION_WINDOWPOS, entryName, $"{Location.X},{Location.Y}, {Width},{Height}");

            if (OnClose != null) OnClose(this);

        }

        private void DialogBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

    }
}

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

        private void ModelessBase2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null) OnClose(this);

        }
    }
}

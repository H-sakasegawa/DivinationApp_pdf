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
    public partial class ModalBase : Form
    {
        public ModalBase()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void FormBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace DivinationApp
{
    public partial class FormGouhouSanpouFilter : Form
    {
        public FormGouhouSanpouFilter()
        {
            InitializeComponent();
        }
        private void FormGouhouSanpouFilter_Load(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var setting = config.AppSettings.Settings[Const.CFGKEY_LIST_FILTER];
            List<string> lstFilter = null;

            var lstNames = FormMain.GetFormMain().GetListFilterStrings();

            if ( setting == null || string.IsNullOrEmpty(setting.Value))
            {
                checkBox1.Checked = true;
                lstFilter = lstNames;
            }
            else
            {
                lstFilter = setting.Value.Split(',').ToList();
            }

 
            //filter文字をグリッドに表示

            foreach ( var name in lstNames)
            {
                int iRow = grdNames.Rows.Add();
                grdNames.Rows[iRow].Cells[1].Value= name;

                if (lstFilter != null)
                {
                    grdNames.Rows[iRow].Cells[0].Value = lstFilter.Contains(name);
                }

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            foreach ( DataGridViewRow row in grdNames.Rows)
            {
                row.Cells[0].Value = checkBox1.Checked;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            List<string> lstDispNames = new List<string>();
            foreach (DataGridViewRow row in grdNames.Rows)
            {
                if ((bool)(row.Cells[0].Value) == true)
                {
                    lstDispNames.Add((string)row.Cells[1].Value);
                }

            }
            if (lstDispNames.Count == 0)
            {
                config.AppSettings.Settings[Const.CFGKEY_LIST_FILTER].Value = "EMPTY";
            }
            else
            {
                config.AppSettings.Settings[Const.CFGKEY_LIST_FILTER].Value = string.Join(",", lstDispNames);
            }

            config.Save();
            this.Close();
        }

    }
}

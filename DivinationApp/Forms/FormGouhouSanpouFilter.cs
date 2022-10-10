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
        List<string> lstFilter = null;
        List<string> lstFilterResult = new List<string>();

        bool bInitialized = false;

        public FormGouhouSanpouFilter()
        {
            InitializeComponent();
        }
        private void FormGouhouSanpouFilter_Load(object sender, EventArgs e)
        {
 
            FormMain frmMain = FormMain.GetFormMain();
            //フィルタ対象文字列（オリジナル）
            var lstNames = frmMain.GetListFilterStrings();
            lstFilter = frmMain.lstGouhouSanpouFilter;

            if (lstNames.Count == lstFilter.Count)
            {
                checkBox1.Checked = true;
            }


            grdNames.CellValueChanged -= grdNames_CellValueChanged;
            {
                //filter文字をグリッドに表示

                foreach (var name in lstNames)
                {
                    int iRow = grdNames.Rows.Add();
                    grdNames.Rows[iRow].Cells[1].Value = name;

                    if (lstFilter != null)
                    {
                        grdNames.Rows[iRow].Cells[0].Value = lstFilter.Contains(name);
                    }

                }
            }
            grdNames.CellValueChanged += grdNames_CellValueChanged;

            bInitialized = true;
        }
        public List<string> GetFilterResult()
        {
            return lstFilterResult;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!bInitialized) return;

            grdNames.CellValueChanged -= grdNames_CellValueChanged;
            {

                foreach (DataGridViewRow row in grdNames.Rows)
                {
                    row.Cells[0].Value = checkBox1.Checked;

                }
            }
            grdNames.CellValueChanged += grdNames_CellValueChanged;
            UpdateFilter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 適応ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void UpdateFilter()
        {
            FormMain frmMain = FormMain.GetFormMain();

            lstFilterResult.Clear();
            foreach (DataGridViewRow row in grdNames.Rows)
            {
                if ((bool)(row.Cells[0].Value) == true)
                {
                    lstFilterResult.Add((string)row.Cells[1].Value);
                }

            }

            frmMain.UpdateFilter(lstFilterResult);

        }

        private void grdNames_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if( e.ColumnIndex ==0)
            {
                //とりあえず適応ボタンで設定すればよいとのことなので、
                //チェックボックスON/OFFとの連動は一旦コメントアウト
  //              UpdateFilter();
            }

        }

        private void grdNames_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var dataGridView = sender as DataGridView;

            //コミットされていない内容がある
            if (dataGridView.IsCurrentCellDirty)
            {
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void FormGouhouSanpouFilter_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateFilter();

        }
    }
}

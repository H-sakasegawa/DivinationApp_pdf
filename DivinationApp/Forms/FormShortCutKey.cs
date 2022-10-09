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
    public partial class FormShortCutKey : ModalBase
    {
        ShortCutkeyMng mng;
        public FormShortCutKey(ShortCutkeyMng mng)
        {
            InitializeComponent();

            this.mng = mng;
        }

        private void FormShortCutKey_Load(object sender, EventArgs e)
        {
            //コンボボックス項目設定

            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            //ComboBoxのリストに表示する項目を設定する
            foreach (var key in Enum.GetNames(typeof(Keys)) )
            {
                column.Items.Add(key);
            }
            column.Width = 80;
            column.HeaderText = "キー";
            column.Sorted = true;
            column.SortMode = DataGridViewColumnSortMode.Automatic;

            dataGridView1.Columns.Add(column);

            var lstKeys = mng.GetShortCutKeys();

            dataGridView1.CellValueChanged -= dataGridView1_CellValueChanged;
            foreach (var key in lstKeys)
            {
                int iRow = dataGridView1.Rows.Add();

                var row = dataGridView1.Rows[iRow];

                row.Cells[0].Value = key.title;
                row.Cells[1].Value = key.Control;
                row.Cells[2].Value = key.Shift;
                row.Cells[3].Value = Enum.GetName(typeof(Keys), key.KeyCode);

                row.Tag = key;
            }
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];
            ShortCutkey keyInf = (ShortCutkey)row.Tag;

            switch (e.ColumnIndex)
            {
                case 1:
                    keyInf.Control = (bool)row.Cells[e.ColumnIndex].Value;
                    break;
                case 2:
                    keyInf.Shift = (bool)row.Cells[e.ColumnIndex].Value;
                    break;
                case 3:
                    keyInf.KeyCode = (Keys)Enum.Parse(typeof(Keys), (string)(row.Cells[e.ColumnIndex].Value));
                    break;
            }


        }
        /// <summary>
        /// コンボボックスが選択された瞬間にChangeイベントを発生させるための処置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var dataGridView = sender as DataGridView;

            //コミットされていない内容がある
            if (dataGridView.IsCurrentCellDirty)
            {
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void FormShortCutKey_FormClosing(object sender, FormClosingEventArgs e)
        {
            //編集内容を保存
            mng.WriteShortCutKey();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];
            ShortCutkey keyInf = (ShortCutkey)row.Tag;
            if (keyInf.KeyCode == Keys.None) return;

            if ((keyInf.Control && !keyInf.Shift && keyInf.KeyCode == Keys.G) ||
                (keyInf.Control && !keyInf.Shift && keyInf.KeyCode == Keys.F))
            {
                string ngKey = "";
                if (keyInf.Control) ngKey += "CTRL+";
                if (keyInf.Shift) ngKey += "SHIFT+";
                ngKey += keyInf.KeyCode.ToString();

                MessageBox.Show($"{keyInf.title}の {ngKey} \nは割り当て済みのショートカットキーです");
                e.Cancel = true;
                return;

            }


            //重複登録チェック
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r == row) continue;
                ShortCutkey keyInf2 = (ShortCutkey)r.Tag;

 
                if ((keyInf.Control == keyInf2.Control) &&
                    (keyInf.Shift == keyInf2.Shift) &&
                    (keyInf.KeyCode == keyInf2.KeyCode)
                    )
                {
                    string ngKey="";
                    if (keyInf.Control) ngKey += "CTRL+";
                    if (keyInf.Shift) ngKey += "SHIFT+";
                    ngKey += keyInf.KeyCode.ToString();

                    MessageBox.Show($"{ngKey} は{keyInf.title}と{keyInf2.title} \nで重複したショートカットキーです");
                    e.Cancel = true;
                    return;
                }
            }

        }
    }
}

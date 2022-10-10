using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace DivinationApp
{
    public partial class FormExplanation : ModelessBase
    {
        //public event Common.CloseHandler OnClose = null;
        ExplanationReader curReader = null;
        ExplanationReader.ExplanationData curData = null;
        string curType = "";

        int curPageNo = 0;

        Image curImage = null;

        string formTitle = "説明";

        DocumentManager docMng;

        //const string explanationFileDefName = "ExplanationFileDef.ini";

        public FormExplanation()
        {
            InitializeComponent();

            docMng = FormMain.GetFormMain().docMng;
        }

        private void FormExplanation_Load(object sender, EventArgs e)
        {
            this.TopMost = true;

            splitContainer2.Dock = DockStyle.Fill;
            splitContainer3.Dock = DockStyle.Fill;

            lstKeys.Dock = DockStyle.Fill;
            lstMainKey.Dock = DockStyle.Fill;
            lstSubKey.Dock = DockStyle.Fill;
            picExplanation.Dock = DockStyle.Fill;

            //拡大縮小で縦横比率を維持
            picExplanation.SizeMode = PictureBoxSizeMode.Zoom;

            picExplanation.MouseWheel
                += new System.Windows.Forms.MouseEventHandler(this.picExplanation_MouseWheel);


            var mainKeys = docMng.GetMainCategories();
            //メインキー項目表示
            lstMainKey.Items.Clear();
            foreach (var name in mainKeys)
            {
                lstMainKey.Items.Add(name);
            }
            if (lstMainKey.Items.Count > 0)
            {
                lstMainKey.SelectedIndex = 0;
            }

            UpdatePagingPanel();

            SetActiveList(lstMainKey);

        }
        /// <summary>
        /// 主キー項目変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstMainKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstSubKey.Items.Clear();
            lstKeys.Items.Clear();

            int idx = lstMainKey.SelectedIndex;
            if (idx < 0) return;

            string mainKey = lstMainKey.Items[idx].ToString();

            var subKeys = docMng.GetSubCategories(mainKey);
            //サブキー項目表示
            foreach (var name in subKeys)
            {
                lstSubKey.Items.Add(name);
            }
            if(lstSubKey.Items.Count>0)
            {
                lstSubKey.SelectedIndex = 0;
            }

        }
        /// <summary>
        /// サブキー項目変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstSubKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstKeys.Items.Clear();

            int idxSub = lstSubKey.SelectedIndex;
            if (idxSub < 0) return;
            string subKey = lstSubKey.Items[idxSub].ToString();


            int idxMain = lstMainKey.SelectedIndex;
            string mainKey = lstMainKey.Items[idxMain].ToString();


            curReader = docMng.GetExplanationReader(mainKey, subKey);

            var keys = curReader.GetExplanationKeys();
            foreach (var item in keys)
            {
                lstKeys.Items.Add(item);
            }
            if (lstKeys.Items.Count > 0)
            {
                lstKeys.SelectedIndex = 0;
            }

        }

        //private void FormExplanation_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (OnClose != null) OnClose(this);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentKey">説明項目キー名</param>
        public void Show( string contentKey = null)
        {

            base.Show();
            if (!string.IsNullOrEmpty(contentKey))
            {
                contentKey = Common.TrimExplanationDataTargetKey(contentKey);

                var result = docMng.GetExplanationReader(contentKey);
                curReader = result.reader;
                if(curReader==null)
                {
                    MessageBox.Show($"指定された項目（{contentKey}）の説明データがありません");
                }else
                {
                    //メインカテゴリを選択
                    SelectMainContentItem( result.mainCategoryName );
                    lstSubKey.SelectedItem = result.subCategoryName;

                    SetCurrentExplanation(contentKey, false);
                }
            }
        }
        private void SelectMainContentItem( string name)
        {
            foreach(var item in lstMainKey.Items)
            {
                if((string)item == name)
                {
                    lstMainKey.SelectedItem = item;
                }
            }
        }

        private void SetCurrentExplanation( string contentKey, bool bResize=true )
        {
            bool bEnable = true;
            if (!string.IsNullOrEmpty(contentKey))
            {
                curData = curReader.GetExplanation(contentKey);
            }else
            {
                //最初の項目をデフォルトとして表示
                var kyes = curReader.GetExplanationKeys();
                if (kyes.Count > 0)
                {
                    curData = curReader.GetExplanation(kyes[0]);
                }
            }
            if (curData != null)
            {
                lblPage.Text = string.Format("{0}/{1}", 1, curData.pictureInfos.Count);
                ShowPage(1);
                if (bResize) ResizeWindow(1);
                lstKeys.SelectedItem = contentKey;
            }
            else
            {
                ShowPage(-1);
                bEnable = false;
                FormExplanation_Resize(null, null);
                lstKeys.SelectedItem = null;
            }
            button1.Enabled = bEnable;
            button2.Enabled = bEnable;
            button3.Enabled = bEnable;
            button4.Enabled = bEnable;
        }

        //指定されたページの画像サイズにピクチャー（ウィンドウは＋α）サイズに合わせる
        private void ResizeWindow(int pageNo)
        {
            if (pageNo > curData.pictureInfos.Count) return;
            if (curData.pictureInfos[pageNo - 1] == null) return;

            ImageConverter imgconv = new ImageConverter();
            Image img = (Image)imgconv.ConvertFrom(curData.pictureInfos[pageNo - 1].pictureData.Data);

            Size szForm = this.Size;
            Size szSplitWin = splitContainer1.Size;
 
            int ofsW = szForm.Width - szSplitWin.Width;
            int ofsH = szForm.Height - szSplitWin.Height;


            this.Width = img.Width + ofsW+ splitContainer1.Panel1.Width + splitContainer1.SplitterWidth;
            this.Height = img.Height + ofsH;

        }

        //private string GetDataFileName( string type)
        //{
        //    string filePath = Path.Combine( FormMain.GetExePath() , explanationFileDefName);
        //    IniFile iniFile = new IniFile(filePath);

        //    return iniFile.GetString("Setting", type);
        //}

         private void ShowPage(int pageNo)
        {
            if (curData == null) return;

            picExplanation.Image = null;
            if (pageNo > curData.pictureInfos.Count) return;
            if (curData.pictureInfos[pageNo - 1] == null) return;
            curPageNo = pageNo;

            if (curPageNo >= 0)
            {
                if (curData == null) return;
                ImageConverter imgconv = new ImageConverter();
                curImage = (Image)imgconv.ConvertFrom(curData.pictureInfos[pageNo - 1].pictureData.Data);
                picExplanation.Image = curImage;

                lblPage.Text = string.Format("{0}/{1}", curPageNo, curData.pictureInfos.Count);
            }
            else
            {
                picExplanation.Image = null;
                lblPage.Text = string.Format("{0}/{1}", 0,0);
            }
        }

        private void PageDown()
        {
            if (curData==null || curPageNo <= 1) return;

            ShowPage(curPageNo - 1);
        }

        private void PageUp()
        {
            if (curData == null || curPageNo >= curData.pictureInfos.Count) return;
            ShowPage(curPageNo + 1);
        }

        // "<"ボタン
        private void button2_Click(object sender, EventArgs e)
        {
            PageDown();
        }
        // ">"ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            PageUp();
        }
        // "|<"ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            ShowPage(1);

        }

        // ">|"ボタン
        private void button4_Click(object sender, EventArgs e)
        {
            ShowPage(curData.pictureInfos.Count);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //左キーが押されているか調べる
            if ((keyData & Keys.KeyCode) == Keys.Left)
            {
                button2_Click(null, null);
                //左キーの本来の処理（左側のコントロールにフォーカスを移す）を
                //させたくないときは、trueを返す
                return true;
            }
            //→キーが押されているか調べる
            else if ((keyData & Keys.KeyCode) == Keys.Right)
            {
                button1_Click(null, null);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void FormExplanation_Resize(object sender, EventArgs e)
        {
            UpdatePagingPanel();
         }

        private void UpdatePagingPanel()
        {
            //ページ切り替えコントロールパネル位置を左右中央に設定
            panel1.Left = (this.Width - panel1.Width) / 2;

        }

        // マウスホイールイベント  
        private void picExplanation_MouseWheel(object sender, MouseEventArgs e)
        {
            if(e.Delta<0)
            {
                PageUp();
            }
            else if( e.Delta>0)
            {
                PageDown();
            }
        }

        private void lstKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = (string)lstKeys.SelectedItem;
            if (string.IsNullOrEmpty(key)) return;

            SetCurrentExplanation(key, false);

        }

  
        private void FormExplanation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        button2_Click(null, null);
                        break;
                    case Keys.Right:
                        button1_Click(null, null);
                        break;
                }
            }

        }

        private void lstMainKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                e.Handled = true;
                return;
            }

            if ( e.KeyCode == Keys.Left)
            {
                e.Handled = true;
            }
            else if( e.KeyCode == Keys.Right)
            {
                SetActiveList(lstSubKey);

                e.Handled = true;
            }
        }

        private void lstSubKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.Left)
            {
                SetActiveList(lstMainKey);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SetActiveList(lstKeys);
                e.Handled = true;
            }

        }

        private void lstKeys_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.Left)
            {
                SetActiveList( lstSubKey );
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                e.Handled = true;
            }

        }

        private void SetActiveList( Control ctrl )
        {
            ActiveControl = ctrl;

            lstMainKey.BackColor = Color.White;
            lstSubKey.BackColor = Color.White;
            lstKeys.BackColor = Color.White;
            ctrl.BackColor = Color.LightYellow;

        }
    }
}

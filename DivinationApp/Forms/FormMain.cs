using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Configuration;


namespace DivinationApp
{
    public partial class FormMain : Form
    {

        TableMng tblMng = TableMng.GetTblManage();
        Persons personList = null;
        static string exePath = "";
        int tabId = -1;


        public ShortCutkeyMng shortCutKeyMng = new ShortCutkeyMng();
        public List<string> lstGouhouSanpouFilter = null;

        public DocumentManager docMng = new DocumentManager();


        List<ModelessBase> lstModlessForms = new List<ModelessBase>();
        FormFinder frmFinder = null;
        FormFinderCustom frmFinderCustom = null;
        FormExplanation frmExplanation = null;
        FormPDF frmPDF = null;


        static FormMain frmMain = null;

        public static string GetExePath() { return exePath; }
        public static FormMain GetFormMain() { return frmMain;  }

        public FormMain()
        {
            InitializeComponent();
            exePath = Path.GetDirectoryName(Application.ExecutablePath);

            docMng.Initialize(Path.Combine(exePath, "ExplanationData"));

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //大運、年運、月運 フィルタ文字列
            lstGouhouSanpouFilter = GetListFilterStrings();

            frmMain = this;

            shortCutKeyMng.LoadShortCutKey();

            //tabControl1.Dock = DockStyle.Fill;
            tabControl1.TabPages.Clear();
            tabControl1.onTabCloseButtonClick += OnTabCloseButtonClick;
        }


        public void addform(TabPage tp, Form f)
        {
            tp.SuspendLayout();

            f.TopLevel = false;
            //no border if needed
            f.FormBorderStyle = FormBorderStyle.None;
            f.AutoScaleMode = AutoScaleMode.Dpi;
            if (!tp.Controls.Contains(f))
            {
                tp.Controls.Add(f);
                f.Dock = DockStyle.Fill;
                f.Show();
            }
            tp.ResumeLayout();
           // Refresh();
        }


        private void FormMain_Load(object sender, EventArgs e)
        {

            personList = Persons.GetPersons();
            //setuiribiTbl = new SetuiribiTable();

            try
            {
                //節入り日テーブル読み込み
                tblMng.setuiribiTbl.ReadTable(exePath + @"\節入り日.xls");

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("節入り日テーブルが読み込めません。\n\n{0}", ex.Message));
                return;
            }



            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string lastDataFile = config.AppSettings.Settings[Const.CFGKEY_LAST_DATAFILE].Value;
            if (string.IsNullOrEmpty(lastDataFile))
            {
                lastDataFile = exePath + @"\名簿.xls";
            }

            if (File.Exists(lastDataFile))
            {
                //基本タブを追加
                AddBasicForm(lastDataFile);
            }

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (TabPage tp in tabControl1.TabPages)
            {
                ((Form)tp.Controls[0]).Close();
            }

        }

        private void OnModelessFormClose(ModelessBase frm)
        {
            lstModlessForms.Remove(frm);
            if (frm == frmFinderCustom) frmFinderCustom = null;
            if (frm == frmExplanation) frmExplanation = null;
            if (frm == frmPDF) frmPDF = null;
        }


        private void mnuAddTab_Click(object sender, EventArgs e)
        {
            FormAddTab frmAddTab = new FormAddTab(personList);
            if (frmAddTab.ShowDialog() == DialogResult.OK)
            {
                AddTab(frmAddTab.selectPerson);
            }
        }
        private void toolAdd_Click(object sender, EventArgs e)
        {
            mnuAddTab_Click(sender, e);
        }

        private void AddTab(Person person)
        {
            ++tabId;
            Form1 frm = new Form1(this, tabId, personList, person);
            frm.onCloseTab += OnTabClose;

            tabControl1.TabPages.Add(person.name +   "　　");
            tabControl1.TabPages[tabControl1.TabPages.Count - 1].Tag = tabId;

            addform(tabControl1.TabPages[tabControl1.TabPages.Count - 1], frm);
            tabControl1.SelectedTab = tabControl1.TabPages[tabControl1.TabPages.Count - 1];

        }

        private void OnTabCloseButtonClick(object sender, EventArgs e)
        {
           TabControlEx.EventCloseTab ev = (TabControlEx.EventCloseTab)e;

           tabControl1.TabPages.RemoveAt(ev.tabIndex );

        }

        private void OnTabClose( int tagId)
        {
            foreach( TabPage tp in tabControl1.TabPages)
            {
                if (tp.Tag == null) continue;
                if( (int)tp.Tag == tagId)
                {
                    tabControl1.TabPages.Remove(tp);
                    break;
                }
            }

        }


        public Form1 GetActiveForm()
        {
            var tab = tabControl1.SelectedTab;
            return (Form1)tab.Controls[0];

        }
        public Person GetActiveFormPerson()
        {
            var frm = GetActiveForm();
            return frm.GetCurrentPerson();

        }
        public Group GetActiveFormGroup()
        {
            var frm = GetActiveForm();
            return frm.GetCurrentGroup();

        }
        private void mnuSerch_Click(object sender, EventArgs e)
        {
            if(frmFinder!=null)
            {
                frmFinder.Show();
                frmFinderCustom.Activate();
                return;
            }
            //アクティブタブ
            var tab = tabControl1.SelectedTab;
            Form1 frm = (Form1)tab.Controls[0];

            Person person = frm.GetCurrentPerson();
            Group group = frm.GetCurrentGroup();

            frmFinder = new FormFinder(this, group, person);
            frmFinder.OnClose += OnFrmFinder_Close;
            frmFinder.Show();
            lstModlessForms.Add(frmFinder);
        }

        private void OnFrmFinder_Close(ModelessBase frm)
        {
            lstModlessForms.Remove(frm);
            if(frm == frmFinder)
                frmFinder = null;
        }

        private void toolFind_Click(object sender, EventArgs e)
        {
            mnuSerch_Click(sender, e);
        }

        /// <summary>
        /// 検索結果を選択したときの大運、年運表示連動
        /// </summary>
        /// <param name="person"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public void SelectFindResult(Person person ,int year, int month=Const.GetuunDispStartGetu)
        {

            //現在表示されている全タブにpersonに該当するタブがあるか？
            foreach(TabPage tp in tabControl1.TabPages)
            {
                Form1 frm = (Form1)tp.Controls[0];
                if (frm.GetCurrentPerson() == person)
                {
                    tabControl1.SelectedTab = tp;
                    DateTime dt = new DateTime(year, month, 1);
                    frm.DispDateView(dt);
                    return;
                }
            }
            //タブ表示されていないメンバーはタブを追加して表示
            AddTab(person);

        }
        public void SelectFindResult(Person person)
        {
            //現在表示されている全タブにpersonに該当するタブがあるか？
            foreach (TabPage tp in tabControl1.TabPages)
            {
                Form1 frm = (Form1)tp.Controls[0];
                if (person.classIdetify == PersonClassIdentity.Person)
                {
                    if (frm.GetCurrentPerson() == person)
                    {
                        tabControl1.SelectedTab = tp;
                        return;
                    }
                }else if(person.classIdetify == PersonClassIdentity.Birthday)
                {
                    Person p = frm.GetCurrentPerson();
                    if(p.classIdetify == person.classIdetify &&  p.birthday.birthday == person.birthday.birthday)
                    {
                        tabControl1.SelectedTab = tp;
                        return;
                    }

                }
            }
            //タブ表示されていないメンバーはタブを追加して表示
            AddTab(person);

        }

        //全検索
        private void mnuPatternCondFind_Click(object sender, EventArgs e)
        {
            toolFindCustom_Click(null, null);
        }
        private void toolFindCustom_Click(object sender, EventArgs e)
        {

            if (frmFinderCustom != null)
            {
                frmFinderCustom.Show();
                frmFinderCustom.Activate();
                return;
            }

            frmFinderCustom = new FormFinderCustom(this);
            frmFinderCustom.OnClose += OnModelessFormClose;
            frmFinderCustom.Show();
            lstModlessForms.Add(frmFinderCustom);
        }


        /// <summary>
        /// 名簿を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolOpen_Click(object sender, EventArgs e)
        {
            mnuOpen_Click(sender, e);
        }
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if( dlg.ShowDialog()==DialogResult.OK)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings[Const.CFGKEY_LAST_DATAFILE].Value = dlg.FileName;
                config.Save();

                //MainFormから表示されているモードレスダイアログを閉じる
                foreach(var frm in lstModlessForms)
                {
                    frm.Close();
                }
                lstModlessForms.Clear();

                //基本タブを追加
                AddBasicForm(dlg.FileName);



            }
        }
        private void AddBasicForm( string dataFile)
        {
            foreach(TabPage tp in tabControl1.TabPages)
            {
                ((Form)tp.Controls[0]).Close();
            }
            //全てのタブを閉じる
            tabControl1.TabPages.Clear();
            try
            {
                //名簿読み込み
                personList.ReadPersonList(dataFile);
                foreach (var person in personList.GetPersonList())
                {
                    //ユーザ情報初期設定
                    person.Init();

                }

                this.Text = string.Format("{0} : {1}", Const.ApplicationName, dataFile); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} \nが読み込めません。\n{1}", dataFile, ex.Message));
                return;
            }


            tabControl1.TabPages.Add("基本");

            Form1 frm = new Form1(this, 0, personList);
            frm.onCloseTab += OnTabClose;
            addform(tabControl1.TabPages[0], frm);
        }

        private void mnuExcelPicture_Click(object sender, EventArgs e)
        {
            FormExcelPictureTest frm = new FormExcelPictureTest();
            frm.ShowDialog();
        }


        public void ShowExplanation(string type, string key)
        {
            if (frmExplanation != null)
            {
                frmExplanation.Show( key);
                frmExplanation.Activate();
                return;
            }

            frmExplanation = new FormExplanation();
            frmExplanation.OnClose += OnModelessFormClose;
            frmExplanation.Show( key);
            lstModlessForms.Add(frmExplanation);
        }
        private void mnuDocViewer_Click(object sender, EventArgs e)
        {
            if (frmExplanation != null)
            {
                frmExplanation.Show();
                frmExplanation.Activate();
                return;
            }

            frmExplanation = new FormExplanation();
            frmExplanation.OnClose += OnModelessFormClose;
            frmExplanation.Show();
            lstModlessForms.Add(frmExplanation);
        }

        /// <summary>
        /// オプションメニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuOption_Click(object sender, EventArgs e)
        {
            FormOption frm = new FormOption();
            frm.ShowDialog();
        }
        /// <summary>
        /// PDF出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPDF_Click(object sender, EventArgs e)
        {
            if (frmPDF != null)
            {
                frmPDF.Show();
                frmPDF.Activate();
                return;
            }

            frmPDF = new FormPDF( personList);
            frmPDF.OnClose += OnModelessFormClose;
            frmPDF.Show();
            lstModlessForms.Add(frmPDF);


        }

        /// <summary>
        /// PDF出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolOutputPDF_Click(object sender, EventArgs e)
        {
            if (frmPDF != null)
            {
                frmPDF.Show();
                frmPDF.Activate();
                return;
            }

            frmPDF = new FormPDF( personList);
            frmPDF.OnClose += OnModelessFormClose;
            frmPDF.Show();
            lstModlessForms.Add(frmPDF);

        }

        /// <summary>
        /// ショートカットキー編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuShortCutKey_Click(object sender, EventArgs e)
        {
            FormShortCutKey frm = new FormShortCutKey(shortCutKeyMng);
            frm.ShowDialog();
        }

        /// <summary>
        /// 合法・散法 表示フィルタ編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuGouhouSanpouFilter_Click(object sender, EventArgs e)
        {
            FormGouhouSanpouFilter frm = new FormGouhouSanpouFilter();
            frm.ShowDialog();

        }
        public void UpdateFilter(List<string> lstFilter)
        {
            lstGouhouSanpouFilter = lstFilter;
            foreach (TabPage tp in tabControl1.TabPages)
            {
                Form1 frm2 = (Form1)tp.Controls[0];
                frm2.UpdateTaiunAndNenunAndGetuun();
            }

        }
        public List<string> GetListFilterStrings()
        {
            //合法・三法
            List<string> lstFilter = tblMng.gouhouSanpouTbl.GetGouhouSanpouItemNames();
            //七殺、干合
            lstFilter.Add(Const.sNanasatu);
            lstFilter.Add(Const.sKangou);
            //納音,準納音
            lstFilter.Add(Const.sNattin);
            lstFilter.Add(Const.sJunNattin);
            //律音,準律音
            lstFilter.Add(Const.sRittin);
            lstFilter.Add(Const.sJunRittin);
            //大半会
            lstFilter.Add(Const.sDaihankai);



            return lstFilter;
        }

        /// <summary>
        /// ドキュメントデータ再読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuReloadDoc_Click(object sender, EventArgs e)
        {
            docMng.Reload();
        }
    }
}

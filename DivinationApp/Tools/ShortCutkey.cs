using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DivinationApp
{
    public enum FunctionID
    {
        None = -1,
        KonkiHou = 1,           //根気法 画面表示
        Junisikanhou,           //十二親干法 画面表示
        ShugosinHou,            //守護神法 画面表示
        NenunHikaku,            //年運比較表 画面表示
        KyokiHenka,             //虚気 変化パターン 画面表示

        Gogyou_Sigou,           //五行(支合）
        Gogyou_Hankai,          //五行(半会）
        Gogyou_Kangou,          //五行(干合）
        Gogyou_Housani,         //五行(方三位）
        Gogyou_SangouKaikyoku,  //五行(三合会局）

        Gotoku_Sigou,           //五行(支合）
        Gotoku_Hankai,          //五行(半会）
        Gotoku_Kangou,          //五行(干合）
        Gotoku_Housani,         //五行(方三位）
        Gotoku_SangouKaikyoku,  //五行(三合会局）

        Disp_Housani,           //三合会局・方三位 表示
        Disp_Zougan,            //蔵元 表示
        Disp_Junisinkanhou,     //十二親干法 表示
    }

    public class ShortCutkeyMng
    {

 
        public List<ShortCutkey> lstShortCutkeys = new List<ShortCutkey>()
        {
#if DEBUG
            new ShortCutkey(FunctionID.KonkiHou,                "根気法 画面表示",      true, false, Keys.K),
            new ShortCutkey(FunctionID.Junisikanhou,            "十二親干法 画面表示",  true, false, Keys.J),
            new ShortCutkey(FunctionID.ShugosinHou,             "守護神法 画面表示",  true, false, Keys.S),
            new ShortCutkey(FunctionID.NenunHikaku,             "年運比較表 画面表示",  true, false, Keys.N),
            new ShortCutkey(FunctionID.KyokiHenka,              "虚気 変化パターン 画面表示",  true, false, Keys.Y),

            new ShortCutkey(FunctionID.Gogyou_Sigou,            "五行(支合）",  true, false, Keys.A),
            new ShortCutkey(FunctionID.Gogyou_Hankai,           "五行(半会）",  true, false, Keys.B),
            new ShortCutkey(FunctionID.Gogyou_Kangou,           "五行(干合）",  true, false, Keys.C),
#else
           new ShortCutkey(FunctionID.KonkiHou,                "根気法 画面表示",      true, false, Keys.None),
            new ShortCutkey(FunctionID.Junisikanhou,            "十二親干法 画面表示",  true, false, Keys.None),
            new ShortCutkey(FunctionID.ShugosinHou,             "守護神法 画面表示",  true, false, Keys.None),
            new ShortCutkey(FunctionID.NenunHikaku,             "年運比較表 画面表示",  true, false, Keys.None),
            new ShortCutkey(FunctionID.KyokiHenka,              "虚気 変化パターン 画面表示",  true, false, Keys.None),

            new ShortCutkey(FunctionID.Gogyou_Sigou,            "五行(支合）",  true, false, Keys.None),
            new ShortCutkey(FunctionID.Gogyou_Hankai,           "五行(半会）",  true, false, Keys.None),
            new ShortCutkey(FunctionID.Gogyou_Kangou,           "五行(干合）",  true, false, Keys.None),
#endif
            new ShortCutkey(FunctionID.Gogyou_Housani,          "五行(方三位）",  true, false, Keys.None),
            new ShortCutkey(FunctionID.Gogyou_SangouKaikyoku,   "五行(三合会局）",  true, false, Keys.None),
            new ShortCutkey(FunctionID.Gotoku_Sigou,            "五徳(支合）",  true, false, Keys.None),
            new ShortCutkey(FunctionID.Gotoku_Hankai,           "五徳(半会）",  true, false, Keys.None),
            new ShortCutkey(FunctionID.Gotoku_Kangou,           "五徳(干合）",  true, false, Keys.None),
            new ShortCutkey(FunctionID.Gotoku_Housani,          "五徳(方三位）",  true, false, Keys.None),
            new ShortCutkey(FunctionID.Gotoku_SangouKaikyoku,   "五徳(三合会局）",  true, false, Keys.None),
            new ShortCutkey(FunctionID.Disp_Housani,            "三合会局・方三位 表示",  true, false,Keys.None),
            new ShortCutkey(FunctionID.Disp_Zougan,             "蔵元 表示",  true, false, Keys.None),
            new ShortCutkey(FunctionID.Disp_Junisinkanhou,      "十二親干法 表示",  true, false, Keys.None),

        };

        public ShortCutkeyMng()
        {

        }

        public FunctionID GetFunction(KeyEventArgs e)
        {
            foreach( var item in lstShortCutkeys)
            {
                if (e.Control != item.Control) continue;
                if (e.Shift != item.Shift) continue;
                if (e.KeyCode != item.KeyCode) continue;

                return item.id;
            }

            return FunctionID.None;
        }

        public bool IsExist(ShortCutkey key)
        {
            foreach (var item in lstShortCutkeys)
            {
                if (key.Control != item.Control) continue;
                if (key.Shift != item.Shift) continue;
                if (key.KeyCode != item.KeyCode) continue;

                return true;
            }

            return false;
        }

        public List<ShortCutkey> GetShortCutKeys()
        {
            return lstShortCutkeys;
        }

        public int AddShortCutkey(FunctionID id ,bool Control, bool Shift, Keys KeyCode)
        {
            ShortCutkey item = lstShortCutkeys.Find(x => x.id == id );
            if(item==null)
            {
                item = new ShortCutkey(id);
            }
            item.Control = Control;
            item.Shift = Shift;
            item.KeyCode = KeyCode;

            return 0;
        }

        public int WriteShortCutKey()
        {
            string filePath = System.IO.Path.Combine(FormMain.GetExePath(), Const.shortCutKeyDefFileName);

            IniFile iniFile = new IniFile(filePath);

            foreach (var inf in lstShortCutkeys)
            {
                string entry = Enum.GetName(typeof(FunctionID), inf.id);

                iniFile.WriteString("ShortCutKey", entry, inf.ToString());
            }
            return 0;
        }

        public int LoadShortCutKey()
        {
            string filePath = System.IO.Path.Combine(FormMain.GetExePath(), Const.shortCutKeyDefFileName);

            IniFile iniFile = new IniFile(filePath);

            foreach (var inf in lstShortCutkeys)
            {
                string entry = Enum.GetName(typeof(FunctionID), inf.id);

                string param = iniFile.GetString("ShortCutKey", entry, "");
                if( string.IsNullOrEmpty(param))
                {
                    //inf.Clear();
                }else
                {
                    inf.Init(param);
                }
            }
            return 0;
        }

    }

    public class ShortCutkey
    {
        public ShortCutkey(FunctionID id, string title=null, bool Control=false, bool Shift=false, Keys KeyCode = Keys.None) 
        { 
            this.id = id;
            this.title = title;
            this.Control = Control;
            this.Shift = Shift;
            this.KeyCode = KeyCode;
        }
        public void Clear()
        {
            Control = false;
            Shift = false;
            KeyCode = Keys.None;
        }
        public void Init( string param)
        {
            var values = param.Split(',');
            title = values[0];
            Control = bool.Parse(values[1]);
            Shift = bool.Parse(values[2]);
            KeyCode = (Keys)Enum.Parse(typeof(Keys), values[3]);

        }
        public override string ToString()
        {
            return title + "," +
                   Control.ToString() + "," +
                   Shift.ToString() + "," +
                   KeyCode.ToString();
        }
        public FunctionID id;
        public string title;
        public bool Control;
        public bool Shift;
        public Keys KeyCode;
    }
}

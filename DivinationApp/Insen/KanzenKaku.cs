using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivinationApp
{
    /// <summary>
    ///完全格
    /// </summary>
    class Kanzenkaku
    {
        public class Result
        {
            public Result( string name, string subInfo=null)
            {
                this.name = name;
                this.subInfo = subInfo;
            }
            public string name;
            public string subInfo;
        }

        class JukanSiGogyouTbl
        {
            public JukanSiGogyouTbl(string kansi, string insenGogyo, bool bIsKan, Const.enumKansiItemID id)
            {
                this.kansi = kansi;
                this.insenGogyo = insenGogyo;
                this.bIsKan = bIsKan;
                this.id = id;

            }
            public string kansi;
            public string insenGogyo;
            public bool bIsKan;
            public Const.enumKansiItemID id;
        }
        class ResultInf
        {
            public string subName=null;
            public string shugosin = null;
        }

        private static Person person;
        public static List<Result> GetKanzenkaku(Person person)
        {
            Kanzenkaku.person = person;

            TableMng tblMng = TableMng.GetTblManage();
            List<Result> lstResult = new List<Result>();

            string[] kan = {person.nikkansi.kan,   person.gekkansi.kan,  person.nenkansi.kan };
            string[] si = {person.nikkansi.si,     person.gekkansi.si,   person.nenkansi.si    };
            Const.enumKansiItemID[] id = new Const.enumKansiItemID[]{ Const.enumKansiItemID.NIKKANSI, Const.enumKansiItemID.GEKKANSI, Const.enumKansiItemID.NENKANSI };

            List<JukanSiGogyouTbl> lstTbl = new List<JukanSiGogyouTbl>();

            for (int i = 0; i < 3; i++)
            {
                lstTbl.Add(new JukanSiGogyouTbl(kan[i], tblMng.jyukanTbl.GetGogyo(kan[i]), true, id[i]));//干
            }
            for (int i = 0; i < 3; i++)
            {
                lstTbl.Add(new JukanSiGogyouTbl(si[i], tblMng.jyunisiTbl.GetGogyo(si[i]), false, id[i]));//支
            }


            //通常
            var result = GetKanzenkaku_Sub( lstTbl);
            if (result != null) lstResult.InsertRange(lstResult.Count, result);


            var kansiAttrTbl = new TableMng.KansiAttrTblMng();


            Person cpyPerson = person.Clone();

            //支合
            if (cpyPerson.IsExistSigou())
            {
                cpyPerson.bRefrectSigou = true;
                cpyPerson.bRefrectHankai = false;
                cpyPerson.bRefrectKangou = false;
                kansiAttrTbl.CreateGogyouAttrMatrix(cpyPerson);
                AttrAndColorRefrectHelper.RefrectGouhou(cpyPerson, null, null, null, 6, kansiAttrTbl);
                MargeAttr(lstTbl, kansiAttrTbl);
                result = GetKanzenkaku_Sub( lstTbl, "支合");
                if (result != null) lstResult.InsertRange(lstResult.Count, result);
            }


            //半会
            if (cpyPerson.IsExistHankai())
            {
                cpyPerson.bRefrectSigou = false;
                cpyPerson.bRefrectHankai = true;
                cpyPerson.bRefrectKangou = false;
                kansiAttrTbl.CreateGogyouAttrMatrix(cpyPerson);
                AttrAndColorRefrectHelper.RefrectGouhou(cpyPerson, null, null, null, 6, kansiAttrTbl);
                MargeAttr(lstTbl, kansiAttrTbl);
                result = GetKanzenkaku_Sub( lstTbl, "半会");
                if (result != null) lstResult.InsertRange(lstResult.Count, result);
            }

            //干合
            if (cpyPerson.IsExistKangou())
            {
                cpyPerson.bRefrectSigou = false;
                cpyPerson.bRefrectHankai = false;
                cpyPerson.bRefrectKangou = true;
                kansiAttrTbl.CreateGogyouAttrMatrix(cpyPerson);
                AttrAndColorRefrectHelper.RefrectKangou(cpyPerson, null, null, null, kansiAttrTbl);
                MargeAttr(lstTbl, kansiAttrTbl);
                result = GetKanzenkaku_Sub( lstTbl, "干合");
                if (result != null) lstResult.InsertRange(lstResult.Count, result);
            }
            //支合、半会
            if (cpyPerson.IsExistSigou() && cpyPerson.IsExistHankai())
            {
                cpyPerson.bRefrectSigou = true;
                cpyPerson.bRefrectHankai = true;
                cpyPerson.bRefrectKangou = false;
                kansiAttrTbl.CreateGogyouAttrMatrix(cpyPerson);
                AttrAndColorRefrectHelper.RefrectGouhou(cpyPerson, null, null, null, 6, kansiAttrTbl);
                MargeAttr(lstTbl, kansiAttrTbl);
                result = GetKanzenkaku_Sub( lstTbl, "支合,半会");
                if (result != null) lstResult.InsertRange(lstResult.Count, result);
            }

            //支合、干合
            if (cpyPerson.IsExistSigou() && cpyPerson.IsExistKangou())
            {
                cpyPerson.bRefrectSigou = true;
                cpyPerson.bRefrectHankai = false;
                cpyPerson.bRefrectKangou = true;
                kansiAttrTbl.CreateGogyouAttrMatrix(cpyPerson);
                AttrAndColorRefrectHelper.RefrectGouhou(cpyPerson, null, null, null, 6, kansiAttrTbl);
                AttrAndColorRefrectHelper.RefrectKangou(cpyPerson, null, null, null, kansiAttrTbl);
                MargeAttr(lstTbl, kansiAttrTbl);
                result = GetKanzenkaku_Sub( lstTbl, "支合,干合");
                if (result != null) lstResult.InsertRange(lstResult.Count, result);
            }
            //半会、干合
            if (cpyPerson.IsExistHankai() && cpyPerson.IsExistKangou())
            {
                cpyPerson.bRefrectSigou = false;
                cpyPerson.bRefrectHankai = true;
                cpyPerson.bRefrectKangou = true;
                kansiAttrTbl.CreateGogyouAttrMatrix(cpyPerson);
                AttrAndColorRefrectHelper.RefrectGouhou(cpyPerson, null, null, null, 6, kansiAttrTbl);
                AttrAndColorRefrectHelper.RefrectKangou(cpyPerson, null, null, null, kansiAttrTbl);
                MargeAttr(lstTbl, kansiAttrTbl);
                result = GetKanzenkaku_Sub( lstTbl, "半会,干合");
                if (result != null) lstResult.InsertRange(lstResult.Count, result);
            }
            return lstResult;
        }

        static void MargeAttr(List<JukanSiGogyouTbl> lstTbl, TableMng.KansiAttrTblMng kansiAttrTbl)
        {
            //日干
            lstTbl[0].insenGogyo = kansiAttrTbl[(int)Const.enumKansiItemID.NIKKANSI].attrKan;
            //月干
            lstTbl[1].insenGogyo = kansiAttrTbl[(int)Const.enumKansiItemID.GEKKANSI].attrKan;
            //年干
            lstTbl[2].insenGogyo = kansiAttrTbl[(int)Const.enumKansiItemID.NENKANSI].attrKan;
            //日支
            lstTbl[3].insenGogyo = kansiAttrTbl[(int)Const.enumKansiItemID.NIKKANSI].attrSi;
            //月支
            lstTbl[4].insenGogyo = kansiAttrTbl[(int)Const.enumKansiItemID.GEKKANSI].attrSi;
            //年支
            lstTbl[5].insenGogyo = kansiAttrTbl[(int)Const.enumKansiItemID.NENKANSI].attrSi;
        }

        static List<Result> GetKanzenkaku_Sub( List<JukanSiGogyouTbl> lstTbl, string gogyouPattern=null)
        {
            List<Result> lstResult = new List<Result>();
            try
            {

                //一気格判定
                if (IsIkkiKaku("木", lstTbl)) { lstResult.Add(new Result("木性一気格(曲直格)")); return lstResult; }
                if (IsIkkiKaku("火", lstTbl)) { lstResult.Add(new Result("火性一気格(炎上格)")); return lstResult; }
                if (IsIkkiKaku("土", lstTbl)) { lstResult.Add(new Result("土性一気格(稼穡格)")); return lstResult; }
                if (IsIkkiKaku("金", lstTbl)) { lstResult.Add(new Result("金性一気格(従革格)")); return lstResult; }
                if (IsIkkiKaku("水", lstTbl)) { lstResult.Add(new Result("水性一気格(潤下格)")); return lstResult; }


                //印綬格判定
                if (IsInjuKaku(lstTbl)) { lstResult.Add(new Result("印綬格")); return lstResult; }


                //従化五格判定
                ResultInf resultInf = new ResultInf();
                if (IsJukaGoKaku( lstTbl, ref resultInf))
                {
                    string name = string.Format("{0}", resultInf.subName);
                    string subInf = null;
                    if (!string.IsNullOrEmpty(resultInf.shugosin))
                    {
                        subInf = string.Format("  {0}が破の守護神", resultInf.shugosin);
                    }
                    lstResult.Add(new Result(name, subInf));

                    return lstResult;
                }
            }
            finally
            {
                if (!string.IsNullOrEmpty(gogyouPattern))
                {
                    foreach (var item in lstResult)
                    {
                        item.name = gogyouPattern + "による" + item.name;
                    }

                }
            }

            return null;
        }

        /// <summary>
        /// 一気格判定
        /// </summary>
        /// <param name="attr">判定属性</param>
        /// <param name="insenGogyo">陰占の五行属性リスト</param>
        /// <returns></returns>
        static bool IsIkkiKaku(string attr, List<JukanSiGogyouTbl> lstTbl)
        {
            //指定されたattrと異なるものが１つもない（全てattr)
            if(lstTbl.FirstOrDefault(x=> x.insenGogyo != attr)==null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 印綬格判定
        /// </summary>
        /// <param name="insenGogyo">陰占の五行属性リスト</param>
        /// <returns></returns>
        static bool IsInjuKaku(List<JukanSiGogyouTbl> lstTbl)
        {
            TableMng tblMng = TableMng.GetTblManage();

            //日干の五行
            string nikkanAttr = lstTbl[0].insenGogyo;

            //日干の五行を生ずる五行
            string createFromAttr = tblMng.gogyouAttrRelationshipTbl.GetCreatFrom(nikkanAttr);

            //日干以外の干支が全て日干を相生するものか？
            for (int i = 1; i < lstTbl.Count; i++)
            {
                if (lstTbl[i].insenGogyo != createFromAttr) return false;
            }
            return true;
        }

        static bool IsJukaGoKaku( List<JukanSiGogyouTbl> lstTbl, ref ResultInf inf)
        {
            TableMng tblMng = TableMng.GetTblManage();

            //日干の五行
            string nikkanAttr = lstTbl[0].insenGogyo;

            //日干の五行を生ずる五行
            string createFromAttr = tblMng.gogyouAttrRelationshipTbl.GetCreatFrom(nikkanAttr);
            //日干の五行が生ずる五行
            string createToAttr = tblMng.gogyouAttrRelationshipTbl.GetCreatTo(nikkanAttr);
            //日干の五行を剋する五行
            string destroyFromAttr = tblMng.gogyouAttrRelationshipTbl.GetDestroyFrom(nikkanAttr);
            //日干の五行が剋する五行
            string destroyToAttr = tblMng.gogyouAttrRelationshipTbl.GetDestroyTo(nikkanAttr);

            List<int> lstNgAttrIndex = new List<int>();
            //■従生格
            //日干以外の干支が全て日干から生じられるものか？
            if (IsCheckJukanGokaku(lstTbl, createToAttr, null, ref lstNgAttrIndex)) { inf.subName = "従生格"; return true; }
            else if (lstNgAttrIndex.Count == 1) 
            {
                inf.subName = "従生格:一点破格";
                inf.shugosin = GetShugosin( lstTbl[lstNgAttrIndex[0]]);
                return true; 
            }

            //■従財格
            //日干以外の干支が全て日干から剋されるものか？
            if (IsCheckJukanGokaku(lstTbl, destroyToAttr, null, ref lstNgAttrIndex)) { inf.subName = "従財格"; return true; }
            else if (lstNgAttrIndex.Count == 1)
            {
                inf.subName = "従財格:一点破格";
                inf.shugosin = GetShugosin( lstTbl[lstNgAttrIndex[0]]);
                return true;
            }

            //■従生財格
            //日干以外の干支が全て日干から生じられるものまたは、剋されるもだけで構成されているか？
            if (IsCheckJukanGokaku(lstTbl, createToAttr, destroyToAttr, ref lstNgAttrIndex)) { inf.subName = "従生財格"; return true; }
            else if (lstNgAttrIndex.Count == 1)
            {
                inf.subName = "従生財格:一点破格";
                inf.shugosin = GetShugosin( lstTbl[lstNgAttrIndex[0]]);
                return true;
            }

            //■従官格
            //日干以外の干支が全て日干を剋するもので構成されるか？
            if (IsCheckJukanGokaku(lstTbl, destroyFromAttr, null, ref lstNgAttrIndex)) { inf.subName = "従官格"; return true; }
            else if (lstNgAttrIndex.Count == 1)
            {
                inf.subName = "従官格:一点破格";
                inf.shugosin = GetShugosin( lstTbl[lstNgAttrIndex[0]]);
                return true;
            }

            //■殺印相生格
            //日干以外の干支が全て日干から生じられるものまたは、剋されるもだけで構成されているか？
            if (IsCheckJukanGokaku(lstTbl, destroyFromAttr, createFromAttr, ref lstNgAttrIndex)) { inf.subName = "殺印相生格"; return true; }
            else if (lstNgAttrIndex.Count == 1)
            {
                inf.subName = "殺印相生格:一点破格";
                inf.shugosin = GetShugosin( lstTbl[lstNgAttrIndex[0]] );
                return true;
            }


            return false;
        }

        static string GetShugosin( JukanSiGogyouTbl tblItem)
        {
            if (tblItem.bIsKan) return tblItem.kansi;

            //支に一点破格の場合、その支の列の蔵元（本元、中元、初元の優先順位で守護神を決める
            Insen insen = new Insen(Kanzenkaku.person);
            int[] aryGensoType = (int[])Enum.GetValues(typeof(NijuhachiGenso.enmGensoType));

            string shugosin = "";

            for (int i= aryGensoType.Length-1; i>=0; i--)//本元, 中元, 初元
            {
                int idx = (int)aryGensoType[i];
                if (tblItem.id == Const.enumKansiItemID.NIKKANSI)//蔵元(日）
                {
                    string s = insen.nikkansiHongen[idx].name; 
                    if (!string.IsNullOrEmpty(s) )
                    {
                        shugosin = s;
                        break;
                    }
                }
                else if (tblItem.id == Const.enumKansiItemID.GEKKANSI)//蔵元(月）
                {
                    string s = insen.gekkansiHongen[idx].name;
                    if (!string.IsNullOrEmpty(s))
                    {
                        shugosin = s;
                        break;
                    }
                }
                else if (tblItem.id == Const.enumKansiItemID.NENKANSI)//蔵元(年
                {
                    string s = insen.nenkansiHongen[idx].name;
                    if (!string.IsNullOrEmpty(s))
                    {
                        shugosin = s;
                        break;
                    }
                }
           
            }
            return shugosin;

        }

        static bool IsCheckJukanGokaku(List<JukanSiGogyouTbl> lstTbl, string chkAttr1, string chkAttr2, ref List<int> lstNgAttrIndex)
        {
            lstNgAttrIndex.Clear();
            bool bResult = true;
            for (int i = 1; i < lstTbl.Count; i++)
            {
                string insenGogyo = lstTbl[i].insenGogyo;

                if (chkAttr2 == null)
                {
                    if (insenGogyo != chkAttr1)
                    {
                        lstNgAttrIndex.Add(i);
                    }
                }
                else
                {
                    if (insenGogyo != chkAttr1 && insenGogyo != chkAttr2)
                    {
                        lstNgAttrIndex.Add(i);
                    }

                }
            }
            return lstNgAttrIndex.Count==0?true:false;
        }


    }

}

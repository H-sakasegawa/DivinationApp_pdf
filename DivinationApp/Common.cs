using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace DivinationApp
{
    /// <summary>
    /// 共通処理
    /// </summary>
    public class Common
    {

        public delegate void CloseHandler(ModelessBase frm);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        private static extern short GetKeyState(int nVirtKey);

        /// <summary>
        /// 経過月数計算
        /// （流用）https://smdn.jp/programming/netfx/tips/calc_elapsed_years/
        /// </summary>
        /// <param name="baseDay"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int GetElapsedMonths(DateTime baseDay, DateTime day)
        {
            if (day < baseDay)
                // 日付が基準日より前の場合は例外とする
                throw new ArgumentException();

            // 経過月数を求める(満月数を考慮しない単純計算)
            var elapsedMonths = (day.Year - baseDay.Year) * 12 + (day.Month - baseDay.Month);

            if (baseDay.Day <= day.Day)
                // baseDayの日部分がdayの日部分以上の場合は、その月を満了しているとみなす
                // (例:1月30日→3月30日以降の場合は満(3-1)ヶ月)
                return elapsedMonths;
            else if (day.Day == DateTime.DaysInMonth(day.Year, day.Month) && day.Day <= baseDay.Day)
                // baseDayの日部分がdayの表す月の末日以降の場合は、その月を満了しているとみなす
                // (例:1月30日→2月28日(平年2月末日)/2月29日(閏年2月末日)以降の場合は満(2-1)ヶ月)
                return elapsedMonths;
            else
                // それ以外の場合は、その月を満了していないとみなす
                // (例:1月30日→3月29日以前の場合は(3-1)ヶ月未満、よって満(3-1-1)ヶ月)
                return elapsedMonths - 1;
        }

        /// <summary>
        /// ラベル表示のBold設定
        /// </summary>
        /// <param name="label"></param>
        /// <param name="bBold"></param>
        public static void SetBold(Label label, bool bBold)
        {
            FontStyle fontStyle = FontStyle.Regular;
            if (bBold) fontStyle = FontStyle.Bold;

            label.Font = new Font(label.Font, fontStyle);
        }

        /// <summary>
        /// enumKansiItemID ⇒ 項目ビット情報変換
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int ConvEnumKansiItemIDToItemBit(Const.enumKansiItemID id)
        {
            switch (id)
            {
                case Const.enumKansiItemID.GETUUN: return Const.bitFlgGetuun;
                case Const.enumKansiItemID.NENUN: return Const.bitFlgNenun;
                case Const.enumKansiItemID.TAIUN: return Const.bitFlgTaiun;
                case Const.enumKansiItemID.NIKKANSI: return Const.bitFlgNiti;
                case Const.enumKansiItemID.GEKKANSI: return Const.bitFlgGetu;
                case Const.enumKansiItemID.NENKANSI: return Const.bitFlgNen;
            }
            return 0;
        }

        public static void SetGroupCombobox(Persons persons, ComboBox cmb, string selectGroup = null)
        {
            var groups = persons.GetGroups();
            cmb.Items.Clear();

            cmb.Items.Add(new Group("全て", Group.GroupType.ALL));

            foreach (var group in groups)
            {
                cmb.Items.Add(group);
            }
            if (string.IsNullOrEmpty(selectGroup) || selectGroup == "全て")
            {
                if (cmb.Items.Count > 0)
                {
                    cmb.SelectedIndex = 0;
                }
            }
            else
            {
                for (int i = 1; i < cmb.Items.Count; i++)
                {
                    if (((Group)cmb.Items[i]).groupName == selectGroup)
                    {
                        cmb.SelectedIndex = i;
                        break;
                    }
                }
            }


        }

        public class LvUnseiItems
        {
            public string title;
            public Kansi targetKansi;
            public Color colorTenchusatu = Color.Black;
            public bool bTenchusatu = false;
            public bool bShugosin = false;
            public bool bImigami = false;
            public bool bKyokiToukan = false;
            public int kyokiTargetBit = 0;  //虚気と判定された干支を指すビット
            public string kyokiTargetAtrr = null;  //虚気と判定された属性
            public string[] sItems = new string[Enum.GetValues(typeof(Const.ColUnseiLv)).Length];

        }

        public class TaiunItems : LvUnseiItems { }
        public class NenunGetuunItems : LvUnseiItems{}

        public static TaiunItems GetTaiunItem(Person person, string title, int kansiNo, int startNen )
        {
            TaiunItems item = new TaiunItems();
            TableMng tblMng = TableMng.GetTblManage();


            item.title = title;

            Kansi taiunKansi = person.GetKansi(kansiNo);
            item.targetKansi = taiunKansi;

            item.sItems[(int)Const.ColUnseiLv.COL_KANSI] = string.Format("{0}{1}", taiunKansi.kan, taiunKansi.si); //干支

            string judai = person.GetJudaiShusei(person.nikkansi.kan, taiunKansi.kan).name;
            string junidai = person.GetJunidaiShusei(person.nikkansi.kan, taiunKansi.si).name;

            //"星"を削除
            judai = judai.Replace("星", "");
            junidai = junidai.Replace("星", "");

            item.sItems[(int)Const.ColUnseiLv.COL_JUDAISHUSEI] = (judai); //十大主星
            item.sItems[(int)Const.ColUnseiLv.COL_JUNIDAIJUUSEI] = (junidai); //十二大従星

            int idxNanasatuItem = 0;

            //日
            GouhouSannpouResult[] gouhouSanpoui = person.GetGouhouSanpouEx(taiunKansi, person.nikkansi, null, null);
            string nanasatu = (person.IsNanasatu(taiunKansi, person.nikkansi, ref idxNanasatuItem) == true && idxNanasatuItem == 1) ? Const.sNanasatu : "";   //七殺
            string kangou = person.GetKangoStr(taiunKansi, person.nikkansi); //干合            
            item.sItems[(int)Const.ColUnseiLv.COL_GOUHOUSANPOU_NITI] = (Common.GetListViewItemString(gouhouSanpoui, kangou, nanasatu));

            //月
            gouhouSanpoui = person.GetGouhouSanpouEx(taiunKansi, person.gekkansi, null, null);
            nanasatu = (person.IsNanasatu(taiunKansi, person.gekkansi, ref idxNanasatuItem) == true && idxNanasatuItem == 1) ? Const.sNanasatu : "";   //七殺
            kangou = person.GetKangoStr(taiunKansi, person.gekkansi); //干合
            item.sItems[(int)Const.ColUnseiLv.COL_GOUHOUSANPOU_GETU] = (Common.GetListViewItemString(gouhouSanpoui, kangou, nanasatu));

            //年
            gouhouSanpoui = person.GetGouhouSanpouEx(taiunKansi, person.nenkansi, null, null);
            nanasatu = (person.IsNanasatu(taiunKansi, person.nenkansi, ref idxNanasatuItem) == true && idxNanasatuItem == 1) ? Const.sNanasatu : "";   //七殺
            kangou = person.GetKangoStr(taiunKansi, person.nenkansi); //干合
            item.sItems[(int)Const.ColUnseiLv.COL_GOUHOUSANPOU_NEN] = (Common.GetListViewItemString(gouhouSanpoui, kangou, nanasatu));

            //--------------------------------
            //詳細
            //--------------------------------
            GetDetailInfo( person, taiunKansi,  item);

            //List<string> lstDetail = new List<string>();
            //string detail = "";
            ////虚気透干
            //KyokiToukan kyokiTokan = new KyokiToukan();
            //KyokiToukan.KyokiChkResult result = kyokiTokan.IsKyokiTokan_Koutenun(
            //                                    person,
            //                                    taiunKansi,
            //                                    null,
            //                                    null,
            //                                    null, 0,
            //                                    null, 0,
            //                                    Const.bitFlgTaiun);
            //if(result!=null)
            //{
            //    lstDetail.Add("虚気");
            //    item.bKyokiToukan = true;
            //    item.kyokiTargetAtrr = result.kyokiAttr;
            //    item.kyokiTargetBit = result.kyokiItemBit;
            //}

            // //争財、争母、争官
            //if (!string.IsNullOrEmpty(JozaiJoboJokan.GetJouzai(person, taiunKansi.kan)))
            //{
            //    lstDetail.Add("争財");
            //}
            //if (!string.IsNullOrEmpty(JozaiJoboJokan.GetJoubo(person, taiunKansi.kan)))
            //{
            //    lstDetail.Add("争母");
            //}
            //if (!string.IsNullOrEmpty(JozaiJoboJokan.GetJoukan(person, taiunKansi.kan)))
            //{
            //    lstDetail.Add("争官");
            //}

            //item.sItems[(int)Const.ColUnseiLv.COL_DETAIL] = string.Join(", ", lstDetail); 

            ////天中殺
            //Color color = Color.Black;
            //foreach( var tenchusatu in person.nikkansi.tenchusatu.ToArray() )
            //{
            //    //支に天中殺文字があるか？
            //    //IsExist()では、干と支で同じものがあるかをチェックしている。
            //    //tenchusatuには支の文字しかこないので、この関数でチェックしてもOK
            //    if (taiunKansi.IsExist(tenchusatu))
            //    {
            //        item.bTenchusatu = true;
            //        color = Color.Red;
            //        break;
            //    }
            //}

            //item.colorTenchusatu = color;

            ////干、支の属性取得
            //string kanAttr = tblMng.jyukanTbl[taiunKansi.kan].gogyou;
            //string siAttr = tblMng.jyunisiTbl[taiunKansi.si].gogyou;


            ////守護神判定
            //item.bShugosin = ShugosinUtil.IsShugosin(person, taiunKansi.kan);


            ////item.bShugosin = false;
            ////if (shugosinAttr.Count>0)
            ////{
            ////    foreach (var shugosin in shugosinAttr)
            ////    {
            ////        //if (kanAttr == shugosinAttr || siAttr == shugosinAttr)
            ////        if (kanAttr == shugosin.gogyouAttr) //干のみ　支は見ない
            ////        {
            ////            item.bShugosin = true;
            ////        }
            ////    }
            ////}
            ////else
            ////{
            ////    if (shugosinKan != null)
            ////    {
            ////        foreach (var kan in shugosinKan)
            ////        {
            ////            if (kan == taiunKansi.kan)
            ////            {
            ////                item.bShugosin = true;
            ////            }
            ////        }
            ////    }
            ////}
            ////忌神判定
            //item.bImigami = ShugosinUtil.IsImigami(person, taiunKansi.kan);

            ////item.bImigami = false;
            ////foreach (var imigami in imigamiAttr)
            ////{
            ////    //if (kanAttr == imigamiAttr || siAttr == imigamiAttr)
            ////    if (kanAttr == imigami.gogyouAttr) //干のみ　支は見ない
            ////    {
            ////        item.bImigami = true;
            ////    }
            ////}

            return item;

        }

        private static void GetDetailInfo(Person person , Kansi kansi, LvUnseiItems item)
        {
            TableMng tblMng = TableMng.GetTblManage();
            //--------------------------------
            //詳細
            //--------------------------------
            List<string> lstDetail = new List<string>();
             //虚気透干
            KyokiToukan kyokiTokan = new KyokiToukan();
            KyokiToukan.KyokiChkResult result = kyokiTokan.IsKyokiTokan_Koutenun(
                                                person,
                                                kansi,
                                                null,
                                                null,
                                                null, 0,
                                                null, 0,
                                                Const.bitFlgTaiun);
            if (result != null)
            {
                lstDetail.Add("虚気");
                item.bKyokiToukan = true;
                item.kyokiTargetAtrr = result.kyokiAttr;
                item.kyokiTargetBit = result.kyokiItemBit;
            }

            //争財、争母、争官
            if (!string.IsNullOrEmpty(JozaiJoboJokan.GetJouzai(person, kansi.kan)))
            {
                lstDetail.Add("争財");
            }
            if (!string.IsNullOrEmpty(JozaiJoboJokan.GetJoubo(person, kansi.kan)))
            {
                lstDetail.Add("争母");
            }
            if (!string.IsNullOrEmpty(JozaiJoboJokan.GetJoukan(person, kansi.kan)))
            {
                lstDetail.Add("争官");
            }

            item.sItems[(int)Const.ColUnseiLv.COL_DETAIL] = string.Join(", ", lstDetail);

            //天中殺
            Color color = Color.Black;
            foreach (var tenchusatu in person.nikkansi.tenchusatu.ToArray())
            {
                //支に天中殺文字があるか？
                //IsExist()では、干と支で同じものがあるかをチェックしている。
                //tenchusatuには支の文字しかこないので、この関数でチェックしてもOK
                if (kansi.IsExist(tenchusatu))
                {
                    item.bTenchusatu = true;
                    color = Color.Red;
                    break;
                }
            }

            item.colorTenchusatu = color;

            //干、支の属性取得
            string kanAttr = tblMng.jyukanTbl[kansi.kan].gogyou;
            string siAttr = tblMng.jyunisiTbl[kansi.si].gogyou;


            //守護神判定
            item.bShugosin = ShugosinUtil.IsShugosin(person, kansi.kan);


            //item.bShugosin = false;
            //if (shugosinAttr.Count>0)
            //{
            //    foreach (var shugosin in shugosinAttr)
            //    {
            //        //if (kanAttr == shugosinAttr || siAttr == shugosinAttr)
            //        if (kanAttr == shugosin.gogyouAttr) //干のみ　支は見ない
            //        {
            //            item.bShugosin = true;
            //        }
            //    }
            //}
            //else
            //{
            //    if (shugosinKan != null)
            //    {
            //        foreach (var kan in shugosinKan)
            //        {
            //            if (kan == taiunKansi.kan)
            //            {
            //                item.bShugosin = true;
            //            }
            //        }
            //    }
            //}
            //忌神判定
            item.bImigami = ShugosinUtil.IsImigami(person, kansi.kan);

            //item.bImigami = false;
            //foreach (var imigami in imigamiAttr)
            //{
            //    //if (kanAttr == imigamiAttr || siAttr == imigamiAttr)
            //    if (kanAttr == imigami.gogyouAttr) //干のみ　支は見ない
            //    {
            //        item.bImigami = true;
            //    }
            //}


        }

        public static int GetTaiunItemIndex(List<TaiunLvItemData> lstTaiunItemData, DateTime today)
        {
            List<int> lstStartYear = lstTaiunItemData.ConvertAll(x => x.startYear);
            return GetTaiunItemIndex(lstStartYear, today.Year, today.Month);


            //int year = today.Year;

            ////大運リストビューで年に該当する行を選択
            //for (int i = 0; i < lstTaiunItemData.Count; i++)
            //{

            //    TaiunLvItemData itemData = lstTaiunItemData[i];
            //    if (itemData.startYear > today.Year)
            //    {
            //        int index = i - 1;
            //        if (index < 0) index = 0;

            //        itemData = lstTaiunItemData[index];
            //        if (itemData.startYear == year)
            //        {
            //            //１月の場合、前年を表示する必要がある
            //            if (today.Month < Const.GetuunDispStartGetu)
            //            {
            //                index--;
            //                if (index < 0) index = 0; //このチェックで引っかかることはない
            //            }
            //        }
            //        return index;

            //    }
            //}
            //return -1;

        }
        public static int GetTaiunItemIndex(List<int> lstStartYear, int year, int month)
        {

            //大運リストビューで年に該当する行を選択
            for (int i = 0; i < lstStartYear.Count; i++)
            {

                int startYear = lstStartYear[i];
                if (startYear > year)
                {
                    int index = i - 1;
                    if (index < 0) index = 0;

                    int startYear2 = lstStartYear[index];
                    if (startYear2 == year)
                    {
                        //１月の場合、前年を表示する必要がある
                        if (month < Const.GetuunDispStartGetu)
                        {
                            index--;
                            if (index < 0) index = 0; //このチェックで引っかかることはない
                        }
                    }
                    return index;

                }
            }
            return -1;

        }


        public static NenunGetuunItems GetNenunItems(
                Person person,
                int year,
                string title,
                Kansi targetKansi, //対象干支（年運）
                TaiunLvItemData taiunLvItemData
            )
        {
            var item =  GetNenunGetuunItems(person, title, targetKansi, taiunLvItemData, null, Const.bitFlgNenun);

            //経歴情報
            if (person.career != null)
            {
                item.sItems[(int)Const.ColUnseiLv.COL_CAREER] = person.career.GetLineString(year); //経歴
            }
            return item;

        }
        public static NenunGetuunItems GetNenunItems(
                Person person,
                string title,
                Kansi targetKansi, //対象干支（年運）
                Kansi taiunKansi
            )
        {
            TaiunLvItemData dummyData = new TaiunLvItemData();
            dummyData.kansi = taiunKansi;
            return GetNenunGetuunItems(person, title, targetKansi, dummyData, null, Const.bitFlgNenun);
        }
        public static NenunGetuunItems GetGetuunItems(
                Person person,
                string title,
                Kansi targetKansi, //対象干支（月運）
                TaiunLvItemData taiunLvItemData,
                GetuunNenunLvItemData nenunLvItemData
            )
        {
            return GetNenunGetuunItems(person, title, targetKansi, taiunLvItemData, nenunLvItemData, Const.bitFlgGetuun);
        }
        /// <summary>
        /// 年運、月運リスト表示データ取得
        /// </summary>
        /// <param name="person"></param>
        /// <param name="title"></param>
        /// <param name="targetkansiNo"></param>
        /// <param name="taiunKansi"></param>
        /// <returns></returns>
        public static NenunGetuunItems GetNenunGetuunItems(
                Person person, 
                string title,
                Kansi targetKansi,
                TaiunLvItemData taiunLvItemData,
                GetuunNenunLvItemData nenunLvItemData,
                int bitTarget
            )
        {
            NenunGetuunItems item = new NenunGetuunItems();
            //TableMng tblMng = TableMng.GetTblManage();

            //bool bKyokiTaiun = taiunLvItemData.bKyokiToukan;

            Kansi taiunKansi = taiunLvItemData.kansi;
            //Kansi nenunKansi = null;
            //string nenunKyokiAttr = null;
            //int nenunKyokiBit = 0;
            //if (bitTarget == Const.bitFlgNenun)
            //{
            //    nenunKansi = targetKansi;
            //}else{
            //    nenunKansi = nenunLvItemData.kansi;
            //    nenunKyokiAttr = nenunLvItemData.kyokiTargetAtrr;
            //    nenunKyokiBit = nenunLvItemData.kyokiTargetBit;
            //}

            //Kansi getuunKansi = null;
            //if (bitTarget == Const.bitFlgGetuun)
            //{
            //    getuunKansi = targetKansi;
            //}

            item.title = title;
            //Kansi targetKansi;
            //if (bitTarget == Const.bitFlgNenun)
            //{
            //    targetKansi = nenunKansi;
            //}
            //else
            //{
            //    targetKansi = getuunKansi;
            //}

            item.targetKansi = targetKansi;

            int idxNanasatuItem = 0;


            string judai = person.GetJudaiShusei(person.nikkansi.kan, targetKansi.kan).name;
            string junidai = person.GetJunidaiShusei(person.nikkansi.kan, targetKansi.si).name;


            item.sItems[(int)Const.ColUnseiLv.COL_KANSI] = string.Format("{0}{1}", targetKansi.kan, targetKansi.si); //干支

            //"星"を削除
            judai = judai.Replace("星", "");
            junidai = junidai.Replace("星", "");

            item.sItems[(int)Const.ColUnseiLv.COL_JUDAISHUSEI] = judai; //十大主星
            item.sItems[(int)Const.ColUnseiLv.COL_JUNIDAIJUUSEI] = junidai; //十二大従星

            //合法三法(日)
            GouhouSannpouResult[] gouhouSanpoui = person.GetGouhouSanpouEx(targetKansi, person.nikkansi, taiunKansi, targetKansi);
            string kangou = person.GetKangoStr(targetKansi, person.nikkansi); //干合            
            string nanasatu = (person.IsNanasatu(targetKansi, person.nikkansi, ref idxNanasatuItem) == true && idxNanasatuItem == 1) ? Const.sNanasatu : "";   //七殺
            item.sItems[(int)Const.ColUnseiLv.COL_GOUHOUSANPOU_NITI] = GetListViewItemString(gouhouSanpoui, kangou, nanasatu);

            //合法三法(月)
            gouhouSanpoui = person.GetGouhouSanpouEx(targetKansi, person.gekkansi, taiunKansi, targetKansi);
            kangou = person.GetKangoStr(targetKansi, person.gekkansi); //干合  
            nanasatu = (person.IsNanasatu(targetKansi, person.gekkansi, ref idxNanasatuItem) == true && idxNanasatuItem == 1) ? Const.sNanasatu : "";   //七殺
            item.sItems[(int)Const.ColUnseiLv.COL_GOUHOUSANPOU_GETU] = GetListViewItemString(gouhouSanpoui, kangou, nanasatu);

            //合法三法(年)
            gouhouSanpoui = person.GetGouhouSanpouEx(targetKansi, person.nenkansi, taiunKansi, targetKansi);
            kangou = person.GetKangoStr(targetKansi, person.nenkansi); //干合  
            nanasatu = (person.IsNanasatu(targetKansi, person.nenkansi, ref idxNanasatuItem) == true && idxNanasatuItem == 1) ? Const.sNanasatu : "";   //七殺
            item.sItems[(int)Const.ColUnseiLv.COL_GOUHOUSANPOU_NEN] = GetListViewItemString(gouhouSanpoui, kangou, nanasatu);

            //--------------------------------
            //詳細
            //--------------------------------
            GetDetailInfo(person, targetKansi, item);
            //List<string> lstDetail = new List<string>();
            //string detail = "";
            ////虚気透干
            //KyokiToukan kyokiTokan = new KyokiToukan();
            //KyokiToukan.KyokiChkResult result = kyokiTokan.IsKyokiTokan_Koutenun(
            //                            person,
            //                            taiunKansi,
            //                            nenunKansi,
            //                            getuunKansi,
            //                            taiunLvItemData.kyokiTargetAtrr,
            //                            taiunLvItemData.kyokiTargetBit,
            //                            nenunKyokiAttr,
            //                            nenunKyokiBit,
            //                            bitTarget
            //                            );
            //if(result!=null)
            //{
            //    lstDetail.Add( "虚気");
            //    item.bKyokiToukan = true;  //虚気
            //    item.kyokiTargetAtrr = result.kyokiAttr;
            //    item.kyokiTargetBit = result.kyokiItemBit;
            //}

            ////争財、争母、争官
            //if (!string.IsNullOrEmpty(JozaiJoboJokan.GetJouzai(person, taiunKansi.kan)) )
            //{
            //    lstDetail.Add("争財");
            //}
            //if (!string.IsNullOrEmpty(JozaiJoboJokan.GetJoubo(person, taiunKansi.kan)))
            //{
            //    lstDetail.Add("争母");
            //}
            //if (!string.IsNullOrEmpty(JozaiJoboJokan.GetJoukan(person, taiunKansi.kan)))
            //{
            //    lstDetail.Add("争官");
            //}

            //item.sItems[(int)Const.ColUnseiLv.COL_DETAIL] = string.Join(", ", lstDetail); 

            ////天中殺
            //item.colorTenchusatu = Color.Black;
            //foreach(var tenchusatu in person.nikkansi.tenchusatu.ToArray())
            //{
            //    //支に天中殺文字があるか？
            //    //IsExist()では、干と支で同じものがあるかをチェックしている。
            //    //tenchusatuには支の文字しかこないので、この関数でチェックしてもOK
            //    if (targetKansi.IsExist(tenchusatu))
            //    {
            //        item.bTenchusatu = true;
            //        item.colorTenchusatu = Color.Red;
            //        break;
            //    }
            //}


            ////干、支の属性取得
            ////string kanAttr = tblMng.jyukanTbl[targetKansi.kan].gogyou;
            ////string siAttr = tblMng.jyunisiTbl[targetKansi.si].gogyou;


            ////守護神判定
            //item.bShugosin = ShugosinUtil.IsShugosin(person, targetKansi.kan);
            ////item.bShugosin = false;
            ////if (shugosinAttr.Count>0)
            ////{
            ////    foreach (var shugosin in shugosinAttr)
            ////    {
            ////        //if (kanAttr == shugosinAttr || siAttr == shugosinAttr)
            ////        if (kanAttr == shugosin.gogyouAttr) //干のみ　支は見ない

            ////        {
            ////            item.bShugosin = true;
            ////        }
            ////    }
            ////}
            ////else
            ////{
            ////    if (choukouShugosinKan != null)
            ////    {
            ////        foreach (var kan in choukouShugosinKan)
            ////        {
            ////            if (kan == targetKansi.kan)
            ////            {
            ////                item.bShugosin = true;
            ////            }
            ////        }
            ////    }
            ////}
            ////忌神判定
            //item.bImigami = ShugosinUtil.IsImigami(person, targetKansi.kan);
            ////item.bImigami = false;
            ////foreach (var imigami in imigamiAttr)
            ////{
            ////    //if (kanAttr == imigamiAttr || siAttr == imigamiAttr)
            ////    if (kanAttr == imigami.gogyouAttr)//干のみ　支は見ない
            ////    {
            ////        item.bImigami = true;
            ////    }
            ////}

            return item;
        }

        public static  string GetListViewItemString(GouhouSannpouResult[] lstGouhouSanpouResult, params string[] ary)
        {
            string result = "";
            foreach (var item in lstGouhouSanpouResult)
            {
                if (!string.IsNullOrEmpty(result)) result += " ";
                if (item.bEnable) result += item.displayName;
                else result += string.Format("[{0}]", item.displayName);
            }
            foreach (var item in ary)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (!string.IsNullOrEmpty(result)) result += " ";
                    result += item;
                }
            }
            return result;
        }

        public static Font FindFont(
           GraphicsBase g,
           string longString,
           Size Room,
           Font PreferedFont
        )
        {
            if (string.IsNullOrEmpty(longString)) return PreferedFont;

            // you should perform some scale functions!!!
            SizeF RealSize = g.MeasureString(longString, PreferedFont);
            float HeightScaleRatio = Room.Height / RealSize.Height;
            float WidthScaleRatio = Room.Width / RealSize.Width;

            float ScaleRatio = (HeightScaleRatio < WidthScaleRatio)
               ? ScaleRatio = HeightScaleRatio
               : ScaleRatio = WidthScaleRatio;

            float ScaleFontSize = PreferedFont.Size * ScaleRatio;
            //基準となるフォントサイズより小さい場合は、新しいサイズのフォントを返す
            if (PreferedFont.Size< ScaleFontSize) return PreferedFont;

            return new Font(PreferedFont.FontFamily, ScaleFontSize);
        }

        //============
        // 指定キー押下状態調査メソッド
        // 指定のキーが押下状態か調査するメソッドです。
        // 第１引数: 調査対象のキーを示すKeys列挙体
        // 戻り値: 判定結果 true:押下 / false:非押下
        public static bool IsKeyLocked(System.Windows.Forms.Keys Key_Value)
        {
            // WindowsAPIで押下判定
            bool Key_State = (GetKeyState((int) Key_Value) & 0x80) != 0;
            return Key_State;
        
        }


        public static int CalcDoubleToIntSize(double value)
        {
            value += 0.4;
            return (int)Math.Round(value, MidpointRounding.AwayFromZero);

        }


        public static string GetExplanationDataFileName(string type)
        {
            string filePath = System.IO.Path.Combine(FormMain.GetExePath(), Const.explanationFileDefName);
            IniFile iniFile = new IniFile(filePath);

            return iniFile.GetString("Setting", type);
        }

        public static string TrimExplanationDataTargetKey( string dispTargetKey)
        {
            if (!string.IsNullOrEmpty(dispTargetKey))
            {
                //キー文字から"(～)"などを除外
                char[] splitKeys = new char[] { '(', ':', '：', '[' };
                int index = dispTargetKey.IndexOfAny(splitKeys);
                if (index >= 0)
                {
                    dispTargetKey = dispTargetKey.Substring(0, index).Trim();
                }
            }
            else
            {
                dispTargetKey = null;
            }

            return dispTargetKey;

        }


    }

}

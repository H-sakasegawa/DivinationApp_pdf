using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace DivinationApp
{
    class AttrAndColorRefrectHelper
    {


        /// <summary>
        /// "土"の数をカウント(宿命のみ）
        /// </summary>
        /// <returns></returns>
        static private int GetAttrDoCount(Person person )
        {
            var tblMng = TableMng.GetTblManage();

            //合法反映前の属性について"土"の数をカウント
            int cnt = 0;
            //日干支
            if (tblMng.jyukanTbl[person.nikkansi.kan].gogyou == Const.sGogyouDo) cnt++;
            if (tblMng.jyunisiTbl[person.nikkansi.si].gogyou == Const.sGogyouDo) cnt++;
            //月干支
            if (tblMng.jyukanTbl[person.gekkansi.kan].gogyou == Const.sGogyouDo) cnt++;
            if (tblMng.jyunisiTbl[person.gekkansi.si].gogyou == Const.sGogyouDo) cnt++;
            //年干支
            if (tblMng.jyukanTbl[person.nenkansi.kan].gogyou == Const.sGogyouDo) cnt++;
            if (tblMng.jyunisiTbl[person.nenkansi.si].gogyou == Const.sGogyouDo) cnt++;

            return cnt;
        }
        /// <summary>
        /// "土"の数をカウント(宿命と月運、年運、大運）
        /// </summary>
        /// <param name="kansiGetuun"></param>
        /// <param name="kansiNenun"></param>
        /// <param name="kansiTaiun"></param>
        /// <returns></returns>
        static private int GetAttrDoCount(Person person, Kansi kansiGetuun, Kansi kansiNenun, Kansi kansiTaiun)
        {
            var tblMng = TableMng.GetTblManage();

            //宿命の合法反映前の属性について"土"の数をカウント
            int cnt = GetAttrDoCount(person);
            //月運
            if (kansiGetuun != null)
            {
                if (tblMng.jyukanTbl[kansiGetuun.kan].gogyou == Const.sGogyouDo) cnt++;
                if (tblMng.jyunisiTbl[kansiGetuun.si].gogyou == Const.sGogyouDo) cnt++;
            }
            //年運
            if (tblMng.jyukanTbl[kansiNenun.kan].gogyou == Const.sGogyouDo) cnt++;
            if (tblMng.jyunisiTbl[kansiNenun.si].gogyou == Const.sGogyouDo) cnt++;
            //大運
            if (tblMng.jyukanTbl[kansiTaiun.kan].gogyou == Const.sGogyouDo) cnt++;
            if (tblMng.jyunisiTbl[kansiTaiun.si].gogyou == Const.sGogyouDo) cnt++;

            return cnt;
        }
        /// <summary>
        /// 合法反映 変換カラー反映
        /// </summary>
        /// <param name="colorNikkansi"></param>
        /// <param name="colorGekkansi"></param>
        /// <param name="colorNenkansi"></param>
        /// <param name="kansiItemNum"></param>
        public static void RefrectGouhou(Person person ,
                                    Color[] colorNikkansi,
                                    Color[] colorGekkansi, 
                                    Color[] colorNenkansi, 
                                    int kansiItemNum, 
                                    TableMng.KansiAttrTblMng kansiAttrTbl
            )
        {
            var tblMng = TableMng.GetTblManage();

            int idxSi = 1;

            //合法反映前の属性について"土"の数をカウント
            int cnt = GetAttrDoCount(person );
            bool bManyAttrDo = (kansiItemNum / 2 < cnt) ? true : false;

            //支合と半会はダブらない
            //================================================
            //支合
            //================================================
            if (person.bRefrectSigou)// 支合 反映指定あり
            {
                //日支 - 月支
                var gogyou = tblMng.sigouTbl.GetSigouAttr(person.nikkansi.si, person.gekkansi.si, bManyAttrDo);
                if (gogyou != null)
                {
                    if (colorNikkansi != null) colorNikkansi[idxSi] = tblMng.gogyouAttrColorTbl[gogyou];
                    if (colorGekkansi != null) colorGekkansi[idxSi] = tblMng.gogyouAttrColorTbl[gogyou];
                    kansiAttrTbl[(int)Const.enumKansiItemID.NIKKANSI].attrSi = gogyou;
                    kansiAttrTbl[(int)Const.enumKansiItemID.GEKKANSI].attrSi = gogyou;
                }
                //日支 - 年支
                gogyou = tblMng.sigouTbl.GetSigouAttr(person.nikkansi.si, person.nenkansi.si, bManyAttrDo);
                if (gogyou != null)
                {
                    if (colorNikkansi != null) colorNikkansi[idxSi] = tblMng.gogyouAttrColorTbl[gogyou];
                    if (colorNenkansi != null) colorNenkansi[idxSi] = tblMng.gogyouAttrColorTbl[gogyou];
                    kansiAttrTbl[(int)Const.enumKansiItemID.NIKKANSI].attrSi = gogyou;
                    kansiAttrTbl[(int)Const.enumKansiItemID.NENKANSI].attrSi = gogyou;
                }
                //月支 - 年支
                gogyou = tblMng.sigouTbl.GetSigouAttr(person.gekkansi.si, person.nenkansi.si, bManyAttrDo);
                if (gogyou != null)
                {
                    if (colorGekkansi != null) colorGekkansi[idxSi] = tblMng.gogyouAttrColorTbl[gogyou];
                    if (colorNenkansi != null) colorNenkansi[idxSi] = tblMng.gogyouAttrColorTbl[gogyou];
                    kansiAttrTbl[(int)Const.enumKansiItemID.GEKKANSI].attrSi = gogyou;
                    kansiAttrTbl[(int)Const.enumKansiItemID.NENKANSI].attrSi = gogyou;
                }
            }
            //================================================
            //半会
            //================================================
            if (person.bRefrectHankai)// 半会 反映指定あり
            {
                //日支 - 月支
                var gogyou = tblMng.hankaiTbl.GetGogyou(person.nikkansi.si, person.gekkansi.si);
                if (gogyou != null)
                {
                    if (colorNikkansi != null) colorNikkansi[idxSi] = tblMng.gogyouAttrColorTbl[gogyou];
                    if (colorGekkansi != null) colorGekkansi[idxSi] = tblMng.gogyouAttrColorTbl[gogyou];
                    kansiAttrTbl[(int)Const.enumKansiItemID.NIKKANSI].attrSi = gogyou;
                    kansiAttrTbl[(int)Const.enumKansiItemID.GEKKANSI].attrSi = gogyou;
                }
                //日支 - 年支
                gogyou = tblMng.hankaiTbl.GetGogyou(person.nikkansi.si, person.nenkansi.si);
                if (gogyou != null)
                {

                    if (colorNikkansi != null) colorNikkansi[idxSi] = tblMng.gogyouAttrColorTbl[gogyou];
                    if (colorNenkansi != null) colorNenkansi[idxSi] = tblMng.gogyouAttrColorTbl[gogyou];
                    kansiAttrTbl[(int)Const.enumKansiItemID.NIKKANSI].attrSi = gogyou;
                    kansiAttrTbl[(int)Const.enumKansiItemID.NENKANSI].attrSi = gogyou;
                }
                //月支 - 年支
                gogyou = tblMng.hankaiTbl.GetGogyou(person.gekkansi.si, person.nenkansi.si);
                if (gogyou != null)
                {
                    if (colorGekkansi != null) colorGekkansi[idxSi] = tblMng.gogyouAttrColorTbl[gogyou];
                    if (colorNenkansi != null) colorNenkansi[idxSi] = tblMng.gogyouAttrColorTbl[gogyou];
                    kansiAttrTbl[(int)Const.enumKansiItemID.GEKKANSI].attrSi = gogyou;
                    kansiAttrTbl[(int)Const.enumKansiItemID.NENKANSI].attrSi = gogyou;
                }
            }
        }

        /// <summary>
        /// 干合 カラー変化設定
        /// 日干支、月干支、日干支の色について干合のカラー設定
        /// </summary>
        /// <param name="colorNikkansi"></param>
        /// <param name="colorGekkansi"></param>
        /// <param name="colorNenkansi"></param>
        public static void RefrectKangou(Person person, 
                                  Color[] colorNikkansi, 
                                  Color[] colorGekkansi, 
                                  Color[] colorNenkansi, 
                                  TableMng.KansiAttrTblMng kansiAttrTbl)
        {
            var tblMng = TableMng.GetTblManage();

            int idxKan = 0;//<==[干][支]の[干]を見るので、idx=0
            //================================================
            //干合
            //================================================
            //日（干） - 月（干）
            var gogyou = tblMng.kangouTbl.GetKangouAttr(person.nikkansi.kan, person.gekkansi.kan);
            if (gogyou != null)
            {
                if (colorNikkansi != null) colorNikkansi[idxKan] = tblMng.gogyouAttrColorTbl[gogyou];
                if (colorGekkansi != null) colorGekkansi[idxKan] = tblMng.gogyouAttrColorTbl[gogyou];
                kansiAttrTbl[(int)Const.enumKansiItemID.NIKKANSI].attrKan = gogyou;
                kansiAttrTbl[(int)Const.enumKansiItemID.GEKKANSI].attrKan = gogyou;
            }
            //日（干） - 年（干）
            gogyou = tblMng.kangouTbl.GetKangouAttr(person.nikkansi.kan, person.nenkansi.kan);
            if (gogyou != null)
            {
                if (colorNikkansi != null) colorNikkansi[idxKan] = tblMng.gogyouAttrColorTbl[gogyou];
                if (colorNenkansi != null) colorNenkansi[idxKan] = tblMng.gogyouAttrColorTbl[gogyou];
                kansiAttrTbl[(int)Const.enumKansiItemID.NIKKANSI].attrKan = gogyou;
                kansiAttrTbl[(int)Const.enumKansiItemID.NENKANSI].attrKan = gogyou;
            }
            //月（干） - 年（干）
            gogyou = tblMng.kangouTbl.GetKangouAttr(person.gekkansi.kan, person.nenkansi.kan);
            if (gogyou != null)
            {
                if (colorGekkansi != null) colorGekkansi[idxKan] = tblMng.gogyouAttrColorTbl[gogyou];
                if (colorNenkansi != null) colorNenkansi[idxKan] = tblMng.gogyouAttrColorTbl[gogyou];
                kansiAttrTbl[(int)Const.enumKansiItemID.GEKKANSI].attrKan = gogyou;
                kansiAttrTbl[(int)Const.enumKansiItemID.NENKANSI].attrKan = gogyou;
            }
        }

    }
}

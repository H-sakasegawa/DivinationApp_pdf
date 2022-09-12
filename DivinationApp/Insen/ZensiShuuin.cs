using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivinationApp
{
    /// <summary>
    ///全支集印
    /// </summary>
    class ZensiShuuin
    {

        public static List<string> GetZensiShuuin(Person person)
        {

            List<string> lstResult = new List<string>();

            TableMng tblMng = TableMng.GetTblManage();

            string nitiKan = person.nikkansi.kan;

            string[] arySi = new string[] { person.nikkansi.si, person.gekkansi.si, person.nenkansi.si };

            //日干の属性
            string nitiAttr = tblMng.jyukanTbl.GetGogyo(nitiKan);
            //日干を生じるもの(五行属性）
            string createFromAttr = tblMng.gogyouAttrRelationshipTbl.GetCreatFrom(nitiAttr);

            //十二親干法の　父、子を取得
            var juniSinKanHou = new JuniSinKanHou();
            var node = juniSinKanHou.Create(person);

            
            //父親
            var father = node.parent.partnerMan.kan;
            string child;
            if (person.gender == Gender.WOMAN)
            {
                //子
                child = node.child.kan;
            }
            else
            {
                //子
                child = node.partnerWoman.child.kan;
            }




            //全地生母
            //日支、月支、年支に日干を生じるものが全て存在する？
            bool bFind = true;
            foreach( var si in arySi)
            {
                if(tblMng.jyunisiTbl.GetGogyo( si ) != createFromAttr)
                {
                    bFind = false;
                    break;
                }
            }
            if (bFind) lstResult.Add( "全地生母" );

            //全地生母(準)
            //蔵元の日、月、年のそれぞれに、日干を生じるものが存在する？
            Insen insen = new Insen(person);
            bFind = false;
            int bFlg = 0;
            foreach (var item in Enum.GetValues(typeof(NijuhachiGenso.enmGensoType)))//初元、中元、本元
            {
                int idx = (int)item;
                string s = insen.nikkansiHongen[idx].name; //蔵元(日）
                if (!string.IsNullOrEmpty(s) && tblMng.jyukanTbl.GetGogyo(s) == createFromAttr)
                {
                    bFlg |= Const.bitFlgNiti;
                }
                s = insen.gekkansiHongen[idx].name; //蔵元(月）
                if (!string.IsNullOrEmpty(s) && tblMng.jyukanTbl.GetGogyo(s) == createFromAttr)
                {
                    bFlg |= Const.bitFlgGetu;
                }
                s = insen.nenkansiHongen[idx].name; //蔵元(年）
                if (!string.IsNullOrEmpty(s) && tblMng.jyukanTbl.GetGogyo(s) == createFromAttr)
                {
                    bFlg |= Const.bitFlgNen;
                }
                if( bFlg == (Const.bitFlgNiti | Const.bitFlgGetu | Const.bitFlgNen) )
                {
                    bFind = true;
                    break;
                }
            }
            if (bFind) lstResult.Add("全地生母(準)");



            //全地生父
            //蔵元の日、月、年のそれぞれに、十二親干法の父に該当するものが存在する？
            bFind = false;
            bFlg = 0;

            foreach (var item in Enum.GetValues(typeof(NijuhachiGenso.enmGensoType)))//初元、中元、本元
            {
                int idx = (int)item;
                string s = insen.nikkansiHongen[idx].name; //蔵元(日）
                if (!string.IsNullOrEmpty(s) && s == father)
                {
                    bFlg |= Const.bitFlgNiti;
                }
                s = insen.gekkansiHongen[idx].name; //蔵元(月）
                if (!string.IsNullOrEmpty(s) && s == father)
                {
                    bFlg |= Const.bitFlgGetu;
                }
                s = insen.nenkansiHongen[idx].name; //蔵元(年）
                if (!string.IsNullOrEmpty(s) && s == father)
                {
                    bFlg |= Const.bitFlgNen;
                }
                if (bFlg == (Const.bitFlgNiti | Const.bitFlgGetu | Const.bitFlgNen))
                {
                    bFind = true;
                    break;
                }
            }
            if (bFind) lstResult.Add("全地生父");



            //全地生父(準)
            //蔵元の日、月、年のそれぞれに、十二親干の父に該当するものまたはそれと陰陽違いが存在する？
            bFind = false;
            string fatherInyou = tblMng.jyukanTbl.GetInyouOtherString(father);
            bFlg = 0;
            foreach (var item in Enum.GetValues(typeof(NijuhachiGenso.enmGensoType)))//初元、中元、本元
            {
                int idx = (int)item;
                string s = insen.nikkansiHongen[idx].name; //蔵元(日）
                if (!string.IsNullOrEmpty(s) && (s == father || s == fatherInyou))
                {
                    bFlg |= Const.bitFlgNiti;
                }
                s = insen.gekkansiHongen[idx].name; //蔵元(月）
                if (!string.IsNullOrEmpty(s) && (s == father || s == fatherInyou))
                {
                    bFlg |= Const.bitFlgGetu;
                }
                s = insen.nenkansiHongen[idx].name; //蔵元(年）
                if (!string.IsNullOrEmpty(s) && (s == father || s == fatherInyou))
                {
                    bFlg |= Const.bitFlgNen;
                }
                if (bFlg == (Const.bitFlgNiti | Const.bitFlgGetu | Const.bitFlgNen))
                {
                    bFind = true;
                    break;
                }
            }
            if (bFind) lstResult.Add("全地生父(準)");



            //全地生子
            //蔵元の日、月、年のそれぞれに、十二親干法の子に該当するものが存在する？
            bFind = false;
            bFlg = 0;

            foreach (var item in Enum.GetValues(typeof(NijuhachiGenso.enmGensoType)))//初元、中元、本元
            {
                int idx = (int)item;
                string s = insen.nikkansiHongen[idx].name; //蔵元(日）
                if (!string.IsNullOrEmpty(s) && s == child)
                {
                    bFlg |= Const.bitFlgNiti;
                }
                s = insen.gekkansiHongen[idx].name; //蔵元(月）
                if (!string.IsNullOrEmpty(s) && s == child)
                {
                    bFlg |= Const.bitFlgGetu;
                }
                s = insen.nenkansiHongen[idx].name; //蔵元(年）
                if (!string.IsNullOrEmpty(s) && s == child)
                {
                    bFlg |= Const.bitFlgNen;
                }
                if (bFlg == (Const.bitFlgNiti | Const.bitFlgGetu | Const.bitFlgNen))
                {
                    bFind = true;
                    break;
                }
            }
            if (bFind) lstResult.Add("全地生子");



            //全地生父(準)
            //蔵元の日、月、年のそれぞれに、十二親干の父に該当するものまたはそれと陰陽違いが存在する？
            bFind = false;
            string childInyou = tblMng.jyukanTbl.GetInyouOtherString(child);
            bFlg = 0;
            foreach (var item in Enum.GetValues(typeof(NijuhachiGenso.enmGensoType)))//初元、中元、本元
            {
                int idx = (int)item;
                string s = insen.nikkansiHongen[idx].name; //蔵元(日）
                if (!string.IsNullOrEmpty(s) && (s == child || s == childInyou))
                {
                    bFlg |= Const.bitFlgNiti;
                }
                s = insen.gekkansiHongen[idx].name; //蔵元(月）
                if (!string.IsNullOrEmpty(s) && (s == child || s == childInyou))
                {
                    bFlg |= Const.bitFlgGetu;
                }
                s = insen.nenkansiHongen[idx].name; //蔵元(年）
                if (!string.IsNullOrEmpty(s) && (s == child || s == childInyou))
                {
                    bFlg |= Const.bitFlgNen;
                }
                if (bFlg == (Const.bitFlgNiti | Const.bitFlgGetu | Const.bitFlgNen))
                {
                    bFind = true;
                    break;
                }
            }
            if (bFind) lstResult.Add("全地生子(準)");

            //全地配偶者
            //蔵元の日、月、年のそれぞれに、日干と干合の関係のものが存在する？
            bFind = false;
            bFlg = 0;
            //日干と干合するもの
            string kangou = tblMng.kangouTbl.GetKangouOtherStr(nitiKan);

            foreach (var item in Enum.GetValues(typeof(NijuhachiGenso.enmGensoType)))//初元、中元、本元
            {
                int idx = (int)item;
                string s = insen.nikkansiHongen[idx].name; //蔵元(日）
                if (!string.IsNullOrEmpty(s) && s == kangou)
                {
                    bFlg |= Const.bitFlgNiti;
                }
                s = insen.gekkansiHongen[idx].name; //蔵元(月）
                if (!string.IsNullOrEmpty(s) && s == kangou)
                {
                    bFlg |= Const.bitFlgGetu;
                }
                s = insen.nenkansiHongen[idx].name; //蔵元(年）
                if (!string.IsNullOrEmpty(s) && s == kangou)
                {
                    bFlg |= Const.bitFlgNen;
                }
                if (bFlg == (Const.bitFlgNiti | Const.bitFlgGetu | Const.bitFlgNen))
                {
                    bFind = true;
                    break;
                }
            }
            if (bFind) lstResult.Add("全地配偶者");

            //全地配偶者(準)
            //蔵元の日、月、年のそれぞれに、日干と干合または干合の陰陽違いのものが存在する？
            bFind = false;
            bFlg = 0;
            //日干と干合するものの陰陽違い
            string kangouInyou = tblMng.jyukanTbl.GetInyouOtherString(nitiKan);

            foreach (var item in Enum.GetValues(typeof(NijuhachiGenso.enmGensoType)))//初元、中元、本元
            {
                int idx = (int)item;
                string s = insen.nikkansiHongen[idx].name; //蔵元(日）
                if (!string.IsNullOrEmpty(s) && ( s == kangou || s == kangouInyou))
                {
                    bFlg |= Const.bitFlgNiti;
                }
                s = insen.gekkansiHongen[idx].name; //蔵元(月）
                if (!string.IsNullOrEmpty(s) && (s == kangou || s == kangouInyou))
                {
                    bFlg |= Const.bitFlgGetu;
                }
                s = insen.nenkansiHongen[idx].name; //蔵元(年）
                if (!string.IsNullOrEmpty(s) && (s == kangou || s == kangouInyou))
                {
                    bFlg |= Const.bitFlgNen;
                }
                if (bFlg == (Const.bitFlgNiti | Const.bitFlgGetu | Const.bitFlgNen))
                {
                    bFind = true;
                    break;
                }
            }
            if (bFind) lstResult.Add("全地配偶者(準)");


            return lstResult;
        }
    }

}

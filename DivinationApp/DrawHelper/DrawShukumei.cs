﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace DivinationApp
{

    /// <summary>
    /// 宿命 表示用クラス
    /// </summary>
    class DrawShukumei : IsouhouBase
    {

        public Point nikkansi;
        public Point gekkansi;
        public Point nenkansi;

        public int nikkansiCenterX;
        public int gekkansiCenterX;
        public int nenkansiCenterX;

        const int bitFlgNiti = 0x04;
        const int bitFlgGetu = 0x02;
        const int bitFlgNen = 0x01;

        bool bDispGogyou = false;
        bool bDispGotoku = false;

        /// <summary>
        /// 宿命 図描画
        /// </summary>
        /// <param name="person">人情報</param>
        /// <param name="pictureBox">描画先のピクチャーボックス</param>
        /// <param name="_bDispGogyou">true...五行反映</param>
        /// <param name="_bDispGotoku">true... 五徳反映</param>
        public DrawShukumei(
                            Person person, 
                            PictureBox pictureBox,
                            bool _bDispGogyou,
                            bool _bDispGotoku
            ) : base(person, pictureBox)
        {


            bDispGogyou = _bDispGogyou;
            bDispGotoku = _bDispGotoku;
        }

        /// <summary>
        /// 表示座標計算
        /// </summary>
        private void CalcCoord()
        {
            nikkansi.X = GetLineOffsetX(); 
            //nikkansi.Y = GetDrawArea().Height / 2 - rangeHeight;
            nikkansi.Y = (idxMtx + 1) * GetLineOffsetY() + 10;

            //PDF出力時に指定された描画開始位置オフセット（通常は0)
            nikkansi.X += (int)drawAreaOffset.X;
            nikkansi.Y += (int)drawAreaOffset.Y;

            nikkansiCenterX = nikkansi.X + rangeWidth / 2;

            gekkansi.X = nikkansi.X + rangeWidth;
            gekkansi.Y = nikkansi.Y;
            gekkansiCenterX = gekkansi.X + rangeWidth / 2;

            nenkansi.X = gekkansi.X + rangeWidth;
            nenkansi.Y = gekkansi.Y;
            nenkansiCenterX = nenkansi.X + rangeWidth / 2;

            drawTopKan = nikkansi.Y;
            drawTopSi = drawTopKan + rangeHeight;
            drawBottomStartY = drawTopSi + rangeHeight;


            //干支表示領域
            rectNikansiKan = new Rectangle(nikkansi.X, nikkansi.Y, rangeWidth, rangeHeight);
            rectNikansiSi = new Rectangle(nikkansi.X, drawTopSi, rangeWidth, rangeHeight);
            rectGekkansiKan = new Rectangle(gekkansi.X, gekkansi.Y, rangeWidth, rangeHeight);
            rectGekkansiSi = new Rectangle(gekkansi.X, drawTopSi, rangeWidth, rangeHeight);
            rectNenkansiKan = new Rectangle(nenkansi.X, nenkansi.Y, rangeWidth, rangeHeight);
            rectNenkansiSi = new Rectangle(nenkansi.X, drawTopSi, rangeWidth, rangeHeight);


        }



        /// <summary>
        /// 描画処理
        /// 本関数は、IsouhouBase::Draw()から呼び出されます
        /// </summary>
        /// <param name="g"></param>
        protected override void DrawItem(GraphicsBase g)
        {
            if (person == null) return;

            int dircUp = -1;
            int dircDown = +1;

            //陰陽
            //-------------------
            bool bInyouNitiGetsuKan = person.IsInyouNitiGetsuKan(); //干（日-月) の関係
            bool bInyouGetsuNenKan = person.IsInyouGetsuNenKan();//干（月-年) の関係
            bool bInyouNiItiNenKan = person.IsInyouNitiNenKan();//干（日-年) の関係
            int idxInyouNitiGetsuKan = SetMatrixUp(bInyouNitiGetsuKan, bitFlgNiti, (bitFlgNiti | bitFlgGetu));//日 - 月
            int idxInyouGetsuNenKan = SetMatrixUp(bInyouGetsuNenKan, bitFlgNen, (bitFlgGetu | bitFlgNen));//月 - 年
            int idxInyouNiItiNenKan = SetMatrixUp(bInyouNiItiNenKan, (bitFlgNiti | bitFlgNen), (bitFlgNiti | bitFlgNen));//日 - 年

            //干合
            //-------------------
            bool bKangouNitiGetsuKan = person.IsKangouNitiGetsuKan();//干合（日-月) の関係
            bool bKangouGetsuNenKan = person.IsKangoGetsuNenKan(); //干合（月-年) の関係
            bool bKangouNiItiNenKan = person.IsKangoNitiNenKan(); //干合（日-年) の関係
            int idxKangouNitiGetsuKan = SetMatrixUp(bKangouNitiGetsuKan, bitFlgNiti, (bitFlgNiti | bitFlgGetu));//日 - 月
            int idxKangouGetsuNenKan = SetMatrixUp(bKangouGetsuNenKan, bitFlgNen, (bitFlgGetu | bitFlgNen));//月 - 年
            int idxKangouNiItiNenKan = SetMatrixUp(bKangouNiItiNenKan, (bitFlgNiti | bitFlgNen), (bitFlgNiti | bitFlgNen));//日 - 年

            //七殺
            //-------------------
            bool bNanasatuNitGetu = person.IsNanasatuNitiGetuKan();
            bool bNanasatuGetuNen = person.IsNanasatuGetuNenKan();
            bool bNanasatuNitNen = person.IsNanasatuNitiNenKan();
            int idxNanasatuNitGetu = SetMatrixUp(bNanasatuNitGetu, bitFlgNiti, (bitFlgNiti | bitFlgGetu));//日 - 月
            int idxNanasatuGetuNen = SetMatrixUp(bNanasatuGetuNen, bitFlgNen, (bitFlgGetu | bitFlgNen));//月 - 年
            int idxNanasatuNitNen = SetMatrixUp(bNanasatuNitNen, (bitFlgNiti | bitFlgNen), (bitFlgNiti | bitFlgNen));//日 - 年

            //合法・散法
            //-------------------
            string[] gouhouSanpouNitiGetu = person.GetGouhouSanpouNitiGetu();
            string[] gouhouSanpouGetuNen = person.GetGouhouSanpouiGetuNen();
            string[] gouhouSanpouNitiNen = person.GetGouhouSanpouiNitiNen();

            //干支の上部に表示する情報の段数から干支表示基準座標を計算
            CalcCoord();

            Color[] colorNikkansi=null;
            Color[] colorGekkansi = null;
            Color[] colorNenkansi = null;

            Color[] colorNikkansiOrg = new Color[2];
            Color[] colorGekkansiOrg = new Color[2];
            Color[] colorNenkansiOrg = new Color[2];

            CreateGogyouAttrMatrix(person);

            if ( bDispGogyou)
            {   //五行色表示
                colorNikkansi = GetGogyouColor(person.nikkansi);
                colorGekkansi = GetGogyouColor(person.gekkansi);
                colorNenkansi =  GetGogyouColor(person.nenkansi);

            }
            else if( bDispGotoku)
            {   //五徳色表示
                string baseKan = person.nikkansi.kan;
                colorNikkansi = GetGotokuColor(baseKan, person.nikkansi, true);
                colorGekkansi = GetGotokuColor(baseKan, person.gekkansi);
                colorNenkansi = GetGotokuColor(baseKan, person.nenkansi);

            }

            if (colorNikkansi != null) colorNikkansi.CopyTo(colorNikkansiOrg, 0);
            if (colorGekkansi != null) colorGekkansi.CopyTo(colorGekkansiOrg, 0);
            if (colorNenkansi != null) colorNenkansi.CopyTo(colorNenkansiOrg, 0);

            if (bDispGogyou | bDispGotoku)
            {
                if (person.bRefrectSigou || person.bRefrectHankai) //支合、半会 反映指定あり
                {
                    //合法反映
                    RefrectGouhou(colorNikkansi, colorGekkansi, colorNenkansi);
                }
                if (person.bRefrectKangou)
                {
                    //干合反映
                    RefrectKangou(colorNikkansi, colorGekkansi, colorNenkansi);
                }
            }

            //干支表示
            DrawKansi(person.nikkansi, rectNikansiKan, rectNikansiSi, colorNikkansi, Const.enumKansiItemID.NIKKANSI);
            DrawKansi(person.gekkansi, rectGekkansiKan, rectGekkansiSi, colorGekkansi, Const.enumKansiItemID.GEKKANSI);
            DrawKansi(person.nenkansi, rectNenkansiKan, rectNenkansiSi, colorNenkansi, Const.enumKansiItemID.NENKANSI);

            //ライン描画
            //陰陽
            //-------------------

            if (bInyouNitiGetsuKan)//日 - 月
            {
                DrawLine(idxInyouNitiGetsuKan, nikkansiCenterX, gekkansiCenterX, drawTopKan, dircUp);
                DrawString(idxInyouNitiGetsuKan, nikkansiCenterX, gekkansiCenterX, drawTopKan, dircUp, strInyou);
            }
            if (bInyouGetsuNenKan)//月 - 年
            {
                DrawLine(idxInyouGetsuNenKan, gekkansiCenterX, nenkansiCenterX, drawTopKan, dircUp);
                DrawString(idxInyouGetsuNenKan, gekkansiCenterX, nenkansiCenterX, drawTopKan, dircUp, strInyou);
            }
            if (bInyouNiItiNenKan)//日 - 年
            {
                DrawLine(idxInyouNiItiNenKan, nikkansiCenterX, nenkansiCenterX, drawTopKan, dircUp);
                DrawString(idxInyouNiItiNenKan, nikkansiCenterX, nenkansiCenterX, drawTopKan, dircUp, strInyou);
            }
            //干合
            //-------------------
            if (bKangouNitiGetsuKan)//日 - 月
            {
                DrawLine(idxKangouNitiGetsuKan, nikkansiCenterX, gekkansiCenterX, drawTopKan, dircUp);
                DrawString(idxKangouNitiGetsuKan, nikkansiCenterX, gekkansiCenterX, drawTopKan, dircUp, strKangou);
            }
            if (bKangouGetsuNenKan)//月 - 年
            {
                DrawLine(idxKangouGetsuNenKan, gekkansiCenterX, nenkansiCenterX, drawTopKan, dircUp);
                DrawString(idxKangouGetsuNenKan, gekkansiCenterX, nenkansiCenterX, drawTopKan, dircUp, strKangou);
            }
            if (bKangouNiItiNenKan)//日 - 年
            {
                DrawLine(idxKangouNiItiNenKan, nikkansiCenterX, nenkansiCenterX, drawTopKan, dircUp);
                DrawString(idxKangouNiItiNenKan, nikkansiCenterX, nenkansiCenterX, drawTopKan, dircUp, strKangou);
            }
            //七殺
            //-------------------
            if (bNanasatuNitGetu)//日 - 月
            {
                DrawLine(idxNanasatuNitGetu, nikkansiCenterX, gekkansiCenterX, drawTopKan, dircUp);
                DrawString(idxNanasatuNitGetu, nikkansiCenterX, gekkansiCenterX, drawTopKan, dircUp, strNanasatu);
            }
            if (bNanasatuGetuNen)//月 - 年
            {
                DrawLine(idxNanasatuGetuNen, gekkansiCenterX, nenkansiCenterX, drawTopKan, dircUp);
                DrawString(idxNanasatuGetuNen, gekkansiCenterX, nenkansiCenterX, drawTopKan, dircUp, strNanasatu);
            }
            if (bNanasatuNitNen)//日 - 年
            {
                DrawLine(idxNanasatuNitNen, nikkansiCenterX, nenkansiCenterX, drawTopKan, dircUp);
                DrawString(idxNanasatuNitNen, nikkansiCenterX, nenkansiCenterX, drawTopKan, dircUp, strNanasatu);
            }

            //合法・散法
            //-------------------
            int idxGouhouSanpouNitiGetu = SetMatrixDown((gouhouSanpouNitiGetu != null), bitFlgNiti, (bitFlgNiti | bitFlgGetu));//日 - 月
            int idxGouhouSanpouGetuNen = SetMatrixDown((gouhouSanpouGetuNen != null), bitFlgNen, (bitFlgGetu | bitFlgNen));//月 - 年
            int idxGouhouSanpouNitiNen = SetMatrixDown((gouhouSanpouNitiNen != null), (bitFlgNiti | bitFlgNen), (bitFlgNiti | bitFlgNen));//日 - 年

            if (gouhouSanpouNitiGetu != null && gouhouSanpouNitiGetu.Length > 0)
            {
                DrawLine(idxGouhouSanpouNitiGetu, nikkansiCenterX, gekkansiCenterX, drawBottomStartY, dircDown);
                DrawString(idxGouhouSanpouNitiGetu, nikkansiCenterX, gekkansiCenterX, drawBottomStartY, dircDown, gouhouSanpouNitiGetu);
            }
            if (gouhouSanpouGetuNen != null && gouhouSanpouGetuNen.Length > 0)
            {
                DrawLine(idxGouhouSanpouGetuNen, gekkansiCenterX, nenkansiCenterX, drawBottomStartY, dircDown);
                DrawString(idxGouhouSanpouGetuNen, gekkansiCenterX, nenkansiCenterX, drawBottomStartY, dircDown, gouhouSanpouGetuNen);
            }

            if (gouhouSanpouNitiNen != null && gouhouSanpouNitiNen.Length > 0)
            {
                DrawLine(idxGouhouSanpouNitiNen, nikkansiCenterX, nenkansiCenterX, drawBottomStartY, dircDown);
                DrawString(idxGouhouSanpouNitiNen, nikkansiCenterX, nenkansiCenterX, drawBottomStartY, dircDown, gouhouSanpouNitiNen);
            }
        }
    }


}

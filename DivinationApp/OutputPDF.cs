using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.fonts;

namespace DivinationApp
{

    class PDFOutput
    {
        Parameter param;
        Document doc;
        int hedderAreaH = 20;
        int lastDrawY = 0;
        public PdfContentByte contentByte;

        float areaLeft = 50;

        string fontName= "Meiryo UI";
        int fntH = 15;

        int writeCategoryNum = 0;

        DrawInsen drawInsen;

        PDFUtility pdfUtil;

        public class Parameter
        {
            public Person person;
            public bool bShukumeiAndKoutenun;
            public bool bKyoki;
            public bool bTaiunHyouAndNenunHyou;
            public bool bGetuunHyou;
            public bool bShugosinHou;
            public bool bKonkiHou;

            public int year;
            public int month;
            public bool bDispGetuun;
            public bool bGogyou;
            public bool bGotoku;
            public bool bRefrectSigou;
            public bool bRefrectHankai;
            public bool bRefrectKangou;
            public bool bRefrectHousani;
            public bool bRefrectSangouKaikyoku;

            public bool bSangouKaikyoku;
            public bool bZougan;
            public bool bJuniSinkanHou;

        }

        public PDFOutput(Parameter param)
        {
            this.param = param;

            pdfUtil = new PDFUtility();
        }
        public int WritePDF(string pdfFilePath)
        {

            // A4サイズで作成
            // Document pdfDoc = new Document(PageSize.A4);
            // 保存ファイルを指定
            //FileStream fileStream = new FileStream(@"C:\Temp\test.pdf", FileMode.Create);
            //PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, fileStream);
            //// PDFオープン
            //pdfDoc.Open();
            //PdfContentByte pdfContentByte = pdfWriter.DirectContent;
            ////フォントの設定
            //Font font =
            //    FontFactory.GetFont("ＭＳ ゴシック",
            //    BaseFont.IDENTITY_H,    //横書き
            //    BaseFont.NOT_EMBEDDED,  //フォントをPDFファイルに組み込まない（重要）
            //    11f,                    //フォントサイズ
            //    iTextSharp.text.Font.NORMAL,           //フォントスタイル
            //    BaseColor.BLACK);       //フォントカラー


            //ColumnText columnText = new ColumnText(pdfContentByte);
            ////SetSimpleColumnで出力
            //columnText.SetSimpleColumn(
            //    new Phrase("私は日本語しゃべれます", font)
            //    , 100   // 始点X座標
            //    , 600   // 始点Y座標
            //    , 250   // 終点X座標
            //    , 620   // 終点Y座標
            //    , 11f
            //    , Element.ALIGN_LEFT // 左寄せ
            //    );

            ////テキスト描画
            //columnText.Go();

            //PDFドキュメントを閉じる
            // pdfDoc.Close();





            pdfUtil.OpenDocument(pdfFilePath);

            ////ドキュメントを作成
            //doc = new Document(PageSize.A4, 0, 0, 0, 0);

            //FileStream fileStream = new FileStream(pdfFilePath, FileMode.Create);
            //PdfWriter pdfWriter = PdfWriter.GetInstance(doc, fileStream);



            ////Font fnt = FontFactory.GetFont("MS P明朝", 10f, BaseColor.BLACK);
            ////ドキュメントを開く
            //doc.Open();

            ////Font fnt1 = new Font(BaseFont.CreateFont
            ////    (@"c:\windows\fonts\msgothic.ttc,0", BaseFont.IDENTITY_H, true), 40);

            //////氏名：
            ////doc.Add(new Paragraph("こんにちは", fnt1));

            //contentByte = pdfWriter.DirectContent;


            ////https://helpx.adobe.com/jp/x-productkb/global/cq08041028.html
            //fontFolder = Environment.SystemDirectory.Replace("system32", "fonts");
            ////            fontName = fontFolder + "\\msgothic.ttc,0";
            //fontName = fontFolder + "\\meiryo.ttc,0";

            //人情報
            WritePersonBaseInfo();

            //陰占
            float lastY = 0;
            WriteInsen(30, 70, ref lastY);

            //陽占
            WriteYousen(areaLeft, lastY + 30, ref lastY);

            //十二支干法
            WriteJuNisiKanHou(areaLeft, lastY + 30, ref lastY);

            if (param.bShukumeiAndKoutenun)//宿命・後天運
            {
                if (writeCategoryNum!=0) pdfUtil.NewPage();
                //宿命
                WriteShukumei(areaLeft, 50, ref lastY);
                //後天運
                WriteKoutenUn(260, 50, ref lastY);
            }
            if (param.bShugosinHou)//守護神法
            {
                //                if (writeCategoryNum != 0) pdfUtil.NewPage();
            }
            if (param.bKonkiHou)//根気法
            {
                //                if (writeCategoryNum != 0) pdfUtil.NewPage();
            }

            if (param.bKyoki)   //虚気変化
            {
//                if (writeCategoryNum != 0) pdfUtil.NewPage();
            }

            if (param.bTaiunHyouAndNenunHyou)//大運表 & 年運表
            {
                if (writeCategoryNum != 0) pdfUtil.NewPage(PDFUtility.A4Dirc.Horizontal); //A4横
                List<TaiunLvItemData> lstTaiunItemData = new List<TaiunLvItemData>();
                WriteTaiunHyoun(areaLeft, 50, ref lastY, ref lstTaiunItemData);

                DateTime dt = new DateTime(param.year, param.month, 1); //GetTaiunItemIndex()では日を使用していなのでとりあえず1日を設定しておく
                int index = Common.GetTaiunItemIndex(lstTaiunItemData, dt);

                if (index >= 0)
                {
                    TaiunLvItemData taiunItem = lstTaiunItemData[index];

                    int year = param.year;
                    //年運リストビューで年に該当する行を選択
                    if (param.month < Const.GetuunDispStartGetu)
                    {
                        //月運で選択される月は、次の年度の月となるので、
                        //年運の選択を１年前に設定する必要がある。
                        year--;
                    }


                    List<GetuunNenunLvItemData> lstNenunItemData = new List<GetuunNenunLvItemData>();
                    WriteNenunHyou(areaLeft, lastY+30, year, taiunItem, ref lastY, ref lstNenunItemData);

                    if (param.bGetuunHyou)//月運表
                    {
                        if (writeCategoryNum != 0) pdfUtil.NewPage(PDFUtility.A4Dirc.Horizontal); //A4横

                        //年に該当する年運データ取得
                        GetuunNenunLvItemData nenunItemData = lstNenunItemData.Find(x => x.keyValue == year);
                        if (nenunItemData != null)
                        {
                            WriteGetuunHyou(areaLeft, 50, param.year, taiunItem, nenunItemData, ref lastY);
                        }
                    }
                }
            }

            //-------------------------------------------
            pdfUtil.CloseDocument();

            OpenFile(pdfFilePath);
            return 0;
        }

        void TEST_Y(float y)
        {
             pdfUtil.DrawLine(50, y, 500, y, 1, BaseColor.RED);

        }
        void DrawCategorySeparater(float y, BaseColor color = null)
        {
            if (color == null) color = BaseColor.BLACK;
            pdfUtil.DrawLine(areaLeft, y, areaLeft + 500, y, 1, color);
        }
        void DrawCategorySeparaterDot(float y, double[] dotPattern = null, BaseColor color = null)
        {
            if (dotPattern == null) dotPattern = new double[] { 2.0, 4.0 };
            if (color == null) color = BaseColor.BLACK;
            pdfUtil.DrawLine(areaLeft, y, areaLeft + 500, y, 1, color, dotPattern);
        }

        /// <summary>
        /// メンバ情報
        /// </summary>
        /// <returns></returns>
        int WritePersonBaseInfo()
        {
            writeCategoryNum++;
            Person person = param.person;

             //doc.Add(new Paragraph(string.Format("氏名    ：{0}", person.name), fnt));
             // doc.Add(new Paragraph(string.Format("生年月日：{0}", person.birthday.birthday), fnt));
             // doc.Add(new Paragraph(string.Format("性別    ：{0}", (person.gender== Gender.MAN?"男性":"女性"), fnt)));


             //フォント名
             pdfUtil.SetFontAndSize(fontName, fntH);

            //描画するテキスト
            int row = 1;
            pdfUtil.DrawString( 50, row * fntH,  "氏名　　：{0}", person.name); row++;
            pdfUtil.DrawString( 50, row * fntH,  "性別　　：{0}", (person.gender == Gender.MAN ? "男性" : "女性")); row++;
            pdfUtil.DrawString( 50, row * fntH,  "生年月日：{0}", person.birthday.birthday); row++;

            //     pdfUtil.DrawLine( new System.Drawing.Point(50, lastDrawY + 5), new System.Drawing.Point(300, lastDrawY + 5), 1, BaseColor.BLUE);

            //pdfUtil.DrawRectangle( new System.Drawing.Point(100, 100), new System.Drawing.Size(60, 90), BaseColor.RED);
            //pdfUtil.FillRectangle(new System.Drawing.Point(120, 120), new System.Drawing.Size(180, 180), BaseColor.RED, BaseColor.GREEN);


            return 0;
        }

        /// <summary>
        /// 陰占表示
        /// </summary>
        /// <param name="x">描画開始左上座標X</param>
        /// <param name="y">描画開始左上座標Y[</param>
        /// <param name="lastY">最大描画Y座標受け取り変数</param>
        /// <returns></returns>
        int WriteInsen(float x, float y, ref float lastY)
        {
            writeCategoryNum++;
            DrawCategorySeparaterDot(y);

            pdfUtil.SetFontAndSize(fontName, 15);
            drawInsen = new DrawInsen(
                                            param.person,
                                            null,
                                            true,
                                            true
                                     );
            drawInsen.DrawPDF(pdfUtil, x, y);

            lastY = ((PDFGraphics)(drawInsen.g)).pdfUtil.maxDrawY;

            //陰占特徴
            Insen insen = new Insen(param.person);
            List<InsenDetail> lstDetail = new List<InsenDetail>();

            float drawY = y + 20;
            insen.GetInsenDetailInfo(param.person, ref lstDetail);
            foreach(var item in lstDetail)
            {
                pdfUtil.DrawString(320, drawY, item.sText); 
                drawY += fntH;

            }
            if(lastY<drawY) lastY = drawY;
            return 0;
        }
        /// <summary>
        /// 陽占表示
        /// </summary>
        /// <param name="x">描画開始左上座標X</param>
        /// <param name="y">描画開始左上座標Y[</param>
        /// <param name="lastY">最大描画Y座標受け取り変数</param>
        /// <returns></returns>
        int WriteYousen(float x, float y, ref float lastY)
        {
            writeCategoryNum++;
            Person person = param.person;

            //  TEST_Y(y);
            DrawCategorySeparaterDot(y);

            y += 10;
            Yousen yousen = new Yousen(param.person);

            int[] junkanHouNo;
            bool bGogyoJunkan = yousen.GetJunkanHou(out junkanHouNo);

            float h = 30;
            float w = 80;
            //------------------
            //十大主星
            //------------------
            //干1 → 蔵x1
            string str = person.judaiShuseiA.name;
            if (junkanHouNo[0] > 0) str += string.Format("({0})", junkanHouNo[0]);
            pdfUtil.DrawString(x, y+h, drawInsen.colorNikkansiHongen.color, str);

            //干1 → 蔵x2
            str = person.judaiShuseiB.name;
            if (junkanHouNo[1] > 0) str += string.Format("({0})", junkanHouNo[1]);
            pdfUtil.DrawString(x + w, y + h, drawInsen.colorGekkansiHongen.color, str);

            //干1 → 蔵x3
            str = person.judaiShuseiC.name;
            if (junkanHouNo[2] > 0) str += string.Format("({0})", junkanHouNo[2]);
            pdfUtil.DrawString(x + w*2, y + h, drawInsen.colorNenkansiHongen.color, str);
            //干1 → 干3
            str = person.judaiShuseiD.name;
            if (junkanHouNo[3] > 0) str += string.Format("({0})", junkanHouNo[3]);
            pdfUtil.DrawString(x + w, y, drawInsen.colorNenkansiKan.color, str);
            //干1 → 干2
            str = person.judaiShuseiE.name;
            if (junkanHouNo[4] > 0) str += string.Format("({0})", junkanHouNo[4]);
            pdfUtil.DrawString(x + w, y + h*2, drawInsen.colorGekkansiKan.color, str);

            //------------------
            //十二大主星
            //------------------
            //干1 → 支3
            pdfUtil.DrawString(x + w * 2, y,         drawInsen.colorNenkansiSi.color, person.junidaiJuseiA.name);
            pdfUtil.DrawString(x + w * 2, y + h * 2, drawInsen.colorGekkansiSi.color, person.junidaiJuseiB.name);
            pdfUtil.DrawString(x        , y + h * 2, drawInsen.colorNikkansiSi.color, person.junidaiJuseiC.name);

            if (bGogyoJunkan)
            {
                pdfUtil.DrawString(x + w, y + h * 3, "(五行循環)");
            }

            lastY = y + h * 3;

            //陽占特徴
            float drawY = y ;
            List<YousenDetail> lstDetail = new List<YousenDetail>();
            yousen.GetYousennDetailInfo(lstDetail);

            foreach (var item in lstDetail)
            {
                pdfUtil.DrawString(320, drawY, item.sText);
                drawY += fntH;

            }
            if( lastY<drawY) lastY = drawY;

            return 0;
        }
        /// <summary>
        /// 十二支干法
        /// </summary>
        /// <param name="x">描画開始左上座標X</param>
        /// <param name="y">描画開始左上座標Y[</param>
        /// <param name="lastY">最大描画Y座標受け取り変数</param>
        /// <returns></returns>
        int WriteJuNisiKanHou(float x, float y, ref float lastY)
        {
            writeCategoryNum++;

            DrawCategorySeparaterDot(y);

            y += 10;
            pdfUtil.DrawString(x, y, "■十二支干法");
            y += 20;


            JuniSinKanHou juniSinKanHou = new JuniSinKanHou();
            DrawJuniSinKanhoun drawJuniSinKanhoun = new DrawJuniSinKanhoun();

            var node = juniSinKanHou.Create(param.person);

            drawJuniSinKanhoun.DrawPDF(pdfUtil, param.person, node,  x,  y);

            //if (lastY < drawY) lastY = drawY;

            return 0;
        }
        /// <summary>
        /// 宿命
        /// </summary>
        /// <param name="x">描画開始左上座標X</param>
        /// <param name="y">描画開始左上座標Y[</param>
        /// <param name="lastY">最大描画Y座標受け取り変数</param>
        /// <returns></returns>
        int WriteShukumei(float x, float y, ref float lastY)
        {
            writeCategoryNum++;
            //  TEST_Y(y);
            pdfUtil.DrawString(x, y, "■宿命");
            y += 20;
            Person person = param.person;

            DrawShukumei drawItem = new DrawShukumei(person, null,
                                                     param.bGogyou,
                                                     param.bGotoku,
                                                     param.bRefrectSigou
                                                     );
            pdfUtil.SaveState();
            pdfUtil.SetFontAndSize(fontName, 12);
            drawItem.DrawPDF(pdfUtil, x, y);
            pdfUtil.RestoreState();

            return 0;
        }
        /// <summary>
        /// 後天運
        /// </summary>
        /// <param name="x">描画開始左上座標X</param>
        /// <param name="y">描画開始左上座標Y[</param>
        /// <param name="lastY">最大描画Y座標受け取り変数</param>
        /// <returns></returns>
        int WriteKoutenUn(float x, float y, ref float lastY)
        {
            writeCategoryNum++;
            // TEST_Y(y);
            pdfUtil.DrawString(x, y, "■後天運");
            y += 20;

            Person person = param.person;

            Kansi taiunKansi = person.GetTaiunKansi(param.year);
            Kansi nenunKansi = person.GetNenkansi(param.year);
            Kansi getuunKansi = person.GetGekkansi(param.year, param.month);


            DrawKoutenUn drawItem = new DrawKoutenUn(param.person,
                                                    null,
                                                    taiunKansi,
                                                    nenunKansi,
                                                    getuunKansi,
                                                    true,
                                                    true,
                                                    param.bDispGetuun,
                                                    param.bSangouKaikyoku,
                                                    param.bGogyou,
                                                    param.bGotoku,
                                                    param.bZougan,
                                                    param.bJuniSinkanHou
               
                                                    );
            pdfUtil.SaveState();
  //          pdfUtil.SetFontAndSize(fontName, 12);

            drawItem.DrawPDF(pdfUtil, x, y);

            pdfUtil.RestoreState();

            return 0;
        }




        /// <summary>
        /// 大運表
        /// </summary>
        /// <param name="x">描画開始左上座標X</param>
        /// <param name="y">描画開始左上座標Y[</param>
        /// <param name="lastY">最大描画Y座標受け取り変数</param>
        /// <returns></returns>
        int WriteTaiunHyoun(float x, float y, ref float lastY, ref List<TaiunLvItemData> lstTaiunLvItemData)
        {
            writeCategoryNum++;
            // TEST_Y(y);
            pdfUtil.DrawString(x, y, "■大運表");
            y += 20;

            Person person = param.person;

            var lv = FormMain.GetFormMain().GetActiveForm().GetTaiunListView();
            DrawTableMng drawTableMng = new DrawTableMng( pdfUtil);
            DrawTableMng.Table tbl = drawTableMng.table;

            for (int i=0; i< lv.Columns.Count; i++)
            {
                tbl.AddColmun(lv.Columns[i].Text, lv.Columns[i].Width);
            }

            //大運表示用の干支リストを取得
            var lstTaiunKansi = person.GetTaiunKansiList();

            List<int> lstStartYear = lstTaiunKansi.ConvertAll(item => item.year);
            int selectYearIndex = Common.GetTaiunItemIndex(lstStartYear, param.year, param.month);


            for (int iRow = 0; iRow < lstTaiunKansi.Count; iRow++)
            {
                var kansiItem = lstTaiunKansi[iRow];
                string title;
                int startYear;
                if (iRow == 0)
                {
                    title = "初旬 0～";
                    startYear = 0;
                }
                else
                {
                    title = string.Format("{0}旬 {1}～", iRow, kansiItem.startYear);
                    startYear = kansiItem.startYear;
                }

                var item = Common.GetTaiunItem(person, title, kansiItem.kansiNo, startYear);
                var rowItem = tbl.AddRow( item.title );
                rowItem.foreColor = item.colorTenchusatu;
                rowItem.backColor = System.Drawing.Color.White;

                if (selectYearIndex == iRow)
                {
                    rowItem.backColor = System.Drawing.Color.Aquamarine;
                }


                for (int iCol = (int)Const.ColUnseiLv.COL_KANSI; iCol < item.sItems.Length; iCol++)
                {
                    var cell = rowItem.AddCell(item.sItems[iCol]);
                    if (iCol == 1)
                    {
                        if (item.bShugosin)
                        {
                            cell.backColor = Const.colorShugosin;
                        }
                        else if (item.bImigami)
                        {
                            cell.backColor = Const.colorImigami;
                        }
                    }
                }


                TaiunLvItemData itemData = new TaiunLvItemData();
                itemData.startNen = startYear;   //開始年
                itemData.startYear = startYear + person.birthday.year;
                itemData.kansi = item.targetKansi;    //干支
                itemData.bShugosin = item.bShugosin;  //守護神
                itemData.bImigami = item.bImigami;  //忌神
                itemData.bKyokiToukan = item.bKyokiToukan;  //虚気
                itemData.kyokiTargetAtrr = item.kyokiTargetAtrr;  //虚気となった属性
                itemData.kyokiTargetBit = item.kyokiTargetBit;  //虚気となった干支のビット
                itemData.bTenchusatu = item.bTenchusatu;
                //if (item.bShugosin)
                //{
                //    itemData.lstItemColors.Add(new LvItemColor(1, Const.colorShugosin));
                //}
                //else if (item.bImigami)
                //{
                //    itemData.lstItemColors.Add(new LvItemColor(1, Const.colorImigami));
                //}

                lstTaiunLvItemData.Add(itemData);
            }

            drawTableMng.DrawTable(x, y, tbl, ref lastY);

            return 0;
        }
        /// <summary>
        /// 年運表
        /// </summary>
        /// <param name="x">描画開始左上座標X</param>
        /// <param name="y">描画開始左上座標Y[</param>
        /// <param name="lastY">最大描画Y座標受け取り変数</param>
        /// <returns></returns>
        int WriteNenunHyou(float x, float y,int startNen, TaiunLvItemData taiunItemData, ref float lastY, ref List<GetuunNenunLvItemData> lstNnenunItemData)
        {
            writeCategoryNum++;
            // TEST_Y(y);
            pdfUtil.DrawString(x, y, "■年運表");
            y += 20;

            Person person = param.person;

            int baseYear = person.birthday.year + taiunItemData.startNen;

            var lv = FormMain.GetFormMain().GetActiveForm().GetNenunListView();
            DrawTableMng drawTableMng = new DrawTableMng(pdfUtil);
            DrawTableMng.Table tbl = drawTableMng.table;

            for (int i = 0; i < lv.Columns.Count; i++)
            {
                tbl.AddColmun(lv.Columns[i].Text, lv.Columns[i].Width);
            }

            int selectYear = param.year;
            //年運リストビューで年に該当する行を選択
            if (param.month < Const.GetuunDispStartGetu)
            {
                //月運で選択される月は、次の年度の月となるので、
                //年運の選択を１年前に設定する必要がある。
                selectYear--;
            }


            //年運表示用の干支リストを取得
            int nenkansiNo = person.GetNenkansiNo(baseYear, true);
            for (int iRow = 0; iRow < 10 + 1; iRow++)
            {
                //順行のみなので、60超えたら1にするだけ
                if (nenkansiNo > 60) nenkansiNo = 1;

                int year = baseYear + iRow;
                Kansi nenunKansi = person.GetKansi(nenkansiNo);
                var item = Common.GetNenunItems(person,
                                            year,
                                            string.Format("{0}歳({1})", (baseYear + iRow) - person.birthday.year, baseYear + iRow),
                                            nenunKansi,
                                            taiunItemData
                                            );

                var rowItem = tbl.AddRow(item.title);
                rowItem.foreColor = item.colorTenchusatu;
                rowItem.backColor = System.Drawing.Color.White;

                if (selectYear == year)
                {
                    rowItem.backColor = System.Drawing.Color.Aquamarine;
                }


                for (int iCol = (int)Const.ColUnseiLv.COL_KANSI; iCol < item.sItems.Length; iCol++)
                {
                    var cell = rowItem.AddCell(item.sItems[iCol]);
                    if (iCol == 1)
                    {
                        if (item.bShugosin)
                        {
                            cell.backColor = Const.colorShugosin;
                        }
                        else if (item.bImigami)
                        {
                            cell.backColor = Const.colorImigami;
                        }
                    }                    
                }
                

                GetuunNenunLvItemData itemData = new GetuunNenunLvItemData();
                itemData.keyValue = year;           //年
                itemData.kansi = item.targetKansi;    //干支
                itemData.bShugosin = item.bShugosin;  //守護神
                itemData.bImigami = item.bImigami;  //忌神
                itemData.bKyokiToukan = item.bKyokiToukan;  //虚気
                itemData.kyokiTargetAtrr = item.kyokiTargetAtrr;  //虚気となった属性
                itemData.kyokiTargetBit = item.kyokiTargetBit;  //虚気となった干支のビット
                itemData.bTenchusatu = item.bTenchusatu;
                //if (item.bShugosin)
                //{
                //    itemData.lstItemColors.Add(new LvItemColor(1, Const.colorShugosin));
                //}
                //else if (item.bImigami)
                //{
                //    itemData.lstItemColors.Add(new LvItemColor(1, Const.colorImigami));
                //}

                lstNnenunItemData.Add(itemData);

                nenkansiNo += 1;
            }

            drawTableMng.DrawTable(x, y, tbl, ref lastY);

            return 0;
        }

        /// <summary>
        /// 月運表
        /// </summary>
        /// <param name="x">描画開始左上座標X</param>
        /// <param name="y">描画開始左上座標Y[</param>
        /// <param name="lastY">最大描画Y座標受け取り変数</param>
        /// <returns></returns>
        int WriteGetuunHyou(float x, float y, int startNen, TaiunLvItemData taiunItemData, GetuunNenunLvItemData nenunItemData, ref float lastY)
        {
            writeCategoryNum++;
            // TEST_Y(y);
            pdfUtil.DrawString(x, y, "■月運表");
            y += 20;

            Person person = param.person;
            TableMng tblMng = TableMng.GetTblManage();

            var lv = FormMain.GetFormMain().GetActiveForm().GetGetuunListView();
            DrawTableMng drawTableMng = new DrawTableMng(pdfUtil);
            DrawTableMng.Table tbl = drawTableMng.table;

            for (int i = 0; i < lv.Columns.Count; i++)
            {
                tbl.AddColmun(lv.Columns[i].Text, lv.Columns[i].Width);
            }
            int year = nenunItemData.keyValue;

            //月運表示用の干支リストを取得
            //2月～12月,1月分を表示
            for (int i = 0; i < 12; i++)
            {
                int mMonth = Const.GetuunDispStartGetu + i;
                if (mMonth > 12)
                {
                    mMonth = (mMonth - 12);
                    year = nenunItemData.keyValue + 1;
                }

                //月干支番号取得(節入り日無視で単純月で取得）
                int gekkansiNo = tblMng.setuiribiTbl.GetGekkansiNo(year, mMonth);

                string title = string.Format("{0}月", mMonth);
                var rowItem = tbl.AddRow(title);

                Kansi getuunKansi = person.GetKansi(gekkansiNo);
                var item = Common.GetGetuunItems(person, title, getuunKansi, taiunItemData, nenunItemData);

                rowItem.foreColor = item.colorTenchusatu;
                rowItem.backColor = System.Drawing.Color.White;

                if (param.month == mMonth)
                {
                    rowItem.backColor = System.Drawing.Color.Aquamarine;
                }

                for (int iCol = (int)Const.ColUnseiLv.COL_KANSI; iCol < item.sItems.Length; iCol++)
                {
                    var cell = rowItem.AddCell(item.sItems[iCol]);
                    if (iCol == 1)
                    {
                        if (item.bShugosin)
                        {
                            cell.backColor = Const.colorShugosin;
                        }
                        else if (item.bImigami)
                        {
                            cell.backColor = Const.colorImigami;
                        }
                    }
                }

                gekkansiNo += 1;
            }

            drawTableMng.DrawTable(x, y, tbl, ref lastY);

            return 0;
        }



        void OpenFile(string fname)
        {
            System.Diagnostics.Process p =
                         System.Diagnostics.Process.Start(fname);
        }

    }

    //----------------------------------------------
    // 表描画クラス
    //----------------------------------------------
    public class DrawTableMng
    {

        public class Table
        {
            public List<ColItem> colItems = new List<ColItem>();
            public List<RowItem> rowItems = new List<RowItem>();

            public ColItem AddColmun(string title, float width)
            {
                ColItem colItem = new ColItem(title, width);
                colItems.Add(colItem);
                return colItem;
            }
            public RowItem AddRow(string title = "")
            {
                RowItem rowItem = new RowItem(this);
                rowItem.AddCell(title);

                rowItems.Add(rowItem);
                return rowItem;
            }

            public float GetColWidth(int iCol)
            {
                if (iCol >= colItems.Count) return 0;
                return colItems[iCol].width;
            }
            public ColItem GetColItem(int iCol)
            {
                if (iCol >= colItems.Count) return null;
                return colItems[iCol];
            }
        }
        public class ColItem
        {
            public ColItem(string title, float width, int align = Element.ALIGN_LEFT)
            {
                this.title = title;
                this.width = width;
                this.align = align;

            }
            public float width;
            public string title;
            public int align;
        }
        public class RowItem
        {
            Table parent;
            public System.Drawing.Color foreColor;
            public System.Drawing.Color backColor;
            public List<CellItem> lstCells = new List<CellItem>();

            public RowItem(Table parentTbl)
            {
                parent = parentTbl;
                foreColor = System.Drawing.Color.Black;
                backColor = System.Drawing.Color.White;
            }

            public CellItem AddCell(string str)
            {
                CellItem item = new CellItem(str);
                item.backColor = backColor;
                lstCells.Add(item);
                return item;
            }
        }
        public class CellItem
        {
            public string str;
           // public System.Drawing.Color foreColor; //文字食
            public System.Drawing.Color backColor; //背景色

            public CellItem(string str)
            {
                this.str = str;
            }
        }


        PDFUtility pdfUtil;

        public Table table = new Table();

        public DrawTableMng( PDFUtility pdfUtil)
        {
            this.pdfUtil = pdfUtil;
        }

        public void DrawTable(float x, float y, Table table, ref float lastY)
        {
            pdfUtil.SaveState();
            DrawTableHeader(x, y, table, ref lastY);

            DrawTableRow(x, lastY, table, ref lastY);

            pdfUtil.RestoreState();
        }

        void DrawTableHeader(float x, float y, Table table, ref float lastY)
        {

            int colX = (int)x;
            int colY = (int)y;
            float hederH = 15;

            pdfUtil.SetFontSize(10);
            //ヘッダー部
            for (int iCol = 0; iCol < table.colItems.Count; iCol++)
            {
                var colItem = table.colItems[iCol];
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
                rect.X = colX;
                rect.Y = colY;
                rect.Width = (int)colItem.width;
                rect.Height = (int)hederH;


                pdfUtil.FillRectangle(rect, System.Drawing.Color.Black, System.Drawing.Color.LightGray);
                pdfUtil.DrawString(rect, colItem.align, System.Drawing.Color.Black, table.colItems[iCol].title);


                colX += (int)colItem.width;
            }

            lastY = y + hederH;
        }


        void DrawTableRow(float x, float y, Table table, ref float lastY)
        {

            int colX = (int)x;
            int colY = (int)y;
            float rowH = 15;

            pdfUtil.SetFontSize(10);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
            rect.Height = (int)rowH;


            for (int iRow = 0; iRow < table.rowItems.Count; iRow++)
            {
                var rowItem = table.rowItems[iRow];
                rect.Y = colY;
                colX = (int)x;

                System.Drawing.Color foreColor = rowItem.foreColor;

                //グリッドセル
                for (int iCol = 0; iCol < table.colItems.Count; iCol++)
                {
                    var colItem = table.GetColItem(iCol);
                    if (colItem == null) break;
                    var cellItem = rowItem.lstCells[iCol];
                    rect.X = colX;
                    rect.Width = (int)colItem.width;
                    int align = colItem.align;

                    pdfUtil.FillRectangle(rect, System.Drawing.Color.Black, cellItem.backColor);

                    pdfUtil.DrawString(rect, align, foreColor, cellItem.str);


                    colX += (int)colItem.width;
                }
                colY += (int)rowH;

            }
            lastY = colY;
        }
    }

    public  class PDFUtility
    {
        Document doc;
        public float lastDrawY = 0;
        public float maxDrawY = 0;
        PdfContentByte cb;
        float fontSize = 0;
        string fontName;
        Font font;
        Stack<float> stackFontSize = new Stack<float>();
        Rectangle tatePageRect;
        Rectangle yokoPageRect;

        public enum A4Dirc
        {
            Vertical = 0 , //縦
            Horizontal,     //横
        }

        public PDFUtility()
        {
        }
        public int OpenDocument( string filePath)
        {
            doc = new Document(PageSize.A4);

            tatePageRect = doc.PageSize;
            yokoPageRect = tatePageRect.Rotate();

            //Fontフォルダを指定
            FontFactory.RegisterDirectory(Environment.SystemDirectory.Replace("system32", "fonts"));
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, fileStream);


            //ドキュメントを開く
            doc.Open();

            cb = pdfWriter.DirectContent;

            return 0;
        }

        public void CloseDocument()
        {
            //ドキュメントを閉じる
            doc.Close();
        }

        public void NewPage(A4Dirc dirc = A4Dirc.Vertical)
        {
            if (dirc == A4Dirc.Vertical)
            {   //縦
                doc.SetPageSize(tatePageRect);
            }else
            {   //横
                doc.SetPageSize(yokoPageRect);
            }
            doc.NewPage();

             cb.SetFontAndSize(font.BaseFont, fontSize);
        }

        public PdfContentByte GetContentByte()
        {
            return cb;
        }

        public void SaveState()
        {
            cb.SaveState();
            stackFontSize.Push(fontSize);
        }
        public void RestoreState()
        {
            cb.RestoreState();
            fontSize = stackFontSize.Pop();
        }


        float Y(float y)
        {
            return (int)doc.PageSize.Height - y;
        }
        float INV_Y(float convtedY)
        {
            return doc.PageSize.Height - convtedY;
        }
        //int ROW(int row, int fntH)
        //{
        //    return Y(fntH*row + hedderAreaH);
        //}

        public void SetFontAndSize(string fontName_, float fontSize, bool bBold=false)
        {
            fontName = fontName_;
            int style = Font.NORMAL;
            if (bBold) style = Font.BOLD;

            font = FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, fontSize, style);

            this.fontSize = fontSize;
            //フォントとフォントサイズの指定
            cb.SetFontAndSize(font.BaseFont, fontSize);


        }

        public void SetFontSize(float fontSize, bool bBold = false)
        {
            int style = Font.NORMAL;
            if (bBold) style = Font.BOLD;

            font = FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, fontSize, style);
          
            this.fontSize = fontSize;
            //フォントとフォントサイズの指定
            cb.SetFontAndSize(font.BaseFont, fontSize);
        }
        public void SetFontColor(BaseColor color)
        {
            cb.SetColorFill(color);
        }
        public void SetLineWidth(float w)
        {
            cb.SetLineWidth(w);
        }

        public System.Drawing.SizeF MeasureString(string text)
        {
            System.Drawing.SizeF sz = new System.Drawing.SizeF();
            sz.Width = font.BaseFont.GetWidthPoint(text, fontSize);
            sz.Height = fontSize;

            return sz;
        }

        public void DrawString(float x, float y, string fmt, params object[] item)
        {
            DrawString(x, y, new BaseColor(System.Drawing.Color.Black), fmt, item);

        }
        public void DrawString(float x, float y, System.Drawing.Color color, string fmt, params object[] item)
        {
            DrawString(x, y, new BaseColor(color), fmt, item);

        }
        public void DrawString(float x, float y, BaseColor color , string fmt, params object[] item)
        {
            cb.SetColorFill( color );
            cb.BeginText(); //テキスト描画開始
            cb.SetTextMatrix(x, Y(y + fontSize));
            cb.ShowText(new PdfTextArray(string.Format(fmt, item)));

            cb.EndText(); //テキスト描画終了

            lastDrawY = y + fontSize;

            if (maxDrawY < lastDrawY) maxDrawY = lastDrawY;

        }

        public void DrawString(Rectangle rect, int align, string fmt, params object[] item)
        {
            DrawString( rect,  align, new BaseColor(System.Drawing.Color.Black), fmt, item);
        }
        public void DrawString(Rectangle rect, int align, BaseColor color, string fmt, params object[] item)
        {
            if (fmt == null) return;
            string str = string.Format(fmt, item);

            System.Drawing.SizeF size = MeasureString(str);
            int x = (int)(rect.Left + 1);
            switch (align)
            {
                case iTextSharp.text.Element.ALIGN_LEFT:
                    x = (int)(rect.Left + 1);
                    break;
                case iTextSharp.text.Element.ALIGN_CENTER:
                    x = (int)(rect.Left + (rect.Width - size.Width) / 2);
                    
                    break;
                case iTextSharp.text.Element.ALIGN_RIGHT:
                    x = (int)(rect.Right - size.Width);
                    break;
            }


            int y = (int)(rect.Bottom + (rect.Height - size.Height) / 2 + size.Height); //y座標は文字の下の位置なので更に文字の高さを加算

            cb.SetColorFill(color);
            cb.BeginText(); //テキスト描画開始
            cb.SetTextMatrix(x, Y(y));
            cb.ShowText(new PdfTextArray(string.Format(fmt, item)));
            cb.EndText(); //テキスト描画終了

            lastDrawY = y;
            if (maxDrawY < lastDrawY) maxDrawY = lastDrawY;


        }
        public void DrawString(System.Drawing.Rectangle rect, int align, System.Drawing.Color color, string fmt, params object[] item)
        {

            BaseColor lineColor_ = new BaseColor(color);
           // Rectangle rect_ = new iTextSharp.text.Rectangle(rect.Left, rect.Bottom, rect.Right, rect.Top);
            Rectangle rect_ = new iTextSharp.text.Rectangle(rect.Left, rect.Top, rect.Right, rect.Bottom);

            DrawString(rect_, align, lineColor_, fmt, item);

        }

        //void WriteTextLine(PdfContentByte cb, int x, int row, int fntH, string fmt, params object[] item)
        //{
        //    int rowY = ROW(row, fntH);
        //    lastDrawY = INV_Y(rowY);
        //    cb.BeginText(); //テキスト描画開始
        //    cb.SetTextMatrix(x, rowY);
        //    cb.ShowText(new PdfTextArray(string.Format(fmt, item)));
        //    cb.EndText(); //テキスト描画終了

        //}
        public void DrawLine(float x1, float y1, float x2, float y2, float width, BaseColor lineColor, double[] dotPattern = null)
        {
            cb.SaveState();
            // lastDrawY = Math.Max(pnt1.Y, pnt2.Y);
            if(dotPattern!=null)
            {
                cb.SetLineDash(dotPattern, 0);
            }
            cb.SetColorStroke(lineColor);
            cb.SetLineWidth(width);
            cb.MoveTo(x1, Y(y1));
            cb.LineTo(x2, Y(y2));
            cb.Stroke();
            cb.RestoreState();

        }
        public void DrawLine(System.Drawing.Point pnt1, System.Drawing.Point pnt2, float width, BaseColor lineColor, double[] dotPattern=null)
        {
            DrawLine(pnt1.X, pnt1.Y, pnt2.X, pnt2.Y, width, lineColor, dotPattern);
        }

        public void DrawLine(float x1, float y1, float x2, float y2, float width, BaseColor lineColor)
        {
            cb.SaveState();
            // lastDrawY = Math.Max(pnt1.Y, pnt2.Y);
            cb.SetColorStroke(lineColor);
            cb.SetLineWidth(width);
            cb.MoveTo(x1, Y(y1));
            cb.LineTo(x2, Y(y2));
            cb.Stroke();
            cb.RestoreState();
        }

        public void DrawRectangle(Rectangle rect, BaseColor lineColor)
        {
            cb.SaveState();
           // lastDrawY = Math.Max(pntLeftTop.Y, pntLeftTop.Y + size.Height);
            cb.SetColorStroke(lineColor);
            cb.MoveTo(rect.Left, Y(rect.Bottom));
            cb.LineTo(rect.Left, Y(rect.Bottom + rect.Height));
            cb.LineTo(rect.Left + rect.Width, Y(rect.Bottom + rect.Height));
            cb.LineTo(rect.Left + rect.Width, Y(rect.Bottom));
            cb.ClosePathStroke();
            cb.RestoreState();

        }

        public void DrawRectangle(System.Drawing.Point pntLeftTop, System.Drawing.Size size, BaseColor lineColor, BaseColor fillColor = null)
        {
            Rectangle rect_ = new Rectangle(pntLeftTop.X, pntLeftTop.Y,
                                            pntLeftTop.X + size.Width, pntLeftTop.Y + size.Height);
            FillRectangle(rect_, lineColor, fillColor);
        }

        public void DrawRectangle(System.Drawing.Rectangle rect, System.Drawing.Color lineColor)
        {
            BaseColor lineColor_ = new BaseColor(lineColor);
            iTextSharp.text.Rectangle rect_ = new iTextSharp.text.Rectangle(rect.Left, rect.Top, rect.Right, rect.Bottom);
            DrawRectangle(rect_, lineColor_);
        }


        public void FillRectangle(iTextSharp.text.Rectangle rect, BaseColor lineColor, BaseColor fillColor=null)
        {
            cb.SaveState();
            //lastDrawY = Math.Max(rect.Top, pntLeftTop.Y + size.Height);
            if (fillColor == null) fillColor = lineColor;
            cb.SetColorStroke(lineColor);
            cb.SetColorFill(fillColor);
            cb.MoveTo(rect.Left, Y(rect.Bottom));
            cb.LineTo(rect.Left, Y(rect.Bottom + rect.Height));
            cb.LineTo(rect.Left + rect.Width, Y(rect.Bottom + rect.Height));
            cb.LineTo(rect.Left + rect.Width, Y(rect.Bottom));
            cb.ClosePathFillStroke();
            cb.RestoreState();

        }
        public void FillRectangle(System.Drawing.Point pntLeftTop, System.Drawing.Size size, BaseColor lineColor, BaseColor fillColor = null)
        {
           Rectangle rect_ = new Rectangle(pntLeftTop.X, pntLeftTop.Y,
                                            pntLeftTop.X + size.Width, pntLeftTop.Y + size.Height);
            FillRectangle(rect_, lineColor, fillColor);
        }
        public void FillRectangle(System.Drawing.Rectangle rect, System.Drawing.Color lineColor)
        {
            BaseColor lineColor_ = new BaseColor(lineColor);
            iTextSharp.text.Rectangle rect_ = new iTextSharp.text.Rectangle(rect.Left, rect.Top, rect.Right, rect.Bottom);
            FillRectangle(rect_, lineColor_);
        }
        public void FillRectangle(System.Drawing.Rectangle rect, System.Drawing.Color lineColor, System.Drawing.Color fillColor)
        {
            BaseColor lineColor_ = new BaseColor(lineColor);
            BaseColor fillColor_ = new BaseColor(fillColor);

            iTextSharp.text.Rectangle rect_ = new iTextSharp.text.Rectangle(rect.Left, rect.Top, rect.Right, rect.Bottom);
            FillRectangle(rect_, lineColor_, fillColor_);
        }


    }
}

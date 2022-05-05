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
            public bool bTaiunHyou;
            public bool bNenunHyou;
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
            WriteYousen(50, lastY + 50, ref lastY);

            //十二支干法
            WriteJuNisiKanHou(50, lastY + 50, ref lastY);

            if (param.bShukumeiAndKoutenun)//宿命・後天運
            {
                if (writeCategoryNum!=0) pdfUtil.NewPage();
                //宿命
                WriteShukumei(50, 50, ref lastY);
                //後天運
                WriteKoutenUn(260, 50, ref lastY);
            }

            if (param.bKyoki)   //虚気変化
            {
                if (writeCategoryNum != 0) pdfUtil.NewPage();
            }

            if (param.bTaiunHyou)//大運表
            {
                if (writeCategoryNum != 0) pdfUtil.NewPage();
            }
            if (param.bNenunHyou)//年運表
            {
                if (writeCategoryNum != 0) pdfUtil.NewPage();
            }
            if (param.bGetuunHyou)//月運表
            {
                if (writeCategoryNum != 0) pdfUtil.NewPage();
            }
            if (param.bShugosinHou)//守護神法
            {
                if (writeCategoryNum != 0) pdfUtil.NewPage();
            }
            if (param.bKonkiHou)//根気法
            {
                if (writeCategoryNum != 0) pdfUtil.NewPage();
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
            pdfUtil.DrawString( 50, row * fntH,  "生年月日：{0}", person.birthday.birthday); row++;
            pdfUtil.DrawString( 50, row * fntH,  "性別　　：{0}", (person.gender == Gender.MAN ? "男性" : "女性")); row++;
            //     pdfUtil.DrawLine( new System.Drawing.Point(50, lastDrawY + 5), new System.Drawing.Point(300, lastDrawY + 5), 1, BaseColor.BLUE);

            //pdfUtil.DrawRectangle( new System.Drawing.Point(100, 100), new System.Drawing.Size(60, 90), BaseColor.RED);
            //pdfUtil.FillRectangle(new System.Drawing.Point(120, 120), new System.Drawing.Size(180, 180), BaseColor.RED, BaseColor.GREEN);


            return 0;
        }

        /// <summary>
        /// 陰占表示
        /// </summary>
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
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="lastY"></param>
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
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="lastY"></param>
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
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="lastY"></param>
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
 
        
        void OpenFile(string fname)
        {
            System.Diagnostics.Process p =
                         System.Diagnostics.Process.Start(fname);
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

        public PDFUtility()
        {
        }
        public int OpenDocument( string filePath)
        {
            doc = new Document(PageSize.A4);

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

        public void NewPage()
        {
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

            font = FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 15, style);

            this.fontSize = fontSize;
            //フォントとフォントサイズの指定
            cb.SetFontAndSize(font.BaseFont, fontSize);


        }

        public void SetFontSize(float fontSize, bool bBold = false)
        {
           // baseFont = BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

 
            int style = Font.NORMAL;
            if (bBold) style = Font.BOLD;


            font = FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 15, style);
          


            //Font fnt = new Font(baseFont, style);

            //if( fnt.IsBold())
            //{
            //    style = style;
            //}


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
            string str = string.Format(fmt, item);

            System.Drawing.SizeF size = MeasureString(str);
            int x = (int)(rect.Left + (rect.Width - size.Width) / 2);
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
            Rectangle rect_ = new iTextSharp.text.Rectangle(rect.Left, rect.Bottom, rect.Right, rect.Top);

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
            iTextSharp.text.Rectangle rect_ = new iTextSharp.text.Rectangle(rect.Left, rect.Bottom, rect.Right, rect.Top);
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
            iTextSharp.text.Rectangle rect_ = new iTextSharp.text.Rectangle(rect.Left, rect.Bottom, rect.Right, rect.Top);
            FillRectangle(rect_, lineColor_);
        }
        public void FillRectangle(System.Drawing.Rectangle rect, System.Drawing.Color lineColor, System.Drawing.Color fillColor)
        {
            BaseColor lineColor_ = new BaseColor(lineColor);
            BaseColor fillColor_ = new BaseColor(fillColor);

            iTextSharp.text.Rectangle rect_ = new iTextSharp.text.Rectangle(rect.Left, rect.Bottom, rect.Right, rect.Top);
            FillRectangle(rect_, lineColor_, fillColor_);
        }


    }
}

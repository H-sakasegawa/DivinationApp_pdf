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
        Person person;
        Document doc;
        int hedderAreaH = 20;
        int lastDrawY = 0;
        public PdfContentByte contentByte;

        PDFUtility pdfUtil;

        public PDFOutput(Person person)
        {
            this.person = person;

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

            WritePersonBaseInfo();

            pdfUtil.CloseDocument();

            OpenFile(pdfFilePath);
            return 0;
        }

        int WritePersonBaseInfo()
        {

            //doc.Add(new Paragraph(string.Format("氏名    ：{0}", person.name), fnt));
            // doc.Add(new Paragraph(string.Format("生年月日：{0}", person.birthday.birthday), fnt));
            // doc.Add(new Paragraph(string.Format("性別    ：{0}", (person.gender== Gender.MAN?"男性":"女性"), fnt)));

 
            //フォント名
            string fontFolder = Environment.SystemDirectory.Replace("system32", "fonts");
            string fontName = fontFolder + "\\msgothic.ttc,0";
            int fntH = 10;
            pdfUtil.SetFontAndSize(fontName, fntH);

            //描画するテキスト
            int row = 1;
            pdfUtil.DrawString( 50, row * fntH,  "氏名    ：{0}", person.name); row++;
            pdfUtil.DrawString( 50, row * fntH,  "生年月日：{0}", person.birthday.birthday); row++;
            pdfUtil.DrawString( 50, row * fntH,  "性別    ：{0}", (person.gender == Gender.MAN ? "男性" : "女性")); row++;
       //     pdfUtil.DrawLine( new System.Drawing.Point(50, lastDrawY + 5), new System.Drawing.Point(300, lastDrawY + 5), 1, BaseColor.BLUE);

            //pdfUtil.DrawRectangle( new System.Drawing.Point(100, 100), new System.Drawing.Size(60, 90), BaseColor.RED);
            //pdfUtil.FillRectangle(new System.Drawing.Point(120, 120), new System.Drawing.Size(180, 180), BaseColor.RED, BaseColor.GREEN);

            pdfUtil.SetFontAndSize(fontName, 15);
            DrawInsen insen = new DrawInsen(
                                            person,
                                            null,
                                            true,
                                            true
                                     );
            insen.DrawPDF(pdfUtil, 50, 100);
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
       // int lastDrawY = 0;
        PdfContentByte cb;
        float fontSize = 0;
        BaseFont baseFont;

        public PDFUtility()
        {
        }
        public int OpenDocument( string filePath)
        {
//            doc = new Document(PageSize.A4, 0, 0, 0, 0);
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

        public PdfContentByte GetContentByte()
        {
            return cb;
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

        public void SetFontAndSize(string fontName, int fontSize)
        {
 
            baseFont = BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            this.fontSize = fontSize;
            //フォントとフォントサイズの指定
            cb.SetFontAndSize(baseFont, fontSize);
            

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
            sz.Width = baseFont.GetWidthPoint(text, fontSize);
            sz.Height = fontSize;

            return sz;
        }

        public void DrawString(float x, float y, string fmt, params object[] item)
        {
            //lastDrawY = y;
           
            cb.BeginText(); //テキスト描画開始
            cb.SetTextMatrix(x, Y(y + fontSize));
            cb.ShowText(new PdfTextArray( string.Format(fmt, item)));
           
            cb.EndText(); //テキスト描画終了

        }

        public void DrawString(Rectangle rect, int align, string fmt, params object[] item)
        {
            string str = string.Format(fmt, item);

            System.Drawing.SizeF size = MeasureString(str);
            int x = (int)(rect.Left + (rect.Width - size.Width) / 2);
            int y = (int)(rect.Top + ( rect.Height - size.Height) / 2 + size.Height); //y座標は文字の下の位置なので更に文字の高さを加算

            cb.BeginText(); //テキスト描画開始
            cb.SetTextMatrix(x, Y(y));
            cb.ShowText(new PdfTextArray(string.Format(fmt, item)));
            cb.EndText(); //テキスト描画終了

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
        public void DrawLine(float x1, float y1, float x2, float y2, float width, BaseColor lineColor)
        {
            // lastDrawY = Math.Max(pnt1.Y, pnt2.Y);
            cb.SetColorStroke(lineColor);
            cb.SetLineWidth(width);
            cb.MoveTo(x1, Y(y1));
            cb.LineTo(x2, Y(y2));
            cb.Stroke();
        }
        public void DrawLine(System.Drawing.Point pnt1, System.Drawing.Point pnt2, float width, BaseColor lineColor)
        {
            DrawLine(pnt1.X, pnt1.Y, pnt2.X, pnt2.Y, width, lineColor);
        }
        public void DrawRectangle(iTextSharp.text.Rectangle rect, BaseColor lineColor)
        {
            cb.SaveState();
           // lastDrawY = Math.Max(pntLeftTop.Y, pntLeftTop.Y + size.Height);
            cb.SetColorStroke(lineColor);
            cb.MoveTo(rect.Left, Y(rect.Top));
            cb.LineTo(rect.Left, Y(rect.Top + rect.Height));
            cb.LineTo(rect.Left + rect.Width, Y(rect.Top + rect.Height));
            cb.LineTo(rect.Left + rect.Width, Y(rect.Top));
            cb.ClosePathStroke();
            cb.RestoreState();
        }
        public void DrawRectangle(System.Drawing.Point pntLeftTop, System.Drawing.Size size, BaseColor lineColor, BaseColor fillColor = null)
        {
            iTextSharp.text.Rectangle rect_ = new iTextSharp.text.Rectangle(pntLeftTop.X, pntLeftTop.Y,
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
            cb.MoveTo(rect.Left, Y(rect.Top));
            cb.LineTo(rect.Left, Y(rect.Top + rect.Height));
            cb.LineTo(rect.Left + rect.Width, Y(rect.Top + rect.Height));
            cb.LineTo(rect.Left + rect.Width, Y(rect.Top));
            cb.ClosePathFillStroke();
            cb.RestoreState();

        }
        public void FillRectangle(System.Drawing.Point pntLeftTop, System.Drawing.Size size, BaseColor lineColor, BaseColor fillColor = null)
        {
            iTextSharp.text.Rectangle rect_ = new iTextSharp.text.Rectangle(pntLeftTop.X, pntLeftTop.Y,
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

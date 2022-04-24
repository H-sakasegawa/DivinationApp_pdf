using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;

namespace DivinationApp
{
    public abstract class GraphicsBase
    {
        public abstract void DrawRectangle(Pen pen, Rectangle rect);
        public abstract void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format);
        public abstract void FillRectangle(Brush brush, Rectangle rect);
        public abstract void FillRectangle(Brush brush, int x, int y, int width, int height);
        public abstract void DrawLine(Pen pen, Point pt1, Point pt2);
        public abstract SizeF MeasureString(string text, Font font);
    }
    public class FormGraphics : GraphicsBase
    {
        Graphics g;

        public FormGraphics(Graphics g)
        {
            this.g = g;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        }

        public void CreateGraphics(Bitmap canvas)
        {
            g = Graphics.FromImage(canvas);
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        }

        public override void DrawRectangle(Pen pen, Rectangle rect)
        {
            g.DrawRectangle(pen, rect);
        }

        public override void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            g.DrawString(s, font, brush, layoutRectangle, format);
        }
        public override void FillRectangle(Brush brush, Rectangle rect)
        {
            g.FillRectangle(brush, rect);
        }
        public override void FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            g.FillRectangle(brush, x,  y,  width,  height);
        }

        public override void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            g.DrawLine(pen, pt1, pt2);
        }
        public override SizeF MeasureString(string text, Font font)
        {
            return g.MeasureString(text, font);
        }

    }
    public class PDFGraphics : GraphicsBase
    {
        PDFUtility pdfUtil;

        public PDFGraphics(PDFUtility pdfUtil)
        {
            this.pdfUtil = pdfUtil;
        }

        public override void DrawRectangle(Pen pen, Rectangle rect)
        {
            pdfUtil.SetLineWidth(pen.Width);
            pdfUtil.DrawRectangle(rect, pen.Color);
        }
        public override void DrawString(string s, Font font, Brush brush, RectangleF rect, StringFormat format)
        {
            iTextSharp.text.Rectangle r = new iTextSharp.text.Rectangle(0,0,0,0);
            r.Left = rect.X;
            r.Top = rect.Y + rect.Height;
            r.Right = rect.X + rect.Width;
            r.Bottom = rect.Y;

            int align = 0;
            switch (format.Alignment)
            {
                case StringAlignment.Near:
                    align = iTextSharp.text.Element.ALIGN_LEFT;
                    break;
                case StringAlignment.Center:
                    align = iTextSharp.text.Element.ALIGN_CENTER;
                    break;
                case StringAlignment.Far:
                    align = iTextSharp.text.Element.ALIGN_RIGHT;
                    break;
            }

            pdfUtil.DrawString(r, align, s);
        }
        public override void FillRectangle(Brush brush, Rectangle rect)
        {
            iTextSharp.text.BaseColor lineColor_ = new iTextSharp.text.BaseColor(((SolidBrush)brush).Color);
            iTextSharp.text.Rectangle r = new iTextSharp.text.Rectangle(rect.Left, rect.Bottom, rect.Right, rect.Top);
            r.Left = rect.X;
            r.Top = rect.Y + rect.Height;
            r.Right = rect.X + rect.Width;
            r.Bottom = rect.Y;


            pdfUtil.FillRectangle(r, lineColor_);
        }
        public override void FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            Rectangle r = new Rectangle(x, y, width, height);
            FillRectangle(brush, r);
        }


        public override void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            iTextSharp.text.BaseColor lineColor_ = new iTextSharp.text.BaseColor(pen.Color);
            pdfUtil.DrawLine( pt1, pt2, pen.Width, lineColor_);
        }
        public override SizeF MeasureString(string text, Font font)
        {
#if false
            string fontName = font.OriginalFontName;
            iTextSharp.text.pdf.BaseFont baseFont = iTextSharp.text.pdf.BaseFont.CreateFont
                                        (fontName,
                                        iTextSharp.text.pdf.BaseFont.IDENTITY_H,
                                        iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            System.Drawing.SizeF sz = new System.Drawing.SizeF();
            sz.Width = baseFont.GetWidthPoint(text, font.GetHeight());
            sz.Height = font.GetHeight();
#else
            return pdfUtil.MeasureString(text);
#endif
        }

    }



}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using NPOI.XSSF.UserModel;

namespace DivinationApp
{
    /// <summary>
    /// 説明用Excelファイルから画像データを管理提供するクラス
    /// </summary>
    public class ExplanationReader
    {

        public class ExplanationData
        {
            public ExplanationData(string contentKey, ExcelReader.InfoBase info)
            {
                this.contentKey = contentKey;
                AddPictureInfo(info);
                
            }
 
            public void AddPictureInfo(ExcelReader.InfoBase info)
            {
                switch (info.type)
                {
                    case ExcelReader.InfoType.PICTURE:
                        pictureInfos.Add(info);
                        break;
                    case ExcelReader.InfoType.TEXT:
                        textInfos.Add(info);
                        break;
                }
                CalcColMaxNum(info);
            }

            public int GetPictureMaxWidth()
            {
                int maxW = 0;
                foreach(ExcelReader.PictureInfo inf  in pictureInfos)
                {
                    if (maxW < inf.width) maxW = inf.width;
                }
                return maxW;
            }
            public int GetPictureMaxHeight()
            {
                int maxH = 0;
                foreach (ExcelReader.PictureInfo inf in pictureInfos)
                {
                    if (maxH < inf.height) maxH = inf.height;
                }
                return maxH;

            }

            /// <summary>
            /// Picture、Textデータの最大カラム
            /// </summary>
            /// <returns></returns>
            public int Count
            {
                get { return maxColNum; }
            }

            /// <summary>
            /// 指定されたページ番号(1～）のデータを返す
            /// </summary>
            /// <param name="pageNo"></param>
            /// <returns></returns>
             public ExcelReader.InfoBase this[ int pageNo ]
            {
                get
                {
                    //Page１はColNo=2～始まる
                    var value = pictureInfos.Find(x => x.col == pageNo);
                    if (value != null) return value;

                    return textInfos.Find(x => x.col == pageNo);
                }
            }


            void CalcColMaxNum(ExcelReader.InfoBase inf)
            {
                if (inf.col > maxColNum) maxColNum = inf.col;
            }


            public string contentKey;
            private List<ExcelReader.InfoBase> pictureInfos = new List<ExcelReader.InfoBase>();
            private List<ExcelReader.InfoBase> textInfos = new List<ExcelReader.InfoBase>();

            int maxColNum =0;
        }

        //Key:項目キー名
        private Dictionary<string, ExplanationData> dic = new Dictionary<string, ExplanationData>();
        public string keyName;

        public int ReadExcel(string keyName, string excelFilePath)
        {
            this.keyName = keyName;
            return ReadExcel(excelFilePath);
        }
        public int ReadExcel( string excelFilePath)
        {
            if ( !File.Exists(excelFilePath))
            {
                return -1;
            }

            string exePath = FormMain.GetExePath();

            var workbook = ExcelReader.GetWorkbook(excelFilePath, "xlsx");
            if( workbook==null)
            {
                MessageBox.Show($"{excelFilePath}\nを開けません");
                return -1;
            }
            XSSFSheet sheet = (XSSFSheet)((XSSFWorkbook)workbook).GetSheetAt(0);
            List<ExcelReader.PictureInfo> lstCellInfos = ExcelReader.GetPicture(sheet);

            int iRow = 0;
            while (true)
            {
                // //行のテキスト
                List<ExcelReader.TextInfo> lstTextInfos = new List<ExcelReader.TextInfo>();
                int cellNum = ExcelReader.GetCellCount(sheet, iRow);
                if (cellNum < 0) break;
                if (string.IsNullOrEmpty(ExcelReader.CellValue(sheet, iRow, 0)))
                {
                    iRow++;
                    continue;
                }

                for (int iCol = 0; iCol < cellNum; iCol++)
                {
                    string text = ExcelReader.CellValue(sheet, iRow, iCol);
                    if (string.IsNullOrEmpty(text)) continue;

                    ExcelReader.TextInfo textInfo = new ExcelReader.TextInfo();
                    textInfo.col = iCol;
                    textInfo.row = iRow;
                    textInfo.textData = text.Replace("\n", "\r\n");
                    lstTextInfos.Add(textInfo);
                }

                // //説明項目キー文字
                //string sKeyItem = ExcelReader.CellValue(sheet, iRow, 0);
                string sKeyItem = lstTextInfos[0].textData;
                if (string.IsNullOrEmpty(sKeyItem)) break;

                var sKeyAry = sKeyItem.Split('\n');
                //string Explanation = ExcelReader.CellValue(sheet, iRow, 1);


                List<ExcelReader.PictureInfo> pictureInfos = lstCellInfos.FindAll(x => x.row == iRow+1)
                                                                         .OrderBy(x=> x.col).ToList();
                foreach (var sKey in sKeyAry)
                {
                    foreach (var info in pictureInfos)
                    {
                        if (dic.ContainsKey(sKey))
                        {
                            dic[sKey].AddPictureInfo(info);
                        }
                        else
                        {
                            dic.Add(sKey, new ExplanationData(sKey, info));
                        }
                    }

                    foreach (var info in lstTextInfos)
                    {
                        if (dic.ContainsKey(sKey))
                        {
                            dic[sKey].AddPictureInfo(info);
                        }
                        else
                        {
                            dic.Add(sKey, new ExplanationData(sKey, info));
                        }
                    }

                }
                iRow++;
            }

            return 0;

        }
   
        public void Clear()
        {
            dic.Clear();
        }

        public ExplanationData GetExplanation(string sKey)
        {
            if (!dic.ContainsKey(sKey)) return null;
            return dic[sKey];
        }

        public List<string> GetExplanationKeys()
        {
            return dic.Keys.ToList();
        }
    }
}

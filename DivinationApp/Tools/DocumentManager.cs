using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DivinationApp
{
    public class DocumentManager
    {
        public class ContentFindResult
        {
            public string mainCategoryName;
            public string subCategoryName;
            public string contentKeyName;
            public ExplanationReader reader;

        }
        public class SubCategoryFindResult
        {
            public string mainCategoryName;
            public string subCategoryName;
            public ExplanationReader reader;

        }

        /// <summary>
        /// １つのメインカテゴリに含まれるサブカテゴリ群（Excelファイル）管理クラス
        /// </summary>
        class DocMainCategory
        {
            public string mainCategoryName;　//メインカテゴリ名
            //Dictionary< サブカテゴリ名, サブカテゴリのExcelReader>
            public Dictionary<string, ExplanationReader> mapExcel = new Dictionary<string, ExplanationReader>();

            public DocMainCategory(string keyName)
            {
                this.mainCategoryName = keyName;
            }
            public int Add( string filePath)
            {
                string subCategoryName = Path.GetFileNameWithoutExtension(filePath);

                ExplanationReader reader = new ExplanationReader();
                reader.ReadExcel(subCategoryName, filePath);

                mapExcel.Add(subCategoryName, reader);
                return 0;
            }

            public List<string> GetSubCategories()
            {
                return mapExcel.Keys.ToList();
            }
            /// <summary>
            /// subCategoryに該当するサブカテゴリーのExcelReader取得
            /// </summary>
            /// <param name="subKey"></param>
            /// <returns></returns>
            public ExplanationReader GetExplanationSubCategoryReader(string subCategory)
            {
                if (!mapExcel.ContainsKey(subCategory)) return null;
                return mapExcel[subCategory];
            }
            /// <summary>
            /// すべてのサブカテゴリからcontentsKeyが含まれるサブカテゴリーのExcelReaderを取得
            /// </summary>
            /// <param name="contentsKey"></param>
            /// <returns></returns>
            public List<SubCategoryFindResult> GetExplanationReaderByContentsKey(string contentsKey)
            {
                List<SubCategoryFindResult> lstResult = new List<SubCategoryFindResult>();

                foreach (var pairExcel in mapExcel)
                {
                    var keys = pairExcel.Value.GetExplanationKeys();
                    foreach (var key in keys)
                    {
                        if (key == contentsKey)
                        {
                            SubCategoryFindResult result = new SubCategoryFindResult();
                            result.mainCategoryName = mainCategoryName;
                            result.subCategoryName = pairExcel.Key;
                            result.reader = pairExcel.Value;

                            lstResult.Add(result);
                            break;
                        }
                    }
                }
                return lstResult;
            }



            public override string ToString()
            {
                return mainCategoryName;
            }

        }

        Dictionary<string, DocMainCategory> dicDocuments = new Dictionary<string, DocMainCategory>();
        string targetPath;

        public DocumentManager(  )
        {
        }

        public int Initialize(string docPath)
        {
            targetPath = docPath;


            //指定されたドキュメントフォルダ直下のフォルダ名を収集
            var dirs = Directory.GetDirectories(docPath);

            foreach( var dirPath in dirs)
            {
                var files = Directory.GetFiles(dirPath, "*.xls*");
                string dirName = Path.GetFileName(dirPath);

                DocMainCategory docKind = new DocMainCategory(dirName );
                dicDocuments.Add(dirName, docKind);

                foreach (var filePath in files)
                {
                    docKind.Add(filePath);
                }
            }

            return 0;
        }

        public int Reload()
        {
            dicDocuments.Clear();

            return Initialize(targetPath);
        }

        /// <summary>
        /// メインカテゴリキー文字一覧取得
        /// </summary>
        /// <returns></returns>
        public List<string> GetMainCategories()
        {
            return dicDocuments.Keys.ToList();
        }
        /// <summary>
        /// サブカテゴリ文字一覧取得
        /// </summary>
        /// <param name="mainKey"></param>
        /// <returns></returns>
        public List<string> GetSubCategories( string mainKey)
        {
            if (!dicDocuments.ContainsKey(mainKey)) return null;

            var doc = dicDocuments[mainKey];

            return doc.GetSubCategories();
        }
        /// <summary>
        /// メインカテゴリ内のサブカテゴリに該当する説明データ取得
        /// </summary>
        /// <param name="mainKey"></param>
        /// <param name="subKey"></param>
        /// <returns></returns>
        public ExplanationReader GetExplanationReader(string mainKey, string subKey)
        {
            if (!dicDocuments.ContainsKey(mainKey)) return null;
            var doc = dicDocuments[mainKey];

            return doc.GetExplanationSubCategoryReader(subKey);

        }

        /// <summary>
        /// サブカテゴリを検索して返す
        /// </summary>
        /// <param name="subCutegory"></param>
        /// <returns></returns>
        public SubCategoryFindResult GetExplanationSubCategory(string subCutegory)
        {
            foreach (var pairMain in dicDocuments)
            {
                DocMainCategory mainCategory = pairMain.Value;

                var subCategory = mainCategory.GetExplanationSubCategoryReader(subCutegory);
                if (subCategory!=null)
                {
                    SubCategoryFindResult result = new SubCategoryFindResult();
                    result.mainCategoryName = pairMain.Key;
                    result.subCategoryName = subCutegory;
                    result.reader = subCategory;
                    return result;
                }
            }
            return null;
        }


        /// <summary>
        /// 全てのドキュメントから指定されたキーがコンテンツキーとして
        /// 登録されているExplanationReaderを取得
        /// </summary>
        /// <param name="contentKey"></param>
        /// <returns></returns>
        public ContentFindResult GetExplanationContents(string contentKey)
        {
            List<ExplanationReader> lstReader = new List<ExplanationReader>();

            ContentFindResult result = new ContentFindResult();

            //とりあえず、重複したコンテンツキーはないものとして、
            //最初に見つかった項目のみとする
            foreach ( var pairMain in dicDocuments)
            {
                DocMainCategory mainCategory = pairMain.Value;

               var subCategories =  mainCategory.GetExplanationReaderByContentsKey(contentKey);
                if (subCategories.Count > 0)
                {
                    result.mainCategoryName = mainCategory.mainCategoryName;
                    result.subCategoryName  = subCategories[0].subCategoryName;
                    result.contentKeyName   = contentKey;
                    result.reader           = subCategories[0].reader;

                    return result;
                }
            }
            return null;
        }
  
    }
}

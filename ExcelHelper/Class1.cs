using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExcelHelper
{
    public class Class1
    {
        /// <summary>
        /// Figures out 
        /// </summary>
        /// <param name="worksheetName"></param>
        /// <param name="fileName"></param>
        /// <param name="NumberOfColumns"></param>
        /// <returns></returns>
        public ExcelRange ExcelGetDataRange(string worksheetName, string fileName, int NumberOfColumns)
        {

            int rowCount = GetRowCount(fileName, worksheetName);

            var file = Utils.GetFileInfo(fileName);

            using (ExcelPackage xlPackage = new ExcelPackage(file))
            {
                if (!xlPackage.Workbook.Worksheets.Any(x => x.Name == worksheetName))
                {
                    return null;
                }
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[worksheetName];
                //Get row count
                string Letter = GetColumnName(NumberOfColumns);
                return worksheet.Cells[$"A1:{Letter}{rowCount}"];
            }
        }

        /// <summary>
        /// Just used for figuring out what the ExcelRange needs to be.
        /// It's just going to check column B to make sure if it's null or empty.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="worksheetName"></param>
        /// <param name="offset">Default=1, skip the top row</param>
        /// <returns></returns>
        public int GetRowCount(string fileName, string worksheetName, int offset = 1)
        {
            int currentRow = 2;
            bool hasSomething = true;
            var file = Utils.GetFileInfo(fileName);

            using (ExcelPackage xlPackage = new ExcelPackage(file))
            {
                if (!xlPackage.Workbook.Worksheets.Any(x => x.Name == worksheetName))
                {
                    return -1;
                }
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[worksheetName];
                //Get row count

                while (hasSomething)
                {
                    var test1 = worksheet.Cells[1, currentRow].Value;
                    if (hasSomething = !string.IsNullOrWhiteSpace(test1.ToString()))
                    {
                        currentRow++;
                    }
                }
                return currentRow - 1;
            }
            return 0;
        }


        public void ReadRows(string fileName, int rowCount, int ColumnCount)
        {

        }

        static string GetColumnName(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];

            return value;
        }
    }

    public class Utils
    {
        static DirectoryInfo _outputDir = null;
        public static DirectoryInfo OutputDir
        {
            get
            {
                return _outputDir;
            }
            set
            {
                _outputDir = value;
                if (!_outputDir.Exists)
                {
                    _outputDir.Create();
                }
            }
        }
        public static FileInfo GetFileInfo(string file, bool deleteIfExists = true)
        {
            var fi = new FileInfo(OutputDir.FullName + Path.DirectorySeparatorChar + file);
            if (deleteIfExists && fi.Exists)
            {
                fi.Delete();  // ensures we create a new workbook
            }
            return fi;
        }
        public static FileInfo GetFileInfo(DirectoryInfo altOutputDir, string file, bool deleteIfExists = true)
        {
            var fi = new FileInfo(altOutputDir.FullName + Path.DirectorySeparatorChar + file);
            if (deleteIfExists && fi.Exists)
            {
                fi.Delete();  // ensures we create a new workbook
            }
            return fi;
        }

        internal static DirectoryInfo GetDirectoryInfo(string directory)
        {
            var di = new DirectoryInfo(_outputDir.FullName + Path.DirectorySeparatorChar + directory);
            if (!di.Exists)
            {
                di.Create();
            }
            return di;
        }
    }


}

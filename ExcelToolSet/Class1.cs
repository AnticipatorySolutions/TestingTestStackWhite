using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;


namespace ExcelToolSet
{
    public class ExcelWriter {
        Application xlApp;
        Workbook xlWorkBook;
        Worksheet xlWorkSheet;
        string xlFileAddress;

        public ExcelWriter(string fileAddress) {
            DateTime dateTime = DateTime.Now;
            string date = dateTime.ToShortDateString();
            string time = dateTime.ToLongTimeString();
            date = date.Replace("/", "_");
            time = time.Replace(":", "_").Replace(" ", "_");
            
            fileAddress = $"{fileAddress}_{date}_{time}";
            
            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Add();
            xlFileAddress = fileAddress;
        }

        public void WriteToFile(List<List<string>> information) {
            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Add();
            
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.Item[1];
            int trackX = 0, trackY = 0;
            
            foreach (var y in information) {
                trackY++;
                foreach (var x in y) {
                    trackX++;
                    xlWorkSheet.Cells[trackY, trackX] = x;                    
                }                
                trackX = 0;
            }
            xlWorkBook.SaveAs($"{xlFileAddress}.xls");

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
        }


    }
    

    public class ExcelReader {
        Application xlApp;
        Workbook xlWorkBook;
        Worksheet xlWorkSheet;
        Range range;

        public ExcelReader(string xlFileAddress) {
            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Open(xlFileAddress, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);            
        }
        public ReturnObject ReadSheet(int sheetIndex, int colLim = 0) {
            int rowLimit, columnLimit;
            Dictionary<string, int> header = new Dictionary<string, int>();
            List<List<String>> information = new List<List<string>>();

            ReturnObject results = new ReturnObject();

            string tempValueHolder;
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.Item[sheetIndex];
            range = xlWorkSheet.UsedRange;
            if (colLim <= range.Columns.Count) {
                columnLimit = range.Columns.Count;
            } else {
                columnLimit = colLim;
            }
            
            rowLimit = range.Rows.Count;

            for (int x = 1; x <= columnLimit; x++) {
                tempValueHolder = System.Convert.ToString((range.Cells[2, x] as Range).Value2);
                
                header.Add(tempValueHolder, x-1);
            }
            
            //starts at 3 because all datasheets will have 2 header rows
            for (int y = 3; y <= rowLimit; y++) {
                List < string > internalList = new List<string>();
                
                for(int x = 1; x <= columnLimit; x++) {
                    tempValueHolder = System.Convert.ToString((range.Cells[y, x] as Range).Value2);
                    if (tempValueHolder == null || tempValueHolder.Length == 0) {
                        tempValueHolder = "";
                    }
                    internalList.Add(tempValueHolder);
                }
                information.Add(internalList);
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            results.results = information;
            results.headers = header;
            return results;
        }

        public class ReturnObject {
            public List<List<string>> results = new List<List<string>>();
            public Dictionary<string, int> headers = new Dictionary<string, int>();

        }

    }
}
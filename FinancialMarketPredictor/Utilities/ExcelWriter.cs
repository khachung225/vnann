using System;
using System.Collections.Generic;
using System.IO;
using ExcelLibrary.SpreadSheet;
using FinancialMarketPredictor.Entities;

namespace FinancialMarketPredictor.Utilities
{
    public  class ExcelWriter
    {
        private  Workbook _workbook;
//        //create new xls file
//string file = "C:\\newdoc.xls";
//Workbook workbook = new Workbook();
//Worksheet worksheet = new Worksheet("First Sheet");
//worksheet.Cells[0, 1] = new Cell((short)1);
//worksheet.Cells[2, 0] = new Cell(9999999);
//worksheet.Cells[3, 3] = new Cell((decimal)3.45);
//worksheet.Cells[2, 2] = new Cell("Text string");
//worksheet.Cells[2, 4] = new Cell("Second string");
//worksheet.Cells[4, 0] = new Cell(32764.5, "#,##0.00");
//worksheet.Cells[5, 1] = new Cell(DateTime.Now, @"YYYY\-MM\-DD");
//worksheet.Cells.ColumnWidth[0, 1] = 3000;
//workbook.Worksheets.Add(worksheet);
//workbook.Save(file);

//// open xls file
//Workbook book = Workbook.Load(file);
//Worksheet sheet = book.Worksheets[0];

// // traverse cells
// foreach (Pair<Pair<int, int>, Cell> cell in sheet.Cells)
// {
//     dgvCells[cell.Left.Right, cell.Left.Left].Value = cell.Right.Value;
// }

// // traverse rows by Index
// for (int rowIndex = sheet.Cells.FirstRowIndex; 
//        rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
// {
//     Row row = sheet.Cells.GetRow(rowIndex);
//     for (int colIndex = row.FirstColIndex; 
//        colIndex <= row.LastColIndex; colIndex++)
//     {
//         Cell cell = row.GetCell(colIndex);
//     }
// }

        public void OpenFlie(string filename )
        {

            if (File.Exists(filename))
            {
                _workbook = Workbook.Load(filename);
            }
            else
            {
                
           
            //create new xls file

                Workbook _workbook = new Workbook();
            Worksheet worksheet = new Worksheet("Summary");

            _workbook.Worksheets.Add(worksheet);
            _workbook.Save(filename);
            }

        }
        public void  AddNewSheet(string sheetName,string error, string predic, string timetrain,  List<CommodityResults> resultses)
        {
            
        }

        
    }
}

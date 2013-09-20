using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;


namespace mytools
{
    class HandleExcel
    {
        //不允许null的参数使用missing.value
        Missing miss = Missing.Value;
        
        public bool ExportExcel(string filePath
                                ,ListView listView
                                ,ExcelStruct eStruct)
        {
            //产生一个Excel应用
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            //应用不可见
            excelApp.Visible = false;
            //把wBook加到Excel应用中
            Workbook wBook = excelApp.Workbooks.Add(true);

            Worksheet wSheet = wBook.Worksheets[1] as Worksheet;
            Range range = null;

            int rows = listView.Items.Count;
            int columns = listView.Columns.Count;

            FillRange(wSheet, range, eStruct);
            

            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < columns; j++)
                {
                    FillSingleCell(wSheet, listView.Items[i-1].SubItems[j-1].Text, i, j);
                }
            }

            wSheet.Columns.AutoFit();

            excelApp.DisplayAlerts = false;
            excelApp.AlertBeforeOverwriting = false;
            //保存工作簿   
            wBook.Save();
            //保存excel文件   
            excelApp.Save("D:\\1.XLS");
            excelApp.SaveWorkspace("D:\\1.XLS");
            excelApp.Quit();

            int generation = System.GC.GetGeneration(excelApp);
            excelApp = null;
            System.GC.Collect(generation);

            return true;
        }

        /// <summary>
        /// 向指定sheet中的指定单元格填充内容
        /// </summary>
        /// <param name="wSheet"></param>
        /// <param name="fillInfo"></param>
        /// <param name="cellX"></param>
        /// <param name="cellY"></param>
        /// <returns></returns>
        public bool FillSingleCell(Worksheet wSheet
                                  , string fillInfo
                                  , int cellX
                                  , int cellY)
                               //   , ref string errInfo)
        {

              


            wSheet.Cells[cellX, cellY] = fillInfo;
            return true;
        }


        public bool FillRange(Worksheet wSheet
                             ,Range range
                             ,ExcelStruct eStruct)
        {
            range = wSheet.get_Range(wSheet.Cells[eStruct.X_ini, eStruct.Y_ini], wSheet.Cells[eStruct.X_end, eStruct.Y_end]);

            /*
            range.Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThick;
            
            SaveFileDialog sfd = new SaveFileDialog();
            
            sfd.Filter = "XLS 文件(*.XLS)|*.XLS";
            sfd.RestoreDirectory = true;
            sfd.FileName = textBoxAgentName.Text + dateTimePickerStart.Value.ToString("yyyyMMdd") + choiceString + "资料文件汇总.XLS";        
            */

            range.EntireColumn.AutoFit();
            range.Borders.LineStyle = eStruct.bordersLineStyle;
            range.NumberFormatLocal = eStruct.numberFormat;
            range.Font.Bold = eStruct.fontBond;
            range.Font.Size = eStruct.fontSize;
            range.Font.ColorIndex = eStruct.fontColor;
            range.Interior.ColorIndex = eStruct.interiorColor;
            range.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black.ToArgb());
            return true;
        }

        

                                    


    }
}

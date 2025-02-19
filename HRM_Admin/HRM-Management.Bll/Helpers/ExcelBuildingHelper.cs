using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace HRM_Management.Bll.Helpers
{
    public static class ExcelBuildingHelper
    {
        public static void CreateStyles(this ExcelWorkbook workbook)
        {
            var headerStyle = workbook.Styles.CreateNamedStyle("Header");
            headerStyle.Style.Font.Bold = true;
            headerStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerStyle.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#79c2d0"));
            headerStyle.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            headerStyle.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            headerStyle.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            headerStyle.Style.Border.Top.Style = ExcelBorderStyle.Thin;

            var noDataStyle = workbook.Styles.CreateNamedStyle("NoData");
            noDataStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
            noDataStyle.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#393e46"));
            noDataStyle.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#e3e3e3"));
            noDataStyle.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            noDataStyle.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            noDataStyle.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            noDataStyle.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        }
        
        public static void DrawBorders(this ExcelWorksheet ws, int rowsCount, int columnsCount)
        {
            ws.Cells[1, 1, rowsCount, columnsCount].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            ws.Cells[1, 1, rowsCount, columnsCount].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.Cells[1, 1, rowsCount, columnsCount].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.Cells[1, 1, rowsCount, columnsCount].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        }
    }
}

using HRM_Management.Bll.Helpers;
using HRM_Management.Core.Helpers;
using HRM_Management.Core.Services;
using OfficeOpenXml;
using System.Data;

namespace HRM_Management.Bll.Services
{
    public class ExcelService : IExcelService
    {
        private readonly IGraphQLFetchingService _fetchingService;

        public ExcelService(IGraphQLFetchingService fetchingService)
        {
            _fetchingService = fetchingService;
        }

        public async Task<byte[]> ExportFromQueryAsync(string query, string? customTableName = null)
        {
            var tableName = query.GetTableNameFromQuery();
            var json = await _fetchingService.Execute(query);
            var dt = json.JsonToDataTable(tableName);
            return await DataTableToExcelAsync(dt, customTableName ?? tableName);
        }

        private async Task<byte[]> DataTableToExcelAsync(DataTable dt, string worksheetName = "Sheet1")
        {
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add(worksheetName);

            ws.DrawBorders(dt.Rows.Count + 1, dt.Columns.Count);
            ws.Workbook.CreateStyles();
            ws.Cells[1, 1, 1, dt.Columns.Count].StyleName = "Header";

            for (var i = 0; i < dt.Columns.Count; i++)
            {
                ws.Cells[1, i + 1].Value = dt.Columns[i].ColumnName;
            }
            for (var row = 0; row < dt.Rows.Count; row++)
            {
                for (var col = 0; col < dt.Columns.Count; col++)
                {
                    var value = (string)dt.Rows[row][col];
                    ws.Cells[row + 2, col + 1].Value = value;
                    if (value == Constants.NO_DATA_TABLE_VALUE) ws.Cells[row + 2, col + 1].StyleName = "NoData";
                }
            }
            ws.Cells.AutoFitColumns();
            return await package.GetAsByteArrayAsync();
        }
    }
}

using System.Data;

namespace HRM_Management.Core.Services
{
    public interface IExcelService 
    {
        Task<byte[]> ExportFromQueryAsync(string query, string? customTableName = null);
    }
}

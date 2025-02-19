using HRM_Management.Core.Helpers;
using System.Data;
using System.Text.Json.Nodes;

namespace HRM_Management.Bll.Helpers
{
    public static class JsonDataHelper
    {
        public static string GetTableNameFromQuery(this string query)
        {
            return query
                .Split('{', '(').First(x => !"nodes query"
                                                .Contains(x.Trim()))
                .Trim();
        }

        public static DataTable JsonToDataTable(this string jsonData, string tableName)
        {
            var json = JsonNode.Parse(jsonData)!.AsObject();
            var data = json["data"][tableName]["nodes"].AsArray();
            var dt = new DataTable();

            foreach (var item in data)
            {
                var row = dt.NewRow();
                foreach (var property in item.AsObject())
                {
                    WriteProp(property, dt, row);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        private static void WriteProp(KeyValuePair<string, JsonNode?> node, DataTable dt, DataRow row)
        {
            try
            {
                var asObject = node.Value.AsObject();
                foreach (var property in node.Value.AsObject())
                {
                    WriteProp(property, dt, row);
                }
            }
            catch (NullReferenceException)
            {
                var colName = Constants.FIELD_NAMES_INTERPRETATIONS.FirstOrDefault(x => x.Key == node.Key).Value ?? node.Key;
                row[colName] = Constants.NO_DATA_TABLE_VALUE;
            }
            catch (InvalidOperationException)
            {
                var colName = Constants.FIELD_NAMES_INTERPRETATIONS.FirstOrDefault(x => x.Key == node.Key).Value ?? node.Key;
                if (!dt.Columns.Contains(colName)) dt.Columns.Add(colName);
                row[colName] = node.Value;
            }
        }
    }
}

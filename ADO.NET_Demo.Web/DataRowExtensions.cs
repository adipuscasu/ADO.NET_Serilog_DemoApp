using System.Data;

namespace ADO.NET_Demo.Web
{
    public static class DataRowExtensions
    {
        public static string GetStringValue(this DataRow row, string columnName)
        {
            if (row == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(columnName))
            {
                return null;
            }
            return row[columnName].ToString();
        }
    }
}

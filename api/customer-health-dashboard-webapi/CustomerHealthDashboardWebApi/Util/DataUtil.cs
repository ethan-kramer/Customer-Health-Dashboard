using CustomerHealthDashboardWebApi.Ext;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CustomerHealthDashboardWebApi.Util
{
    public static class DataUtil
    {
        public static List<Dictionary<string, object>> ExecuteQueryAsDictionary(this DbContext dbContext, string query, List<System.Data.Common.DbParameter> commandParameters = null)
        {
            var results = new List<Dictionary<string, object>>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                if (commandParameters != null)
                {
                    foreach (var param in commandParameters)
                    {
                        command.Parameters.Add(param);
                    }
                }

                dbContext.Database.OpenConnection();

                var dt = new DataTable();

                using (var dr = command.ExecuteReader())
                {
                    dt.Load(dr);

                }
                foreach (System.Data.DataRow dataRow in dt.Rows)
                {
                    var dictionary = new Dictionary<string, Object>();

                    foreach (System.Data.DataColumn dataColumn in dt.Columns)
                    {
                        var columName = dataColumn.ColumnName;
                        var columnValue = dataRow[dataColumn.ColumnName];
                        if (columnValue == null)
                        {
                            columnValue = ObjectUtil.GetDefaultTypeValue(dataColumn.DataType);
                        }
                        dictionary.Add(columName, columnValue);
                    }
                    results.Add(dictionary);
                }
            }
            return results;
        }
    }
}

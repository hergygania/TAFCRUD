using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Inspiro.Helper
{
    public static class DataTableHelper
    {
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            var value = propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?)
                            ? DateTime.Parse(row[prop.Name].ToString())
                            : Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType);

                            propertyInfo.SetValue(obj, value, null);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static List<Dictionary<string, object>> DataTableToListDictionary(DataTable dt)
        {
            var lst = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(Convert.ToString(col.ColumnName), dr[col]);
                }
                lst.Add(row);
            }

            return lst;
        }
    }
}

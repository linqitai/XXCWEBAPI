using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace XXCWEBAPI.Utils
{
    /// <summary>
    /// ConvertHelper 的摘要说明
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        /// table转json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            string code = dt.Rows.Count > 0 ? "1" : "0";
            string msg = dt.Rows.Count > 0 ? "操作成功" : "没有获取到数据或者请求出错";
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"code\":\"" + code + "\",\"msg\":\"" + msg + "\",\"data\":");
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString().Replace("\"", "\\\""));
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            if (dt.Rows.Count > 0)
            {
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            }
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        /// <summary>
        /// table转json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string IntToJson(int result)
        {
            string code = result == 0 ? "0" : "1";
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"code\":\"" + code + "\"}");
            return jsonBuilder.ToString();
        }
    }
}
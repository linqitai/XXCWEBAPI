using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using XXCWEBAPI.Models;
using XXCWEBAPI.Utils;

namespace XXCWEBAPI.Controllers
{   
    [RoutePrefix("api/visitors")]
    public class VisitorsController : ApiController
    {
        [HttpGet, Route("testLink")]
        public string TestLink()
        {
            string result = SQLHelper.LinkSqlDatabase();
            return ConvertHelper.resultJson(1, result);
            //return mssqlserver;
        }
        [HttpPost, Route("getListByTime")]
        /// <summary>
        /// flag (time:按照时间查询 top:按照次数查询)
        /// </summary>
        public string GetListByTime(VisitorsSearchModel v)
        {
            //string Flag = "Phone";
            //if (number.Length == 11)
            //{
            //    Flag = "Phone";
            //}
            //else {
            //    Flag = "SActualNo";
            //}
            string sql = "";
            SqlParameter[] pms = null;
            string timeStart = v.StartDate + " 00:00:01";
            string timeEnd = v.EndDate + " 23:59:59";
            pms = new SqlParameter[]{
                new SqlParameter("@timeStart",SqlDbType.NVarChar){Value = (timeStart)},
                new SqlParameter("@timeEnd",SqlDbType.NVarChar){Value = (timeEnd)}
            };
            
            //new SqlParameter("@Phone",SqlDbType.NVarChar){Value = (v.Phone)},
            //string sql_str = 
            sql = "select * from XXCLOUDVisitor.dbo.Table_Visitors where CreateTime between @timeStart and @timeEnd";
            DataTable dt;
            try
            {
                dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, pms);
                return "{\"code\":1,\"count\":" + dt.Rows.Count + ",\"data\":" + ConvertHelper.DataTableToJson(dt) + "}";
            }
            catch (Exception e)
            {
                //在webapi中要想抛出异常必须这样抛出，否则只抛出一个默认500的异常
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.ToString()),
                    ReasonPhrase = "error"
                };
                 throw new HttpResponseException(resp);
            }
        }
        [HttpPost, Route("getListByTimeAndPhone")]
        /// <summary>
        /// flag (time:按照时间查询 top:按照次数查询)
        /// </summary>
        public string getListByTimeAndPhone(VisitorsSearchModel v)
        {
            //string Flag = "Phone";
            //if (number.Length == 11)
            //{
            //    Flag = "Phone";
            //}
            //else {
            //    Flag = "SActualNo";
            //}
            string sql = "";
            SqlParameter[] pms = null;
            string timeStart = v.StartDate + " 00:00:01";
            string timeEnd = v.EndDate + " 23:59:59";
            pms = new SqlParameter[]{
                new SqlParameter("@timeStart",SqlDbType.NVarChar){Value = (timeStart)},
                new SqlParameter("@timeEnd",SqlDbType.NVarChar){Value = (timeEnd)},
                new SqlParameter("@Phone",SqlDbType.NVarChar){Value = (v.Phone)}
            };

            //new SqlParameter("@Phone",SqlDbType.NVarChar){Value = (v.Phone)},
            //string sql_str = 
            sql = "select * from XXCLOUDVisitor.dbo.Table_Visitors where CreateTime between @timeStart and @timeEnd and Phone = @Phone";
            DataTable dt;
            try
            {
                dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, pms);
                return "{\"code\":1,\"count\":" + dt.Rows.Count + ",\"data\":" + ConvertHelper.DataTableToJson(dt) + "}";
            }
            catch (Exception e)
            {
                //在webapi中要想抛出异常必须这样抛出，否则只抛出一个默认500的异常
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.ToString()),
                    ReasonPhrase = "error"
                };
                throw new HttpResponseException(resp);
            }
        }
        [HttpPost, Route("add")]
        public string Add(Visitors v)
        {
            string wramStr = "";
            if (string.IsNullOrEmpty(v.Name))
            {
                wramStr = "访客姓名不能为空";
                return ConvertHelper.resultJson(0, wramStr);
            }
            if (string.IsNullOrEmpty(v.Phone))
            {
                wramStr = "联系电话不能为空";
                return ConvertHelper.resultJson(0, wramStr);
            }
            if (string.IsNullOrEmpty(v.IdentityNumber))
            {
                wramStr = "证件号不能为空";
                return ConvertHelper.resultJson(0, wramStr);
            }
            string sql = "insert into XXCLOUDVisitor.dbo.Table_Visitors(Name, Sex, Phone, IdentityNumber, Reason, Number, PlateNumber, Unit, Date, StartTime, EndTime, Remark, Type, CreateTime)" +
                "values(@Name, @Sex, @Phone, @IdentityNumber, @Reason, @Number, @PlateNumber, @Unit, @Date, @StartTime,@EndTime, @Remark, @Type, @CreateTime)";
                DateTime dt = DateTime.Now;
                SqlParameter[] pms = new SqlParameter[]{
                    new SqlParameter("@Name",SqlDbType.NVarChar){Value=DataHelper.IsNullReturnLine(v.Name)},
                    new SqlParameter("@Sex",SqlDbType.NVarChar){Value=DataHelper.IsNullReturnLine(v.Sex)},
                    new SqlParameter("@Phone",SqlDbType.NVarChar){Value= DataHelper.IsNullReturnLine(v.Phone)},
                    new SqlParameter("@IdentityNumber",SqlDbType.NVarChar){Value=DataHelper.IsNullReturnLine(v.IdentityNumber)},
                    new SqlParameter("@Reason",SqlDbType.NVarChar){Value=DataHelper.IsNullReturnLine(v.Reason)},
                    new SqlParameter("@Number",SqlDbType.NVarChar){Value=DataHelper.IsNullReturnLine(v.Number)},
                    new SqlParameter("@PlateNumber",SqlDbType.NVarChar){Value=DataHelper.IsNullReturnLine(v.PlateNumber)},
                    new SqlParameter("@Unit",SqlDbType.NVarChar){Value=DataHelper.IsNullReturnLine(v.Unit)},
                    new SqlParameter("@Date",SqlDbType.NVarChar){Value=DataHelper.IsNullReturnLine(v.Date)},
                    new SqlParameter("@StartTime",SqlDbType.NVarChar){Value=DataHelper.IsNullReturnLine(v.StartTime)},
                    new SqlParameter("@EndTime",SqlDbType.NVarChar){Value=DataHelper.IsNullReturnLine(v.EndTime)},
                    new SqlParameter("@Remark",SqlDbType.NVarChar){Value=DataHelper.IsNullReturnLine(v.Remark)},
                    new SqlParameter("@Type",SqlDbType.NVarChar){Value=DataHelper.IsNullReturnLine(v.Type)},
                    new SqlParameter("@CreateTime",SqlDbType.NVarChar){Value=dt.ToString("yyyy-MM-dd")}
                };
                try
                {
                    int result = SQLHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, pms);
                    return ConvertHelper.IntToJson(result);
                }
                catch (Exception e)
                {
                    //在webapi中要想抛出异常必须这样抛出，否则只抛出一个默认500的异常
                    var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(e.ToString()),
                        ReasonPhrase = "error"
                    };
                    throw new HttpResponseException(resp);
                }
        }
    }
}

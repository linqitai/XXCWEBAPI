using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XXCWEBAPI.Models
{
    /// <summary>
    /// [VisitorsSearchModel]表实体类
    /// 作者:linqitai
    /// 创建时间:2019-05-22 14:34:19
    /// </summary>
    [Serializable]
    public partial class VisitorsSearchModel
    {
        public VisitorsSearchModel()
        { }
        private string _StartDate;
        /// <summary>
        /// 
        /// </summary>
        public string StartDate
        {
            set { _StartDate = value; }
            get { return _StartDate; }
        }
        private string _EndDate;
        /// <summary>
        /// 
        /// </summary>
        public string EndDate
        {
            set { _EndDate = value; }
            get { return _EndDate; }
        }
        private string _Phone;
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _Phone = value; }
            get { return _Phone; }
        }
    }
}
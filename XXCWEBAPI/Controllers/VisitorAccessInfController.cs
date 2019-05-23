using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XXCWEBAPI.Models;
using XXCWEBAPI.Utils;

namespace XXCWEBAPI.Controllers
{
    [RoutePrefix("api/visitorAccessInf")]
    public class VisitorAccessInfController : ApiController
    {
        [HttpGet, Route("getList")]
        public string GetList()
        {
            string sql = "select * from T_VisitorAccessInf";
            //string sql = "select * from T_BlacklistInf";
            DataTable dt = SQLHelper.ExecuteDataTable(sql, System.Data.CommandType.Text, null);
            return ConvertHelper.DataTableToJson(dt); 
        }
        [HttpPost, Route("addInf")]
        public string AddInf(VisitorAccessInf v)
        {
            string sql = "insert into T_VisitorAccessInf(VName, VSex, VNation, VBirthDate, VAddress, VIssuingAuthority, VExpiryDate, VCertificatePhoto, VLocalePhoto, VCertificateType, VCertificateNumber, VType, VFromCourtId, VInTime, VOutTime, VInPost, VOutPost, VInDoorkeeper, VOutDoorkeeper, VVisitingReason, VIntervieweeDept, VInterviewee, VOffice, VOfficePhone, VExtensionPhone, VMobilePhone, VRemark) values(@VName, @VSex, @VNation, @VBirthDate, @VAddress, @VIssuingAuthority, @VExpiryDate, @VCertificatePhoto, @VLocalePhoto, @VCertificateType, @VCertificateNumber, @VType, @VFromCourtId, @VInTime, @VOutTime, @VInPost, @VOutPost, @VInDoorkeeper, @VOutDoorkeeper, @VVisitingReason, @VIntervieweeDept, @VInterviewee, @VOffice, @VOfficePhone, @VExtensionPhone, @VMobilePhone, @VRemark)";
            SqlParameter[] pms = new SqlParameter[]{
            new SqlParameter("@VName",SqlDbType.NVarChar){Value=v.VName},
            new SqlParameter("@VSex",SqlDbType.NVarChar){Value=v.VSex},
            new SqlParameter("@VNation",SqlDbType.NVarChar){Value=v.VNation},
            new SqlParameter("@VBirthDate",SqlDbType.NVarChar){Value=v.VBirthDate},
            new SqlParameter("@VAddress",SqlDbType.NVarChar){Value=v.VAddress},
            new SqlParameter("@VIssuingAuthority",SqlDbType.NVarChar){Value=v.VIssuingAuthority},
            new SqlParameter("@VExpiryDate",SqlDbType.NVarChar){Value=v.VExpiryDate},
            new SqlParameter("@VCertificatePhoto",SqlDbType.Image){Value=v.VCertificatePhoto},
            new SqlParameter("@VLocalePhoto",SqlDbType.Image){Value=v.VLocalePhoto},
            new SqlParameter("@VCertificateType",SqlDbType.NVarChar){Value=v.VCertificateType},
            new SqlParameter("@VCertificateNumber",SqlDbType.NVarChar){Value=v.VCertificateNumber},
            new SqlParameter("@VType",SqlDbType.NVarChar){Value=v.VType},
            new SqlParameter("@VFromCourtId",SqlDbType.NVarChar){Value=v.VFromCourtId},
            new SqlParameter("@VInTime",SqlDbType.NVarChar){Value=v.VInTime},
            new SqlParameter("@VOutTime",SqlDbType.NVarChar){Value=v.VOutTime},
            new SqlParameter("@VInPost",SqlDbType.NVarChar){Value=v.VInPost},
            new SqlParameter("@VOutPost",SqlDbType.NVarChar){Value=v.VOutPost},
            new SqlParameter("@VInDoorkeeper",SqlDbType.NVarChar){Value=v.VInDoorkeeper},
            new SqlParameter("@VOutDoorkeeper",SqlDbType.NVarChar){Value=v.VOutDoorkeeper},
            new SqlParameter("@VVisitingReason",SqlDbType.NVarChar){Value=v.VVisitingReason},
            new SqlParameter("@VIntervieweeDept",SqlDbType.NVarChar){Value=v.VIntervieweeDept},
            new SqlParameter("@VInterviewee",SqlDbType.NVarChar){Value=v.VInterviewee},
            new SqlParameter("@VOffice",SqlDbType.NVarChar){Value=v.VOffice},
            new SqlParameter("@VOfficePhone",SqlDbType.NVarChar){Value=v.VOfficePhone},
            new SqlParameter("@VExtensionPhone",SqlDbType.NVarChar){Value=v.VExtensionPhone},
            new SqlParameter("@VMobilePhone",SqlDbType.NVarChar){Value=v.VMobilePhone},
            new SqlParameter("@VRemark",SqlDbType.NVarChar){Value=v.VRemark},
        };
            int result = SQLHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, pms);
            return ConvertHelper.IntToJson(result);
        }
    }
}

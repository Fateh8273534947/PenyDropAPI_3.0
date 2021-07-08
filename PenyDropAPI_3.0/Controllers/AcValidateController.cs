using AesEncEndToEnd;
using PenyDropAPI_3._0.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PenyDropAPI_3._0.Controllers
{
    public class AcValidateController : ApiController
    {
        [Route("api/Validate")]
        [HttpPost]
        public AcValResponse GetLiveBankData(InputAcvalClass ul)
        {
            AcValResponse res = new AcValResponse();
            if (ul.AppID == "" || ul.AppID == null)
            {
                res.Message = "Invalid AppId";
                res.Status = "Failure";
                res.ResCode = "AccErr002";
                return res;
            }
            if (ul.MerchantKey == "" || ul.MerchantKey == null)
            {
                res.Message = "Invalid MerchantKey";
                res.Status = "Failure";
                res.ResCode = "AccErr001";
                return res;
            }
            if (ul.ProductId == null || ul.ProductId == "")
            {
                res.Message = "Mandatory Data blank";
                res.Status = "Failure";
                res.ResCode = "AccErr003";
                return res;
            }

            if (ul.BranchId == null || ul.BranchId == "")
            {
                res.Message = "Mandatory Data blank";
                res.Status = "Failure";
                res.ResCode = "AccErr003";
                return res;
            }
            if (ul.TrxRef == null || ul.TrxRef == "")
            {
                res.Message = "Mandatory Data blank";
                res.Status = "Failure";
                res.ResCode = "AccErr003";
                return res;
            }

            string UserId = "";
            string EntityId = "";
            DataSet DSGlobal = GetData.GetSetpData(ClientEncrptionClass.DecryptAESEncEndToEnd(ul.AppID), ClientEncrptionClass.DecryptAESEncEndToEnd(ul.MerchantKey), ul.IFSC, "", ul.ProductId, ul.BranchId, ul.TrxRef);
            if (DSGlobal == null)
            {
                res.Message = "Invalid MerchantKey";
                res.Status = "Failure";
                res.ResCode = "AccErr001";
                return res;
            }
            if (DSGlobal.Tables[0].Rows.Count > 0)
            {
                UserId = DSGlobal.Tables[0].Rows[0]["UserId"].ToString();
                EntityId = Convert.ToString(DSGlobal.Tables[0].Rows[0]["EntityId"]);
            }
            if (DSGlobal != null && DSGlobal.Tables[0].Rows.Count == 0)
            {
                res.Message = "Invalid MerchantKey";
                res.Status = "Failure";
                res.ResCode = "AccErr001";
                return res;
            }
            if (ul.ProductId != "" && ul.ProductId != null && DSGlobal.Tables[2].Rows.Count == 0)
            {
                res.Message = "Mandatory Data blank";
                res.Status = "Failure";
                res.ResCode = "AccErr003";
                return res;
            }
            if (ul.BranchId != "" && ul.BranchId != null && DSGlobal.Tables[3].Rows.Count == 0)
            {
                res.Message = "Mandatory Data blank";
                res.Status = "Failure";
                res.ResCode = "AccErr003";
                return res;
            }
            if (DSGlobal.Tables[5].Rows.Count > 0)
            {
                res.Message = "Duplicate Transaction Reference Number";
                res.Status = "Failure";
                res.ResCode = "AccErr004";
                return res;
            }
            AcValidateClass obj = new AcValidateClass();
            DataTable dt = obj.AccountVal_Kotak(Convert.ToString(DSGlobal.Tables[6].Rows[0][0]), ClientEncrptionClass.DecryptAESEncEndToEnd(ul.BankAc), ul.IFSC, ClientEncrptionClass.DecryptAESEncEndToEnd(ul.AppID), Convert.ToString(DSGlobal.Tables[1].Rows[0]["APIURL"]), Convert.ToString(DSGlobal.Tables[1].Rows[0]["APIKey"]), Convert.ToString(DSGlobal.Tables[1].Rows[0]["APIBankName"]), UserId, EntityId, ul.TrxRef);
            if (dt.Rows.Count > 0)
            {
                res.Message = Convert.ToString(dt.Rows[0]["Description"]);
                res.Status = "Success";
                res.ResCode = GetResCode(Convert.ToString(dt.Rows[0]["Description"]));
                res.TrxRef = ul.TrxRef;
                res.NACHLive = Convert.ToString(DSGlobal.Tables[4].Rows[0]["PLive"]);
                res.NBLive = Convert.ToString(DSGlobal.Tables[4].Rows[0]["NetBanking"]);
                res.DCLive = Convert.ToString(DSGlobal.Tables[4].Rows[0]["DebitCard"]);
                res.AdLive = Convert.ToString(DSGlobal.Tables[4].Rows[0]["ALive"]);
                res.PhLIve = Convert.ToString(DSGlobal.Tables[4].Rows[0]["PLive"]);
                res.CustomerNameAsPerBank = Convert.ToString(dt.Rows[0]["BankReturnCustNme"]);
                return res;

            }
            return res;
        }
        public string GetResCode(string Status)
        {
            if (Status.ToUpper() == "Invalid Beneficiary details".ToUpper())
            {
                return "AccRes001";
            }
            if (Status.ToUpper() == "Invalid IFSC".ToUpper())
            {
                return "AccRes002";
            }
            if (Status.ToUpper() == "Bank/NPCI Service Down, Please try after some time".ToUpper())
            {
                return "AccRes003";
            }
            if (Status.ToUpper() == "Technical Issue at Bank, Retry after some time".ToUpper())
            {
                return "AccRes004";
            }
            if (Status.ToUpper() == "Account can not be validated in real time, try different bank".ToUpper())
            {
                return "AccRes005";
            }
            if (Status.ToUpper() == "Account and Name validated".ToUpper())
            {
                return "AccRes006";
            }
            if (Status.ToUpper() == "Account Blocked/Frozen".ToUpper())
            {
                return "AccRes007";
            }
            if (Status.ToUpper() == "Invalid A/c Number".ToUpper())
            {
                return "AccRes008";
            }

            if (Status.ToUpper() == "Invalid Account (NRE Account)".ToUpper())
            {
                return "AccRes009";
            }
            if (Status.ToUpper() == "Account Validated and NAME Validation is upto 20 char only incase more than 20 char match first 20 char and take informed decission".ToUpper())
            {
                return "AccRes010";
            }
            if (Status.ToUpper() == "Account validated but name mismatch".ToUpper())
            {
                return "AccRes011";
            }
            return "AccRes001";
        }
    }
}

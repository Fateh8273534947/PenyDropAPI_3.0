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
    public class GetLiveModeController : ApiController
    {
        [Route("api/GetLiveBank")]
        [HttpPost]
        public BankLiveResponse GetLiveBankData(InputResponseClass ul)
        {
            BankLiveResponse res = new BankLiveResponse();

            try
            {
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
                if (ul.BankCode == "" || ul.BankCode == null)
                {
                    res.Message = "Mandatory Data blank";
                    res.ResCode = "AccErr003";
                    res.Status = "Failure";
                    return res;
                }
                if (ul.BankCode.Trim().ToUpper() == "NONE")
                {
                    if (ul.IFSC == "" || ul.IFSC == null)
                    {
                        res.Message = "Mandatory Data blank";
                        res.ResCode = "AccErr003";
                        res.Status = "Failure";
                        return res;
                    }
                }
                if (ul.UserId != null && ul.TokenId != "")
                {
                    ul.UserId = ClientEncrptionClass.DecryptAESEncEndToEnd(ul.UserId);
                }
                DataSet DSGlobal = GetData.GetSetpData(ClientEncrptionClass.DecryptAESEncEndToEnd(ul.AppID), ClientEncrptionClass.DecryptAESEncEndToEnd(ul.MerchantKey), ul.IFSC, ul.BankCode,ul.UserId,ul.TokenId);
                if (DSGlobal == null)
                {
                    res.Message = "Invalid MerchantKey";
                    res.Status = "Failure";
                    res.ResCode = "AccErr001";
                    return res;
                }
                if (DSGlobal != null && DSGlobal.Tables[0].Rows.Count == 0)
                {
                    res.Message = "Invalid MerchantKey";
                    res.Status = "Failure";
                    res.ResCode = "AccErr001";
                    return res;
                }
                if (ul.UserId != "" && ul.UserId != null)
                {
                    if (Convert.ToString(DSGlobal.Tables[2].Rows[0][0]) == "2")
                    {
                        res.Message = "Session expired please login again";
                        res.Status = "Failure";
                        res.ResCode = "AccErr003";
                        return res;
                    }
                    if (DSGlobal.Tables[3].Rows.Count == 0)
                    {
                        res.Message = "Mandatory Data blank or invalid";
                        res.Status = "Failure";
                        res.ResCode = "AccErr003";
                        return res;
                    }
                }
                if (DSGlobal.Tables[1].Rows.Count > 0)
                {
                    res.Message = "Bank Data received successfully";
                    res.Status = "Success";
                    res.ResCode = "AccRes001";
                    res.NACHLive = Convert.ToString(DSGlobal.Tables[1].Rows[0]["PLive"]);
                    res.PhLIve = Convert.ToString(DSGlobal.Tables[1].Rows[0]["PLive"]);
                    res.NBLive = Convert.ToString(DSGlobal.Tables[1].Rows[0]["NetBanking"]);
                    res.DCLive = Convert.ToString(DSGlobal.Tables[1].Rows[0]["DebitCard"]);
                    res.AdLive = Convert.ToString(DSGlobal.Tables[1].Rows[0]["ALive"]);
                    return res;
                }

            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.ResCode = "AccErr003";
                res.Status = "Failure";
            }
            return res;
        }
    }
}

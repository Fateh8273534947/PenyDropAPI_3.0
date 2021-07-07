using AESEncryption;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace PenyDropAPI_3._0.Models
{
    public class AcValidateClass
    {
        public DataTable AccountVal_Kotak(string MandateId, string AcNo, string IFSC, string Appid, string APIUrl, string APIKey, string APIBankName, string UserId, string EntityID,string TraceNumber)
        {
            DataSet ds_Response = null;
            try
            {
                SqlConnection con = new SqlConnection(GlobalMethods.GlobalClass.connectionString);
                long uni = 0;
                string hash_string = string.Empty, action1 = string.Empty;//, TraceNumber = GetTraceNumber(Appid);
                string GMTformattedDateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");

                //DataSet dsRequestData = CommonManger.FillDatasetWithParam("sp_Payment_Kotak", "@QueryType", "@BeniACNo", "@BeniAcType", "@BeniAmount", "@BeniIFSC", "@ChkSum", "@UserId", "@EntityId", "@Filler1", "@Filler2", "@Filler3", "@Filler4", "@Filler5", "@MandateId", "@MerchantId", "@MessageCode", "@Remarks", "@RequestDateTime", "@RequestType","@TraceNo", "@ActivityId", "@Appid", "InsertpaymentReq_Kotak", AESEncryptionClass.EncryptAES(AcNo.ToString()), "10", "1.00", IFSC.ToString(), Convert.ToString(uni), UserId.ToString(), EntityID.ToString(), "Yoeki Soft Pvt. Ltd","9810462147", "", "", "", MandateId.ToString(), APIBankName, "6210", "Payment", GMTformattedDateTime, "R", TraceNumber, ActivityID.ToString(), AppID.ToString());

                string query = "sp_Payment_Kotak";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "InsertpaymentReq_Kotak");
                cmd.Parameters.AddWithValue("@BeniACNo", AcNo);
                cmd.Parameters.AddWithValue("@BeniAcType", "10");
                cmd.Parameters.AddWithValue("@BeniAmount", "1.00");
                cmd.Parameters.AddWithValue("@BeniIFSC", IFSC);
                cmd.Parameters.AddWithValue("@ChkSum", Convert.ToString(uni));
                cmd.Parameters.AddWithValue("@UserId", UserId);

                cmd.Parameters.AddWithValue("@EntityId", EntityID);
                cmd.Parameters.AddWithValue("@Filler1", "Yoeki Soft Pvt. Ltd");
                cmd.Parameters.AddWithValue("@Filler2", "9810462147");
                cmd.Parameters.AddWithValue("@Filler3", "");
                cmd.Parameters.AddWithValue("@Filler4", "");

                cmd.Parameters.AddWithValue("@Filler5", "");
                cmd.Parameters.AddWithValue("@MandateId", MandateId);
                cmd.Parameters.AddWithValue("@MerchantId", APIBankName);
                cmd.Parameters.AddWithValue("@MessageCode", "6210");
                cmd.Parameters.AddWithValue("@Remarks", "Payment");

                cmd.Parameters.AddWithValue("@RequestDateTime", GMTformattedDateTime);
                cmd.Parameters.AddWithValue("@RequestType", "R");
                cmd.Parameters.AddWithValue("@TraceNo", TraceNumber);
                cmd.Parameters.AddWithValue("@ActivityId", 0);
                cmd.Parameters.AddWithValue("@Appid", Appid);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsRequestData = new DataSet();
                da.Fill(dsRequestData);

                TraceNumber = Convert.ToString(dsRequestData.Tables[1].Rows[0][0]);
                if (dsRequestData != null && dsRequestData.Tables[0].Rows.Count > 0)
                {
                    string msg = hash_string + '|' + Convert.ToString(uni); //hash_string = hash_string + '|' + Convert.ToString(uni);
                    string ActionUrl = action1 + msg;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                    System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true; // **** Always accept
            };


                    string webData = "";

                    //if (IsLocal)
                    //{
                    //    if (ActivityID == 0)
                    //    { webData = "6220|24082018120636|YK17|wIw4GP24082018122440|KPY00|Successful Transaction|823612654816|KMB0000037731||Avinash Gupta|1658580580"; }
                    //    else
                    //    {
                    //        if (MandateId % 5 == 0)
                    //        {
                    //            webData = "6220|24082018120636|YK17|wIw4GP24082018122440|KPY00|Successful Transaction|823612654816|KMB0000037731||Avinash Gupta|1658580580";
                    //        }
                    //        else if (MandateId % 2 == 0)
                    //        {
                    //            webData = "6220|24082018120636|YK17|wIw4GP24082018122440|KPYM5|Account Closed|823612654816|KMB0000037731||Avinash Gupta|1658580580";
                    //        }
                    //        else if (MandateId % 3 == 0)
                    //        {
                    //            webData = "6220|24082018120636|YK17|wIw4GP24082018122440|KPYM1|Invalid A/c Number or IFSC Code|823612654816|KMB0000037731||Avinash Gupta|1658580580";
                    //        }
                    //        else
                    //        { webData = "6220|24082018120636|YK17|wIw4GP24082018122440|KPYM1|Invalid A/c Number or IFSC Code|823612654816|KMB0000037731||Avinash Gupta|1658580580"; }
                    //    }
                    //}
                    //else
                    //{
                    //System.Net.WebClient wc = new System.Net.WebClient();
                    //webData = wc.DownloadString(ActionUrl);
                    var inputAPI = new
                    {
                        AppID = AESEncryptionClass.EncryptAES(Appid.ToString()),
                        MandateID = AESEncryptionClass.EncryptAES(MandateId.ToString()),
                        TransRefrenceNo = AESEncryptionClass.EncryptAES(TraceNumber),
                        AccountNo = AESEncryptionClass.EncryptAES(AcNo),
                        IFSC = AESEncryptionClass.EncryptAES(IFSC),
                        CustomerName = "",
                        APIKey = AESEncryptionClass.EncryptAES(APIKey),
                    };
                    try
                    {
                        string inputJson = (new JavaScriptSerializer()).Serialize(inputAPI);
                        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                        WebClient client = new WebClient();
                        client.Headers["Content-type"] = "application/json";
                        client.Encoding = Encoding.UTF8;
                        string json = client.UploadString(APIUrl, inputJson);

                        PenyRes resdata = JsonConvert.DeserializeObject<PenyRes>(json);

                        webData = "6220|" + GMTformattedDateTime + "|" + APIBankName + "|" + TraceNumber + "|" + resdata.ResponseStatusCode + "|" + resdata.ResponseStatusDesc + "|823612654816|" + resdata.BankRefNo + "||" + resdata.RetCustomerName + "|1658580580";

                    }
                    catch (Exception ex)
                    {
                    }
                    // }

                    string[] Data = webData.Split('|');

                    //ds_Response = CommonManger.FillDatasetWithParam("sp_Payment_Kotak", "@QueryType", "@BankRefNo", "@BeniName", "@ChkSum", "@UserId", "@EntityId","@ErrorReason", "@MandateId", "@MerchantId", "@MessageCode", "@RRN", "@RequestDateTime", "@ResponseCode", "@TraceNo", "@BeniIFSC", "@ActivityId", "@PaymentRequestId", "@Appid","InsertpaymentRes_Kotak", Data[7], Data[9], Data[10], UserId.ToString(), EntityID.ToString(),Data[5], MandateId.ToString(), Data[2], Data[0], Data[6], Data[1], Data[4], Data[3], IFSC.ToString(), ActivityID.ToString(), dsRequestData.Tables[0].Rows[0]["PaymentRequestId"].ToString(), Appid);


                    string query1 = "sp_Payment_Kotak";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@QueryType", "InsertpaymentRes_Kotak");
                    cmd1.Parameters.AddWithValue("@BankRefNo", Data[7]);
                    cmd1.Parameters.AddWithValue("@BeniName", Data[9]);
                    cmd1.Parameters.AddWithValue("@ChkSum", Data[10]);
                    cmd1.Parameters.AddWithValue("@UserId", UserId);
                    cmd1.Parameters.AddWithValue("@EntityId", EntityID);
                    cmd1.Parameters.AddWithValue("@ErrorReason", Data[5]);
                    cmd1.Parameters.AddWithValue("@MandateId", MandateId);
                    cmd1.Parameters.AddWithValue("@MerchantId", Data[2]);
                    cmd1.Parameters.AddWithValue("@MessageCode", Data[0]);
                    cmd1.Parameters.AddWithValue("@RRN", Data[6]);
                    cmd1.Parameters.AddWithValue("@RequestDateTime", Data[1]);
                    cmd1.Parameters.AddWithValue("@ResponseCode", Data[4]);
                    cmd1.Parameters.AddWithValue("@TraceNo", Data[3]);
                    cmd1.Parameters.AddWithValue("@ActivityId", 0);
                    cmd1.Parameters.AddWithValue("@BeniIFSC", IFSC);
                    cmd1.Parameters.AddWithValue("@PaymentRequestId", dsRequestData.Tables[0].Rows[0]["PaymentRequestId"].ToString());
                    cmd1.Parameters.AddWithValue("@Appid", Appid);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    ds_Response = new DataSet();
                    da1.Fill(ds_Response);

                }
                return ds_Response.Tables[0];
            }
            catch (Exception ex)
            { return null; }
        }
        private static string GetTraceNumber(string AppId)
        {
            string GMTformattedDateTime = DateTime.Now.ToString("ddMMyyHHmmss");
            string TraceNumber = "";
            TraceNumber = AppId.Substring(0, 1) + AppId.Substring(AppId.Length - 3, 3) + GMTformattedDateTime + CreateRandomCode(4);
            return TraceNumber;
        }
        public static string CreateRandomCode(int CodeLength)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            // Loop Starts to Generate Random Number 
            for (int i = 0; i < CodeLength; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks)); // Used here System DateTime to give more diversification.
                }
                int t = rand.Next(62);
                if (temp != -1 && temp == t)
                {
                    // Recursive Calling of parent function 
                    return CreateRandomCode(CodeLength);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PenyDropAPI_3._0.Models
{
    public class GetData
    {
        public static DataSet GetSetpData(string AppId, string EnitityMarchantKey, string IFSC, string BankCode, string ProductName, string BranchName,string RefNO)
        {
            DataSet dsData = new DataSet();
            SqlConnection con = new SqlConnection(GlobalMethods.GlobalClass.connectionString);
            string query = "Sp_WebAPI_GetLiveBank";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@QueryType", "GetAcValSetpData");
            cmd.Parameters.AddWithValue("@EnitityMarchantKey", EnitityMarchantKey);
            cmd.Parameters.AddWithValue("@AppId", AppId);
            cmd.Parameters.AddWithValue("@IFSC", IFSC);
            cmd.Parameters.AddWithValue("@BankCode", BankCode);
            cmd.Parameters.AddWithValue("@ProductName", ProductName);
            cmd.Parameters.AddWithValue("@BranchName", BranchName);
            cmd.Parameters.AddWithValue("@RefNo", RefNO);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsData);
            return dsData;

        }
        public static DataSet GetSetpData(string AppId, string EnitityMarchantKey, string IFSC, string BankCode)
        {
            DataSet dsData = new DataSet();
            SqlConnection con = new SqlConnection(GlobalMethods.GlobalClass.connectionString);
            string query = "Sp_WebAPI_GetLiveBank";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@QueryType", "GetSetpData");
            cmd.Parameters.AddWithValue("@EnitityMarchantKey", EnitityMarchantKey);
            cmd.Parameters.AddWithValue("@AppId", AppId);
            cmd.Parameters.AddWithValue("@IFSC", IFSC);
            cmd.Parameters.AddWithValue("@BankCode", BankCode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsData);
            return dsData;

        }
    }
}
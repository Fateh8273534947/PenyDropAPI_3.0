﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PenyDropAPI_3._0.Models
{
    public class BankLiveResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public string ResCode { get; set; }
        public string NACHLive { get; set; }
        public string NBLive { get; set; }
        public string DCLive { get; set; }
        public string AdLive { get; set; }
        public string PhLIve { get; set; }
    }
    public class AcValResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public string ResCode { get; set; }
        public string NACHLive { get; set; }
        public string NBLive { get; set; }
        public string DCLive { get; set; }
        public string AdLive { get; set; }
        public string PhLIve { get; set; }
        public string TrxRef { get; set; }
        public string BankReturnCustName { get; set; }

    }

    public class InputResponseClass
    {
        public string AppID { get; set; }
        public string MerchantKey { get; set; }
        public string IFSC { get; set; }
        public string BankCode { get; set; }
    }
    public class InputAcvalClass
    {
        public string AppID { get; set; }
        public string MerchantKey { get; set; }
        public string BankAc { get; set; }
        public string IFSC { get; set; }
        public string TrxRef { get; set; }
        public string ProductId { get; set; }
        public string BranchId { get; set; }


    }
}
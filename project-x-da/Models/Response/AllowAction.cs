using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace project_x_da.Models.Response
{
    public class AllowAction
    {

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool AllowSelect { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool AllowCreate { get; set; }

        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool AllowView { get; set; }
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool AllowEdit { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool AllowDelete { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool AllowRequest { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool AllowAdvanceMoney { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool AllowPublic { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool AllowRefund { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RequestAction AllowRequestActions { get; set; }

        public class RequestAction
        {

            public bool AllowSubmit { get; set; }

            public bool AllowApprove { get; set; }

            public bool AllowReject { get; set; }

            public bool AllowCancel { get; set; }
        }

        public static AllowAction.RequestAction GetRequestAllow(string status, string caller = "")
        {
            if (string.IsNullOrEmpty(status))
                return null;
            if (caller == "")
                //return null;
                return new AllowAction.RequestAction
                {
                    AllowApprove = false,
                    AllowCancel = false,
                    AllowReject = false,
                    AllowSubmit = false,
                };

            AllowAction.RequestAction result = new AllowAction.RequestAction
            {
                AllowApprove = false,
                AllowCancel = false,
                AllowReject = false,
                AllowSubmit = false,
            };

            status = status.ToLower();
            switch (status)
            {
                case "open":
                    result.AllowSubmit = true;
                    break;
                case "waiting":
                    result.AllowApprove = true;
                    result.AllowCancel = true;
                    result.AllowReject = true;
                    break;
                case "approved":
                    result.AllowReject = true;
                    result.AllowCancel = true;
                    break;
                case "approve":
                    result.AllowCancel = true;
                    break;
                case "reject":
                    result.AllowReject = true;
                    result.AllowSubmit = true;
                    break;
                case "cancel":
                    result.AllowSubmit = true;
                    break;
                default:
                    //result = null;
                    break;
            }
            return result;
        }

    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace project_x_da.Models.Response
{
    public class DefaultResponseContext
    {
        public DefaultResponseContext()
        {
            Message = "";
            Data = null;
            Count = -999;
            Pages = -999;
            CountAll = -999;
            Summary = null;
            AllowActions = null;
        }
        public string Message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }
        [DefaultValue(-999)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Count { get; set; }
        [DefaultValue(-999)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Pages { get; set; }

        [DefaultValue(-999)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int CountAll { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Summary { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AllowAction AllowActions { get; set; }
    }
}
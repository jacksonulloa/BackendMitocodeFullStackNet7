using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.ResponseBase
{
    public class BaseResponse
    {
        [JsonProperty(Order = 1)]
        public string codResp { get; set; } = string.Empty;
        [JsonProperty(Order = 2)]
        public string descResp { get; set; } = string.Empty;
    }
}

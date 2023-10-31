using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.ResponseBase
{
    public class BaseResponseSingular<T> : BaseResponse
    {
        [JsonProperty(Order = 3)]
        public T? Data { get; set; }
    }
}

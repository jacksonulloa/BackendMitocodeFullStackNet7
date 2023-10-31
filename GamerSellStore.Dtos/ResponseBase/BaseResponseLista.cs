using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.ResponseBase
{
    public class BaseResponseLista<T> : BaseResponse
    {
        [JsonProperty(Order = 3)]
        public ICollection<T>? Data { get; set; }
        [JsonProperty(Order = 4)]
        public int TotalPages { get; set; }
    }
}

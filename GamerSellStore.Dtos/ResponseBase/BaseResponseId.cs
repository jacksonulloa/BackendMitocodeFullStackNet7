using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.ResponseBase
{
    public class BaseResponseId : BaseResponse
    {
        public int Id { get; set; } = 0;
    }
}

using AutoMapper;
using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Services.Profiles
{
    public class ConsolaProfile : Profile
    {
        public ConsolaProfile()
        {
            CreateMap<ConsolaAddDtoRequest, Consola>()
                .ForMember(x => x.Estado, y => y.MapFrom(z => z.Estado == 1 ? true : false));
            CreateMap<Consola, ConsolaSearchDtoResponse>()
                .ForMember(x => x.Estado, y => y.MapFrom(z => z.Estado ? 1 : 0));
            CreateMap<ConsolaDtoRequest, Consola>()
                .ForMember(x => x.Estado, y => y.MapFrom(z => z.Estado == 1 ? true : false));
        }
    }
}

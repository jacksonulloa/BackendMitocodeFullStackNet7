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
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<PublisherAddDtoRequest, Publisher>()
                .ForMember(x => x.Estado, y => y.MapFrom(z => z.Estado == 1 ? true : false));
            CreateMap<Publisher, PublisherSearchDtoResponse>()
                .ForMember(x => x.Estado, y => y.MapFrom(z => z.Estado ? 1 : 0));
            CreateMap<PublisherDtoRequest, Publisher>()
                .ForMember(x => x.Estado, y => y.MapFrom(z => z.Estado == 1 ? true : false));
        }
    }
}

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
    public class ClasificacionProfile:Profile
    {
        public ClasificacionProfile()
        {
            CreateMap<ClasificacionAddDtoRequest, Clasificacion>()
                .ForMember(x => x.Estado, y => y.MapFrom(z => z.Estado == 1 ? true : false));
            CreateMap<Clasificacion, ClasificacionSearchDtoResponse>()
                .ForMember(x => x.Estado, y => y.MapFrom(z => z.Estado ? 1 : 0));
            CreateMap<ClasificacionDtoRequest, Clasificacion>()
                .ForMember(x => x.Estado, y => y.MapFrom(z => z.Estado == 1 ? true : false));
        }
    }
}

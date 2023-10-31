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
    public class TituloProfile : Profile
    {
        public TituloProfile()
        {
            CreateMap<TituloAddDtoRequest, Titulo>()
                .ForMember(x => x.Estado, y => y.MapFrom(z => z.Estado == 1 ? true : false));
            CreateMap<Titulo, TituloSearchDtoResponse>()
                .ForMember(d => d.Consola, o => o.MapFrom(x => x.Consola.Nombre))
                .ForMember(d => d.Clasificacion, o => o.MapFrom(x => x.Clasificacion.Nombre))
                .ForMember(d => d.Genero, o => o.MapFrom(x => x.Genero.Nombre))
                .ForMember(d => d.Publisher, o => o.MapFrom(x => x.Publisher.Nombre))
                .ForMember(x => x.EstadoDesc, y => y.MapFrom(z => z.Estado ? "Activo" : "Inactivo"))
                .ForMember(x => x.AgotadoDesc, y => y.MapFrom(z => z.Agotado == 0 ? "Disponible" : "Agotado"));
            CreateMap<Titulo, TituloFindDtoResponse>()
                .ForMember(x => x.Estado, y => y.MapFrom(z => z.Estado ? 1 : 0));
            CreateMap<TituloDtoRequest, Titulo>()
                .ForMember(x => x.Estado, y => y.MapFrom(z => z.Estado == 1 ? true : false));
        }
    }
}

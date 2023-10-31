using AutoMapper;
using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Services.Profiles
{
    public class EvaluacionProfile : Profile
    {
        private static readonly CultureInfo Culture = new("es-PE");
        public EvaluacionProfile()
        {
            CreateMap<EvaluacionAddDtoRequest, Evaluacion>();

            CreateMap<Evaluacion, EvaluacionDtoResponse>()
                .ForMember(d => d.EvaluacionId, o => o.MapFrom(x => x.Id))
                .ForMember(d => d.FechaStr, o => o.MapFrom(x => x.Fecha.ToString("D", Culture)))
                .ForMember(d => d.HoraStr, o => o.MapFrom(x => x.Fecha.ToString("T", Culture)))
                .ForMember(d => d.Titulo, o => o.MapFrom(x => x.Titulo.Nombre))
                .ForMember(d => d.Cliente, o => o.MapFrom(x => x.Cliente.Nombre));
            //.ForMember(d => d.HoraStrTxn, o => o.MapFrom(x => x.FechaTxn.ToString("dd/MM/yyyy HH:mm", Culture)))

        }
    }
}

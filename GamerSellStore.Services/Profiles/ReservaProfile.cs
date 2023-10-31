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
    public class ReservaProfile : Profile
    {
        private static readonly CultureInfo Culture = new("es-PE");

        public ReservaProfile()
        {
            CreateMap<ReservaAddDtoRequest, Reserva>()
                .ForMember(d => d.Cantidad, o => o.MapFrom(x => x.CantidadReserva));

            CreateMap<Reserva, ReservaDtoResponse>()
                .ForMember(d => d.ReservaId, o => o.MapFrom(x => x.Id))
                .ForMember(d => d.FechaStrTxn, o => o.MapFrom(x => x.FechaTxn.ToString("D", Culture)))
                .ForMember(d => d.HoraStrTxn, o => o.MapFrom(x => x.FechaTxn.ToString("T", Culture)))
                .ForMember(d => d.Titulo, o => o.MapFrom(x => x.Titulo.Nombre))
                .ForMember(d => d.Cliente, o => o.MapFrom(x => x.Cliente.Nombre))
                .ForMember(d => d.CantidadReservada, o => o.MapFrom(x => x.Cantidad));
                //.ForMember(d => d.HoraStrTxn, o => o.MapFrom(x => x.FechaTxn.ToString("dd/MM/yyyy HH:mm", Culture)))

        }
        
    }
}

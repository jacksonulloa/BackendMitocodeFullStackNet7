using Azure.Core;
using GamerSellStore.Dtos.Request.Correo;
using GamerSellStore.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Services.Interfaces
{
    public interface ICorreo
    {
        Task EnviarCorreoAsync(CorreoDtoRequest request);
    }
}

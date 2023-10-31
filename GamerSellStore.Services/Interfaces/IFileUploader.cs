using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Services.Interfaces
{
    public interface IFileUploader
    {
        Task<string> CargarImagenAsync(string? base64Image, string? nombreArchivo);
    }
}

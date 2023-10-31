using GamerSellStore.Entities;
using GamerSellStore.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Services.Implementations
{
    public class FileUploader : IFileUploader
    {
        private readonly ILogger<FileUploader> logger;
        private readonly AppSettings appSettings;

        //Para usar como repositorio de imagenes el entorno local
        public FileUploader(IOptions<AppSettings> options, ILogger<FileUploader> _logger) 
        { 
            logger = _logger;
            appSettings = options.Value;
        }

        public async Task<string> CargarImagenAsync(string? base64Image, string? nombreArchivo)
        {
            string resp="";
            if(string.IsNullOrEmpty(base64Image) || string.IsNullOrEmpty(nombreArchivo))
            {
                return string.Empty;
            }
            try
            {
                var bytes = Convert.FromBase64String(base64Image);
                var path = Path.Combine(appSettings.storageConfiguration.Path, nombreArchivo);
                await using var filestream = new FileStream(path, FileMode.Create);
                //Escribimos el archivo de la posicion 0 hasta el final sin comprimir
                await filestream.WriteAsync(bytes, 0, bytes.Length);
                resp = $"{appSettings.storageConfiguration.PublicUrl}{nombreArchivo}";
                logger.LogInformation($"Se subio la imagen {resp}");
            }
            catch(Exception exc)
            {
                logger.LogError($"Error al subir el archivo:[{nombreArchivo}], {exc.Message}");
                return string.Empty;
            }
            return resp;
        }
    }
}

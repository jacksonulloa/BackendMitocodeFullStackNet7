using GamerSellStore.Dtos.Request;
using GamerSellStore.Services.Interfaces;

namespace GamerSellStore.Api.Endpoints
{
    public static class HomeEndPoints
    {
        public static void MapHomeEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/Home", async (IReservaService iReservaService, 
                IGeneroService iGeneroService, IConsolaService iConsolaService, 
                IClasificacionService iClasificacionService, IPublisherService iPublisherService, 
                CancellationToken cancellationToken) =>
            {
                var reservas = await iReservaService.ListarAsync(new ReservaBusqueda()
                {
                    pagina = 1,
                    filas = 50
                }, cancellationToken);

                var generos = await iGeneroService.ListarAsync();
                var clasificaciones = await iClasificacionService.ListarAsync();
                var publishers = await iPublisherService.ListarAsync();
                var consolas = await iConsolaService.ListarAsync();

                return Results.Ok(new
                {
                    Reservas = reservas.Data,
                    Generos = generos.Data,
                    Clasificaciones = clasificaciones.Data,
                    Publishers = publishers.Data,
                    Consolas = consolas.Data,
                    codResp = "00",
                    descResp = "Conforme"
                });
            }).WithDescription("Permite mostrar los endpoints de la pagina principal").WithOpenApi();
        }
    }
}

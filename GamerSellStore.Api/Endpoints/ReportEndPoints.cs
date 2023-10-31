using GamerSellStore.Dtos.Request;
using GamerSellStore.Services.Interfaces;

namespace GamerSellStore.Api.Endpoints
{
    public static class ReportEndPoints
    {
        public static void MapReports(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("api/Reports")
                .WithDescription("Reportes de Gamer Sell Store")
                .WithTags("Reports");

            group.MapGet("/", async (IReservaService iReservaService, string dateStart, string dateEnd) =>
            {
                ReservaReporteDtoRequest request = new ReservaReporteDtoRequest();
                request.Desde = dateStart;
                request.Hasta = dateEnd;
                var response = await iReservaService.ReporteReservaAsync(request);

                return response is not null ? Results.Ok(response) : Results.BadRequest(response);
            });
        }
    }
}

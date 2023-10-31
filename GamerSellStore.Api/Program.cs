using AutoMapper;
using GamerSellStore.Api.Endpoints;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;
using GamerSellStore.Entities;
using GamerSellStore.Persistence;
using GamerSellStore.Repositories;
using GamerSellStore.Services.Implementations;
using GamerSellStore.Services.Interfaces;
using GamerSellStore.Services.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

var corsConfiguration = "GamerSellStoreCors";

var builder = WebApplication.CreateBuilder(args);
//var logger = new LoggerConfiguration()
//    //.WriteTo.Console(LogEventLevel.Information)
//    .WriteTo.File("..\\logs\\log.log", rollingInterval: RollingInterval.Day,
//         restrictedToMinimumLevel: LogEventLevel.Warning,
//         fileSizeLimitBytes: 4 * 1024)
//    .CreateLogger();
var logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(
                //path: $".\\bin\\Debug\\net7.0\\logs\\log-{DateTime.Now:yyyyMMdd}.log",
                path: $"{builder.Configuration["SeriLogConfig:PathCreation"]}log-{DateTime.Now:yyyyMMdd}.log",
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 10_000_000,
                rollOnFileSizeLimit: true,
                shared: true)
            //flushToDiskInterval: TimeSpan.FromSeconds(1))
            .CreateLogger();
builder.Logging.AddSerilog(logger);

//Mapeo del contenido del archivo appsettings.json a una clase
builder.Services.Configure<AppSettings>(builder.Configuration);

//Añadiendo y configurando el CORs
builder.Services.AddCors(setup => {
    setup.AddPolicy(corsConfiguration, policy => {
        policy.AllowAnyOrigin(); //Que cualquiera pueda consumir el api
        policy.AllowAnyHeader(); //Que aca se puede usar bearer token
        policy.AllowAnyMethod(); //Se habilite el patch y el curl
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GamerSellStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GamerSellStoreDb"));
    options.EnableSensitiveDataLogging();
    options.ConfigureWarnings(configurationBuilder => configurationBuilder
        .Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
    options.ConfigureWarnings(x => x
        .Ignore(CoreEventId.SensitiveDataLoggingEnabledWarning));
});

builder.Services.AddIdentity<GamerSellStoreUserIdentity, IdentityRole>(politicas =>
{
    politicas.Password.RequireDigit = true;
    politicas.Password.RequireLowercase = true;
    politicas.Password.RequireUppercase = false;
    politicas.Password.RequireNonAlphanumeric = true;
    politicas.Password.RequiredLength = 6;
    politicas.User.RequireUniqueEmail = true;
    //Politicas de bloqueo de cuentas
    politicas.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    politicas.Lockout.MaxFailedAccessAttempts = 5;
}).AddEntityFrameworkStores<GamerSellStoreDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddTransient<IGeneroRepository, GeneroRepository>();
builder.Services.AddTransient<IConsolaRepository, ConsolaRepository>();
builder.Services.AddTransient<IClasificacionRepository, ClasificacionRepository>();
builder.Services.AddTransient<IPublisherRepository, PublisherRepository>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<ITituloRepository, TituloRepository>();
builder.Services.AddTransient<IReservaRepository, ReservaRepository>();
builder.Services.AddTransient<IEvaluacionRepository, EvaluacionRepository>();

builder.Services.AddTransient<IGeneroService, GeneroService>();
builder.Services.AddTransient<IConsolaService, ConsolaService>();
builder.Services.AddTransient<IClasificacionService, ClasificacionService>();
builder.Services.AddTransient<IPublisherService, PublisherService>();
builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<ITituloService, TituloService>();
builder.Services.AddTransient<IReservaService, ReservaService>();
builder.Services.AddTransient<IEvaluacionService, EvaluacionService>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICorreo, Correo>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<GeneroProfile>();
    config.AddProfile<ConsolaProfile>();
    config.AddProfile<ClasificacionProfile>();
    config.AddProfile<PublisherProfile>();
    config.AddProfile<ClienteProfile>();
    config.AddProfile<TituloProfile>();
    config.AddProfile<ReservaProfile>();
    config.AddProfile<EvaluacionProfile>();
});

//Configurando el JWT - Añadiendo AUTENTICACIÓN (Valida usuario y password)
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
 {
     var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ??
         throw new InvalidOperationException("No se configuro el Jwt"));
     x.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = builder.Configuration["Jwt:Issuer"],
         ValidAudience = builder.Configuration["Jwt:Audience"],
         IssuerSigningKey = new SymmetricSecurityKey(key)
     };
 });

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Configurando el JWT - Añadiendo AUTORIZACIÓN (Valida los permisos)
app.UseAuthentication();
app.UseAuthorization();

//Pasando el CORs en el middleware siempre debajo del authenticacion y el authorization
app.UseCors(corsConfiguration);

app.MapReports();
app.MapHomeEndpoints();

app.MapControllers();
//Para que se puedan ver las imagenes en el directorio virtual
app.UseStaticFiles();

//(Comentado por correccion por parte del instructor)Para permitir trabajar sin problemas a los AllowAnonymous
//app.Use(async (context, next) =>
//{
//    var identity = context.User.Identity;
//    if (identity is { IsAuthenticated: true })
//    {
//        var fechaExpiracion = context.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Expiration)!.Value;

//        if (fechaExpiracion is not null && DateTime.Parse(fechaExpiracion) <= DateTime.Now)
//        {
//            var response = new LoginDtoResponse()
//            {
//                codResp = "99",
//                descResp = $"El token ya ha expirado a las {fechaExpiracion}",
//                NombreCompleto = "",
//                Roles = new List<string>(),
//                Token = ""
//            };
//            context.Response.StatusCode = 401;
//            await context.Response.WriteAsJsonAsync(JsonSerializer.Serialize(response));
//        }
//        await next();
//    }
//    else
//    {
//        await next();
//    }
//});

//Un scope permite tener en un contexto los servicios inyectados previamente
await using (var scope = app.Services.CreateAsyncScope())
{
    if (app.Environment.IsDevelopment())
    {
        //Indicamos que primero aplique las migraciones
        var db = scope.ServiceProvider.GetRequiredService<GamerSellStoreDbContext>(); //Creamos instancia del db context
        await db.Database.MigrateAsync(); //Esto ejecuta las migraciones de forma automatica
    }
    //Aqui vamos a ejecutar la creacion del usuario admin y los roles por default
    await UserDataSeeder.Seed(scope.ServiceProvider);
}

app.Run();


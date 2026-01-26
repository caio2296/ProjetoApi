using Aplicacao;
using Aplicacao.Interface;
using Dominio.Interface;
using Dominio.Interface.Generico;
using Dominio.Servicos;
using Dominio.Servicos.Interfaces;
using Infraestrutura.Repositorio;
using Infraestrutura.Repositorio.Generico;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Projeto.Middleware;
using Projeto.Token;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        "logs/api-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7
    )
    .WriteTo.File(
        "Logs/memory-log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7
    )
    .CreateLogger();



builder.Host.UseSerilog();


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddScoped<IFrutas>(provider =>
    new RepositorioFrutas(
        provider.GetRequiredService<IConfiguration>()
            .GetConnectionString("Default")!
    ));
builder.Services.AddScoped<ICalendar>(provider =>
    new RepositorioCalendar(
        provider.GetRequiredService<IConfiguration>()
            .GetConnectionString("Default")!));

builder.Services.AddScoped<IUsuario>(provider =>
    new RepositorioUsuario(
        provider.GetRequiredService<IConfiguration>()
                  .GetConnectionString("Default")!));

builder.Services.AddScoped<IFiltros>(provider =>
    new RepositorioFiltro(
        provider.GetRequiredService<IConfiguration>()
                  .GetConnectionString("Default")!));

builder.Services.AddControllers();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IFiltroAplicacao, FiltroAplicacao>();

builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();
builder.Services.AddScoped<ICalendarAplicacao, CalendarAplicacao>();
builder.Services.AddScoped<IFrutasAplicacao, FrutasAplicacao>();
builder.Services.AddScoped<TokenJwtBuilder>();

builder.Services.AddScoped<IFiltrosServicos, FiltrosServico>();
builder.Services.AddScoped<IFrutasServicos, FrutasServico>();
builder.Services.AddScoped<IUsuarioServico, UsuarioServico>();
builder.Services.AddScoped<ICalendarService, CalendarService>();

builder.Services.AddSingleton(typeof(IGenerico<>), typeof(RepositorioGenerico<>));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("UsuarioTipo", "adm");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando o Bearer.
                        Entre com 'Bearer ' [espaço] então coloque seu token.
                        Exemplo: 'Bearer 12345oiuytr'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
} );

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "Security.Bearer",
            ValidAudience = "Security.Bearer",

            IssuerSigningKey = JwtSecurityKey.Creater("MinhaSuperChaveJWT_Secreta_123456789!")
        };

        option.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

var frontClient = "http://localhost:4200";
app.UseCors(x =>
x.AllowAnyMethod()
.AllowAnyHeader()
.WithOrigins(frontClient));

var chaveSecreta = "MinhaSuperChaveJWT_Secreta_123456789!";
app.UseMiddleware<JwtTokenMiddleware>(chaveSecreta, builder.Configuration.GetConnectionString("Default"));
app.UseMiddleware<MemoryLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

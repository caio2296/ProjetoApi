using Aplicacao;
using Aplicacao.Interface;
using Dominio.Interface;
using Dominio.Interface.Generico;
using Dominio.Servicos;
using Dominio.Servicos.Interfaces;
using Entidades.SendEmail;
using Infraestrutura.Repositorio;
using Infraestrutura.Repositorio.Generico;
using Infraestrutura.SendEmail;
using Infraestrutura.Worker;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Projeto.Middleware;
using Projeto.Token;
using Serilog;
using Serilog.Events;
using System.Data;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(
        "logs/api-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        outputTemplate:
        "{Timestamp:HH:mm:ss} [{Level:u3}] ({ThreadId}) {Message:lj}{NewLine}{Exception}"
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

// 🔹 Mapeia EmailSettings
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings")
);

// 🔹 Infraestrutura (quem envia de fato)
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();

// 🔹 Application
builder.Services.AddScoped<ISendEmailService,SendEmailService>();

//serviço de email desacoplado

builder.Services.AddSingleton<IEmailQueue, EmailQueue>();
builder.Services.AddHostedService<EmailBackgroundService>();

builder.Services.AddHostedService<WarmupService>();


builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.DefaultIgnoreCondition =
                JsonIgnoreCondition.WhenWritingNull;
        }); 

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IFiltroAplicacao, FiltroAplicacao>();

builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();
builder.Services.AddScoped<ICalendarAplicacao, CalendarAplicacao>();
builder.Services.AddScoped<IFrutasAplicacao, FrutasAplicacao>();
builder.Services.AddScoped<ISendEmailAplicacao, SendEmailAplicacao>();
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

//compressão para a serialização dos json

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.DefaultIgnoreCondition =
        JsonIgnoreCondition.WhenWritingNull;
    opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opt.SerializerOptions.WriteIndented = false;
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;

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


// WARMUP para evitar cold start (JWT + Database + Stored Procedures)
app.Lifetime.ApplicationStarted.Register(() =>
{
    Task.Run(async () =>
    {
        try
        {
            using var scope = app.Services.CreateScope();

            // =========================
            // 🔐 JWT Warmup
            // =========================
            var tokenBuilder =
                scope.ServiceProvider.GetRequiredService<TokenJwtBuilder>();

            tokenBuilder.GerarTokenJwt("0", "adm", "warmup@email.com");

            Log.Information("JWT Warmup executado com sucesso.");

            var connString = builder.Configuration.GetConnectionString("Default");

            await using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            // =========================
            // 👤 Warmup SP SelecionarUsuario
            // =========================
            await using (var cmdUsuario = new SqlCommand("SelecionarUsuario", conn))
            {
                cmdUsuario.CommandType = CommandType.StoredProcedure;

                // ❗ evitar AddWithValue
                cmdUsuario.Parameters
                    .Add("@Email", SqlDbType.VarChar, 150)
                    .Value = "warmup@email.com";

                await cmdUsuario.ExecuteScalarAsync();
            }

            Log.Information("Warmup SelecionarUsuario executado.");

            // =========================
            // 🔎 Warmup SP sp_MontaJsonPorPagina
            // =========================
            await using (var cmdFiltro = new SqlCommand("dbo.sp_MontaJsonPorPagina", conn))
            {
                cmdFiltro.CommandType = CommandType.StoredProcedure;

                cmdFiltro.Parameters
                    .Add("@IdPagina", SqlDbType.Int)
                    .Value = 1;

                var outputParam = new SqlParameter("@JsonFinal", SqlDbType.NVarChar, -1)
                {
                    Direction = ParameterDirection.Output
                };

                cmdFiltro.Parameters.Add(outputParam);

                await cmdFiltro.ExecuteNonQueryAsync();

                //// ✅ VERIFICA RESULTADO
                //var jsonResultado = outputParam.Value?.ToString();

                //Log.Information("Warmup retorno JSON tamanho: {Length}",
                //    jsonResultado?.Length ?? 0);
            }

            // =========================
            // 🔎 Warmup SP sp_MontaJsonPorPagina
            // =========================
            await using (var cmdCalendar = new SqlCommand("GetCalendario", conn))
            {
                cmdCalendar.CommandType = CommandType.StoredProcedure;


                await cmdCalendar.ExecuteScalarAsync();

                Log.Information("Warmup Calendario executado.");
            }

            Log.Information("Warmup Filtros executado.");

            Log.Information("🔥 Warmup JWT + Database + Stored Procedures concluído.");

            //// =========================
            //// 🌐 HTTP PIPELINE WARMUP
            //// =========================

            //var baseUrl = "https://localhost:44325"; // ⚠️ AJUSTE PARA SUA PORTA

            //using var httpClient = new HttpClient
            //{
            //    BaseAddress = new Uri(baseUrl)
            //};

            //await Task.WhenAll(
            //    httpClient.GetAsync("/api/BuscarFiltro/1")

            //);

            //Log.Information("🔥 Warmup HTTP Pipeline executado.");

            //Log.Information("🚀 Warmup COMPLETO finalizado.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erro no warmup geral");
        }
    });
});


//compressão para a serialização dos json

app.UseResponseCompression();

app.Run();

using Aplicacao;
using Aplicacao.Interface;
using Dominio.Interface;
using Dominio.Interface.Generico;
using Infraestrutura.Repositorio;
using Infraestrutura.Repositorio.Generico;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<Contexto>(options =>
//            options.UseSqlServer(
//                 builder.Configuration.GetConnectionString("Default")));


// Já inclui o IConfiguration automaticamente
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Registre seu repositório injetando a connection string
builder.Services.AddScoped<IFrutas>(provider =>
    new RepositorioFrutas(
        provider.GetRequiredService<IConfiguration>()
            .GetConnectionString("Default")!
    ));
builder.Services.AddScoped<ICalendar>(provider =>
    new RepositorioCalendar(
        provider.GetRequiredService<IConfiguration>()
            .GetConnectionString("Default")!));

builder.Services.AddControllers();

builder.Services.AddScoped<ICalendarAplicacao, CalendarAplicacao>();
builder.Services.AddScoped<IFrutasAplicacao, FrutasAplicacao>();

builder.Services.AddSingleton(typeof(IGenerico<>), typeof(RepositorioGenerico<>));

//builder.Services.AddScoped<IFrutas, RepositorioFrutas>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var frontClient = "http://localhost:4200";
app.UseCors(x =>
x.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
.WithOrigins(frontClient));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

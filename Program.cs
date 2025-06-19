using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Muralis.Desafio.Api.Data;
using Muralis.Desafio.Api.Services;
using Muralis.Desafio.Api.Services.Interfaces;
using System.Text.Json.Serialization; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; 
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<IViaCepService, ViaCepService>();
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        await context.Response.WriteAsJsonAsync(new
        {
            erro = exception?.Message ?? "Erro desconhecido."
        });
    });
});


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
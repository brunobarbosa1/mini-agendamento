using Agendamento.Application.Service;
using Agendamento.Data.Data;
using Agendamento.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Agendamento.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.   
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();
builder.Services.AddControllers();
builder.ConfigureStringEnum();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapControllers();
}

app.UseHttpsRedirection();
app.Run();
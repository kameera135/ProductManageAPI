using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductManageAPI.Interfaces;
using ProductManageAPI.Models;
using ProductManageAPI.Repositories;
using ProductManageAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//database service
builder.Services.AddDbContextFactory<PmsContext>(opts=>
opts.UseSqlServer(builder.Configuration.GetConnectionString("PMSDB")));

builder.Services.AddScoped<IAuthRepository, AuthRepositories>();
builder.Services.AddScoped<IAuthService, AuthServices>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();

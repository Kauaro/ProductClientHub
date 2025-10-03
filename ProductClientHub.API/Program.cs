using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Filters;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.Delete;
using ProductClientHub.API.UseCases.Clients.GetAll;
using ProductClientHub.API.UseCases.Clients.GetClient;
using ProductClientHub.API.UseCases.Clients.Register;
using ProductClientHub.API.UseCases.Clients.Update;
using ProductClientHub.API.UseCases.Products.Delete;
using ProductClientHub.API.UseCases.Products.Register;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductClientHubDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RegisterUsuarioUseCase>();
builder.Services.AddScoped<UpdateUsuarioUseCase>();
builder.Services.AddScoped<GetAllUsuarioUseCase>();
builder.Services.AddScoped<GetUsuarioByIdUseCase>();
builder.Services.AddScoped<DeleteUsuarioUseCase>();
builder.Services.AddScoped<RegisterProjetoUseCase>();
builder.Services.AddScoped<DeleteProjetoUseCase>();


builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilter)));

// 🔹 Configurando o CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection(); // só fora do dev
}

// 🔹 Ativando o CORS antes do Authorization
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

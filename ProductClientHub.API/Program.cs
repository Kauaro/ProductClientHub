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
using SLAProjectHub.API.UseCases.Aluno.GetAll;
using SLAProjectHub.API.UseCases.Aluno.Register;
using SLAProjectHub.API.UseCases.Avaliacao.GetAll;
using SLAProjectHub.API.UseCases.Avaliacao.Register;
using SLAProjectHub.API.UseCases.Projeto.GetAll;
using SLAProjectHub.API.UseCases.Projeto.GetById;
using SLAProjectHub.API.UseCases.Projeto.Register;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductClientHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RegisterUsuarioUseCase>();
builder.Services.AddScoped<UpdateUsuarioUseCase>();
builder.Services.AddScoped<GetAllUsuarioUseCase>();
builder.Services.AddScoped<GetUsuarioByIdUseCase>();
builder.Services.AddScoped<DeleteUsuarioUseCase>();

builder.Services.AddScoped<GetAllProjetosUseCase>();
builder.Services.AddScoped<GetByIdProjetoUseCase>();
builder.Services.AddScoped<GetByCodigoProjeto>();
builder.Services.AddScoped<RegisterProjetoUseCase>();
builder.Services.AddScoped<DeleteProjetoUseCase>();

builder.Services.AddScoped<ProjetoCodigoService>();


builder.Services.AddScoped<RegisterAluno>();
builder.Services.AddScoped<GetAllAluno>();

builder.Services.AddScoped<GetAllAvaliacaoUseCase>();
builder.Services.AddScoped<RegisterAvaliacaoUseCase>();


builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilter)));



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
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

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

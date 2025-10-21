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
using SLAProjectHub.API.UseCases;
using SLAProjectHub.API.UseCases.Aluno.GetAll;
using SLAProjectHub.API.UseCases.Aluno.Register;
using SLAProjectHub.API.UseCases.Avaliacao.GetByCodigo;
using SLAProjectHub.API.UseCases.Avaliacao.GetByMatricula;
using SLAProjectHub.API.UseCases.Avaliacao.Register;
using SLAProjectHub.API.UseCases.Projeto.GetAll;
using SLAProjectHub.API.UseCases.Projeto.GetById;
using SLAProjectHub.API.UseCases.Projeto.GetByIdUsuario;
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
builder.Services.AddScoped<GetByIdUsuarioProjetoUseCase>();
builder.Services.AddScoped<GetByCodigoProjeto>();
builder.Services.AddScoped<RegisterProjetoUseCase>();
builder.Services.AddScoped<DeleteProjetoUseCase>();

builder.Services.AddScoped<ProjetoCodigoService>();

builder.Services.AddSingleton<PasswordService>();


builder.Services.AddScoped<RegisterAluno>();
builder.Services.AddScoped<GetAllAluno>();

builder.Services.AddScoped<GetAvaliacaoByMatricula>();
builder.Services.AddScoped<GetAvaliacaoByCodigo>();
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

/*
// === INÍCIO DO BLOCO DE MIGRAÇÃO AUTOMÁTICA ===
// Cria um escopo de serviço, obtém o DbContext e aplica as migrações.
// Isso garante que o BD seja criado no Azure SQL na primeira execução.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductClientHubDbContext>();

    // ATENÇÃO: Se o banco de dados já tiver sido criado, este comando apenas aplicará migrações pendentes.
    // Se for a primeira execução no Azure, ele criará o BD e as tabelas do zero.
    dbContext.Database.Migrate();
}
// === FIM DO BLOCO DE MIGRAÇÃO AUTOMÁTICA === 

*/


// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


    app.UseHttpsRedirection(); // só fora do dev


app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

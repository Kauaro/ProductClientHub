using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Responses;

namespace ProductClientHub.API.UseCases.Clients.GetAll
{
    public class GetAllUsuarioUseCase
    {

        public ResponseAllUsuarioJson Execute()
        {
            var dbContext = new ProductClientHubDbContext();

            var usuario = dbContext.Usuario.ToList();


            return new ResponseAllUsuarioJson
            {
                Usuario = usuario.Select(usuario => new ResponseShortUsuarioJson
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                }).ToList()
            };
        }
    }
}

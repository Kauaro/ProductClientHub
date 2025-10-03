using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Responses;

namespace ProductClientHub.API.UseCases.Clients.GetAll
{
    public class GetAllUsuarioUseCase
    {
        private readonly ProductClientHubDbContext _context;

        // Recebe o DbContext pelo construtor
        public GetAllUsuarioUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public ResponseAllUsuarioJson Execute()
        {
            var usuario = _context.Usuario.ToList();

            return new ResponseAllUsuarioJson
            {
                Usuario = usuario.Select(u => new ResponseShortUsuarioJson
                {
                    Id = u.Id,
                    Matricula = u.Matricula,
                    Nome = u.Nome,
                    NivelAcesso = u.NivelAcesso,
                }).ToList()
            };
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Infrastructure;
using SLAProjectHub.API.Entities;
using SLAProjectHub.Communication.Responses.Projeto;
using SLAProjectHub.Communication.Responses.Usuario;

namespace SLAProjectHub.API.UseCases.Projeto.GetAll
{
    public class GetAllProjetosUseCase
    {
        private readonly ProductClientHubDbContext _context;

        // Recebe o DbContext pelo construtor
        public GetAllProjetosUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public ResponseAllProjetoJson Execute()
        {
            var projeto = _context.Projeto
                .Include(p => p.Usuario)
                .ToList();

            return new ResponseAllProjetoJson
            {
                Projeto = projeto.Select(u => new ResponseShortProjetoJson
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Tema = u.Tema,
                    UsuarioNome = u.Usuario.Nome
                }).ToList()
            };
        }
    }
}

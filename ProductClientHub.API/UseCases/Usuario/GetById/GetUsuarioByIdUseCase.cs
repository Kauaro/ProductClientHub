using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionBase;

namespace ProductClientHub.API.UseCases.Clients.GetClient
{
    public class GetUsuarioByIdUseCase
    {

        private readonly ProductClientHubDbContext _context;
        public GetUsuarioByIdUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public ResponseUsuarioJson Execute(Guid id)
        {
            var usuario = _context.Usuario.ToList();

            var entity = _context
                .Usuario
                .Include(usuario => usuario.Projeto)
                .FirstOrDefault(usuario => usuario.Id == id);
            
            if (entity is null)
                throw new NotFoundException("Usuario não encontrado.");

            return new ResponseUsuarioJson
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Email = entity.Email,
                Senha = entity.Senha,
                Matricula = entity.Matricula,
                NivelAcesso = entity.NivelAcesso,
                Projeto = entity.Projeto.Select(projeto => new ResponseShortProjetoJson
                {
                    Id = projeto.Id,
                    Nome = projeto.Nome,

                }).ToList()
            };
        }
    }
}

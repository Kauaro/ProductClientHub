using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Exceptions.ExceptionBase;
using SLAProjectHub.Communication.Responses.Projeto;
using SLAProjectHub.Communication.Responses.Usuario;

namespace SLAProjectHub.API.UseCases.Projeto.GetById
{
    public class GetByIdProjetoUseCase
    {

            private readonly ProductClientHubDbContext _context;
            public GetByIdProjetoUseCase(ProductClientHubDbContext context)
            {
                _context = context;
            }

            public ResponseProjetoJson Execute(Guid id)
            {
                var usuario = _context.Projeto.ToList();

                var entity = _context
                    .Projeto
                    .Include(p => p.Usuario)
                    .FirstOrDefault(projeto => projeto.Id == id);

                if (entity is null)
                    throw new NotFoundException("Projeto não encontrado.");

            return new ResponseProjetoJson
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Descricao = entity.Descricao,
                Tema = entity.Tema,
                Aluno = entity.Aluno,
                Codigo = entity.Codigo,
                UsuarioNome = entity.Usuario.Nome
            };
            }
        }
    }

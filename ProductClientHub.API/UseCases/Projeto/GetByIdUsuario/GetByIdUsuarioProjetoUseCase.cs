using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Exceptions.ExceptionBase;
using SLAProjectHub.Communication.Responses.Projeto;

namespace SLAProjectHub.API.UseCases.Projeto.GetByIdUsuario
{
    public class GetByIdUsuarioProjetoUseCase
    {
        private readonly ProductClientHubDbContext _context;

        public GetByIdUsuarioProjetoUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ResponseProjetoJson>> Execute(Guid usuarioId)
        {
            var projetos = await _context
                .Projeto
                .AsNoTracking()
                .Include(p => p.Usuario) // inclui o usuário para acessar o nome
                .Where(p => p.Usuario.Id == usuarioId)
                .ToListAsync();

            if (!projetos.Any())
                throw new NotFoundException("Nenhum projeto encontrado para este usuário.");

            return projetos.Select(p => new ResponseProjetoJson
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Tema = p.Tema,
                Aluno = p.Aluno,
                Codigo = p.Codigo,
                UsuarioNome = p.Usuario.Nome
            });
        }
    }
}

using ProductClientHub.API.Infrastructure;
using SLAProjectHub.Communication.Responses.Aluno;

namespace SLAProjectHub.API.UseCases.Aluno.GetAll
{
    public class GetAllAluno
    {
        private readonly ProductClientHubDbContext _context;
        public GetAllAluno(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public ResponseAllAlunoJson Execute()
        {
            var aluno = _context.Aluno.ToList();
            return new ResponseAllAlunoJson
            {
                Aluno = aluno.Select(a => new ResponseShortAlunoJson
                {
                    Id = a.Id,
                    Matricula = a.Matricula,
                    Nome = a.Nome,
                    Email = a.Email,
                }).ToList()
            };
        }

    }
}

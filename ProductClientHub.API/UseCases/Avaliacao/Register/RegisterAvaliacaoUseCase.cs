using ProductClientHub.API.Infrastructure;
using ProductClientHub.Exceptions.ExceptionBase;
using SLAProjectHub.API.UseCases.Avaliacao.Validator;
using SLAProjectHub.Communication.Requests;
using SLAProjectHub.Communication.Responses.Avaliacao;

namespace SLAProjectHub.API.UseCases.Avaliacao.Register
{
    public class RegisterAvaliacaoUseCase
    {
        private readonly ProductClientHubDbContext _context;



        public RegisterAvaliacaoUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public ResponseShortAvaliacaoJson Execute(string alunoMatricula, RequestAvaliacaoJson request)
        {
            Validate(alunoMatricula, request);

            var entity = new SLAProjectHub.API.Entities.Avaliacao
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                Nota = request.Nota,
                ProjetoCodigo = request.ProjetoCodigo,
                AlunoMatricula = alunoMatricula
            };

            
                _context.Avaliacao.Add(entity);
                _context.SaveChanges();
            


            return new ResponseShortAvaliacaoJson
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Nota = entity.Nota,
            };
        }

        private void Validate(string alunoMatricula, RequestAvaliacaoJson request)
        {

            var alunoExist = _context.Aluno.Any(aluno => aluno.Matricula == alunoMatricula);
            if (!alunoExist)
                throw new NotFoundException("Usuario não existe");

            var validator = new RequestAvaliacaoValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}

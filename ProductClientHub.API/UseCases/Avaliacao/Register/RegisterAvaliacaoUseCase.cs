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

        public ResponseShortAvaliacaoJson Execute(string projetoCodigo, RequestAvaliacaoJson request)
        {
            Validate(projetoCodigo, request);

            var entity = new SLAProjectHub.API.Entities.Avaliacao
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                Nota = request.Nota,
                ProjetoCodigo = projetoCodigo,
                AlunoMatricula = request.AlunoMatricula
            };

            
                _context.Avaliacao.Add(entity);
                _context.SaveChanges();
            


            return new ResponseShortAvaliacaoJson
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Nota = entity.Nota
            };
        }

        private void Validate(string projetoCodigo, RequestAvaliacaoJson request)
        {

            var projetoExist = _context.Projeto.Any(projeto => projeto.Codigo == projetoCodigo);
            if (!projetoExist)
                throw new NotFoundException("Projeto não existe");

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

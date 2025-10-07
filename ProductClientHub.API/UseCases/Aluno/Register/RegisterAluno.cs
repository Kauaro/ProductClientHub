using SLAProjectHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Exceptions.ExceptionBase;
using SLAProjectHub.API.UseCases.Aluno.Validator;
using SLAProjectHub.Communication.Requests;
using SLAProjectHub.Communication.Responses.Aluno;

namespace SLAProjectHub.API.UseCases.Aluno.Register
{
    public class RegisterAluno
    {
        private readonly ProductClientHubDbContext _context;
        public RegisterAluno(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public ResponseShortAlunoJson Execute(RequestAlunoJson request)
        {
            try
            {
                Validate(request);
                var entity = new SLAProjectHub.API.Entities.Aluno
                {
                    Nome = request.Nome,
                    Matricula = request.Matricula,
                    Email = request.Email,
                    Senha = request.Senha
                };
                _context.Aluno.Add(entity);
                _context.SaveChanges();

                return new ResponseShortAlunoJson
                {
                    Id = entity.Id, // Adicione esta linha
                    Matricula = entity.Matricula,
                    Nome = entity.Nome,
                    Email = entity.Email,
                };
            }
            catch (ErrorOnValidationException)
            {
                throw; // já tratado pelo filtro de exceção
            }
            catch (Exception ex)
            {
                // Logue o erro aqui se necessário
                throw new ErrorOnValidationException(new List<string> { "Erro interno ao registrar aluno: " + ex.Message });
            }
        }
            
        private void Validate(RequestAlunoJson request)
        {
            var validator = new RequestAlunoValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}

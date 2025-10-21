using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Exceptions.ExceptionBase;
using SLAProjectHub.API.UseCases.Aluno.Validator;
using SLAProjectHub.Communication.Requests;

namespace SLAProjectHub.API.UseCases.Aluno.Update
{
    public class UpdateAlunoUseCase
    {
        private readonly ProductClientHubDbContext _context;
        public UpdateAlunoUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public void Execute(Guid alunoId, RequestAlunoJson request)
        {
            Validate(request);

            var aluno = _context.Aluno.ToList();

            var entity = _context.Aluno.FirstOrDefault(aluno => aluno.Id == alunoId);
            if (entity is null)
                throw new NotFoundException("Usuario não encontrado.");

            entity.Nome = request.Nome;
            entity.Email = request.Email;
            entity.Matricula = request.Matricula;
            entity.Curso = request.Curso;
            entity.Periodo = request.Periodo;


            _context.Aluno.Update(entity);
            _context.SaveChanges();
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

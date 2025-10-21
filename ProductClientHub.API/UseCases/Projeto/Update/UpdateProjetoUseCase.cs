using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.SharedValidator;
using ProductClientHub.API.UseCases.Products.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Exceptions.ExceptionBase;

namespace SLAProjectHub.API.UseCases.Projeto.Update
{
    public class UpdateProjetoUseCase
    {
        private readonly ProductClientHubDbContext _context;
        public UpdateProjetoUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public void Execute(Guid projetoId, RequestProjetoJson request)
        {
            Validate(request);

            var projeto = _context.Projeto.ToList();

            var entity = _context.Projeto.FirstOrDefault(projeto => projeto.Id == projetoId);
            if (entity is null)
                throw new NotFoundException("Projeto não encontrado.");

            entity.Nome = request.Nome;
            entity.Descricao = request.Descricao;
            entity.Tema = request.Tema;
            entity.Aluno = request.Aluno;

            _context.Projeto.Update(entity);
            _context.SaveChanges();
        }

        private void Validate(RequestProjetoJson request)
        {
            var validator = new RequestProjetoValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}

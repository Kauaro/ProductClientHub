using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Exceptions.ExceptionBase;

namespace ProductClientHub.API.UseCases.Clients.Update
{
    public class UpdateUsuarioUseCase
    {
        public void Execute(Guid usuarioId, RequestUsuarioJson request)
        {
            Validate(request);

            var dbContext = new ProductClientHubDbContext();


            var entity =dbContext.Usuario.FirstOrDefault(usuario => usuario.Id == usuarioId);
            if (entity is null)
                throw new NotFoundException("Usuario não encontrado.");

            entity.Nome = request.Nome;
            entity.Email = request.Email;
            entity.Senha = request.Senha;
            entity.Matricula = request.Matricula;

            dbContext.Usuario.Update(entity);
            dbContext.SaveChanges();
        }

        private void Validate(RequestUsuarioJson request)
        {
            var validator = new RequestUsuarioValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}

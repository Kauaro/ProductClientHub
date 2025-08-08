using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionBase;

namespace ProductClientHub.API.UseCases.Clients.Register
{
    public class RegisterUsuarioUseCase
    {

        public ResponseShortUsuarioJson Execute(RequestUsuarioJson request) 
        {
           
            Validate(request);

            var dbContext = new ProductClientHubDbContext();

            var entity = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Matricula = request.Matricula,
                Senha = request.Senha,

            };

            dbContext.Usuario.Add(entity);

            dbContext.SaveChanges();


            return new ResponseShortUsuarioJson
            {
                Id = entity.Id,
                Nome = entity.Nome,
            };
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

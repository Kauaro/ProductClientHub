using SLAProjectHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Exceptions.ExceptionBase;
using SLAProjectHub.Communication.Responses.Usuario;

namespace ProductClientHub.API.UseCases.Clients.Register
{
    public class RegisterUsuarioUseCase
    {
        private readonly ProductClientHubDbContext _context;
        public RegisterUsuarioUseCase(ProductClientHubDbContext context)
        {
            _context = context;
        }

        public ResponseShortUsuarioJson Execute(RequestUsuarioJson request) 
        {
           
            Validate(request);

            var usuario = _context.Usuario.ToList();

            var entity = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Matricula = request.Matricula,
                Senha = request.Senha,
                NivelAcesso = request.NivelAcesso,

            };

            _context.Usuario.Add(entity);

            _context.SaveChanges();


            return new ResponseShortUsuarioJson
            {
                Matricula = entity.Matricula,
                Nome = entity.Nome,
                NivelAcesso = entity.NivelAcesso,
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

using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Products.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionBase;

namespace ProductClientHub.API.UseCases.Products.Register
{
    public class RegisterProductUseCase
    {
        public ResponseShortProductJson Execute(Guid clientId, RequestProductJson request)
        {
            var dbContext = new ProductClientHubDbContext();

            Validate(dbContext, clientId, request);

            var entity = new Product
            {
                Name = request.Name,
                Brand = request.Brand,
                Price = request.Price,
                ClientId = clientId
            };

            dbContext.Products.Add(entity);

            dbContext.SaveChanges();

            return new ResponseShortProductJson
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        private void Validate(ProductClientHubDbContext dbContext, Guid clientId, RequestProductJson request)
        {
            var clientExist = dbContext.Clients.Any(client => client.Id == clientId);
            if (clientExist == false)
                throw new NotFoundException("Cliente não existe");

            var validator = new RequestProductValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}

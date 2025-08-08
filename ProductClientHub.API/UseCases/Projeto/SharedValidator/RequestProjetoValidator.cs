using FluentValidation;
using ProductClientHub.Communication.Requests;

namespace ProductClientHub.API.UseCases.Products.SharedValidator
{
    public class RequestProjetoValidator : AbstractValidator<RequestProjetoJson>
    {
        public RequestProjetoValidator()
        {
            RuleFor(product => product.Nome).NotEmpty().WithMessage("Nome do produto inválido.");
            RuleFor(product => product.Descricao).NotEmpty().WithMessage("Descrição do projeto inválida");
            RuleFor(product => product.Tema).NotEmpty().WithMessage("Tema inválido.");
            RuleFor(product => product.Aluno).NotEmpty().WithMessage("Alunos participantes inválidos.");
        }
    }
}

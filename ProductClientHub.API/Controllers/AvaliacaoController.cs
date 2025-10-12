using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Products.Register;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using SLAProjectHub.API.UseCases.Avaliacao.GetAll;
using SLAProjectHub.API.UseCases.Avaliacao.Register;
using SLAProjectHub.Communication.Requests;
using SLAProjectHub.Communication.Responses.Usuario;

namespace SLAProjectHub.API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class AvaliacaoController : ControllerBase
        {
            private readonly ProductClientHubDbContext _context;
            private readonly RegisterAvaliacaoUseCase _register;
            private readonly GetAllAvaliacaoUseCase _getAll;



            public AvaliacaoController(
                ProductClientHubDbContext context,
                RegisterAvaliacaoUseCase register,
                GetAllAvaliacaoUseCase getAll)
            {
                _context = context;
                _register = register;
                _getAll = getAll;

            }

            [HttpPost("{projetoCodigo}")]
            [ProducesResponseType(typeof(ResponseShortUsuarioJson), StatusCodes.Status201Created)]
            [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
            public IActionResult Register(
                [FromRoute] string projetoCodigo,
                [FromBody] RequestAvaliacaoJson request)
            {
                var response = _register.Execute(projetoCodigo, request);
                return Created(string.Empty, response);
            }
        }
}

using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Products.Delete;
using ProductClientHub.API.UseCases.Products.Register;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using SLAProjectHub.API.UseCases.Projeto.GetAll;
using SLAProjectHub.API.UseCases.Projeto.GetById;
using SLAProjectHub.Communication.Responses.Projeto;
using SLAProjectHub.Communication.Responses.Usuario;

namespace ProductClientHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly ProductClientHubDbContext _context;
        private readonly RegisterProjetoUseCase _register;
        private readonly GetAllProjetosUseCase _getAll;
        private readonly GetByIdProjetoUseCase _getById;
        private readonly GetByCodigoProjeto _getByCodigo;
        public ProjetoController(
            ProductClientHubDbContext context,
            RegisterProjetoUseCase register,
            GetAllProjetosUseCase getall,
            GetByIdProjetoUseCase getbyid,
            GetByCodigoProjeto getbycodigo
            )
        {
            _context = context;
            _register = register;
            _getAll = getall;
            _getById = getbyid;
            _getByCodigo = getbycodigo;
        }

        [HttpPost("{usuarioId}")]
        [ProducesResponseType(typeof(ResponseShortProjetoJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status404NotFound)]
        public IActionResult Register(
            [FromRoute] Guid usuarioId,
            [FromBody] RequestProjetoJson request,
            [FromServices] RegisterProjetoUseCase useCase) // ✅ UseCase injetado
        {
            var response = useCase.Execute(usuarioId, request);
            return Created(string.Empty, response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status404NotFound)]
        public IActionResult Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteProjetoUseCase useCase) // ✅ UseCase injetado
        {
            useCase.Execute(id);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseAllUsuarioJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAll(
            [FromServices] GetAllProjetosUseCase useCase
            )
        {
            var response = useCase.Execute();
            if (response.Projeto.Count == 0)
                return NoContent();

            return Ok(response);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(typeof(ResponseAllProjetoJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var response = _getById.Execute(id);
            return Ok(response);
        }

        [HttpGet("codigo/{codigo}")]
        [ProducesResponseType(typeof(ResponseAllProjetoJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetByCodigo([FromRoute] String codigo)
        {
            var response = _getByCodigo.Execute(codigo);
            return Ok(response);
        }
    }
}

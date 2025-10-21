using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Products.Delete;
using ProductClientHub.API.UseCases.Products.Register;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using SLAProjectHub.API.UseCases.Projeto.GetAll;
using SLAProjectHub.API.UseCases.Projeto.GetById;
using SLAProjectHub.API.UseCases.Projeto.GetByIdUsuario;
using SLAProjectHub.API.UseCases.Projeto.Update;
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
        private readonly GetByIdUsuarioProjetoUseCase _getByIdUsuario;
        private readonly GetByCodigoProjeto _getByCodigo;
        private readonly UpdateProjetoUseCase _update;
        public ProjetoController(
            ProductClientHubDbContext context,
            RegisterProjetoUseCase register,
            GetAllProjetosUseCase getall,
            GetByIdProjetoUseCase getbyid,
            GetByIdUsuarioProjetoUseCase getbyidusuario,
            GetByCodigoProjeto getbycodigo,
            UpdateProjetoUseCase update

            )
        {
            _context = context;
            _register = register;
            _getAll = getall;
            _getById = getbyid;
            _getByIdUsuario = getbyidusuario;
            _getByCodigo = getbycodigo;
            _update = update;
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

        [HttpGet("usuario/{usuarioId}")]
        [ProducesResponseType(typeof(IEnumerable<ResponseProjetoJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetByIdUsuario([FromRoute] Guid usuarioId)
        {
            var response = await _getByIdUsuario.Execute(usuarioId);

            if (response == null || !response.Any())
                return NoContent();

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

        [HttpPut("editar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status404NotFound)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] RequestProjetoJson request)
        {
            _update.Execute(id, request);
            return NoContent();
        }
    }
}

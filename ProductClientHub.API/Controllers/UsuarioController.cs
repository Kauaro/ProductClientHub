using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.Delete;
using ProductClientHub.API.UseCases.Clients.GetAll;
using ProductClientHub.API.UseCases.Clients.GetClient;
using ProductClientHub.API.UseCases.Clients.Register;
using ProductClientHub.API.UseCases.Clients.Update;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionBase;
using SLAProjectHub.Communication.Requests;
using SLAProjectHub.Communication.Responses;
using SLAProjectHub.Communication.Responses.Usuario;
using SLAProjectHub.API.UseCases;

namespace ProductClientHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ProductClientHubDbContext _context;
        private readonly RegisterUsuarioUseCase _register;
        private readonly UpdateUsuarioUseCase _update;
        private readonly GetAllUsuarioUseCase _getAll;
        private readonly GetUsuarioByIdUseCase _getById;
        private readonly DeleteUsuarioUseCase _delete;

        public UsuarioController(
            ProductClientHubDbContext context,
            RegisterUsuarioUseCase register,
            UpdateUsuarioUseCase update,
            GetAllUsuarioUseCase getAll,
            GetUsuarioByIdUseCase getById,
            DeleteUsuarioUseCase delete)
        {
            _context = context;
            _register = register;
            _update = update;
            _getAll = getAll;
            _getById = getById;
            _delete = delete;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortUsuarioJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestUsuarioJson request)
        {
            var response = _register.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status404NotFound)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] RequestUsuarioJson request)
        {
            _update.Execute(id, request);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseAllUsuarioJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAll()
        {
            var response = _getAll.Execute();
            if (response.Usuario.Count == 0)
                return NoContent();

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseAllUsuarioJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var response = _getById.Execute(id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _delete.Execute(id);
            return NoContent();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] RequestLoginJson request)
        {
            var usuario = _context.Usuario
                .FirstOrDefault(u => u.Matricula == request.Matricula);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Matrícula ou senha inválidos." });
            }

            var passwordService = new PasswordService();


            var senhaCorreta = passwordService.VerifyPassword(request.Senha, usuario.Senha);

            if (!senhaCorreta)
                return Unauthorized(new { message = "Matrícula ou senha inválidos." });



            return Ok(new
            {
                message = "Login realizado com sucesso!",
                id = usuario.Id,
                nome = usuario.Nome,
                matricula = usuario.Matricula,
                role = usuario.NivelAcesso, 
                projetos = new string[] { } 
            });
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;
using YourNamespace.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint para autenticar (login)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            Usuario usuario;

            // Verifica se o login é numérico (CPF) ou email
            if (long.TryParse(loginRequest.Login, out long cpf))
            {
                // Se for numérico, busca por CPF
                usuario = await _context.Cadastro.FirstOrDefaultAsync(u => u.CPF == cpf);
            }
            else
            {
                // Se não for numérico, busca por Email
                usuario = await _context.Cadastro.FirstOrDefaultAsync(u => u.Email == loginRequest.Login);
            }

            // Verifica se o usuário existe e se a senha está correta
            if (usuario == null || usuario.Senha != loginRequest.Senha)
            {
                return Unauthorized(new { message = "Login ou senha incorretos." });
            }

            // Se o login for bem-sucedido, retorne uma resposta de sucesso (pode ser um token JWT ou simplesmente um OK)
            return Ok(new { message = "Login realizado com sucesso!", usuario });
        }

        // Exemplo de endpoint já existente para criar um novo cadastro (usuário)
        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] Usuario usuario)
        {
            if (await _context.Cadastro.AnyAsync(u => u.Email == usuario.Email || u.CPF == usuario.CPF))
            {
                return BadRequest("Email ou CPF já cadastrados.");
            }

            _context.Cadastro.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.IDCadastro }, usuario);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _context.Cadastro.ToListAsync();
            return Ok(usuarios);
        }
    }

    // Classe auxiliar para receber os dados de login
    public class LoginRequest
    {
        public string Login { get; set; } // Pode ser CPF ou Email
        public string Senha { get; set; }
    }
}

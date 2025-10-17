using Microsoft.AspNetCore.Identity;

namespace SLAProjectHub.API.UseCases
{
    public class PasswordService
    {
        private readonly PasswordHasher<object> _hasher = new();

        // Gera o hash da senha para armazenar no banco
        public string HashPassword(string senha)
        {
            return _hasher.HashPassword(null, senha);
        }

        // Verifica se a senha informada corresponde ao hash armazenado
        public bool VerifyPassword(string senha, string hashArmazenado)
        {
            var result = _hasher.VerifyHashedPassword(null, hashArmazenado, senha);
            return result == PasswordVerificationResult.Success ||
                   result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}

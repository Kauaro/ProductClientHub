using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAProjectHub.Communication.Responses.Avaliacao
{
    public class ResponseShortAvaliacaoJson
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string AlunoMatricula { get; set; } = string.Empty;
        public int Nota { get; set; } = 0;
        public string ProjetoNome { get; set; } = string.Empty;
    }
}

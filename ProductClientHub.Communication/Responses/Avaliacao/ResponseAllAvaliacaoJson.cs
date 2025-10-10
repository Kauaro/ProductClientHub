using SLAProjectHub.Communication.Responses.Projeto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAProjectHub.Communication.Responses.Avaliacao
{
    public class ResponseAllAvaliacaoJson
    {
        public List<ResponseShortAvaliacaoJson> Avaliacao { get; set; } = [];
    }
}

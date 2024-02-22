using AdaTech.ImoveisApi.Models;
using System.Text.Json.Serialization;

namespace AdaTech.ImoveisApi.Requests
{
    public class CreateImovelRequest
    {
        [JsonIgnore]
        public string? Endereco { get; set; }
        public string? Proprietario { get; set; }
        public string? Cep { get; set; }

        public record CriarImovelRequest(string? Endereco, string? Cep, string? Proprietario);
    }
}

using AdaTech.ImoveisApi.Models;
using AdaTech.ImoveisApi.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdaTech.ImoveisApi.Controllers
{
    [Route("api/imovel")]
    [ApiController]
    public class ImoveisController : ControllerBase //Classe base com métodos definidos
    {
        private static List<Imovel> _imoveis; //Prtence a classe, não ao objeto (mesma para todos)

        public ImoveisController()
        {
            if (_imoveis == null)
            {
                _imoveis = new List<Imovel>();
            }
        }

        [HttpGet]  //Especifica que recebe
        public IActionResult Get()
        {
            //Retorno 200 OK
            //Retorno 201 CREATED
            //Retorno 204 NoContent
            //Retorno 500 QUEBROU
            //Retorno 404 NOT FOUND
            //Retorno 400 ERRO CLIENTE
            //(mandou algo errado, exemplo: id => int / {"id": "teste"} -> Cliente mandou string e não int)

            // permite retornar um http stattus code
            //return BadRequest();
            return Ok();
        }

        [HttpGet("{id}", Name = "Pegar o imovel com este ID")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var imovel = _imoveis.FirstOrDefault(x => x.Id == id);

            if (imovel == null)
            {
                return NotFound($"Imovel de id {id} não encontrado");
            }

            return Ok(imovel);
        }

        [HttpPost("/imovel", Name = "Adicionar imovel")]                                                   ///api/imovelwid=10 (Q) e [HttpPost("{xxxx}")] (R)
        public async Task<IActionResult> Add([FromBody] CreateImovelRequest imovelRequest)  //[FromQuery] e [FromRoute]
        {
            string? endereco = null;

            if (!string.IsNullOrEmpty(imovelRequest.Cep))
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://brasilapi.com.br/api/cep/v1/");

                var request = await httpClient.GetAsync(imovelRequest.Cep);
                //200 -> 299
                if(request.IsSuccessStatusCode)
                {
                    var enderecoCep = await request.Content.ReadFromJsonAsync<Endereco>();
                    endereco = enderecoCep.EscreverEndereco();
                }
            }

            endereco ??= imovelRequest.Endereco;

            if (string.IsNullOrWhiteSpace(imovelRequest.Endereco))
            {
                return BadRequest("O endereço deve ser preenchido"); 
            }

            var imovel = new Imovel(Guid.NewGuid(), imovelRequest.Endereco, imovelRequest.Proprietario);

            _imoveis.Add(imovel);
            return Created($"/api/imovel/{imovel.Id}", imovel);
        }

        [HttpDelete("/imovel", Name = "Deletar imóvel")]
        public IActionResult Delete([FromQuery] Guid id)
        {
            var imovelDeletado = _imoveis.FirstOrDefault(_imoveis => _imoveis.Id == id);
            if(imovelDeletado == null)
            {
                return NotFound();
            }

            _imoveis.Remove(imovelDeletado);
            return Ok(imovelDeletado);
        }

        [HttpPut("/imovel/{id}", Name = "Atualizar imovel")]
        public IActionResult Update([FromBody] UpdateImovelRequest request, Guid id)
        {
            var imovelAntigo = _imoveis.FirstOrDefault(_imoveis => _imoveis.Id == id);
            if (imovelAntigo == null)
            {
                return NotFound("Não existe i´móvel com este ID");
            }

            var imovelAtualizado = new Imovel(imovelAntigo.Id, request.Proprietario, request.Endereco);

            _imoveis.Remove(imovelAntigo);
            _imoveis.Add(imovelAtualizado);
            _imoveis = _imoveis.OrderBy(_imoveis=> _imoveis.Id).ToList();
            return Ok(imovelAtualizado);
        }
    }
}

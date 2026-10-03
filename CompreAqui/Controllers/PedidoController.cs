using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace GistApi.Controllers;

[ApiController]
[Route("api/pedidos")]
public class PedidoController : ControllerBase
{
  private readonly IHttpClientFactory _httpClientFactory;
  private readonly IConfiguration _configuration;

  // Injeção de dependência das configurações e do HttpClientFactory
  //public PedidoController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
  //{
  //  _httpClientFactory = httpClientFactory;
  //  _configuration = configuration;
  //}

  /// <summary>
  /// Busca o conteúdo atual do Gist (pedido.json)
  /// GET: api/pedido
  /// </summary>
  [HttpGet]
  public async Task<IActionResult> ObterPedido()
  {
    return Content("[ \"pedido\": \"3\"]", "application/json"); 
    var client = _httpClientFactory.CreateClient("GitHubClient");
    var gistId = _configuration["GitHub:GistId"];

    var response = await client.GetAsync($"gists/{gistId}");

    if (!response.IsSuccessStatusCode)
    {
      return StatusCode((int)response.StatusCode, "Erro ao buscar o Gist no GitHub");
    }

    var jsonContent = await response.Content.ReadAsStringAsync();
    return Content(jsonContent, "application/json");
  }

  /// <summary>
  /// Atualiza o conteúdo do pedido.json no Gist
  /// PUT: api/pedido
  /// </summary>
  [HttpPut]
  public async Task<IActionResult> AtualizarPedido([FromBody] JsonElement novoConteudoPedido)
  {
    var client = _httpClientFactory.CreateClient("GitHubClient");
    var gistId = _configuration["GitHub:GistId"];

    var payload = new
    {
      files = new
      {
        pedido_json = new
        {
          filename = "pedido.json",
          content = novoConteudoPedido.GetRawText()
        }
      }
    };

    var jsonPayload = JsonSerializer.Serialize(payload);
    var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    var response = await client.PatchAsync($"gists/{gistId}", requestContent);

    if (!response.IsSuccessStatusCode)
    {
      var errorMsg = await response.Content.ReadAsStringAsync();
      return StatusCode((int)response.StatusCode, $"Erro ao atualizar Gist: {errorMsg}");
    }

    return Ok(new { mensagem = "Gist atualizado com sucesso!" });
  }
}
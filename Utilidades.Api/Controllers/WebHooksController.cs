using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMtz84.WebHook;

namespace Utilidades.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class WebHooksController : ControllerBase
    {
        private readonly WebHookService _webHookService;

        public WebHooksController(WebHookService webHookService)
        {
            _webHookService = webHookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<WebHook>>> Get()
        {
            return await _webHookService.ObtenerWebHooks();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] object objeto)
        {
            WebHook webHook = new WebHook
            {
                Body = objeto.ToString()
            };
            await _webHookService.GuardarWebHook(webHook);

            return CreatedAtAction(nameof(Get), new { id = webHook.Encodedkey });
        }
    }
}

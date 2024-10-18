using EmbalagemApi.Application.Comand;
using EmbalagemApi.Extension;
using EmbalagemApi.Models;
using EmbalagemApi.Services;
using EmbalagemApi.View;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmbalagemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpacotamentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmpacotamentoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("empacotar")]
        public async Task<IActionResult> EmpacotarPedidos([FromBody] List<Pedido> pedidos)
        {
            var resultado = await _mediator.Send(new EmpacotarPedidosCommand(pedidos));
            var retorno = resultado.ConvertToViewlModel();
            return Ok(retorno);
        }

    }

}

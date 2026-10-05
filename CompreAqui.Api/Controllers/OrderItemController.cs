using CompreAqui.Domain.Commands.Inputs.OrderItemCommands;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CompreAqui.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderItemController: ControllerBase
    {
        private readonly OrderItemHandler _handler;
        private readonly IAppSettings _settings;
        public OrderItemController(OrderItemHandler handler, IAppSettings settings)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        [HttpPost("")]
        public async Task<ICommandResult> GetAllOrderItem([FromBody]GetAllOrderItemCommand orderItem)
        {
            return await _handler.GetAllOrderItem(orderItem);
        }
    }
}

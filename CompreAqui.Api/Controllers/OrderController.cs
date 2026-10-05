using CompreAqui.Domain.Commands.Inputs.OrderCommands;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CompreAqui.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class OrderController : ControllerBase
    {
        private readonly OrderHandler _handler;
        private readonly IAppSettings _settings;
        public OrderController(OrderHandler handler, IAppSettings settings)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        [HttpPost("")]
        public async Task<ICommandResult> GetAll([FromBody]GetAllOrderCommand order)
        {
            return await _handler.GetAllOrderByUserId(order);
        }

        /// <summary>
        /// Cria um novo pedido
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ICommandResult> PostOrder([FromBody]CreateOrderCommand order)
        {
            return await _handler.CreateOrder(order);
        }

        [HttpPut("update/userviewed")]
        public async Task<ICommandResult> PutOrderUserViewer(UpdateOrderUserViewedCommand order)
        {
            return await _handler.UpdateOrderUserViewed(order);
        }

        [HttpPut("updatemessage")]
        public async Task<ICommandResult> PutUserAddressById(UpdateMessageOrderCommand order)
        {
            return await _handler.UpdateMessageOrder(order);
        }

        [HttpPost("message")]
        public async Task<ICommandResult> GetMessageOrder([FromBody]GetOrderByIdCommand order)
        {
            return await _handler.GetOrderById(order);
        }


        [HttpPost("newmessages")]
        public async Task<ICommandResult> GetNewMessageOrder([FromBody]GetAllOrderCommand order)
        {
            return await _handler.GetOrderNewMessageById(order);
        }
    }
}

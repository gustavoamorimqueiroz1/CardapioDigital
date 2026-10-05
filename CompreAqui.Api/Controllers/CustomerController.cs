using CompreAqui.Api.Tools;
using CompreAqui.Domain.Commands.Inputs;
using CompreAqui.Domain.Commands.Inputs.CustomerCommands;
using CompreAqui.Domain.Commands.Output;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Enums;
using CompreAqui.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CompreAqui.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerHandler _handler;
        private readonly IAppSettings _settings;

        public CustomerController(CustomerHandler handler, IAppSettings settings)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }


        /// <summary>
        /// Busca todos clientes do sistema
        /// </summary>
        /// <returns>Lista clientes (JSON)</returns>
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {

            var customers = await _handler.GetAll();
            if (customers.Success)
            {
                return Ok(customers);
            }
            else
            {
                return StatusCode(500, customers);
            }

        }

    }
}
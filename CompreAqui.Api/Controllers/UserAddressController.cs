using System;
using System.Threading.Tasks;
using CompreAqui.Domain.Commands.Inputs.UserAddressCommands;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompreAqui.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly UserAddressHandler _handler;
        private readonly IAppSettings _settings;

        /// <summary>
        /// Construtor controller Endereço de Usuário
        /// </summary>
        /// <param name="handler">Servico de acesso aos Dados</param>
        /// <param name="settings">Configurações Gerais do sistema</param>
        public UserAddressController(UserAddressHandler handler, IAppSettings settings)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Busca todos os endereços dos usuários do sistema
        /// </summary>
        /// <returns>Lista os endereços dos usuários (JSON)</returns>
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var address = await _handler.GetAll();
            if (address.Success)
            {
                return Ok(address);
            }
            else
            {
                return StatusCode(500, address);
            }
        }

        /// <summary>
        /// Busca endereço do usuário pelo userid
        /// </summary>
        /// <returns>Lista endereço do usuário pelo userid (JSON)</returns>
        [HttpPost("userid")]
        public async Task<IActionResult> GetCustomerByUserId([FromBody]GetUserAddressByIdUserCommand useraddress)
        {
            var address = await _handler.GetUserAddressByUserId(useraddress);
            if (address.Success)
            {
                return Ok(address);
            }
            else
            {
                return StatusCode(500, address);
            }
        }

        /// <summary>
        /// Adiciona um novo endereço do usuário
        /// </summary>
        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<IActionResult> PostUserAddress([FromBody]CreateUserAddressCommand useraddress)
        {
            var address = await _handler.CreateUserAddress(useraddress);
            if (address.Success)
            {
                return Ok(address);
            }
            else
            {
                switch (address.Status)
                {
                    case 400:
                        return BadRequest(address);
                    case 406:
                        return BadRequest(address);
                    case 500:
                        return StatusCode(500, address);
                    default:
                        return BadRequest(address);
                }
            }
        }

        [HttpPut("updatebyid")]
        public async Task<IActionResult> PutUserAddressById(UpdateUserAddressByIdCommand useraddress)
        {
            var address = await _handler.UpdateUserAddressById(useraddress);
            if (address.Success)
            {
                return Ok(address);
            }
            else
            {
                switch (address.Status)
                {
                    case 400:
                        return BadRequest(address);
                    case 406:
                        return BadRequest(address);
                    case 500:
                        return StatusCode(500, address);
                    default:
                        return BadRequest(address);
                }
            }
        }
    }
}
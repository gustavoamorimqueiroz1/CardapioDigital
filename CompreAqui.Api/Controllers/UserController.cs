using CompreAqui.Api.Tools;
using CompreAqui.Domain.Commands.Inputs;
using CompreAqui.Domain.Commands.Inputs.UserCommands;
using CompreAqui.Domain.Commands.Output;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Enums;
using CompreAqui.Domain.Handlers;
using CompreAqui.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CompreAqui.Api.Controllers
{
    /// <summary>
    /// Controller Usuário
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
 
    public class UserController : ControllerBase
    {
        private readonly UserHandler _handler;
        private readonly IAppSettings _settings;

        /// <summary>
        /// Construtor controller Usuário
        /// </summary>
        /// <param name="handler">Servico de acesso aos Dados</param>
        /// <param name="settings">Configurações Gerais do sistema</param>
        public UserController(UserHandler handler, IAppSettings settings)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Busca todos usuários do sistema
        /// </summary>
        /// <returns>Lista usuários (JSON)</returns>
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _handler.GetAll();
            if (users.Success)
            {
                return Ok(users);
            }
            else
            {
                return StatusCode(500, users);
            }
        }

        /// <summary>
        /// Busca usuário pelo username
        /// </summary>
        /// <returns>Lista usuários (JSON)</returns>
        [HttpPost("username")]
        public async Task<IActionResult> GetUserByName(GetUserByUserNameCommand user)
        {
            var users = await _handler.GetByUserName(user);
            if (users.Success)
            {
                return Ok(users);
            }
            else
            {
                return StatusCode(500, users);
            }
        }

        /// <summary>
        /// Busca usuário pelo e-mail
        /// </summary>
        /// <returns>Lista usuário pelo e-mail (JSON)</returns>
        [HttpPost("email")]
        public async Task<IActionResult> GetUserByEmail([FromBody]GetUserByEmailCommand user)
        {
            var users = await _handler.GetByEmail(user);
            if (users.Success)
            {
                return Ok(users);
            }
            else
            {
                return StatusCode(500, users);
            }
        }

        /// <summary>
        /// Adiciona um novo usuário
        /// </summary>
        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<IActionResult> PostUser(CreateUserCommand user)
        {
            user.Password = user.Password.Criptografa();
            var users = await _handler.CreateUser(user);
            if (users.Success)
            {
                return Ok(users);
            }
            else
            {
                switch (users.Status)
                {
                    case 400:
                        return BadRequest(users);
                    case 406:
                        return BadRequest(users);
                    case 500:
                        return StatusCode(500, users);
                    default:
                        return BadRequest(users);
                }
            }
        }

        /// <summary>
        /// Login do sistema pelo username e password
        /// </summary>
        /// <param name="loginUsuario">Objeto Usuário contendo username e senha</param>
        /// <returns>Token JWT para autenticação no sistema</returns>
        [HttpPost("login/username")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUserName([FromBody]GetUserNameLoginCommand loginUsuario)
        {
            loginUsuario.Password = loginUsuario.Password.Criptografa();
            var login = await _handler.UserLogin(loginUsuario);
            if(login != null)
            {
                var user = new User 
                {
                    Guid = login.Guid,
                    UserName = login.UserName,
                    Email = login.Email
                };

                var resp = new CommandResult((int)EStatus.Ok, true, $"Usuario logado com sucesso! {User.Identity.Name}", 
                    TokenGenerator.GenerateTokenUser(_settings.GetJwtSettings(), user).Result); ;

                return Ok(resp);
            }
            else
            {
                var resp = new CommandResult((int)EStatus.BadRequest, false, "Username ou senha inválidos!", null); ;
                return BadRequest(resp);
            }
        }

        [HttpPost("location")]
        [AllowAnonymous]
        public async Task<IActionResult> Location([FromBody] LocationCommand location)
        {
            try
            {
                double latitude = Convert.ToDouble(location.latitude);
                double longitude = Convert.ToDouble(location.longitude);

                string cidade = "";
                if (latitude < -20.340000 && latitude > -20.384000 && longitude < -41.929000 && longitude > -42.977000)
                {
                    cidade = "MANHUMIRIM";
                }
                else
                {
                    if (latitude < -20.408000 && latitude > -20.447000 && longitude < -41.940000 && longitude > -42.985000)
                    {
                        cidade = "ALTO JEQUITIBÁ";
                    }
                    else
                    {
                        //if (latitude < -20.2290000 && latitude > -20.288000 && longitude < -42.004000 && longitude > -42.066000)
                        //{
                        //    cidade = "MANHUAÇU";
                        //}
                        //else
                        //{
                        cidade = "";
                        //}
                    }
                }

                var resp = new CommandResult((int)EStatus.Ok, true, "Cidade consultada com sucesso!", cidade);
                return Ok(resp);
            }
            catch (Exception)
            {
                var resp = new CommandResult((int)EStatus.BadRequest, false, "Erro ao consultar cidade", null); ;
                return BadRequest(resp);
            }
        }

        /// <summary>
        /// Login do sistema pelo e-mail e password
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST 
        ///     {
        ///        "email": "YOUR EMAIL",
        ///        "password": "YOUR PASSWORD"
        ///     }
        /// </remarks>
        /// <param name="loginUsuario">Objeto Usuário contendo email e senha</param>
        /// <returns>Token JWT para autenticação no sistema</returns>
        [HttpPost("login/email")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginEmail([FromBody]GetEmailLoginCommand loginUsuario)
        {
            loginUsuario.Password = loginUsuario.Password.Criptografa();
            var login = await _handler.EmailLogin(loginUsuario);
            if (login != null)
            {
                var resp = new CommandResult((int)EStatus.Ok, true, "Usuário logado com sucesso!", TokenGenerator.Gerar(_settings.GetJwtSettings()).Result);        
                return Ok(resp);
            }
            else
            {
                var resp = new CommandResult((int)EStatus.BadRequest, false, "Email ou senha inválidos!", null); ;
                return BadRequest(resp);
            }
        }

        /// <summary>
        /// Esqueci minha senha através do e-mail
        /// </summary>
        /// <param name="reset">Objeto que recebe o e-mail do usuário</param>
        /// <returns>Token JWT para autenticação no sistema</returns>
        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordCommand reset)
        {
            if(reset.Email != null )
            {
                var users = await _handler.PostResetPassword(reset);
                if (users.Success)
                {
                    return Ok(users);
                }
                else
                {
                    switch (users.Status)
                    {
                        case 400:
                            return BadRequest(users);
                        case 406:
                            return BadRequest(users);
                        case 500:
                            return StatusCode(500, users);
                        default:
                            return BadRequest(users);
                    }
                }
             
            }
            else
            {
                var resp = new CommandResult((int)EStatus.NotAcceptable, false, "Email não pode ser nulo!", null); ;
                return BadRequest(resp);
            }
        }

        /// <summary>
        /// Altera a senha do usuário
        /// </summary>
        /// <param name="update"></param>
        /// <returns>Altera a senha do usuário</returns>
        [HttpPut("changepassword")]
        //[AllowAnonymous]
        public async Task<IActionResult> ChangePassword(UpdateUserPasswordCommand update)
        {
            var users = await _handler.UpdatePassword(update);
            if (users.Success)
            {
                return Ok(users);
            }
            else
            {
                switch (users.Status)
                {
                    case 400:
                        return BadRequest(users);
                    case 406:
                        return BadRequest(users);
                    case 500:
                        return StatusCode(500, users);
                    default:
                        return BadRequest(users);
                }
            }
        }


        /// <summary>
        /// Altera informações do usuário
        /// </summary>
        /// <param name="update"></param>
        /// <returns>Altera informações do usuário</returns>
        [HttpPut("updateuser")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand update)
        {
            var users = await _handler.UpdateUser(update);
            if (users.Success)
            {
                return Ok(users);
            }
            else
            {
                switch (users.Status)
                {
                    case 400:
                        return BadRequest(users);
                    case 406:
                        return BadRequest(users);
                    case 500:
                        return StatusCode(500, users);
                    default:
                        return BadRequest(users);
                }
            }
        }

    }
}

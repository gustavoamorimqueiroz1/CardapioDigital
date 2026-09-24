using System.Net.Http;
using System.Threading.Tasks;
using CompreAqui.Api.Tools;
using CompreAqui.Domain.Commands.Inputs;
using CompreAqui.Domain.Commands.Inputs.UserCommands;
using CompreAqui.Domain.Commands.Output;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Enums;
using CompreAqui.Domain.Handlers;
using CompreAqui.Domain.Models.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CompreAqui.Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class ExternalAuthController : ControllerBase
    {
        private readonly FacebookAuthSettings _fbAuthSettings;
        private static readonly HttpClient Client = new HttpClient();
        private readonly UserHandler _userHandler;
        private readonly GlobalVariables _globalVariables;
        private readonly IAppSettings _settings;

        public ExternalAuthController(IAppSettings settings, IOptions<FacebookAuthSettings> fbAuthSettingsAccessor, UserHandler userHandler, IOptions<GlobalVariables> globalVariables)
        {
            _settings = settings;
            _fbAuthSettings = fbAuthSettingsAccessor.Value;
            _userHandler = userHandler;
            _globalVariables = globalVariables.Value;
        }

        /// <summary>
        /// Autenticação via token do Facebook
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login/facebook")]
        [AllowAnonymous]
        public async Task<ICommandResult> Facebook([FromBody]FacebookCommand model)
        {

            // 1.generate an app access token
            var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_fbAuthSettings.AppId}&client_secret={_fbAuthSettings.AppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
            // 2. validate the user access token
            var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                return new CommandResult((int)EStatus.InvalidToken, false, "Token inválido !", null);
            }

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v6.0/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt

            var userCommand = new GetUserByEmailCommand
            {
                Email = userInfo.Email
            };

            var user = await _userHandler.GetByEmail(userCommand);

            if (user.Data == null)
            {
                var appUser = new CreateFacebookUserCommand
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    FacebookId = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    Password = _globalVariables.DefaultUserPassword.Criptografa()

                };

                var result = await _userHandler.CreateFacebookUser(appUser);

                if (!result.Success) return new CommandResult((int)EStatus.InternalServerError, false, "Não foi possível adicionar um novo usuário!", null); 

            }

            var userNameCommand = new GetUserByEmailCommand
            {
                Email = userInfo.Email
            };

            // generate the jwt for the local user...
            var localUser = await _userHandler.GetByEmail(userNameCommand);

            if (localUser.Data == null)
            {
                return new CommandResult((int)EStatus.InternalServerError, false, "Erro ao tentar criar uma cadastro de usuário.", null);
            }

            var jwt = await TokenGenerator.Gerar(_settings.GetJwtSettings());

            return new CommandResult((int)EStatus.Ok, true, "Token gerado com sucesso!", jwt);
        }

    }
}
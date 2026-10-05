using CompreAqui.Domain.Attributes;
using CompreAqui.Domain.Commands.Inputs;
using CompreAqui.Domain.Commands.Inputs.UserCommands;
using CompreAqui.Domain.Commands.Output;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Enums;
using CompreAqui.Domain.Models.Entities;
using CompreAqui.Domain.Models.Settings;
using CompreAqui.Domain.Tools;
using System;
using System.Threading.Tasks;

namespace CompreAqui.Domain.Handlers
{
    public class UserHandler : EntityHandler<User>, IHandler 
    {
        private readonly EmailSettings _emailSettings;
        private readonly AuthenticatedUser _authUser;

        public UserHandler(IContext context, EmailSettings emailSettings, AuthenticatedUser authUser) : base(context)
        {
            _emailSettings = emailSettings;
            _authUser = authUser;
        }
        
        #region Get Methods

        public new async Task<ICommandResult> GetAll()
        {
            try
            {
                string sql = $"SELECT * FROM users ";
                var retorno = await Repository.GetListBySql<User>(sql);
                ICommandResult resultadoServico = retorno.Count == 0
                    ? new CommandResult((int)EStatus.NotFound, false, "Nenhum usuário encontrado !", null)
                    : new CommandResult((int)EStatus.Ok, true, "Usuários encontrados !", retorno);
                return resultadoServico;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao tentar buscar todos os usuários ! Erro: ", ex.Message);
            }
        }

        public async Task<ICommandResult> GetByEmail(GetUserByEmailCommand user)
        {
            try
            {
                string sql = $"Select * from users where users.email = '{user.Email}'";
                var retorno = await Repository.GetOneBySql(sql);
                ICommandResult resultadoServico = retorno == null
                   ? new CommandResult((int)EStatus.NotFound, false, "Usuário não encontrado !", null)
                   : new CommandResult((int)EStatus.Ok, true, "Usuário encontrado pelo e-mail!", retorno);
                return resultadoServico;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao tentar buscar um usuário pelo e-mail. Erro: ", ex.Message);
            }
        }

        public async Task<ICommandResult> GetByUserName(GetUserByUserNameCommand user)
        {
            try
            {
                string sql = $"Select * from users where users.username = '{user.UserName}'";
                var retorno = await Repository.GetOneBySql(sql);
                ICommandResult resultadoServico = retorno == null
                   ? new CommandResult((int)EStatus.NotFound, false, "Username não encontrado !", null)
                   : new CommandResult((int)EStatus.Ok, true, "Username encontrado !", retorno);
                return resultadoServico;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao tentar buscar o usuário pelo username. Erro: ", ex.Message);
            }
        }

        public async Task<User> UserLogin(GetUserNameLoginCommand user)
        {
            try
            {
                string sql = $"SELECT * FROM users WHERE username = '{user.UserName}' AND password = '{user.Password}' ";
                var retorno = await Repository.GetOneBySql(sql);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao tentar buscar um usuário pelo username e senha. Erro: ", ex.Message);
            }
        }

        public async Task<User> EmailLogin(GetEmailLoginCommand user)
        {
            try
            {
                string sql = $"SELECT * FROM users WHERE users.email = '{user.Email}' AND users.password = '{user.Password}'";
                var retorno = await Repository.GetOneBySql(sql);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao tentar buscar um usuário por e-mail e senha ! Erro: ", ex.Message);
            }
        }

        #endregion

        #region Posts Methods
        public async Task<ICommandResult> CreateUser(CreateUserCommand user)
        {
            try
            {
                var sqlExisteEmail = $"select * from users where email = '{user.Email}'";
                var retornoExistenciaEmail = await Repository.GetOneBySql(sqlExisteEmail);
                var sqlExisteUsuario = $"select * from users where username = '{user.UserName}'";
                var retornoExistenciaUsuario = await Repository.GetOneBySql(sqlExisteUsuario);
                if (retornoExistenciaEmail == null && retornoExistenciaUsuario == null)
                {
                    string sql = $"INSERT INTO users " +
                        $"(username, email, password,phonenumber, lastacess) " +
                        $"VALUES " +
                        $"('{user.UserName}', '{user.Email}', '{user.Password}', '{user.PhoneNumber}', {DateTime.Now.Day}) ";
                    var retorno = await Repository.InsertBySql(sql);
                    ICommandResult resultadoServico = retorno == false
                        ? new CommandResult((int)EStatus.InternalServerError, false, "Não foi possível adicionar um novo usuário !", null)
                        : new CommandResult((int)EStatus.Created, true, "O usuário foi adicionado com sucesso !", retorno);
                    return resultadoServico;
                }
                else
                {
                    return new CommandResult((int)EStatus.Forbidden, true, "Usuário ou email já cadastrado !", null);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("23505"))
                {
                    return new CommandResult((int)EStatus.Forbidden, true, "Usuário já cadastrado !", null);
                }
                else
                {
                    return new CommandResult((int)EStatus.InternalServerError, false, "Erro Interno ao tentar adicionar um usuário. !", null);
                }
            }
        }

        public async Task<ICommandResult> CreateFacebookUser(CreateFacebookUserCommand user)
        {
            try
            {
                string sql =
                       $"INSERT INTO users (username, email, password, firstname, lastname, facebookid, createdat) " +
                       $"VALUES " +
                       $"('{user.UserName}', '{user.Email}', '{user.Password}', '{user.FirstName}','{user.LastName}', {user.FacebookId}, '{DateTime.Now.ToString().Replace("/", ".")}')";
                var retorno = await Repository.InsertBySql(sql);
                ICommandResult resultadoServico = retorno == false
                    ? new CommandResult((int)EStatus.BadRequest, false, 
                        "Não foi possível adicionar o usuário!", null)
                    : new CommandResult((int)EStatus.Created, true, "O usuário foi adicionado com sucesso !", retorno);
                return resultadoServico;
            }
            catch 
            {
                return new CommandResult((int)EStatus.InternalServerError, false,
                    "Erro ao tentar adicionar um novo usuário!", null);
              
            }
        }

        public async Task<ICommandResult> PostResetPassword(ResetPasswordCommand resetPassword)
        {

            try
            {
                string sql = $"Select * from users where users.email = '{resetPassword.Email}'";
                var retorno = await Repository.GetOneBySql(sql);

                if (retorno == null)
                {
                    return new CommandResult((int)EStatus.NoContent, true, "E-mail não encontrado !", null);
                }
                else
                {
                    try
                    {
                        var randomPassword = RandomTool.RandomString(10);  // Generate a password with 10 characters
                        resetPassword.Password = randomPassword.Criptografa();
                        var reset = await ResetPassword(resetPassword); // Atualiza a senha gerada no banco de acordo com o e-mail

                        if (reset)
                        {
                            var subject = "CompreAqui - Recuperação de senha!";
                            var message = new EmailMessageCommand(new string[] { retorno.Email }, subject, RecoveryMessageHtml(randomPassword, retorno.UserName));
                            return new CommandResult((int)EStatus.Ok, true, "{ \"senha\": \"" + randomPassword + "\", \"usuario\": \"" + retorno.UserName + "\" }", null);
                        }
                        else
                        {
                            return new CommandResult((int)EStatus.BadRequest, false,
                                "Não foi possível resetar a senha!", null);
                        }
                        //if (reset)
                        //{
                        //var subject = "CompreAqui - Recuperação de senha!";
                        //var message = new EmailMessageCommand(new string[] { retorno.Email }, subject, RecoveryMessageHtml(randomPassword, retorno.UserName));
                        //var senderHandler = new EmailSenderTool(_emailSettings);
                        //    try
                        //    {
                        //        senderHandler.SendEmail(message); //Envia um e-mail com a nova senha
                        //    }
                        //    catch (Exception E)
                        //    {
                        //        return new CommandResult((int)EStatus.BadRequest, false,"Erro ao enviar email!"+E.Message, null);
                        //    }
                        //    return new CommandResult((int)EStatus.Ok, true, "Uma nova senha foi enviada para seu e-mail!", null);
                        //}
                        //else
                        //{
                        //    return new CommandResult((int)EStatus.BadRequest, false,
                        //        "Não foi possível resetar a senha!", null);
                        //}
                        }
                    catch 
                    {
                        return new CommandResult((int)EStatus.BadRequest, false,
                            $"Não foi possível resetar a senha ou enviar o email!", null);
                    }
                }
            }
            catch 
            {
                return new CommandResult((int)EStatus.InternalServerError, false, 
                    $"Erro ao tentar resetar a senha.", null);
            }
        }

        #endregion

        #region Update Methods

        public async Task<ICommandResult> UpdatePassword(UpdateUserPasswordCommand update)
        {
            try
            {

                return await UpdateUserPassword(update);
            }
            catch 
            {
                return new CommandResult((int)EStatus.InternalServerError, false,
                    $"Erro ao tentar atualizar a senha do usuário.", null);
            }
        }

        public async Task<ICommandResult> UpdateUser(UpdateUserCommand update)
        {
            try
            {
                return await UpdatedUser(update);
            }
            catch 
            {
                return new CommandResult((int)EStatus.InternalServerError, false,
                   $"Erro ao tentar atualizar o usuário.", null);
            }
        }
        #endregion

        #region Internal Methods

        internal async Task<bool> ResetPassword(ResetPasswordCommand user)
        {
            try
            {
                string sql = $"Update users set password = '{user.Password}' where email = '{user.Email}'";
                var retorno = await Repository.UpdateBySql(sql);
                return retorno;
            }
            catch 
            {
                return false;
            }
        }
        
        internal string RecoveryMessageHtml(string password, string user)
        {
            string mensage =
                " <p><h1 style=\"background - color:DodgerBlue;\"><b> CompreAqui </b></h1></p> " +
                "<img src=\"https://www.eshopex.com/br/assets/img/icons/eshopex-compra.png\"> " +
                $"<p> Usuário: <b> {user}</b></p>" +
                $"<p> Sua nova senha é: <b> {password}</b></p>" +
                $"<p> Se desejar, você pode mudá-la no aplicativo! </p>";
            return mensage;
        }

        internal async Task<ICommandResult> UpdateUserPassword(UpdateUserPasswordCommand update)
        {
            try
            {
                string sql = $"Update users set password = '{update.Password.Criptografa()}' where guid = '{update.Guid}' ";
                //var retorno = await Repository.GetOneBySql(sql);
                var retorno = await Repository.UpdateBySql(sql);
                ICommandResult resultadoServico = retorno != true
                   ? new CommandResult((int)EStatus.BadRequest, false, "Não foi possível atualizar a senha! Usuário não encontrado.", null)
                   : new CommandResult((int)EStatus.Ok, true, "Senha atualizada com sucesso!", retorno);
                return resultadoServico;
            }
            catch 
            {
                return new CommandResult((int)EStatus.InternalServerError, false,
                    $"Erro ao tentar atualizar a senha do usuário.", null);
            }
        }
      
        internal async Task<ICommandResult> UpdatedUser(UpdateUserCommand update)
        {
            try
            {
                string findUser = $"Select * from users where guid = '{update.Guid}' ";
                var user = await Repository.GetOneBySql(findUser);
                var sqlExisteEmail = $"select * from users where email = '{update.Email}' and users.guid != '{update.Guid}'";
                var retornoExistenciaEmail = await Repository.GetOneBySql(sqlExisteEmail);
                var sqlExisteUsuario = $"select * from users where username = '{update.UserName}' and users.guid != '{update.Guid}'";
                var retornoExistenciaUsuario = await Repository.GetOneBySql(sqlExisteUsuario);

                if (retornoExistenciaEmail == null && retornoExistenciaUsuario == null)
                {
                    // Verifica campos nulos e substitui pelos campos do usuário
                    #region VerifyNullFields

                    if (string.IsNullOrEmpty(update.UserName) || string.IsNullOrWhiteSpace(update.UserName))
                    {
                        update.UserName = user.UserName;
                    }

                    if (string.IsNullOrEmpty(update.FirstName) || string.IsNullOrWhiteSpace(update.FirstName))
                    {
                        update.FirstName = user.FirstName;
                    }

                    if (string.IsNullOrEmpty(update.LastName) || string.IsNullOrWhiteSpace(update.LastName))
                    {
                        update.LastName = user.LastName;
                    }

                    if (string.IsNullOrEmpty(update.PhoneNumber) || string.IsNullOrWhiteSpace(update.PhoneNumber))
                    {
                        update.PhoneNumber = user.PhoneNumber;
                    }

                    if (update.Rating == 0)
                    {
                        update.Rating = user.Rating;
                    }

                    #endregion

                    string sql =
                        $"Update users set email = '{update.Email}', " +
                        $"username = '{update.UserName}', firstname = '{update.FirstName}', " +
                        $"lastname = '{update.LastName}', phonenumber = '{update.PhoneNumber}', rating = {update.Rating}  " +
                        $"where guid = '{update.Guid}' ";
                    var retorno = await Repository.UpdateBySql(sql);
                    ICommandResult resultadoServico = retorno == false
                       ? new CommandResult((int)EStatus.BadRequest, false, "Não foi possível atualizar o usuário! Usuário não encontrado.", null)
                       : new CommandResult((int)EStatus.Ok, true, "Usuário atualizado com sucesso!", retorno);
                    return resultadoServico;
                }
                else
                {
                    return new CommandResult((int)EStatus.BadRequest, false, "usuário ou email existentes.", null);
                }
            }
            catch
            {
                return new CommandResult((int)EStatus.InternalServerError, false,
                    $"Erro ao tentar atualizar o usuário.", null);
            }
        }
        #endregion

    }
}

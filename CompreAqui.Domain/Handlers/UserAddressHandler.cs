using CompreAqui.Domain.Attributes;
using CompreAqui.Domain.Commands.Inputs.UserAddressCommands;
using CompreAqui.Domain.Commands.Inputs.UserCommands;
using CompreAqui.Domain.Commands.Output;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Enums;
using CompreAqui.Domain.Models.Entities;
using CompreAqui.Domain.Models.Settings;
using System;
using System.Threading.Tasks;

namespace CompreAqui.Domain.Handlers
{
    public class UserAddressHandler : EntityHandler<UserAddress>, IHandler
    {
        private readonly AuthenticatedUser _authUser;
        private readonly UserHandler _user;
        protected readonly IDapperRepository<User> userRepository;

        public UserAddressHandler(IContext context) : base(context)
        {   
        }

        public new async Task<ICommandResult> GetAll()
        {
            try
            {
                string sql = $"SELECT * FROM useraddress ";
                var retorno = await Repository.GetListBySql<UserAddress>(sql);
                ICommandResult resultadoServico = retorno.Count == 0
                    ? new CommandResult((int)EStatus.NoContent, true, "Nenhum usuário encontrado!", null)
                    : new CommandResult((int)EStatus.Ok, true, "Usuários encontrados!", retorno);
                return resultadoServico;
            }
            catch 
            {
                return new CommandResult((int)EStatus.InternalServerError, false, 
                    "Erro interno ao tentar buscar todos os usuários!", null);
            }
        }

        public async Task<ICommandResult> GetUserAddressByUserId(GetUserAddressByIdUserCommand user)
        {
            try
            {
                string sql = $"Select * from useraddress where userid = '{user.UserId}'";
                var retorno = await Repository.GetListBySql<UserAddress>(sql);
                ICommandResult resultadoServico = retorno == null
                   ? new CommandResult((int)EStatus.NoContent, true, "Endereço do usuário não encontrado !", null)
                   : new CommandResult((int)EStatus.Ok, true, "Endereço do usuário encontrado !", retorno);
                return resultadoServico;
            }
            catch (Exception ex)
            {
                return new CommandResult((int)EStatus.InternalServerError, false,
                    "Erro ao tentar buscar o endereço do usuário pelo UserId!", null);
            }
        }

        public async Task<ICommandResult> CreateUserAddress(CreateUserAddressCommand userAddress)
        {
            try
            {

                string sql = $"INSERT INTO useraddress " +
                    $"(userid, mainaddress, zipcode, street, number, neighborhood, " +
                    $"district, city, state, complement, reference) " +
                    $"VALUES " +
                    $"({userAddress.UserId}, '{userAddress.MainAddress}', '{userAddress.ZipCode}', " +
                    $" '{userAddress.Street}', '{userAddress.Number}', '{userAddress.Neighborhood}', " +
                    $" '{userAddress.District}', '{userAddress.City}', '{userAddress.State}', '{userAddress.Reference}', " +
                    $" '{userAddress.Complement}')";
                var retorno = await Repository.InsertBySql(sql);

                string sqlRetornoInsert = $"SELECT * FROM useraddress where userid = {userAddress.UserId} and mainaddress = 'true'";
                var retornoInsert = await Repository.GetListBySql<UserAddress>(sqlRetornoInsert);
                
                if(retornoInsert.Count != 0)
                {
                    ICommandResult resultadoServico = retorno == false
                    ? new CommandResult((int)EStatus.NotAcceptable, false, "Não foi possível adicionar um novo endereço de usuário! " +
                    "Verifique se há campos necessários nulos.", null)
                    : new CommandResult((int)EStatus.Created, true, "O endereço do usuário foi adicionado com sucesso!", retornoInsert[0].Id);
                    return resultadoServico;
                }
                else
                {
                    return new CommandResult((int)EStatus.NotAcceptable, false, "Não foi possível adicionar um novo endereço de usuário! " +
                    "Verifique se há campos necessários nulos.", null);
                }
            }
            catch
            {
                return new CommandResult((int)EStatus.InternalServerError, false,
                "Erro interno ao tentar adicionar um novo endereço de usuário!", null);
            }
        }

        public async Task<ICommandResult> UpdateUserAddressById(UpdateUserAddressByIdCommand address)
        {
            try
            {
                var service = await PutUserAddressById(address);

                ICommandResult result = service == false
                    ? new CommandResult((int)EStatus.NotAcceptable, false, "Erro ao tentar atualizar o endereço do usuário!", false)
                    : new CommandResult((int)EStatus.Ok, true, "Endereço do usuário atualizado com sucesso!", service);
                return result;
            }
            catch 
            {

                return new CommandResult((int)EStatus.InternalServerError, false,
                           "Erro interno ao tentar atualizar o endereço do usuário pelo ID!", null);
            }
        }

        #region Internal Methods
        
        internal async Task<bool> PutUserAddressById(UpdateUserAddressByIdCommand address)
        {
            try
            {
                string sql = $"update useraddress " +
                    $"set " +
                    $" mainaddress = '{address.MainAddress}', " +
                    $"zipcode = '{address.ZipCode}', street = '{address.Street}', " +
                    $"number = '{address.Number}', neighborhood = '{address.Neighborhood}', " +
                    $" district = '{address.District}', city = '{address.City}', " +
                    $"state = '{address.State}', complement = '{address.Complement}', reference = '{address.Reference}' " +
                    $"where id = '{address.Id}' ";
                var service = await Repository.UpdateBySql(sql);
                return service;
            }
            catch 
            {
                return false;
            }
        }
        #endregion
    }
}

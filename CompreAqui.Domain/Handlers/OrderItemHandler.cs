using CompreAqui.Domain.Attributes;
using CompreAqui.Domain.Commands.Inputs.OrderItemCommands;
using CompreAqui.Domain.Commands.Output;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Enums;
using CompreAqui.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompreAqui.Domain.Handlers
{
    public class OrderItemHandler: EntityHandler<OrderItem>, IHandler
    {

        public OrderItemHandler(IContext context) : base(context)
        {
        }

        public async Task<ICommandResult> GetAllOrderItem(GetAllOrderItemCommand orderItem)
        {
            try
            {
                string sql = $"SELECT * FROM orderitems where orderid = {orderItem.OrderId}";
                var retorno = await Repository.GetListBySql<OrderItem>(sql);
                ICommandResult resultadoServico = retorno.Count == 0
                    ? new CommandResult((int)EStatus.NotFound, false, "Nenhum item de pedido encontrado !", null)
                    : new CommandResult((int)EStatus.Ok, true, "Itens encontrados !", retorno);
                return resultadoServico;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao tentar buscar todos os itens de pedidos ! Erro: ", ex.Message);
            }
        }

        public bool CreateListOrderItemsByListStrings(List<string> sqls)
        {
            var transiction =  Repository.TransactionEntityListBySqlList(sqls);
            return transiction;
        }
    }
}

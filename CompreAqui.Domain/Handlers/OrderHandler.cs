using CompreAqui.Domain.Attributes;
using CompreAqui.Domain.Commands.Inputs.OrderCommands;
using CompreAqui.Domain.Commands.Output;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Enums;
using CompreAqui.Domain.Models.Entities;
using CompreAqui.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompreAqui.Domain.Handlers
{
    public class OrderHandler : EntityHandler<Order>, IHandler
    {
        private readonly OrderItemHandler _itemHandler;

        public OrderHandler(IContext context, OrderItemHandler itemHandler) : base(context)
        {
            _itemHandler = itemHandler;
        }

        public new async Task<ICommandResult> GetAllOrderByUserId(GetAllOrderCommand order)
        {
            try
            {
                string sql = $"SELECT * FROM orders where userid = {order.userId} order by date desc";
                var retorno = await Repository.GetListBySql<Order>(sql);
                ICommandResult resultadoServico = retorno.Count == 0
                    ? new CommandResult((int)EStatus.NoContent, true, "Nenhum pedido encontrado!", null)
                    : new CommandResult((int)EStatus.Ok, true, "Pedidos encontrados!", retorno);

                string sqlItens = $"SELECT orderitems.orderid, orderitems.productid, orderitems.description, orderitems.quantity, orderitems.unity,orderitems.price FROM orderitems inner join orders on orders.id = orderitems.orderid where orders.userid = {order.userId}";

                var retornoItens = await Repository.GetListBySql<OrderItem>(sqlItens);
                ICommandResult resultadoServicoItens = retorno.Count == 0
                    ? new CommandResult((int)EStatus.NoContent, true, "Nenhum item do pedido encontrado!", null)
                    : new CommandResult((int)EStatus.Ok, true, "Item(s) do pedido encontrado(s)!", retorno);

                foreach (var pedido in retorno)
                {
                    List<OrderItem> listaOrderItems = new List<OrderItem>();
                    foreach (var itemPedido in retornoItens)
                    {
                        if(itemPedido.OrderId == pedido.Id)
                        {
                            OrderItem itemPedido_ = new OrderItem();
                            itemPedido_.OrderId = pedido.Id;
                            itemPedido_.Description = itemPedido.Description;
                            itemPedido_.Price = itemPedido.Price;
                            itemPedido_.Quantity = itemPedido.Quantity;
                            itemPedido_.Unity = itemPedido.Unity;
                            listaOrderItems.Add(itemPedido_);       
                        }
                    }
                    ListOrderItems listOrder = new ListOrderItems();
                    listOrder.lista = listaOrderItems;
                    UtilitiesJson jsonconv = new UtilitiesJson();
                    pedido.Description = jsonconv.ConverteObjectParaJSon(listOrder);
                }

                return resultadoServico;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao tentar buscar todos os pedidos! Erro: ", ex.Message);
            }
        }

        public new async Task<ICommandResult> GetOrderById(GetOrderByIdCommand order)
        {
            try
            {
                string sql = $"SELECT * FROM orders where id = {order.Id}";
                var retorno = await Repository.GetListBySql<Order>(sql);
                ICommandResult resultadoServico = retorno.Count == 0
                    ? new CommandResult((int)EStatus.NotFound, false, "Nenhum pedido encontrado !", null)
                    : new CommandResult((int)EStatus.Ok, true, "Pedidos encontrados !", retorno);

                return resultadoServico;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao tentar pedidos! Erro: ", ex.Message);
            }
        }

        public new async Task<ICommandResult> GetOrderNewMessageById(GetAllOrderCommand order)
        {
            try
            {
                string sql = $"SELECT * FROM orders where userid = {order.userId} and userviewed = 'false' order by date desc";
                var retorno = await Repository.GetListBySql<Order>(sql);
                ICommandResult resultadoServico = retorno.Count == 0
                    ? new CommandResult((int)EStatus.NotFound, false, "Nenhuma nova mensagem encontrada!", null)
                    : new CommandResult((int)EStatus.Ok, true, "Nova mensagem encontrada!", retorno);

                return resultadoServico;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao tentar buscar pedidos! Erro: ", ex.Message);
            }
        }

        public new async Task<ICommandResult> UpdateMessageOrder(UpdateMessageOrderCommand order)
        {
            try
            {
                var retorno = await PutMessageOrder(order);
                ICommandResult resultadoServico = !retorno
                    ? new CommandResult((int)EStatus.NotFound, false, "Nenhum pedido encontrado !", null)
                    : new CommandResult((int)EStatus.Ok, true, "Mensagem atualizada!", retorno);

                return resultadoServico;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao tentar atualizar mensagem! Erro: ", ex.Message);
            }
        }

        internal async Task<bool> PutMessageOrder(UpdateMessageOrderCommand order)
        {
            try
            {
                string sql = $"update orders set messages = CONCAT(messages,'{order.Message}') where id = {order.Id}; update orders set customerviewed = 'false' where id = {order.Id} ";
                var service = await Repository.UpdateBySql(sql);

                return service;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao tentar atualizar o mensagem!", ex.Message);
            }
        }

        public async Task<ICommandResult> CreateOrder(CreateOrderCommand order)
        {
            try
            {
                // Cria um novo pedido
                var insertOrder = await CreateNewOrder(order);

                if (insertOrder == false)
                {
                    return new CommandResult((int)EStatus.NotFound, false, "Não foi possível criar um novo pedido !", null);
                }
                else
                {
                    try
                    {
                        // Busca o pedido criado
                        var pedido = await FindLastOrder(order);

                        // Após criar o pedido cria uma lista de strings contendo as sqls de inserções
                        // dos itens do pedido
                        var listStrings = CreateStringList(order.OrderItemList, pedido.Id);

                        // Realiza a transação de acordo com a quantidade de sqls de inserção
                        var transation = _itemHandler.CreateListOrderItemsByListStrings(listStrings);

                        ICommandResult returnTransation = transation == false
                            ? new CommandResult((int)EStatus.BadRequest, true, "Não foi possível realizar a inserção dos itens do pedido!", transation)
                            : new CommandResult((int)EStatus.Ok, true, "Itens do pedido cadastrados com sucesso !", transation);
                        return returnTransation;
                    }
                    catch (Exception ex)
                    {
                        // Busca o pedido criado
                        var pedido = await FindLastOrder(order);
                        // Deleta o pedido criado
                        var delete = DeleteOrder(pedido.Id);
                        throw new ArgumentException("Erro ao inserir itens do pedido!", ex.Message);
                    }
                }
            }
            catch(Exception ex)
            {
            
                throw new ArgumentException("Erro ao tentar criar o pedido ou inserir os itens do pedido!", ex.Message);
            }
        }

        public async Task<ICommandResult> UpdateOrderUserViewed(UpdateOrderUserViewedCommand order)
        {
            string sql = $"update orders set userviewed = '{order.UserViewed}' where id = '{order.Id}'";
            var update = await Repository.UpdateBySql(sql);
            ICommandResult result = update == false
                ? new CommandResult((int)EStatus.InternalServerError, false, "Erro ao tentar atualizar o status da mensagem do pedido!", false)
                : new CommandResult((int)EStatus.Ok, true, "Status da mensagem do pedido atualizado com sucesso!", update);
            return result;
        }

        #region Internal Methods
        internal async Task<bool> CreateNewOrder(CreateOrderCommand order)
        {
            try
            {
                //pega a data e hora do fuso horário de brasília. Trata, a fato do servidor estar nos Estados unidos e os campos de pedidos estarem com datetime errados.
                TimeZoneInfo Standard_Time = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                DateTime dataHoraLocal = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Standard_Time);

                string sqlOrder = $"INSERT INTO orders " +
                     $"(userid, customerid, total, status, description, creditcard, useraddressid, date, userviewed, customerviewed, catchinstore) " +
                     $"values " +
                     $"(" +
                     $" {order.UserId}, {order.CustomerId},  " +
                     $" {order.Total.ToString().Replace(",",".")}, '{order.Status}', '{order.Description}', '{order.CreditCard}', " +
                     $" {order.UserAddressId}, " +
                     $" '{dataHoraLocal.Month}/{dataHoraLocal.Day}/{dataHoraLocal.Year} {dataHoraLocal.Hour}:{dataHoraLocal.Minute}:{dataHoraLocal.Second}','true', 'true','{order.CatchInStore}' )";//CURRENT_TIMESTAMP -> pega a hora da base (no caso a hora do servidor azure nos estados unidos)
                var insertOrder = await Repository.InsertBySql(sqlOrder);
                return insertOrder;
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Erro ao tentar inserir um novo pedido!", ex.Message);
            }
        }
        
        internal async Task<Order> FindLastOrder(CreateOrderCommand order)
        {
            try
            {
                string sqlBuscaPedido = $"Select * from orders where userid = {order.UserId} order by id";
                var buscaPedido = await Repository.GetListBySql<Order>(sqlBuscaPedido);
                var pedido = buscaPedido.LastOrDefault();
                return pedido;
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Erro ao tentar buscar o ultimo pedido!", ex.Message);
            }
        }

        internal async Task<bool> DeleteOrder(int id)
        {
            try
            {
                string sql = $"Delete from orders where id = {id}";
                var delete = await Repository.DeleteBySql(sql);
                return delete;
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Não foi possível deletar o pedido!", ex.Message);
            }
        }
       
        internal List<string> CreateStringList(List<OrderItem> orderItems, int orderId)
        {
            List<string> listStrings = new List<string>();

            foreach (var item in orderItems)
            {
                // Adiciona o id do pedido nos orderitems
                item.OrderId = orderId;

                string insertOrderItem =
                    "INSERT INTO orderitems " +
                    "(" +
                        "orderid, productid, quantity, price, description, unity" +
                    ") " +
                    "values " +
                    "(" +
                        $" {item.OrderId}, {item.ProductId},{item.Quantity.ToString().Replace(",", ".")}, " +
                        $" {item.Price.ToString().Replace(",",".")}, '{item.Description}', '{item.Unity}' " +
                    ");";
                listStrings.Add(insertOrderItem);
            }
            return listStrings;
        }
        #endregion

        #region Class Utilities
        //Classe temporaria para serializacao
        public class ListOrderItems
        {
            public List<OrderItem> lista;
        }
        #endregion

    }
}

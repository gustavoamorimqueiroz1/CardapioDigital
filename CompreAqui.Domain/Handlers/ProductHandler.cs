using CompreAqui.Domain.Attributes;
using CompreAqui.Domain.Commands.Inputs;
using CompreAqui.Domain.Commands.Inputs.ProductCommands;
using CompreAqui.Domain.Commands.Output;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Enums;
using CompreAqui.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace CompreAqui.Domain.Handlers
{
    public class ProductHandler : EntityHandler<Product>, IHandler
    {
        public ProductHandler(IContext context) : base(context)
        {
        }

        public async Task<ICommandResult> GetAllProducts(GetAllProductsCommand product)
        {
            try
            {
                string sql = $"SELECT * FROM products where customerid = {product.CustomerId} order by categoryid asc, productname asc";
                var retorno = await Repository.GetListBySql<Product>(sql);
                ICommandResult resultadoServico = retorno.Count == 0
                    ? new CommandResult((int)EStatus.NoContent, true, "Nenhum produto encontrado!", null)
                    : new CommandResult((int)EStatus.Ok, true, "Produtos encontrados com sucesso!", retorno);
                return resultadoServico;
            }
            catch
            {
                return new CommandResult((int)EStatus.InternalServerError, false,
                    "Erro ao buscar todos os produtos!", null);
            }
        }

    }
}

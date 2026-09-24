using CompreAqui.Domain.Attributes;
using CompreAqui.Domain.Commands.Inputs;
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
    public class CategoryProductHandler : EntityHandler<CategoryProduct>, IHandler
    {
        public CategoryProductHandler(IContext context) : base(context)
        {
        }

        public new async Task<ICommandResult> GetAll()
        {
            try
            {
                string sql = $"SELECT * FROM categories ";
                var retorno = await Repository.GetListBySql<CategoryProduct>(sql);
                ICommandResult resultadoServico = retorno.Count == 0
                    ? new CommandResult((int)EStatus.NoContent, true, "Nenhuma categoria encontrada !", null)
                    : new CommandResult((int)EStatus.Ok, true, "Categorias encontrados com sucesso!", retorno);
                return resultadoServico;
            }
            catch 
            {
                return new CommandResult((int)EStatus.InternalServerError, false, "Erro interno ao buscar as categorias!", null);
            }
        }
    }
}

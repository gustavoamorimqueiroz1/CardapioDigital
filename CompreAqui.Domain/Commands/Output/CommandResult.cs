using CompreAqui.Domain.Contracts;

namespace CompreAqui.Domain.Commands.Output
{
        /// <summary>
        /// Classe que contém retorno padrão da API
        /// </summary>
        public class CommandResult : ICommandResult
        {
            public CommandResult() { }

            public CommandResult(int status, bool success, string message, object data)
            {
                Status = status;
                Success = success;
                Message = message;
                Data = data;
            }
            /// <summary>
            /// Status do retorno da API
            /// <para>1XX - Informativo</para>
            /// <para>2XX - Sucesso</para>
            /// <para>3xx - Redirecionamento</para>
            /// <para>4xx - Erro de cliente</para>
            /// <para>5xx - Outros erros</para>
            /// <para>7xx - Retorno especial</para>
            /// <para>8xx - Erro Interno da API</para>
            /// </summary>
            public int Status { get; set; }
            /// <summary>
            /// Define se o processamento obteve sucesso
            /// </summary>
            public bool Success { get; set; }
            /// <summary>
            /// Mensagem de retorno da api
            /// </summary>      
            public string Message { get; set; }
            /// <summary>
            /// Objeto retornado da api
            /// </summary>
            public object Data { get; set; }
        }
    
}

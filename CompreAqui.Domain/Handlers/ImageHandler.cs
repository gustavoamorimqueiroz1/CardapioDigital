using Azure.Storage.Blobs;
using CompreAqui.Domain.Commands.Inputs.ImageCommands;
using CompreAqui.Domain.Commands.Output;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Enums;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CompreAqui.Domain.Handlers
{
    public class ImageHandler : IHandler
    {

        public ICommandResult UploadImage(GetBase64ImageCommand command)
        {
            try
            {
                var result = UploadBase64ImageToAzure(command.Image, command.Container);
                ICommandResult resultadoServico = result == null
                    ? new CommandResult((int)EStatus.BadRequest, false,
                        "Não foi possível salvar a imagem no Azure!", null)
                    : new CommandResult((int)EStatus.Ok, true,
                        "Upload no Azure realizado com sucesso!", null);
                return resultadoServico;
            }
            catch (Exception ex)
            {
                return new CommandResult((int)EStatus.InternalServerError, false, 
                    "Erro interno ao tentar enviar a imagem para o Azure!", null);
            }
        }


        #region Métodos Upload 
        public string UploadBase64ImageToAzure(string base64Image, string container)
        {
            // Gera um nome pra imagem
            var fileName = Guid.NewGuid().ToString() + ".jpg";

            // Limpa o hash enviado
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

            // Gera um arry de Bytes
            byte[] imageBytes = Convert.FromBase64String(data);

            // Define o BLOB no qual a imagem será armazenada
            var blobClient = new BlobClient("ADICIONAR CONNECTION STRING DO AZURE", container, fileName);

            // Envia a imagem
            using (var stream = new MemoryStream(imageBytes))
            {
                blobClient.Upload(stream);
            }

            // Retorna a URL da imagem
            return blobClient.Uri.AbsoluteUri;

        }

        #endregion
    }
}

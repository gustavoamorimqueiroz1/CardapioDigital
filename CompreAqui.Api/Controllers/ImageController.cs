using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompreAqui.Domain.Commands.Inputs.ImageCommands;
using CompreAqui.Domain.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompreAqui.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageHandler _handler;

        public ImageController(ImageHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Insere uma imagem em Base64 no Azure
        /// </summary>
        /// <returns>Insere Imagem(JSON)</returns>
        [HttpGet("add/azure")]
        public  IActionResult UploadImageAzure([FromBody]GetBase64ImageCommand command)
        {
            var image = _handler.UploadImage(command);
            if (image.Success)
            {
                return Ok(image);
            }
            else
            {
                switch (image.Status)
                {
                    case 400:
                        return BadRequest(image);
                    case 500:
                        return StatusCode(500, image);
                    default:
                        return BadRequest(image);
                }

            }
        }

    }
}

using System;
using System.Threading.Tasks;
using CompreAqui.Domain.Commands.Inputs;
using CompreAqui.Domain.Commands.Inputs.ProductCommands;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompreAqui.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ProductHandler _handler;
        private readonly IAppSettings _settings;


        public ProductController(ProductHandler handler, IAppSettings settings)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
  
        }

        /// <summary>
        /// Busca todos produtos do sistema
        /// </summary>
        /// <returns>Lista produtos (JSON)</returns>
        [HttpPost("")]
        public async Task<IActionResult> GetAllProducts([FromBody]GetAllProductsCommand product)
        {
      
            var products = await _handler.GetAllProducts(product);
            if (products.Success)
            {
                return Ok(products);
            }
            else
            {
                return StatusCode(500, products);
            }
        }
    }
}
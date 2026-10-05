using System;
using System.Threading.Tasks;
using CompreAqui.Domain.Commands.Inputs;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Handlers;
using CompreAqui.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompreAqui.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryProductController : ControllerBase
    {
        private readonly CategoryProductHandler _handler;
        private readonly IAppSettings _settings;

        public CategoryProductController(CategoryProductHandler handler, IAppSettings settings)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Busca todas as categorias de produto do sistema
        /// </summary>
        /// <returns>Lista categorias de produto (JSON)</returns>
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _handler.GetAll();
            if (categories.Success)
            {
                return Ok(categories);
            }
            else
            {
                return StatusCode(500, categories);
            }
        }
    }
}
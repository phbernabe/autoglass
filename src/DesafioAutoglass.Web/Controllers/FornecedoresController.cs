using DesafioAutoglass.Application.Dtos.Fornecedor;
using DesafioAutoglass.Application.Interfaces;
using DesafioAutoglass.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DesafioAutoglass.Web.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly IFornecedorAppService _application;

        public FornecedoresController(IFornecedorAppService application)
        {
            _application = application;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var fornecedor = await _application.GetAsync(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(Get), fornecedor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AdicionarFornecedorInputDto input)
        {
            try
            {
                var fornecedor = await _application.InsertAsync(input);

                return Ok(fornecedor);
            }
            catch (ModelValidationException vex)
            {
                return Problem(vex.Message, "", 422);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] EditarFornecedorInputDto input)
        {
            try
            {
                await _application.UpdateAsync(input);

                return Ok();
            }
            catch (EntityNotFoundException enfex)
            {
                return NotFound(enfex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, "", 500);
            }
        }
    }
}

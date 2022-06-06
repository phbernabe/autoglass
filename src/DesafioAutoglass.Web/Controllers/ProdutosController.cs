using DesafioAutoglass.Application.Dtos.Produto;
using DesafioAutoglass.Application.Interfaces;
using DesafioAutoglass.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioAutoglass.Application.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoAppService _application;

        public ProdutosController(IProdutoAppService application)
        {
            _application = application;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoOutputDto>> Get(int id)
        {
            var produto = await _application.GetAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoOutputDto>>> Get(string descricao, DateTime? validoAte, string fornecedor, string cnpj, int skip, int take = 20)
        {
            var produtos = _application.Search(descricao, validoAte, fornecedor, cnpj, skip, take);

            return Ok(produtos);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoOutputDto>> Post([FromBody] AdicionarProdutoInputDto input)
        {
            try
            {
                var produto = await _application.InsertAsync(input);

                return CreatedAtAction(nameof(Get), new { id = produto.Codigo }, produto);
            }
            catch (ModelValidationException vex)
            {
                return Problem(JsonConvert.SerializeObject(vex.ValidationMessages), "", 422);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] EditarProdutoInputDto input)
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _application.RemoveAsync(id);

            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Final.Expections;
using Projeto.Final.InputModel;
using Projeto.Final.Services;
using Projeto.Final.ViewModel;

namespace Projeto.Final.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class BancoDeDadosController : ControllerBase
    {
        private readonly IProdutoService _ProdutoService;

        public BancoDeDadosController(IProdutoService produtoService)
        {
            _ProdutoService = produtoService;
        }
        /// <summary>
        /// Buscar todos os produtos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os produtos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de produtos</response>
        /// <response code="204">Caso não haja produtos</response> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var Produtos = await _ProdutoService.Obter(pagina, quantidade);

            if (Produtos.Count() == 0)
                return NoContent();

            return Ok(Produtos);
        }

        /// <summary>
        /// Buscar um produto pelo seu Id
        /// </summary>
        /// <param name="idProduto">Id do produto buscado</param>
        /// <response code="200">Retorna o produto filtrado</response>
        /// <response code="204">Caso não haja produto com este id</response>   
        [HttpGet("{idProduto:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Obter([FromRoute] Guid idProduto)
        {
            var Produto = await _ProdutoService.Obter(idProduto);
            if (Produto == null)
                return NoContent();

            return Ok(Produto);
        }

        /// <summary>
        /// Inserir um produto no catálogo
        /// </summary>
        /// <param name="ProdutoInputModel">Dados do jogo a ser inserido</param>
        /// <response code="200">Caso o produto seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um produto com mesmo nome</response>  
        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> InserirProduto([FromBody] ProdutoInputModel ProdutoInputModel)
        {
            try
            {
                var Produto = await _ProdutoService.Inserir(ProdutoInputModel);

                return Ok(Produto);
            }
            catch (ProdutoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um produto com esse nome do cliente");
            }
        }

        /// <summary>
        /// Atualizar um produto no catálogo
        /// </summary>
        /// /// <param name="idProduto">Id do produto a ser atualizado</param>
        /// <param name="ProdutoInputModel">Novos dados para atualizar o produto indicado</param>
        /// <response code="200">Caso o produto seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um produto com este Id</response> 
        [HttpPut("{idProduto:guid}")]
        public async Task<ActionResult> AtualizarProduto([FromRoute] Guid idProduto, [FromBody] ProdutoInputModel ProdutoInputModel)
        {
            try
            {
                await _ProdutoService.Atualizar(idProduto, ProdutoInputModel);
                return Ok();
            }
            catch (ProdutoNaoCadastradoException ex)

            {
                return NotFound("Não existe esse produto");
            }
        }

        /// <summary>
        /// Atualizar o preço de um produto
        /// </summary>
        /// /// <param name="idProduto">Id do produto a ser atualizado</param>
        /// <param name="preco">Novo preço do produto</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um produto com este Id</response> 
        [HttpPatch("{idProduto}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarProduto([FromRoute ]Guid idProduto, [FromRoute] double preco)
        {
            try
            {
                await _ProdutoService.Atualizar(idProduto, preco);

                   return Ok();
            }
            catch (ProdutoNaoCadastradoException ex)
            {
                return NotFound("Não existe este Produto");
            }
        }

        /// <summary>
        /// Excluir um produto
        /// </summary>
        /// /// <param name="idProduto">Id do produto a ser excluído</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um produto com este Id</response>  
        [HttpDelete("{idProduto:guid}")]
        public async Task<ActionResult> ApagarProduto([FromRoute] Guid idProduto)
        {
            try
            {
                await _ProdutoService.Remover(idProduto);

                return Ok();
            }

            catch(ProdutoNaoCadastradoException ex)
            {
                return NotFound ("Não existe este Produto");
            }
        }

    }
}

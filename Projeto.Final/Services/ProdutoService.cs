using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Final.Entities;
using Projeto.Final.Expections;
using Projeto.Final.InputModel;
using Projeto.Final.Repositories;
using Projeto.Final.ViewModel;

namespace Projeto.Final.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<ProdutoViewModel>> Obter(int pagina, int quantidade)
        {
            var produtos = await _produtoRepository.Obter(pagina, quantidade);

            return produtos.Select(produto => new ProdutoViewModel
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco
            }).ToList();
        }
        public async Task<ProdutoViewModel> Obter(Guid id)
        {
            var produto = await _produtoRepository.Obter(id);

            if (produto == null)
                return null;

            return new ProdutoViewModel
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco
            };
        }

        public async Task<ProdutoViewModel> Inserir(ProdutoInputModel produto)
        {
            var entidadeProduto = await _produtoRepository.Obter(produto.Nome, produto.Descricao);

            if (entidadeProduto.Count > 0)
                throw new ProdutoJaCadastradoException();

            var produtoInsert = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco
            };

            await _produtoRepository.Inserir(produtoInsert);

            return new ProdutoViewModel
            {
                Id = produtoInsert.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco

            };
        }

        public async Task Atualizar(Guid id, ProdutoInputModel produto)
        {
            var entidadeProduto = await _produtoRepository.Obter(id);

            if (entidadeProduto == null)
                throw new ProdutoNaoCadastradoException();

            entidadeProduto.Nome = produto.Nome;
            entidadeProduto.Descricao = produto.Descricao;
            entidadeProduto.Preco = produto.Preco;

            await _produtoRepository.Atualizar(entidadeProduto);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeProduto = await _produtoRepository.Obter(id);

            if (entidadeProduto == null)
                throw new ProdutoNaoCadastradoException();

            entidadeProduto.Preco = preco;

            await _produtoRepository.Atualizar(entidadeProduto);
        }

        public async Task Remover (Guid id)
        {
            var produto = await _produtoRepository.Obter(id);

            if (produto == null)
                throw new ProdutoNaoCadastradoException();

            await _produtoRepository.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}

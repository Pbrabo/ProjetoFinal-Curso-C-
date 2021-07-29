using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Final.Entities;

namespace Projeto.Final.Repositories
{
    public interface IProdutoRepository : IDisposable
    {
        Task<List<Produto>> Obter(int pagina, int quantidade);
        Task<Produto> Obter(Guid id);
        Task<List<Produto>> Obter(string nome, string descricao);
        Task Inserir(Produto produto);
        Task Atualizar (Produto produto);
        Task Remover(Guid id);
    }
}

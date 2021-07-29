using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Final.InputModel;
using Projeto.Final.ViewModel;

namespace Projeto.Final.Services
{
    public interface IProdutoService : IDisposable
    {
        Task<List<ProdutoViewModel>> Obter(int pagina, int quantindade);
        Task<ProdutoViewModel> Obter(Guid id);
        Task<ProdutoViewModel> Inserir(ProdutoInputModel produto);
        Task Atualizar(Guid id, ProdutoInputModel produto);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);

    }
}

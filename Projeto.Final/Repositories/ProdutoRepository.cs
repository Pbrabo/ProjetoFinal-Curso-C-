using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Final.Entities;

namespace Projeto.Final.Repositories
{
     public class ProdutoRepository : IProdutoRepository
    {
        private static Dictionary<Guid, Produto> produtos = new Dictionary<Guid, Produto>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Produto{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "iPhone 11, preto", Descricao = "128gb", Preco = 4200} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Produto{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Fifa 21", Descricao = "EA Games, para PS5", Preco = 200} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Produto{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Echo Dot", Descricao = "Alexia 4ª geração", Preco = 450} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Produto{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Apple Watch Serie 6 GPS 40MM Space Gray", Descricao = "Monitor de oxigênio no sangue, Mensagens, GPS", Preco = 2999} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Produto{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Galaxy Watch 3 45mm Lte - Preto", Descricao = "O Galaxy Watch3 tem um design clássico e refinado. Sua caixa de metal é feita com material premium em Aço Inoxidável, além de resistente seu formato proporciona conforto no pulso", Preco = 1800} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Produto{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "Grand Theft Auto V", Descricao = "Um jogo criado pela rockstar que fez muito sucesso em seu lançamento, versão para Xbox", Preco = 190} },
        };

        public Task<List<Produto>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(produtos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }
        
        public Task<Produto> Obter(Guid id)
        {
            if (!produtos.ContainsKey(id))
                return Task.FromResult<Produto>(null);
            
            return Task.FromResult(produtos[id]);
        }
        
        public Task<List<Produto>> Obter(string nome, string Descricao)
         {
            return Task.FromResult(produtos.Values.Where(produto => produto.Nome.Equals(nome) && produto.Descricao.Equals(Descricao)).ToList());
        }
        
        public Task<List<Produto>> ObterSemLambda(string nome, string descricao)
        {
            var retorno = new List<Produto>();
            
            foreach (var produto in produtos.Values)
            {
                if (produto.Nome.Equals(nome) && produto.Descricao.Equals(descricao))
                    retorno.Add(produto);
            }
            
            return Task.FromResult(retorno);
        }
        
        public Task Inserir(Produto produto)
        {
            produtos.Add(produto.Id, produto);
            return Task.CompletedTask;
        }
        
        public Task Atualizar(Produto produto)
        {
            produtos[produto.Id] = produto;
            return Task.CompletedTask;
        }
        
        public Task Remover(Guid id)
        {
            produtos.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}

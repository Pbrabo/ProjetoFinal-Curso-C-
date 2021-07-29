using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Projeto.Final.Entities;

namespace Projeto.Final.Repositories
{
    public class ProdutoSqlServerRepository : IProdutoRepository
    {
        private readonly SqlConnection sqlConnection;

        public ProdutoSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Produto>> Obter(int pagina, int quantidade)
        {
            var produtos = new List<Produto>();

            var comando = $"select * from Produtos order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            
            while (sqlDataReader.Read())
            {
                produtos.Add(new Produto
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Descricao = (string)sqlDataReader["Descricao"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return produtos;
        }

        public async Task<Produto> Obter(Guid id)
        {
            Produto produto = null;

            var comando = $"select * from Produtos where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                produto = new Produto
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Descricao = (string)sqlDataReader["Descricao"],
                    Preco = (double)sqlDataReader["Preco"]
                };
            }

            await sqlConnection.CloseAsync();

            return produto;
        }

        public async Task<List<Produto>> Obter(string nome, string descricao)
        {
            var produtos = new List<Produto>();

            var comando = $"select * from Produtos where Nome = '{nome}' and Descricao = '{descricao}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                produtos.Add(new Produto
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Descricao = (string)sqlDataReader["Descricao"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return produtos;
        }

        public async Task Inserir(Produto produto)
        {
            var comando = $"insert Produtos (Id, Nome, Descricao, Preco) values ('{produto.Id}', '{produto.Nome}', '{produto.Descricao}', {produto.Preco.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Produto produto)
        {
            var comando = $"update Produtos set Nome = '{produto.Nome}', Descricao = '{produto.Descricao}', Preco = {produto.Preco.ToString().Replace(",", ".")} where Id = '{produto.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Produtos where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}



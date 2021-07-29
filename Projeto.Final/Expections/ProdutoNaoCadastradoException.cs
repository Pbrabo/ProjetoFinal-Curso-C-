using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Final.Expections
{
    public class ProdutoNaoCadastradoException : Exception
    {
        public ProdutoNaoCadastradoException()
            : base("Este produto ainda não foi cadastrado")
        { }
    }
}

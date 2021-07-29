using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Final.Expections
{
    public class ProdutoJaCadastradoException : Exception
    {
        public ProdutoJaCadastradoException()
            : base("Este produto já esta cadastrado")
        { }
    }
}

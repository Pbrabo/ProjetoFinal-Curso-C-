using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Final.InputModel
{
    public class ProdutoInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome do produto deve conter entre 1 e 100 caracteres")]

        public string Nome { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "O nome do produto deve conter entre 10 e 1000 caracteres")]
        public string Descricao { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "O preço deve ser no minimo 1 real e no maximo 100.000,00 reais")]
        public double Preco { get; set; }
    }
}

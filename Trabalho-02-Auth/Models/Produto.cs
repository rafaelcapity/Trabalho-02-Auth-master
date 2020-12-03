using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Trabalho_02_Auth.Models.Enum;

namespace Trabalho_02_Auth.Models
{   
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome do Produto")]
        [Display(Name = "Nome:")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o Descrição do Produto")]
        [Display(Name = "Descrição:")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o Cor do Produto")]
        [Display(Name = "Cor:")]
        public CorProduto CorProduto { get; set; }

        [Required(ErrorMessage = "Informe o Status Produtor  do Produto")]
        [Display(Name = "Status Produtoor:")]
        public StatusProduto StatusProduto { get; set; }


        [ForeignKey("Fornecedor")]
        [Required(ErrorMessage = "Informe o Fornecedor do Produto")]
        [Display(Name = "Fornecedor:")]
        public int IdFornecedor { get; set; }


        /*Ef Relation*/

        public Fornecedor Fornecedor { get; set; }

    }
}
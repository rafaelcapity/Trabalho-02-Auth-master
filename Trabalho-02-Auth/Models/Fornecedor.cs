using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trabalho_02_Auth.Models

{   [Table("Fornecedor")]
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Informe o Nome do Fornecedor")]
        [Display(Name = "Nome:")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o CNPJ do Fornecedor")]
        [Display(Name = "CNPJ:")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "Informe o E-mail do Fornecedor")]
        [Display(Name = "E-mail:")]
        [EmailAddress(ErrorMessage = "E-mail Invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o Telefone do Fornecedor")]
        [Display(Name = "Telefone:")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        /*Ef Relation*/

        public ICollection<Produto> Produtos{ get; set; }
    }
}
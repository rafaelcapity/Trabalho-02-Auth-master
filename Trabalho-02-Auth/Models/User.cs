using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Trabalho_02_Auth.Models.Enum;

namespace Trabalho_02_Auth.Models
{   
    [Table("User")]
    public class User
    {
       
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe Nome do Usuario")]
        [Display(Name = "Nome:")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "E-mail invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe uma senha")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength =3,ErrorMessage = "Senha Minima de 3 caracter")]
        public string Senha { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "informe a confirmarção de senha ")]
        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage = "Senha Não Confere")]
        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "Selecione o Nivel de acesso do Usuario")]
        public NivelUsuario NivelUsuario { get; set; }


    }
}
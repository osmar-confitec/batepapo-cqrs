using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BatePapo.ViewModel
{
   public class CustomerViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome é requerido")]
        [MinLength(2)]
        [MaxLength(150)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Sobrenome é requerido")]
        [MinLength(2)]
        [MaxLength(150)]
        [DisplayName("Sobrenome")]
        public string SurfaceName { get; set; }


        [Required(ErrorMessage = "O CPF é requerido")]
        [StringLength(11)]
        [DisplayName("CPF")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O Apelido é requerido")]
        [MinLength(2)]
        [MaxLength(150)]
        [DisplayName("Apelido")]
        public string NickName { get; set; }


        [Required(ErrorMessage = "A Data de Nascimento é requerida")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Data de Nascimento")]
        public DateTime BirthDate { get; set; }
    }
}
